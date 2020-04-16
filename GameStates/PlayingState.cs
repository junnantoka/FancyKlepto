using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FancyKlepto.GameObjects;
using FancyKlepto.GameObjects.MapObjects;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace FancyKlepto.GameStates
{
    class PlayingState : GameObjectList
    {
        Song Loop;
        SoundEffect Level_Win, Level_Lose;
        SoundEffect Input_Correct, Input_Wrong;


        Player thePlayer;
        MainGoal goal;
        Door door;
        xAxis xaxis;
        yAxis yaxis;
        SwitchBoard switchBoard1;
        SwitchBoard switchBoard2;
        Venster venster;
        Score score;
        InputAnswer inputanswer;

        GameObjectList times;
        GameObjectList floors;
        GameObjectList walls;
        GameObjectList goals;
        GameObjectList guards;
        GameObjectList lasers;
        GameObjectList vensters;

        public float timer, total_time, time;
        public float timebarSpace;

        public PlayingState()
        {
            Reset();
            timebarSpace = 10.768F;
            this.Add(new SpriteGameObject("spr_background"));

            thePlayer = new Player(3, 13);
            switchBoard1 = new SwitchBoard(14, 10, Color.Red);
            switchBoard2 = new SwitchBoard(6, 12, Color.Blue);
            door = new Door(2, 0);

            Mouse.SetPosition(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);

            floors = new GameObjectList();
            walls = new GameObjectList();
            venster = new Venster(0, 0, "Map/spr_venster_352");
            goals = new GameObjectList();
            vensters = new GameObjectList();

            xaxis = new xAxis(8);
            yaxis = new yAxis(10);
            goal = new MainGoal(20, 11);
            guards = new GameObjectList();
            lasers = new GameObjectList();
            times = new GameObjectList();
            score = new Score((int) time);
            inputanswer = new InputAnswer(75, 720);

            this.Add(floors);
            this.Add(lasers);
            this.Add(walls);
            this.Add(switchBoard1);
            this.Add(switchBoard2);
            this.Add(door);
            this.Add(xaxis);
            this.Add(yaxis);
            this.Add(goal);
            this.Add(goals);
            this.Add(guards);
            this.Add(thePlayer);
            this.Add(venster);
            this.Add(times);
            this.Add(score);
            this.Add(inputanswer);

            goals.Add(new ExtraGoal(3, 3));
            guards.Add(new Guard(new Vector2(13, 2), new Vector2(25, 7)));
            FloorSetup();
            WallSetup();
            TimeBarSetup();
            SoundSetup();
            lasers.Add(new Laser(new Vector2(1, 11), new Vector2(8, 6), Color.Red, xaxis.gridPos, yaxis.gridPos));
            lasers.Add(new Laser(new Vector2(23, 7), new Vector2(28, 12), Color.Blue, xaxis.gridPos, yaxis.gridPos));

            foreach(Laser laser in lasers.Children)
            {
                laser.formulPos.X = laser.gridPos.X - xaxis.gridPos;
                laser.formulPos.Y = laser.gridPos.Y - yaxis.gridPos;
                laser.formulPos2.X = laser.gridPos.X - xaxis.gridPos;
                laser.formulPos2.Y = laser.gridPos.Y - yaxis.gridPos;
            }
        }
        public override void Reset()
        {
            base.Reset();
            total_time = 5 * 60;
            time = total_time;
            timer = 0;
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Keys.R))
            {
                foreach (GameObject gameObject in Children)
                {
                    gameObject.Reset();
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (goal.PixelCollision(thePlayer))
            {
                if (inputHelper.IsKeyDown(Keys.Space))
                {
                    goal.Hold(thePlayer);
                    if (!door.open)
                    {
                        door.Timer = 1;
                    }
                    door.open = true;
                    if (!goal.hold)
                    {
                        goal.Timer = GameEnvironment.Random.Next(1, 3);
                    }
                    goal.hold = true;
                }
            }
            else
            {
                goal.hold = false;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            foreach (ExtraGoal extraGoal in goals.Children)
            {
                if (extraGoal.PixelCollision(thePlayer))
                {
                    if (inputHelper.IsKeyDown(Keys.Space))
                    {
                        extraGoal.Hold(thePlayer);
                        if (!extraGoal.hold)
                        {
                            extraGoal.Timer = GameEnvironment.Random.Next(1, 3);
                            score.score += 1000;
                        }
                        extraGoal.hold = true;
                    }
                }
                else
                {
                    extraGoal.hold = false;
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (thePlayer.PixelCollision(switchBoard1) || thePlayer.PixelCollision(switchBoard2))
            {
                if (inputHelper.KeyPressed(Keys.Space))
                {
                    if (!venster.open)
                    {
                        venster.Timer = 1;
                        venster.open = true;
                        inputanswer.open = true;
                    }
                    foreach (TimeBar timebar in times.Children)
                    {
                        timebar.open = true;
                    }
                }
            }
            else if (!thePlayer.PixelCollision(switchBoard1) && !thePlayer.PixelCollision(switchBoard2))
            {
                if (venster.open)
                {
                    venster.Timer = 1;
                }
                venster.open = false;
                inputanswer.open = false;
                foreach (TimeBar timebar in times.Children)
                {
                    timebar.open = false;
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (thePlayer.PixelCollision(switchBoard1))
            {
                foreach (Laser laser in lasers.Children)
                {
                    if (laser.color == switchBoard1.color && laser.Formula != inputanswer.text && inputHelper.KeyPressed(Keys.Enter))
                    {
                        score.score -= 500;
                    }
                    if (laser.color == switchBoard1.color && laser.Formula == inputanswer.text && inputHelper.KeyPressed(Keys.Enter))
                    {
                        laser.Active = false;
                    }
                }
            }

            if (thePlayer.PixelCollision(switchBoard2))
            {
                foreach (Laser laser in lasers.Children)
                {
                    if (laser.color == switchBoard2.color && laser.Formula != inputanswer.text && inputHelper.KeyPressed(Keys.Enter))
                    {
                        score.score -= 500;
                    }
                    if (laser.color == switchBoard2.color && laser.Formula == inputanswer.text && inputHelper.KeyPressed(Keys.Enter))
                    {
                        laser.Active = false;
                    }
                }
            }

            if (inputHelper.KeyPressed(Keys.Enter) && venster.open)
            {
                inputanswer.Button_Enter.Play();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            foreach (Wall wall in walls.Children)
            {
                if (door.Open)
                {
                    if (wall.CollidesWith(door))
                    {
                        wall.Die = true;
                    }
                }
                if (switchBoard1.CollidesWith(wall) || switchBoard2.CollidesWith(wall))
                {
                    wall.Die = true;
                }

                if (thePlayer.xaxisCol(wall))
                {
                    if (thePlayer.Intersection(wall).Y > 0)
                        thePlayer.yCol(wall);
                }
                if (thePlayer.yaxisCol(wall))
                {
                    if (thePlayer.Intersection(wall).X > 0)
                        thePlayer.xCol(wall);
                }
                foreach (Guard guard in guards.Children)
                {
                    if (guard.xaxisCol(wall))
                    {
                        if (guard.Intersection(wall).Y > 0)
                            guard.yCol(wall);
                    }
                    if (guard.yaxisCol(wall))
                    {
                        if (guard.Intersection(wall).X > 0)
                            guard.xCol(wall);
                    }
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            foreach (Laser laser in lasers.Children)
            {
                if (Collision.LineRect(laser.Position, laser.position2, thePlayer.BoundingBox)&& laser.Active)
                {
                    laser.Alert = 1;
                    laser.Col = 1;
                    thePlayer.Reset();
                    score.score -= 500;
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            foreach (Guard guard in guards.Children)
            {
                if (thePlayer.PixelCollision(guard))
                {
                    thePlayer.Reset();
                    score.score -= 500;
                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (door.Open)
            {
                if (goal.hold && thePlayer.PixelCollision(door))
                {
                    GameEnvironment.GameStateManager.SwitchTo("EndStateWon");
                    Level_Win.Play();
                    Reset();
                    score.Reset();
                    thePlayer.Reset();
                    lasers.Reset();
                    door.Reset();
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timer++;
            if (timer % 60 == 0)
            {
                time--;
            }
            foreach (TimeBar timebar in times.Children)
            {
                if ((total_time - time) / total_time > timebar.Position.Y / GameEnvironment.Screen.Y)
                {
                    if (timebar.Sprite.color != Color.DarkBlue && timebar.open)
                    {
                        timebar.Color_Off.Play();
                    }
                    timebar.Sprite.color = Color.DarkBlue;
                }
            }
        }

        public void FloorSetup()
        {
            for (int i = 0; i < 29; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    floors.Add(new Floor(i, j));
                }
            }
        }
        public void WallSetup()
        {
            #region wall_color
            for (int i = 1; i < 6; i++)
            {
                walls.Add(new Wall(i, 0, "Map/wall_color"));
            }
            for (int i = 9; i < 28; i++)
            {
                walls.Add(new Wall(i, 0, "Map/wall_color"));
            }
            for (int i = 6; i < 9; i++)
            {
                walls.Add(new Wall(i, 5, "Map/wall_color"));
            }
            for (int i = 17; i < 22; i++)
            {
                walls.Add(new Wall(i, 7, "Map/wall_color"));
            }
            #endregion
            #region wall_bot
            for (int i = 1; i < 6; i++)
            {
                walls.Add(new Wall(i, 15, "Map/wall_bot"));
            }
            for (int i = 7; i < 8; i++)
            {
                walls.Add(new Wall(i, 11, "Map/wall_bot"));
            }
            for (int i = 9; i < 14; i++)
            {
                walls.Add(new Wall(i, 15, "Map/wall_bot"));
            }
            for (int i = 15; i < 22; i++)
            {
                walls.Add(new Wall(i, 5, "Map/wall_bot"));
            }
            for (int i = 17; i < 28; i++)
            {
                walls.Add(new Wall(i, 15, "Map/wall_bot"));
            }
            #endregion
            #region wall_left
            for (int j = 1; j < 15; j++)
            {
                walls.Add(new Wall(0, j, "Map/wall_left"));
            }
            for (int j = 1; j < 5; j++)
            {
                walls.Add(new Wall(8, j, "Map/wall_left"));
            }
            for (int j = 12; j < 15; j++)
            {
                walls.Add(new Wall(8, j, "Map/wall_left"));
            }
            for (int j = 8; j < 15; j++)
            {
                walls.Add(new Wall(16, j, "Map/wall_left"));
            }
            for (int j = 6; j < 7; j++)
            {
                walls.Add(new Wall(22, j, "Map/wall_left"));
            }

            #endregion
            #region wall_right
            for (int j = 1; j < 5; j++)
            {
                walls.Add(new Wall(6, j, "Map/wall_right"));
            }
            for (int j = 12; j < 15; j++)
            {
                walls.Add(new Wall(6, j, "Map/wall_right"));
            }
            for (int j = 6; j < 15; j++)
            {
                walls.Add(new Wall(14, j, "Map/wall_right"));
            }
            for (int j = 1; j < 15; j++)
            {
                walls.Add(new Wall(28, j, "Map/wall_right"));
            }
            #endregion
            #region wall_inside
            walls.Add(new Wall(0, 0, "Map/wall_inside_left_top"));
            walls.Add(new Wall(8, 0, "Map/wall_inside_left_top"));
            walls.Add(new Wall(16, 7, "Map/wall_inside_left_top"));

            walls.Add(new Wall(0, 15, "Map/wall_inside_left_bot"));
            walls.Add(new Wall(8, 15, "Map/wall_inside_left_bot"));
            walls.Add(new Wall(16, 15, "Map/wall_inside_left_bot"));


            walls.Add(new Wall(6, 15, "Map/wall_inside_right_bot"));
            walls.Add(new Wall(14, 15, "Map/wall_inside_right_bot"));
            walls.Add(new Wall(28, 15, "Map/wall_inside_right_bot"));


            walls.Add(new Wall(6, 0, "Map/wall_inside_right_top"));
            walls.Add(new Wall(28, 0, "Map/wall_inside_right_top"));

            for (int j = 0; j < 5; j++)
            {
                walls.Add(new Wall(7, j, "Map/wall_inside"));
            }
            for (int j = 12; j < 16; j++)
            {
                walls.Add(new Wall(7, j, "Map/wall_inside"));
            }
            for (int j = 6; j < 16; j++)
            {
                walls.Add(new Wall(15, j, "Map/wall_inside"));
            }

            for (int i = 16; i < 22; i++)
            {
                walls.Add(new Wall(i, 6, "Map/wall_inside"));
            }
            #endregion
            #region corners
            walls.Add(new Wall(8, 11, "Map/wall_right_top"));
            walls.Add(new Wall(22, 5, "Map/wall_right_top"));

            walls.Add(new Wall(6, 11, "Map/wall_left_top"));
            walls.Add(new Wall(14, 5, "Map/wall_left_top"));


            walls.Add(new Wall(22, 7, "Map/wall_right_bot"));
            #endregion
        }
        public void SoundSetup()
        {
            Level_Win = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Level Win");
            Level_Lose = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Slide");

            Input_Correct = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Correct");
            Input_Wrong = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Wrong");


            //Loop = GameEnvironment.AssetManager.Content.Load<Song>("Loop");
        }
        public void TimeBarSetup()
        {
            for (int i = 0; i < 100; i++)
            {
                times.Add(new TimeBar(new Vector2(8, i * timebarSpace), time));
            }
        }
    }
}
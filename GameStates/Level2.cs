﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FancyKlepto.GameObjects;
using FancyKlepto.GameObjects.MapObjects;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using FancyKlepto.GameObjects.MapObjects.axis;

namespace FancyKlepto.GameStates
{
    class Level2 : GameObjectList
    {
        SwitchBoard currentSwitchboard;
        Song Loop;
        SoundEffect Level_Win, Level_Lose;
        SoundEffect Input_Correct, Input_Wrong;

        Player thePlayer;
        MainGoal goal;
        Door door;
        Xaxis xaxis;
        Yaxis yaxis;
        Score score;

        InputScreen inputScreen;
        InputAnswer inputanswer;

        SpriteGameObject timeGround;
        GameObjectList times;
        GameObjectList floors;
        GameObjectList walls;
        GameObjectList goals;
        GameObjectList guards;
        GameObjectList lasers;
        GameObjectList switchBoards;
        Axis_numbers Axis_nums;

        public float timer, total_time, time;
        public float timebarSpace;
        public int timebarCounter;

        public Level2()
        {
            Console.WriteLine("Level2");
            Reset();
            timebarSpace = 10.768F;
            this.Add(new SpriteGameObject("spr_background"));

            thePlayer = new Player(3, 3);
            door = new Door(14, 0);
            goal = new MainGoal(26, 3);
            xaxis = new Xaxis(8, "Map/spr_horizontal_art_blue");
            yaxis = new Yaxis(20, "Map/spr_vertical_art_blue");

            Mouse.SetPosition(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);

            floors = new GameObjectList();
            walls = new GameObjectList();
            goals = new GameObjectList();

            inputScreen = new InputScreen(GameEnvironment.Screen.X / 2 - 64 * 2, GameEnvironment.Screen.Y);
            inputanswer = new InputAnswer(GameEnvironment.Screen.X / 2 - 64 * 2, GameEnvironment.Screen.Y);
            timeGround = new SpriteGameObject("Map/time_ground");

            guards = new GameObjectList();
            lasers = new GameObjectList();
            times = new GameObjectList();
            score = new Score(12, 20, (int)time);
            Axis_nums = new Axis_numbers(20, 8);
            switchBoards = new GameObjectList();

            this.Add(floors);
            this.Add(switchBoards);
            this.Add(lasers);
            this.Add(walls);
            this.Add(door);
            this.Add(xaxis);
            this.Add(yaxis);
            this.Add(Axis_nums);
            this.Add(goal);
            this.Add(goals);
            this.Add(guards);
            this.Add(thePlayer);
            this.Add(inputScreen);
            this.Add(timeGround);
            this.Add(times);
            this.Add(score);
            this.Add(inputanswer);

            goals.Add(new ExtraGoal(11, 9));
            guards.Add(new Guard(new Vector2(3, 13), new Vector2(25, 13)));
            FloorSetup();
            WallSetup();
            TimeBarSetup();
            SoundSetup();
            lasers.Add(new Laser(new Vector2(11, 6), new Vector2(14, 10), Color.Red, xaxis.gridPos, yaxis.gridPos));
            lasers.Add(new Laser(new Vector2(23, 10), new Vector2(28, 7), Color.Blue, xaxis.gridPos, yaxis.gridPos));
            switchBoards.Add(new SwitchBoard(3, 7, Color.Blue));
            switchBoards.Add(new SwitchBoard(22, 3, Color.Red));
        }
        public override void Reset()
        {
            base.Reset();
            total_time = 5 * 60;
            time = total_time;
            timer = 0;
            timebarCounter = 0;
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
                        door.Timer = true;
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
            foreach (SwitchBoard switchBoard in switchBoards.Children)
            {
                if (thePlayer.PixelCollision(switchBoard))
                {
                    currentSwitchboard = switchBoard;
                    if (inputHelper.KeyPressed(Keys.Space))
                    {
                        if (!inputScreen.open)
                        {
                            inputScreen.Timer = 1;
                            inputScreen.open = true;
                            inputanswer.open = true;
                        }
                        foreach (TimeBar timebar in times.Children)
                        {
                            timebar.open = true;
                        }
                    }
                    foreach (Laser laser in lasers.Children)
                    {
                        if (laser.color == switchBoard.color && laser.Formula != inputanswer.text && inputHelper.KeyPressed(Keys.Enter))
                        {
                            score.score -= 500;
                        }
                        if (laser.color == switchBoard.color && laser.Formula == inputanswer.text && inputHelper.KeyPressed(Keys.Enter))
                        {
                            laser.Active = false;
                            switchBoard.solved = true;
                        }
                    }
                }
            }
            if (inputHelper.KeyPressed(Keys.Enter))
            {
                inputanswer.Reset();
            }
            if (currentSwitchboard != null && !thePlayer.CollidesWith(currentSwitchboard))
            {
                foreach (Laser laser in lasers.Children)
                {
                    if (inputScreen.open)
                    {
                        inputScreen.Timer = 1;
                    }
                    inputScreen.open = false;
                    inputanswer.open = false;
                    inputanswer.Reset();
                    foreach (TimeBar timebar in times.Children)
                    {
                        timebar.open = false;
                    }
                }
            }

            if (inputHelper.KeyPressed(Keys.Enter) && inputScreen.open)
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

                if (thePlayer.XaxisCol(wall))
                {
                    if (thePlayer.Intersection(wall).Y > 0)
                        thePlayer.Ycol(wall);
                }
                if (thePlayer.YaxisCol(wall))
                {
                    if (thePlayer.Intersection(wall).X > 0)
                        thePlayer.Xcol(wall);
                }
                foreach (Guard guard in guards.Children)
                {
                    if (guard.XaxisCol(wall))
                    {
                        if (guard.Intersection(wall).Y > 0)
                            guard.Ycol(wall);
                    }
                    if (guard.YaxisCol(wall))
                    {
                        if (guard.Intersection(wall).X > 0)
                            guard.Xcol(wall);
                    }
                }
                foreach(SwitchBoard sw in switchBoards.Children)
                {

                    if (sw.CollidesWith(wall) )
                    {
                        wall.Die = true;
                    }
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            foreach (Laser laser in lasers.Children)
            {
                if (Collision.LineRect(laser.Position, laser.position2, thePlayer.BoundingBox) && laser.Active)
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
            if (door.Open && score.score != 0)
            {
                if (goal.hold && thePlayer.PixelCollision(door))
                {
                    GameEnvironment.GameStateManager.SwitchTo("Level3");
                    Level_Win.Play();
                    Reset();
                    score.Reset();
                    thePlayer.Reset();
                    lasers.Reset();
                    door.Reset();
                }
            }
            else
            {
                GameEnvironment.GameStateManager.SwitchTo("EndStateLost");
                Reset();
                score.Reset();
                thePlayer.Reset();
                lasers.Reset();
                door.Reset();
            }
            if (timebarCounter == 100)
            {
                GameEnvironment.GameStateManager.SwitchTo("EndStateLost");
                Reset();
                score.Reset();
                thePlayer.Reset();
                lasers.Reset();
                door.Reset();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (thePlayer.CollidesWith(door) && !door.open)
            {
                if (thePlayer.XaxisCol(door))
                {
                    if (thePlayer.Intersection(door).Y > 0)
                        thePlayer.Ycol(door);
                }
                if (thePlayer.YaxisCol(door))
                {
                    if (thePlayer.Intersection(door).X > 0)
                        thePlayer.Xcol(door);
                }
                foreach (Guard guard in guards.Children)
                {
                    if (guard.XaxisCol(door))
                    {
                        if (guard.Intersection(door).Y > 0)
                            guard.Ycol(door);
                    }
                    if (guard.YaxisCol(door))
                    {
                        if (guard.Intersection(door).X > 0)
                            guard.Xcol(door);
                    }
                }
            }
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
            for (int i = 1; i < 21; i++)
            {
                walls.Add(new Wall(i, 0, "Map/wall_color"));
            }
            for (int i = 23; i < 28; i++)
            {
                walls.Add(new Wall(i, 0, "Map/wall_color"));
            }
            for (int i = 1; i < 9; i++)
            {
                walls.Add(new Wall(i, 7, "Map/wall_color"));
            }
            for (int i = 9; i < 14; i++)
            {
                walls.Add(new Wall(i, 11, "Map/wall_color"));
            }
            for (int i = 17; i < 23; i++)
            {
                walls.Add(new Wall(i, 11, "Map/wall_color"));
            }
            #endregion
            #region wall_bot
            for (int i = 1; i < 10; i++)
            {
                walls.Add(new Wall(i, 6, "Map/wall_bot"));
            }
            for (int i = 11; i < 13; i++)
            {
                walls.Add(new Wall(i, 10, "Map/wall_bot"));
            }
            for (int i = 18; i < 21; i++)
            {
                walls.Add(new Wall(i, 10, "Map/wall_bot"));
            }
            for (int i = 1; i < 28; i++)
            {
                walls.Add(new Wall(i, 15, "Map/wall_bot"));
            }
            #endregion
            #region wall_left
            for (int j = 1; j < 6; j++)
            {
                walls.Add(new Wall(0, j, "Map/wall_left"));
            }
            for (int j = 8; j < 15; j++)
            {
                walls.Add(new Wall(0, j, "Map/wall_left"));
            }
            for (int j = 7; j < 10; j++)
            {
                walls.Add(new Wall(10, j, "Map/wall_left"));
            }
            for (int j = 1; j < 11; j++)
            {
                walls.Add(new Wall(22, j, "Map/wall_left"));
            }
            #endregion
            #region wall_right
            for (int j = 8; j < 11; j++)
            {
                walls.Add(new Wall(9, j, "Map/wall_right"));
            }
            for (int j = 1; j < 10; j++)
            {
                walls.Add(new Wall(21, j, "Map/wall_right"));
            }
            for (int j = 1; j < 15; j++)
            {
                walls.Add(new Wall(28, j, "Map/wall_right"));
            }
            #endregion
            #region wall_corners
            walls.Add(new Wall(10, 6, "Map/wall_right_top"));
            walls.Add(new Wall(13, 10, "Map/wall_right_top"));

            walls.Add(new Wall(17, 10, "Map/wall_left_top"));
            #endregion
            #region wall_inside
            walls.Add(new Wall(0, 0, "Map/wall_inside_left_top"));
            walls.Add(new Wall(22, 0, "Map/wall_inside_left_top"));
            walls.Add(new Wall(0, 7, "Map/wall_inside_left_top"));


            walls.Add(new Wall(0, 6, "Map/wall_inside_left_bot"));
            walls.Add(new Wall(0, 15, "Map/wall_inside_left_bot"));
            walls.Add(new Wall(10, 10, "Map/wall_inside_left_bot"));


            walls.Add(new Wall(21, 10, "Map/wall_inside_right_bot"));
            walls.Add(new Wall(28, 15, "Map/wall_inside_right_bot"));


            walls.Add(new Wall(28,0, "Map/wall_inside_right_top"));
            walls.Add(new Wall(9,7, "Map/wall_inside_right_top"));
            walls.Add(new Wall(21, 0, "Map/wall_inside_right_top"));
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
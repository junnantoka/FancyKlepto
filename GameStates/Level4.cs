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
    class Level4 : GameObjectList
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

        public Level4()
        {
            Console.WriteLine("Level4");
            Reset();
            timebarSpace = 10.768F;
            this.Add(new SpriteGameObject("spr_background"));

            thePlayer = new Player(2, 2);
            door = new Door(1, 0);
            xaxis = new Xaxis(8, "Map/spr_horizontal_art_blue");
            yaxis = new Yaxis(13, "Map/spr_vertical_art_blue");
            goal = new MainGoal(3, 13);

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
            Axis_nums = new Axis_numbers(13, 8);
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

            goals.Add(new ExtraGoal(7, 2));
            goals.Add(new ExtraGoal(12, 2));
            goals.Add(new ExtraGoal(22, 2));
            goals.Add(new ExtraGoal(26, 2));
            goals.Add(new ExtraGoal(27, 2));
            goals.Add(new ExtraGoal(26, 3));
            goals.Add(new ExtraGoal(27, 3));
            goals.Add(new ExtraGoal(17, 8));
            goals.Add(new ExtraGoal(9, 13));
            goals.Add(new ExtraGoal(26, 13));
            FloorSetup();
            WallSetup();
            TimeBarSetup();
            SoundSetup();
            lasers.Add(new Laser(new Vector2(21, 9), new Vector2(28, 6), Color.Red, xaxis.gridPos, yaxis.gridPos));
            lasers.Add(new Laser(new Vector2(13, 10), new Vector2(19, 4), Color.Blue, xaxis.gridPos, yaxis.gridPos));
            lasers.Add(new Laser(new Vector2(14, 12), new Vector2(26, 15), Color.Yellow, xaxis.gridPos, yaxis.gridPos));
            lasers.Add(new Laser(new Vector2(7, 12), new Vector2(5, 15), Color.Purple, xaxis.gridPos, yaxis.gridPos));
            lasers.Add(new Laser(new Vector2(1, 9), new Vector2(5, 11), Color.Green, xaxis.gridPos, yaxis.gridPos));
            lasers.Add(new Laser(new Vector2(26, 5), new Vector2(28, 5), Color.DarkGray, xaxis.gridPos, yaxis.gridPos));
            lasers.Add(new Laser(new Vector2(1, 5), new Vector2(6, 10), Color.Aqua, xaxis.gridPos, yaxis.gridPos));
            switchBoards.Add(new SwitchBoard(15, 10, Color.Red));
            switchBoards.Add(new SwitchBoard(6, 10, Color.Blue));
            switchBoards.Add(new SwitchBoard(23, 10, Color.Yellow));
            switchBoards.Add(new SwitchBoard(20, 11, Color.Purple));
            switchBoards.Add(new SwitchBoard(10, 11, Color.Green));
            switchBoards.Add(new SwitchBoard(17, 11, Color.DarkGray));
            switchBoards.Add(new SwitchBoard(4, 4, Color.Aqua));
            guards.Add(new Guard(new Vector2(2, 13), new Vector2(25, 13)));
            guards.Add(new Guard(new Vector2(26, 13), new Vector2(27, 1)));
            guards.Add(new Guard(new Vector2(5, 8), new Vector2(11, 2)));
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
                Reset();
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
                foreach (SwitchBoard sw in switchBoards.Children)
                {

                    if (sw.CollidesWith(wall))
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
                    GameEnvironment.GameStateManager.SwitchTo("Level5");
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
                        timebarCounter++;
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
            for (int i = 6; i <= 13; i++)
            {
                walls.Add(new Wall(i, 0, "Map/wall_color"));
            }
            for (int i = 16; i <= 23; i++)
            {
                walls.Add(new Wall(i, 0, "Map/wall_color"));
            }
            for (int i = 26; i <= 27; i++)
            {
                walls.Add(new Wall(i, 0, "Map/wall_color"));
            }
            for (int i = 4; i <= 5; i++)
            {
                walls.Add(new Wall(i, 6, "Map/wall_color"));
            }
            for (int i = 14; i <= 15; i++)
            {
                walls.Add(new Wall(i, 6, "Map/wall_color"));
            }
            for (int i = 24; i <= 25; i++)
            {
                walls.Add(new Wall(i, 6, "Map/wall_color"));
            }
            for (int i = 5; i <= 25; i++)
            {
                walls.Add(new Wall(i, 11, "Map/wall_color"));
            }
            #endregion
            #region wall_bot
            for (int i = 1; i <= 27; i++)
            {
                walls.Add(new Wall(i, 15, "Map/wall_bot"));
            }
            for (int i = 6; i <= 8; i++)
            {
                walls.Add(new Wall(i, 10, "Map/wall_bot"));
            }
            for (int i = 11; i <= 18; i++)
            {
                walls.Add(new Wall(i, 10, "Map/wall_bot"));
            }
            for (int i = 21; i <= 24; i++)
            {
                walls.Add(new Wall(i, 10, "Map/wall_bot"));
            }
            #endregion
            #region wall_left
            for (int j = 1; j <= 14; j++)
            {
                walls.Add(new Wall(0, j, "Map/wall_left"));
            }
            for (int j = 1; j <= 5; j++)
            {
                walls.Add(new Wall(5, j, "Map/wall_left"));
            }
            for (int j = 1; j <= 5; j++)
            {
                walls.Add(new Wall(15, j, "Map/wall_left"));
            }
            for (int j = 1; j <= 5; j++)
            {
                walls.Add(new Wall(25, j, "Map/wall_left"));
            }
            for (int j = 5; j <= 9; j++)
            {
                walls.Add(new Wall(10, j, "Map/wall_left"));
            }
            for (int j = 5; j <= 9; j++)
            {
                walls.Add(new Wall(20, j, "Map/wall_left"));
            }
            #endregion
            #region wall_right
            for (int j = 1; j <= 14; j++)
            {
                walls.Add(new Wall(28, j, "Map/wall_right"));
            }
            for (int j = 1; j <= 5; j++)
            {
                walls.Add(new Wall(4, j, "Map/wall_right"));
            }
            for (int j = 1; j <= 5; j++)
            {
                walls.Add(new Wall(14, j, "Map/wall_right"));
            }
            for (int j = 1; j <= 5; j++)
            {
                walls.Add(new Wall(24, j, "Map/wall_right"));
            }
            for (int j = 5; j <= 9; j++)
            {
                walls.Add(new Wall(9, j, "Map/wall_right"));
            }
            for (int j = 5; j <= 9; j++)
            {
                walls.Add(new Wall(19, j, "Map/wall_right"));
            }
            #endregion
            #region wall_inside

            walls.Add(new Wall(0, 0, "Map/wall_inside_left_top"));
            walls.Add(new Wall(5, 0, "Map/wall_inside_left_top"));
            walls.Add(new Wall(15, 0, "Map/wall_inside_left_top"));
            walls.Add(new Wall(25, 0, "Map/wall_inside_left_top"));


            walls.Add(new Wall(0, 15, "Map/wall_inside_left_bot"));
            walls.Add(new Wall(10, 10, "Map/wall_inside_left_bot"));
            walls.Add(new Wall(20, 10, "Map/wall_inside_left_bot"));

            walls.Add(new Wall(9, 10, "Map/wall_inside_right_bot"));
            walls.Add(new Wall(19, 10, "Map/wall_inside_right_bot"));
            walls.Add(new Wall(28, 15, "Map/wall_inside_right_bot"));

            walls.Add(new Wall(28, 0, "Map/wall_inside_right_top"));
            walls.Add(new Wall(24, 0, "Map/wall_inside_right_top"));
            walls.Add(new Wall(14, 0, "Map/wall_inside_right_top"));
            walls.Add(new Wall(4, 0, "Map/wall_inside_right_top"));
            #endregion
            #region corners
            walls.Add(new Wall(25, 10, "Map/wall_right_top"));
            walls.Add(new Wall(20, 4, "Map/wall_right_top"));
            walls.Add(new Wall(10, 4, "Map/wall_right_top"));

            walls.Add(new Wall(5, 10, "Map/wall_left_top"));
            walls.Add(new Wall(9, 4, "Map/wall_left_top"));
            walls.Add(new Wall(19, 4, "Map/wall_left_top"));
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
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FancyKlepto.GameObjects;
using FancyKlepto.GameObjects.MapObjects;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace FancyKlepto.GameStates
{
    class PlayingState : GameObjectList
    {
        Song Loop;
        SoundEffect Level_Win, Level_Lose;
        SoundEffect Input_Correct, Input_Wrong;
        SoundEffect Button_Enter, Button_Typing1, Button_Typing2, Button_Typing3;

        Player thePlayer;
        MainGoal goal;
        Door door;
        xAxis xaxis;
        yAxis yaxis;
        SwitchBoard switchBoard1;
        SwitchBoard switchBoard2;
        Venster_Object venster;

        GameObjectList times;
        GameObjectList floors;
        GameObjectList walls;
        GameObjectList goals;
        GameObjectList guards;
        GameObjectList lasers;

        public float timer, total_time,time;
        public float timebarSpace;

        public PlayingState()
        {
            Reset();
            timebarSpace = 10.768F;
            this.Add(new SpriteGameObject("spr_background"));
            thePlayer = new Player(3, 13);
            switchBoard1 = new SwitchBoard(14, 10, Color.Red);
            switchBoard2 = new SwitchBoard(6, 12, Color.Yellow);
            door = new Door(2, 15);

            Mouse.SetPosition(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);

            floors = new GameObjectList();
            walls = new GameObjectList();
            venster = new Venster_Object(0, 0, "spr_venster_352");
            goals = new GameObjectList();

            xaxis = new xAxis(8);
            yaxis = new yAxis(10);
            goal = new MainGoal(19, 10);
            guards = new GameObjectList();
            lasers = new GameObjectList();
            times = new GameObjectList();
            FloorSetup();
            WallSetup();
            TimeBarSetup();
            SoundSetup();

            this.Add(floors);
            this.Add(walls);
            this.Add(switchBoard1);
            this.Add(switchBoard2);
            this.Add(door);
            this.Add(lasers);
            this.Add(xaxis);
            this.Add(yaxis);
            this.Add(goal);
            this.Add(goals);
            this.Add(guards);
            this.Add(venster);
            this.Add(times);
            this.Add(thePlayer);

            goals.Add(new ExtraGoal(3, 3));
            guards.Add(new Guard(new Vector2(13, 2), new Vector2(25, 7)));
            lasers.Add(new Laser(new Vector2(1, 6), new Vector2(6, 5), Color.Red));
            //lasers.Add(new Laser(new Vector2(23, 7), new Vector2(28, 12), Color.Yellow));


        }
        public override void Reset()
        {
            base.Reset();
            total_time = 5*60;
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

            if (goal.CollidesWith(thePlayer))
            {
                goal.hold = true;
                if (inputHelper.IsKeyDown(Keys.Space))
                {
                    goal.Hold(thePlayer);
                    door.Visible = true;
                }
            }
            else
            {
                goal.hold = false;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            foreach (ExtraGoal extraGoal in goals.Children)
            {
                if (extraGoal.CollidesWith(thePlayer))
                {
                    extraGoal.hold = true;
                    if (inputHelper.IsKeyDown(Keys.Space))
                    {
                        extraGoal.Hold(thePlayer);
                    }
                }
                else
                {
                    extraGoal.hold = false;
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (thePlayer.CollidesWith(switchBoard1) || thePlayer.CollidesWith(switchBoard2))
            {
                    if (inputHelper.KeyPressed(Keys.Space))
                    {
                        venster.open = true;
                        foreach (TimeBar timebar in times.Children)
                        {
                            timebar.open = true;
                        }
                    }
            }
            else if (!thePlayer.CollidesWith(switchBoard1) && !thePlayer.CollidesWith(switchBoard2))
            {
                    venster.open = false;
                    foreach (TimeBar timebar in times.Children)
                    {
                        timebar.open = false;
                    }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            foreach (Wall wall in walls.Children)
            {
                if (door.Visible)
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
                if (Collision.LineRect(laser.Position, laser.position2, thePlayer.BoundingBox))
                {
                    thePlayer.Reset();
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            foreach (Guard guard in guards.Children)
            {
                if (guard.CollidesWith(thePlayer))
                {
                    thePlayer.Reset();
                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (door.Visible)
            {
                if (goal.hold && thePlayer.CollidesWith(door))
                {
                    GameEnvironment.GameStateManager.SwitchTo("EndStateWon");
                    Reset();
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
            foreach(TimeBar timebar in times.Children)
            {
                if ((total_time-time)/ total_time > timebar.Position.Y / GameEnvironment.Screen.Y)
                {
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
            for (int j = 0; j < 16; j++)
            {
                walls.Add(new Wall(0, j));
            }
            for (int j = 0; j < 16; j++)
            {
                walls.Add(new Wall(28, j));
            }
            for (int j = 0; j < 6; j++)
            {
                walls.Add(new Wall(6, j));
                walls.Add(new Wall(7, j));
                walls.Add(new Wall(8, j));
            }
            for (int j = 11; j < 16; j++)
            {
                walls.Add(new Wall(6, j));
                walls.Add(new Wall(7, j));
                walls.Add(new Wall(8, j));
            }
            for (int j = 11; j < 16; j++)
            {
                walls.Add(new Wall(14, j));
            }
            for (int j = 5; j < 9; j++)
            {
                walls.Add(new Wall(14, j));
            }
            for (int j = 5; j < 16; j++)
            {
                walls.Add(new Wall(15, j));
                walls.Add(new Wall(16, j));
            }

            for (int i = 0; i < 29; i++)
            {
                walls.Add(new Wall(i, 0));
                walls.Add(new Wall(i, 15));
            }
            for (int i = 17; i < 23; i++)
            {
                walls.Add(new Wall(i, 5));
                walls.Add(new Wall(i, 6));
                walls.Add(new Wall(i, 7));
            }
        }
        public void SoundSetup()
        {

            //Level_Win =      GameEnvironment.AssetManager.Content.Load<SoundEffect>("Level Win");
            //Level_Lose =   GameEnvironment.AssetManager.Content.Load<SoundEffect>("Slide");
            
            //Input_Correct =  GameEnvironment.AssetManager.Content.Load<SoundEffect>("Correct");
            //Input_Wrong =    GameEnvironment.AssetManager.Content.Load<SoundEffect>("Wrong");
            
            //Button_Enter =   GameEnvironment.AssetManager.Content.Load<SoundEffect>("Enter");
            //Button_Typing1 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("typing1");
            //Button_Typing2 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("typing2");
            //Button_Typing3 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("typing3");
            
            //Loop = GameEnvironment.AssetManager.Content.Load<Song>("Loop");
        }
        public void TimeBarSetup()
        {
            for (int i = 0; i<100; i++)
            {
                times.Add(new TimeBar(new Vector2(8,i* timebarSpace), time));
            }
        }
    }
}
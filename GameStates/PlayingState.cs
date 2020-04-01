using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FancyKlepto.GameObjects;
using FancyKlepto.GameObjects.MapObjects;

namespace FancyKlepto.GameStates
{
    class PlayingState : GameObjectList
    {
        Player thePlayer;
        MainGoal goal;
        Door door;

        GameObjectList floors;
        GameObjectList vensters;
        GameObjectList walls;
        GameObjectList goals;
        GameObjectList guards;
        GameObjectList lasers;
        GameObjectList switchboards;


        public PlayingState()
        {
            this.Add(new SpriteGameObject("spr_background"));
            thePlayer = new Player(3, 13);
            goal = new MainGoal(19, 10);
            door = new Door(2,15);

            Mouse.SetPosition(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);

            //score = new Score();
            floors = new GameObjectList();
            walls = new GameObjectList();
            vensters = new GameObjectList();
            goals = new GameObjectList();
            guards = new GameObjectList();
            lasers = new GameObjectList();
            switchboards = new GameObjectList();

            this.Add(floors);
            this.Add(switchboards);
            this.Add(walls);
            this.Add(door);
            this.Add(goals);
            this.Add(lasers);
            this.Add(goal);
            this.Add(guards);
            this.Add(vensters);
            this.Add(thePlayer);

            goals.Add(new ExtraGoal(2, 2));
            guards.Add(new Guard(new Vector2(13, 2), new Vector2(25, 5)));
            lasers.Add(new Laser(new Vector2(1, 6), new Vector2(6, 5), "spr_laser_pixel_green"));
            //lasers.Add(new Laser(new Vector2(23, 7), new Vector2(28, 12), "spr_laser_pixel_purple"));
            switchboards.Add(new SwitchBoard(14, 10));

            FloorSetup();
            WallSetup();
            VensterSetup();
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
            foreach (SwitchBoard switchBoard in switchboards.Children)
            {
                if (thePlayer.CollidesWith(switchBoard))
                {
                    foreach (Venster_Object venster in vensters.Children)
                    {
                        if (inputHelper.KeyPressed(Keys.Space))
                        {
                            venster.open = true;
                        }
                    }
                }
                else
                {
                    foreach (Venster_Object venster in vensters.Children)
                    {
                        venster.open = false;
                    }
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            foreach (Wall wall in walls.Children)
            {
                if (Math.Abs(thePlayer.Position.X - wall.Position.X) <= thePlayer.Sprite.Width * 2 &&
                    Math.Abs(thePlayer.Position.Y - wall.Position.Y) <= thePlayer.Sprite.Height * 2)
                {
                    thePlayer.Collision(wall);
                }

                if (door.Visible)
                {
                    if (wall.CollidesWith(door))
                    {
                        wall.Die = true;
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
                foreach (Wall wall in walls.Children)
                {
                    if (Math.Abs(guard.Position.X - wall.Position.X) <= guard.Sprite.Width * 2 &&
                        Math.Abs(guard.Position.Y - wall.Position.Y) <= guard.Sprite.Height * 2)
                    {
                        guard.Collision(wall);
                    }
                }
                if (guard.CollidesWith(thePlayer))
                {
                    thePlayer.Reset();
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (door.Visible && thePlayer.CollidesWith(goal) && goal.hold)
            {
                GameEnvironment.GameStateManager.SwitchTo("EndStateWon");
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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

        public void VensterSetup()
        {
            vensters.Add(new Venster_Object(0, 0, "spr_point_bar"));
            vensters.Add(new Venster_Object(5, 1067, "spr_point_bar_point"));
            vensters.Add(new Venster_Object(5, 1067 - 8 - 2 * unitSpacing, "spr_point_bar_point"));
            vensters.Add(new Venster_Object(32, 0, "spr_venster_background"));
            vensters.Add(new Venster_Object(64, 34, "spr_nickname"));
            vensters.Add(new Venster_Object(64, 132, "spr_lineair_visualiseren"));
            vensters.Add(new Venster_Object(64, 420, "spr_input"));
            vensters.Add(new Venster_Object(64, 516, "spr_xyz"));
            vensters.Add(new Venster_Object(64, 730, "spr_collected_items"));
            vensters.Add(new Venster_Object(64, 917, "spr_lives"));
            vensters.Add(new Venster_Object(64, 990, "spr_score"));
        }
    }
}
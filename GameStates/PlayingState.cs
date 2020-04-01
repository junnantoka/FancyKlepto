using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FancyKlepto.GameObjects;

namespace FancyKlepto.GameStates
{
    class PlayingState : GameObjectList
    {
        Player thePlayer;
        MainGoal goal;


        GameObjectList goals;
        GameObjectList guards;
        GameObjectList lasers;
        GameObjectList switchboards;


        public PlayingState()
        {
            this.Add(new SpriteGameObject("spr_background"));
            thePlayer = new Player(3, 13);
            goal = new MainGoal(2, 2);

            Mouse.SetPosition(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);

            //score = new Score();

            goals = new GameObjectList();
            guards = new GameObjectList();
            lasers = new GameObjectList();
            switchboards = new GameObjectList();

            this.Add(goals);
            this.Add(guards);
            this.Add(lasers);
            this.Add(switchboards);

            goals.Add(new ExtraGoal(19, 10));
            guards.Add(new Guard(new Vector2(65, 65), new Vector2(GameEnvironment.Screen.X, 65)));
            lasers.Add(new Laser(new Vector2(1, 6), new Vector2(6, 5), "spr_laser_pixel_green"));
            lasers.Add(new Laser(new Vector2(23, 7), new Vector2(28, 12), "spr_laser_pixel_purple"));
            switchboards.Add(new SwitchBoard(14, 9));

            FloorSetup();
            WallSetup();
            this.Add(thePlayer);
            VensterSetup();
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.KeyPressed(Keys.R))
            {
                thePlayer.Reset();
                goal.Reset();
                goals.Reset();
                switchboards.Reset();
                lasers.Reset();
                guards.Reset();
            }

            foreach (GameObjectList gameobject1 in Children)
            {
                foreach (GameObjectList gameobject2 in Children)
                {
                    if (gameobject1 is Player)
                    {

                        if (gameobject2 is MainGoal && gameobject1.Overlaps(gameobject2))
                        {
                            if (inputHelper.IsKeyDown(Keys.Space))
                            {
                                gameobject2.position.X = thePlayer.position.X + thePlayer.texture.Width / 2 - gameobject2.texture.Width / 2;
                                gameobject2.position.Y = thePlayer.position.Y + thePlayer.texture.Height / 2 - gameobject2.texture.Height / 2;
                            }
                        }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        if (gameobject2 is ExtraGoal && gameobject1.Overlaps(gameobject2))
                        {
                            if (inputHelper.IsKeyDown(Keys.Space))
                            {
                                gameobject2.position.X = thePlayer.position.X + thePlayer.texture.Width / 2 - gameobject2.texture.Width / 2;
                                gameobject2.position.Y = thePlayer.position.Y + thePlayer.texture.Height / 2 - gameobject2.texture.Height / 2;
                            }
                        }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        if (gameobject2 is SwitchBoard)
                        {
                            foreach (GameObject venster in Children)
                            {
                                if (venster is Venster_Object)
                                {
                                    if (gameobject1.Overlaps(gameobject2))
                                    {
                                        if (inputHelper.KeyPressed(Keys.Space))
                                        {
                                            venster.open = true;
                                        }
                                    }
                                    else
                                    {
                                        venster.open = false;
                                    }
                                }
                            }
                        }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        if (gameobject2 is Wall && gameobject1.Overlaps(gameobject2))
                        {
                            Vector2 wallPos = gameobject2.position;
                            Texture2D wallTex = gameobject2.texture;
                            //////////////////////////////////////////////////////////////////////                  horizontal
                            if (wallPos.X > thePlayer.position.X && gameobject1.Overlaps(gameobject2))
                            {
                                thePlayer.position.X -= Math.Abs(thePlayer.velocity.X);
                                thePlayer.velocity.X = 0;
                            }
                            if (wallPos.X < thePlayer.position.X && gameobject1.Overlaps(gameobject2))
                            {
                                thePlayer.position.X += Math.Abs(thePlayer.velocity.X);
                                thePlayer.velocity.X = 0;
                            }
                            //////////////////////////////////////////////////////////////////////                  vertical
                            if (wallPos.Y < thePlayer.position.Y && gameobject1.Overlaps(gameobject2))
                            {
                                thePlayer.position.Y += Math.Abs(thePlayer.velocity.Y);
                                thePlayer.velocity.Y = 0;
                            }
                            if (wallPos.Y > thePlayer.position.Y && gameobject1.Overlaps(gameobject2))
                            {
                                thePlayer.position.Y -= Math.Abs(thePlayer.velocity.Y);
                                thePlayer.velocity.Y = 0;
                            }
                        }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //if(gameobject2 is Laser)
                        //{
                        //    if (Collision.LineRect(gameobject2.position,gameobject2.position2,player.BoundingBox))
                        //    {
                        //        player.Reset();
                        //    }
                        //}
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            CollisionChecker();
            base.Update(gameTime);
        }

        public void CollisionChecker()
        {
            for (int i = 0; i < guard.Children.Count; i++)
            {
                if (guard.Overlaps(thePlayer))
                {
                    thePlayer.Reset();
                }
            }
        }

        public void FloorSetup()
        {
            for (int i = 0; i < 29; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    this.Add(new Floor(i, j));
                }
            }
        }
        public void WallSetup()
        {
            for (int j = 0; j < 16; j++)
            {
                this.Add(new Wall(0, j));
            }
            for (int j = 0; j < 16; j++)
            {
                this.Add(new Wall(28, j));
            }
            for (int j = 0; j < 6; j++)
            {
                this.Add(new Wall(6, j));
                this.Add(new Wall(7, j));
                this.Add(new Wall(8, j));
            }
            for (int j = 11; j < 16; j++)
            {
                this.Add(new Wall(6, j));
                this.Add(new Wall(7, j));
                this.Add(new Wall(8, j));
            }
            for (int j = 11; j < 16; j++)
            {
                this.Add(new Wall(14, j));
            }
            for (int j = 5; j < 9; j++)
            {
                this.Add(new Wall(14, j));
            }
            for (int j = 5; j < 16; j++)
            {
                this.Add(new Wall(15, j));
                this.Add(new Wall(16, j));
            }

            for (int i = 0; i < 29; i++)
            {
                this.Add(new Wall(i, 0));
                this.Add(new Wall(i, 15));
            }
            for (int i = 17; i < 23; i++)
            {
                this.Add(new Wall(i, 5));
                this.Add(new Wall(i, 6));
                this.Add(new Wall(i, 7));
            }
        }

        public void VensterSetup()
        {
            this.Add(new Venster_Object(0, 0, "spr_point_bar"));
            this.Add(new Venster_Object(5, 1067, "spr_point_bar_point"));
            this.Add(new Venster_Object(5, 1067 - 8 - 2 * unitSpacing, "spr_point_bar_point"));
            this.Add(new Venster_Object(32, 0, "spr_venster_background"));
            this.Add(new Venster_Object(64, 34, "spr_nickname"));
            this.Add(new Venster_Object(64, 132, "spr_lineair_visualiseren"));
            this.Add(new Venster_Object(64, 420, "spr_input"));
            this.Add(new Venster_Object(64, 516, "spr_xyz"));
            this.Add(new Venster_Object(64, 730, "spr_collected_items"));
            this.Add(new Venster_Object(64, 917, "spr_lives"));
            this.Add(new Venster_Object(64, 990, "spr_score"));
        }
    }
}
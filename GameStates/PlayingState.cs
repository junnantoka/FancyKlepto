using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyKlepto.GameManagement;
using Microsoft.Xna.Framework.Graphics;
using FancyKlepto.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FancyKlepto.GameStates
{
    class PlayingState : GameObjectList
    {
        Player player = new Player(3, 13);
        MainGoal goal1 = new MainGoal(2, 2);
        ExtraGoal goal2 = new ExtraGoal(19, 10);
        Guard guard = new Guard(new Vector2(100));
        Guard guard1 = new Guard(new Vector2(27, 3));

        SwitchBoard switchboard1 = new SwitchBoard(14, 9);
        SwitchBoard switchboard2 = new SwitchBoard(14, 10);
        public PlayingState()
        {
            this.Add(new SpriteGameObject("spr_background"));
            //gameObjectList.Add(new Map("spr_1.3"));
            FloorSetup();
            WallSetup();
            VensterSetup();

            this.Add(goal1);
            this.Add(goal2);
            this.Add(player);
            this.Add(guard);
            this.Add(guard1);
            this.Add(switchboard1);
            this.Add(switchboard2);
            //Map
        }

        public override void Update(GameTime gameTime)
        {
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.R))
            {
                player.Reset();
                guard.Reset();
                goal1.Reset();
                goal2.Reset();
                switchboard1.Reset();
                switchboard2.Reset();
            }
            base.Update(gameTime);

            foreach (SpriteGameObject gameObject in Children)
            {
                checkForCollision(gameObject);
            }
        }

        public void checkForCollision(SpriteGameObject pObject)
        {
            //Player collisions with smth.
            if (pObject is Player)
            {
                for (int i = 0; i < Children.Count; i++)
                {
                    if (Children[i] is Guard && pObject.Overlaps(Children[i]))
                    {
                        player.Reset();
                    }
                    if (Children[i] is MainGoal && pObject.Overlaps(Children[i]))
                    {
                        if (GameEnvironment.KeyboardState.IsKeyDown(Keys.Space))
                        {
                            Children[i].position.X = player.position.X + player.texture.Width / 2 - Children[i].texture.Width / 2;
                            Children[i].position.Y = player.position.Y + player.texture.Height / 2 - Children[i].texture.Height / 2;
                        }
                    }
                    if (Children[i] is ExtraGoal && pObject.Overlaps(Children[i]))
                    {
                        if (GameEnvironment.KeyboardState.IsKeyDown(Keys.Space))
                        {
                            Children[i].position.X = player.position.X + player.texture.Width / 2 - Children[i].texture.Width / 2;
                            Children[i].position.Y = player.position.Y + player.texture.Height / 2 - Children[i].texture.Height / 2;
                        }
                    }
                    if (Children[i] is SwitchBoard && pObject.Overlaps(gameObjectList[i]))
                    {
                        if (player.key.IsKeyDown(Keys.Space))
                        {
                            for (int j = 0; j < Children.Count; j++)
                            {
                                if (Children[j] is Venster_Object)
                                {
                                    Children[j].visual = true;
                                }
                            }
                        }
                    }
                    else if (Children[i] is SwitchBoard && !pObject.Overlaps(gameObjectList[i]))
                    {
                        for (int j = 0; j < Children.Count; j++)
                        {
                            if (Children[j] is Venster_Object)
                            {
                               Children[j].visual = false;
                            }
                        }
                    }

                    if (Children[i] is Wall)
                    {
                        Vector2 wallPos = Children[i].position;
                        Texture2D wallTex = Children[i].texture;
                        //horizontal
                        if (wallPos.X > player.position.X && pObject.Overlaps(Children[i]))
                        {
                            player.position.X -= Math.Abs(player.velocity.X);
                            player.moveRight = false;
                            player.velocity.X = 0;
                        }
                        else if (player.position.X + player.texture.Width + unitSpacing < wallPos.X)
                        {
                            player.moveRight = true;
                        }
                        ////////////////////////////////////////////////////////////////////
                        if (wallPos.X < player.position.X && pObject.Overlaps(Children[i]))
                        {
                            player.position.X += Math.Abs(player.velocity.X);
                            player.moveLeft = false;
                            player.velocity.X = 0;
                        }
                        else if (player.position.X > wallPos.X + wallTex.Width + unitSpacing)
                        {
                            player.moveLeft = true;
                        }
                        ////////////////////////////////////////////////////////////////////
                        //vertical
                        if (wallPos.Y < player.position.Y && pObject.Overlaps(Children[i]))
                        {
                            player.position.Y += Math.Abs(player.velocity.Y);
                            player.moveUp = false;
                            player.velocity.Y = 0;
                        }
                        else if (player.position.Y > wallPos.X + wallTex.Height + unitSpacing)
                        {
                            player.moveUp = true;
                        }
                        ////////////////////////////////////////////////////////////////////
                        if (wallPos.Y > player.position.Y && pObject.Overlaps(Children[i]))
                        {
                            player.position.Y -= Math.Abs(player.velocity.Y);
                            player.moveDown = false;
                            player.velocity.Y = 0;
                        }
                        else if (player.position.Y + player.texture.Height + unitSpacing < wallPos.Y)
                        {
                            player.moveDown = true;
                        }
                        ////////////////////////////////////////////////////////////////////
                    }
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
            this.Add(new Venster_Object(1568, 0, "spr_point_bar"));
            this.Add(new Venster_Object(1600, 0, "spr_venster_background"));
            this.Add(new Venster_Object(1632, 34, "spr_nickname"));
            this.Add(new Venster_Object(1632, 132, "spr_lineair_visualiseren"));
            this.Add(new Venster_Object(1632, 420, "spr_input"));
            this.Add(new Venster_Object(1632, 516, "spr_xyz"));
            this.Add(new Venster_Object(1632, 730, "spr_collected_items"));
            this.Add(new Venster_Object(1632, 917, "spr_lives"));
            this.Add(new Venster_Object(1632, 990, "spr_score"));

            this.Add(new Venster_Object(1573, 1067, "spr_point_bar_point"));
            this.Add(new Venster_Object(1573, 1067 - 8 - 2 * unitSpacing, "spr_point_bar_point"));
        }
    }
}
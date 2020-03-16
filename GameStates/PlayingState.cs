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
    class PlayingState : GameState
    {
<<<<<<< HEAD
        Player player = new Player();
        MainGoal goal1 = new MainGoal(new Vector2(400, 390), "Main Reward");
        Guard guard = new Guard(new Vector2(90, 50));
        public PlayingState()
        {
            gameObjectList.Add(new GameObject("spr_background"));
            gameObjectList.Add(new Map("Level_A"));//Map
=======
        Player player = new Player(3,13);
        MainGoal goal1 = new MainGoal(2,2);
        ExtraGoal goal2 = new ExtraGoal(19, 10);
        Guard guard = new Guard(new Vector2(100));
        Guard guard1 = new Guard(new Vector2(27, 3));

        SwitchBoard switchboard1 = new SwitchBoard(14 , 9);
        SwitchBoard switchboard2 = new SwitchBoard(14, 10);
        public PlayingState()
        {
            gameObjectList.Add(new GameObject("spr_background"));
            //gameObjectList.Add(new Map("spr_1.3"));
            FloorSetup();
            WallSetup();
            VensterSetup();

>>>>>>> master
            gameObjectList.Add(goal1);
            gameObjectList.Add(goal2);
            gameObjectList.Add(player);
            gameObjectList.Add(guard);
            gameObjectList.Add(guard1);
            gameObjectList.Add(switchboard1);
            gameObjectList.Add(switchboard2);
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
            foreach (GameObject gameObject in gameObjectList)
            {
                checkForCollision(gameObject);
            }
        }

        public void checkForCollision(GameObject pObject)
        {
            //Player collisions with smth.
            if (pObject is Player)
            {
                for (int i = 0; i < gameObjectList.Count; i++)
                {
                    if (gameObjectList[i] is Guard && pObject.Overlaps(gameObjectList[i]))
                    {
                        player.Reset();
                    }
<<<<<<< HEAD

=======
>>>>>>> master
                    if (gameObjectList[i] is MainGoal && pObject.Overlaps(gameObjectList[i]))
                    {
                        if (GameEnvironment.KeyboardState.IsKeyDown(Keys.Space))
                        {
                            gameObjectList[i].position.X = player.position.X + player.texture.Width/2 - gameObjectList[i].texture.Width/2;
                            gameObjectList[i].position.Y = player.position.Y + player.texture.Height/2 - gameObjectList[i].texture.Height/2;
                        }
                    }
<<<<<<< HEAD

                    if (gameObjectList[i] is Wall && pObject.Overlaps(gameObjectList[i]))
=======
                    if (gameObjectList[i] is ExtraGoal && pObject.Overlaps(gameObjectList[i]))
                    {
                        if (GameEnvironment.KeyboardState.IsKeyDown(Keys.Space))
                        {
                            gameObjectList[i].position.X = player.position.X + player.texture.Width / 2 - gameObjectList[i].texture.Width / 2;
                            gameObjectList[i].position.Y = player.position.Y + player.texture.Height / 2 - gameObjectList[i].texture.Height / 2;
                        }
                    }
                    if (gameObjectList[i] is SwitchBoard && pObject.Overlaps(gameObjectList[i]))
>>>>>>> master
                    {
                        if (player.key.IsKeyDown(Keys.Space))
                        {
<<<<<<< HEAD
                            player.velocity.X *= 1;
=======
                            for (int j = 0; j < gameObjectList.Count; j++)
                            {
                                if (gameObjectList[j] is Venster_Object)
                                {
                                    gameObjectList[j].visual = true;
                                }
                            }
>>>>>>> master
                        }
                    }
                    else if (gameObjectList[i] is SwitchBoard && !pObject.Overlaps(gameObjectList[i]))
                    {
                        for (int j = 0; j < gameObjectList.Count; j++)
                        {
<<<<<<< HEAD
                            player.velocity.X *= 1;
=======
                            if (gameObjectList[j] is Venster_Object)
                            {
                               gameObjectList[j].visual = false;
                            }
>>>>>>> master
                        }
                    }

                    if (gameObjectList[i] is Wall)
                    {
                        Vector2 wallPos = gameObjectList[i].position;
                        Texture2D wallTex = gameObjectList[i].texture;
                        //horizontal
                        if (wallPos.X > player.position.X && pObject.Overlaps(gameObjectList[i]))
                        {
                            player.position.X -= Math.Abs(player.velocity.X);
                            player.moveRight = false;
                            player.velocity.X = 0;
                        }
                        else if (player.position.X + player.texture.Width + unitSpacing<wallPos.X)
                        {
                            player.moveRight = true;
                        }
                        ////////////////////////////////////////////////////////////////////
                        if (wallPos.X < player.position.X && pObject.Overlaps(gameObjectList[i]))
                        {
                            player.position.X += Math.Abs(player.velocity.X);
                            player.moveLeft = false;
                            player.velocity.X = 0;
                        }
                        else if (player.position.X> wallPos.X+wallTex.Width +  unitSpacing)
                        {
                            player.moveLeft = true;
                        }
                        ////////////////////////////////////////////////////////////////////
                        //vertical
                        if (wallPos.Y < player.position.Y && pObject.Overlaps(gameObjectList[i]))
                        {
                            player.position.Y += Math.Abs(player.velocity.Y);
                            player.moveUp = false;
                            player.velocity.Y = 0;
                        }
                        else if (player.position.Y> wallPos.X+wallTex.Height + unitSpacing)
                        {
                            player.moveUp = true;
                        }
                        ////////////////////////////////////////////////////////////////////
                        if (wallPos.Y > player.position.Y && pObject.Overlaps(gameObjectList[i]))
                        {
                            player.position.Y -= Math.Abs(player.velocity.Y);
                            player.moveDown = false;
                            player.velocity.Y = 0;
                        }
                        else if (player.position.Y+player.texture.Height + unitSpacing<wallPos.Y)
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
            for (int i = 0; i < 29; i++) { 
                for (int j = 0; j < 16; j++)
                {
                    gameObjectList.Add(new Floor(i, j));
                }
            }
        }
        public void WallSetup()
        {
            for (int j = 0; j < 16; j++)
            {
                gameObjectList.Add(new Wall(0, j));
            }
            for (int j = 0; j < 16; j++)
            {
                gameObjectList.Add(new Wall(28, j));
            }
            for (int j = 0; j < 6; j++)
            {
                gameObjectList.Add(new Wall(6, j));
                gameObjectList.Add(new Wall(7, j));
                gameObjectList.Add(new Wall(8, j));
            }
            for (int j = 11; j < 16; j++)
            {
                gameObjectList.Add(new Wall(6, j));
                gameObjectList.Add(new Wall(7, j));
                gameObjectList.Add(new Wall(8, j));
            }
            for (int j = 11; j < 16;j++)
            {
                gameObjectList.Add(new Wall(14, j));
            }
            for (int j = 5; j < 9; j++)
            {
                gameObjectList.Add(new Wall(14, j));
            }
            for (int j = 5; j<16; j++)
            {
                gameObjectList.Add(new Wall(15, j));
                gameObjectList.Add(new Wall(16, j));
            }

            for (int i = 0; i < 29; i++)
            {
                gameObjectList.Add(new Wall(i, 0));
                gameObjectList.Add(new Wall(i, 15));
            }
            for (int i= 17; i < 23; i++)
            {
                gameObjectList.Add(new Wall(i, 5));
                gameObjectList.Add(new Wall(i, 6));
                gameObjectList.Add(new Wall(i, 7));
            }
        }

        public void VensterSetup()
        {
            gameObjectList.Add(new Venster_Object(1568, 0, "spr_point_bar"));
            gameObjectList.Add(new Venster_Object(1600, 0, "spr_venster_background"));
            gameObjectList.Add(new Venster_Object(1632, 34, "spr_nickname"));
            gameObjectList.Add(new Venster_Object(1632, 132, "spr_lineair_visualiseren"));
            gameObjectList.Add(new Venster_Object(1632, 420, "spr_input"));
            gameObjectList.Add(new Venster_Object(1632, 516, "spr_xyz"));
            gameObjectList.Add(new Venster_Object(1632, 730, "spr_collected_items"));
            gameObjectList.Add(new Venster_Object(1632, 917, "spr_lives"));
            gameObjectList.Add(new Venster_Object(1632, 990, "spr_score"));

            gameObjectList.Add(new Venster_Object(1573, 1067, "spr_point_bar_point"));
            gameObjectList.Add(new Venster_Object(1573, 1067-8-2*unitSpacing, "spr_point_bar_point"));
        }
    }
}
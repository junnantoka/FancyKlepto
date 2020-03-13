using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyKlepto.GameManagement;
using Microsoft.Xna.Framework.Graphics;
using FancyKlepto.GameObjects;
using Microsoft.Xna.Framework;

namespace FancyKlepto.GameStates
{
    class PlayingState : GameState
    {
        Player player = new Player();
        MainGoal goal1 = new MainGoal(2,2);
        ExtraGoal goal2 = new ExtraGoal(19, 10);
        Guard guard = new Guard(new Vector2(100));
        Guard guard1 = new Guard(new Vector2(27, 3));

        SwitchBoard switchboard1 = new SwitchBoard(14 , 9);
        SwitchBoard switchboard2 = new SwitchBoard(14, 10);
        public PlayingState()
        {
            gameObjectList.Add(new GameObject("spr_background"));
            gameObjectList.Add(new Map("spr_1.3"));
            WallSetup();

            gameObjectList.Add(goal1);
            gameObjectList.Add(goal2);
            gameObjectList.Add(player);
            gameObjectList.Add(guard);
            gameObjectList.Add(guard1);
            //gameObjectList.Add(switchboard1);
            gameObjectList.Add(switchboard2);
            //Map

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (GameObject gameObject in gameObjectList)
            {
                checkForCollision(gameObject);
            }
        }

        public void checkForCollision(GameObject pObject)
        {
            //Player collisions with smth.
            if (pObject.GetType() == typeof(Player))
            {
                for (int i = 0; i < gameObjectList.Count; i++)
                {
                    if (gameObjectList[i].GetType() == typeof(Guard) && pObject.Overlaps(gameObjectList[i]))
                    {
                        player.Reset();
                    }

                    if (gameObjectList[i].GetType() == typeof(MainGoal) && pObject.Overlaps(gameObjectList[i]))
                    {
                        player.Reset();
                    }

                    if (gameObjectList[i].GetType() == typeof(ExtraGoal) && pObject.Overlaps(gameObjectList[i]))
                    {
                        player.Reset();
                    }

                    if (gameObjectList[i].GetType() == typeof(SwitchBoard) && pObject.Overlaps(gameObjectList[i]))
                    {
                        player.Reset();
                    }

                    if (gameObjectList[i].GetType() == typeof(Wall) && pObject.Overlaps(gameObjectList[i]))
                    {
                        if (gameObjectList[i].position.X > player.position.X)
                        {
                            player.velocity.X = -1;
                        }
                        if (gameObjectList[i].position.X < player.position.X)
                        {
                            player.velocity.X = 1;
                        }

                        if (gameObjectList[i].position.Y < player.position.Y)
                        {
                            player.velocity.Y = 1;
                        }

                        if (gameObjectList[i].position.Y > player.position.Y)
                        {
                            player.velocity.Y = -1;
                        }

                    }
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
    }
}

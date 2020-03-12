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
        MainGoal goal1 = new MainGoal("Main Reward");
        Guard guard = new Guard(new Vector2(100));
        public PlayingState()
        {
            gameObjectList.Add(new GameObject("spr_background"));
            //Map
            gameObjectList.Add(new Map("Level_A"));
            gameObjectList.Add(goal1);
            gameObjectList.Add(player);
            gameObjectList.Add(guard);
            WallSetup();
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

                    if (gameObjectList[i].GetType() == typeof(Wall) && pObject.Overlaps(gameObjectList[i]))
                    {
                        //player.Reset();
                        if (gameObjectList[i].position.X < player.position.X + player.texture.Width)
                        {
                            player.velocity.X *= -1;
                        }
                        if (gameObjectList[i].position.X + gameObjectList[i].texture.Width > player.position.X)
                        {
                            player.velocity.X *= -1;
                        }

                        if (gameObjectList[i].position.Y < player.position.Y)
                        {
                            player.velocity.Y *= -1;
                        }

                        if (gameObjectList[i].position.Y + gameObjectList[i].texture.Height > player.position.Y)
                        {
                            player.velocity.Y *= -1;
                        }

                    }
                }
            }
        }

        public void WallSetup()
        {
            for (int j = 0; j < 48; j++)
            {
                gameObjectList.Add(new Wall(9, j));
            }
        }
    }
}

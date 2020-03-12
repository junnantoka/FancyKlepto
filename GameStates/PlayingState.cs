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
        public PlayingState()
        {
            gameObjectList.Add(new GameObject("spr_background"));
            //Map
            gameObjectList.Add(new Map("Level_A"));
            gameObjectList.Add(new GameObject("Grid"));
            gameObjectList.Add(player);
            //Position1, Position2,Sprite naam
            gameObjectList.Add(new Laser(new Vector2(390, 390), new Vector2(470, 390), "spr_laser_pixel_green"));
            gameObjectList.Add(new Guard());
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //Collision with border of screen
            for (int i = 0; i < gameObjectList.Count; i++)
            {
                if (gameObjectList[i] is Guard)
                {
                    if (gameObjectList[i].Overlaps(player))
                    {
                        player.Reset();
                        gameObjectList[i].Reset();
                    }
                }
            }      
        }
    }
}

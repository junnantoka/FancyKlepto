using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using FancyKlepto.GameManagement;
using FancyKlepto.GameObjects;

namespace FancyKlepto
{
    class Guard : GameObject
    {
        Map map = new Map("spr_1.3");
        int frameCounter = 0;
        public Guard(Vector2 position) : base("spr_guard")
        {
            Reset();
            this.position = position;
            velocity = new Vector2(10, 10);
        }

        public override void Update()
        {
            base.Update();
            frameCounter++; //keep track of frames
            if (frameCounter > 10)
            {
                position.X += velocity.X;
                frameCounter = 0;
            }

            //Collision with border of screen
            if (position.X >= map.texture.Width - texture.Width)
            {
                velocity.X = -velocity.X;
            }
            if (position.X <= 0)
            {
                velocity.X = -velocity.X;
            }
        }
    }
}

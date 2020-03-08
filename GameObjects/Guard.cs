using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using FancyKlepto.GameManagement;

namespace FancyKlepto.GameObjects
{
    class Guard : GameObject
    {
        bool reverse = false;
        public Guard(Vector2 position, Vector2 velocity) : base("player")
        {
            Reset();
            this.position = position;
            this.velocity = velocity;
        }

        public override void Update()
        {
            base.Update();
            position.X += velocity.X;
            if (position.X > GameEnvironment.Screen.X - texture.Width)
            {
                velocity.X = -velocity.X;
            }
            if (position.X < 0 - texture.Width)
            {
                velocity.X = -velocity.X;
            }
        }
    }
}

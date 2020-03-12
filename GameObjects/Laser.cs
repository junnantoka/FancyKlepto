using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyKlepto.GameManagement;

namespace FancyKlepto
{
    class Laser : GameObject
    {
        private bool Active;
        private Vector2 position2;
        private float angle;
        private float radius;
        public Laser(Vector2 position, Vector2 position2, string assetName) : base(assetName)
        {
            this.position = position;
            this.position2 = position2;

        }

        public override void Update()
        {
            base.Update();
            if (GameEnvironment.KeyboardState.IsKeyUp(Keys.Space))
            {
                Active = true;
            }
            else
            {
                Active = false;
            }
            
            radius = (float)Math.Sqrt((position.X-position2.X) * (position.X - position2.X) + (position.Y-position2.Y) * (position.Y - position2.Y));
            angle = (float)Math.Atan2(position2.Y-position.Y,position2.X-position.X);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (Active)
            {
                spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)radius, 7), new Rectangle(0, 0, 1, 1), Color.White, angle, new Vector2(0, 0), SpriteEffects.None, 1);
            }
        }
    }
}
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
        private Vector2 WantedSize;
        private float angle;
        public Laser(Vector2 Position, string assetName, Vector2 WantedSize, float angle) : base(assetName)
        {
            this.position = Position;
            this.WantedSize = WantedSize;
            this.angle = angle;
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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (Active)
            {
                spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)WantedSize.X, (int)WantedSize.Y), new Rectangle(0, 0, 1, 1), Color.White, angle, new Vector2(0, 0), SpriteEffects.None, 1);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace FancyKlepto.GameObjects.MapObjects
{
    class Venster_Objects : SpriteGameObject
    {
        private Vector2 openPos;
        private float screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public Venster_Objects(int x, int y, string texture) : base(texture)
        {
            velocity = new Vector2(15, 0);
            position = new Vector2(x+ screenWidth, y);
            defPos = position;
            // 352 is total width of the open window
            openPos.X = defPos.X - 352;
        }

        public override void Update(GameTime gameTime)
        {
            Move();
        }
        public void Move()
        {
            if (open && position.X > openPos.X)
            {
                position.X -= velocity.X;
                if (position.X < openPos.X)
                {
                    position.X = openPos.X;
                }
            }
            else if (!open && position.X < defPos.X)
            {
                position.X += velocity.X;
                if (position.X > defPos.X)
                {
                    position.X = defPos.X;
                }
            }
        }
    }
}

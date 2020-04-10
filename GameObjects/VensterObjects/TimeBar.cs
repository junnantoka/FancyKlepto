using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace FancyKlepto.GameObjects
{
    class TimeBar : SpriteGameObject
    {
        private float screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        private Vector2 openPos;
        public SoundEffect Color_Off;
        public TimeBar(Vector2 pos,float Time) : base("Map/Time")
        {
            Color_Off = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/beep");
            velocity = new Vector2(15, 0);
            position = pos;
            position.Y += 3;
            position.X += screenWidth;
            defPos = position;

            // 352 is total width of the open window
            openPos.X = defPos.X - 352;

            Reset();
        }
        public override void Reset()
        {
            base.Reset(); if (position.Y >= 0 && position.Y <= 540)
                sprite.color = Color.Green;
            if (position.Y > 540 && position.Y <= 945)
                sprite.color = Color.Yellow;
            if (position.Y > 945 && position.Y <= 1080)
                sprite.color = Color.Red;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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

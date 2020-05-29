using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using FancyKlepto.GameObjects.MapObjects;

namespace FancyKlepto.GameObjects
{
    class TimeBar : SpriteGameObject
    {
        public SoundEffect Color_Off;
        public TimeBar(Vector2 pos,float Time) : base("Map/Time")
        {
            Color_Off = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/beep");
            position = pos;

            Reset();
        }
        public override void Reset()
        {
            //Depending on position on screen the texture gets a tint
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
        }
    }
}

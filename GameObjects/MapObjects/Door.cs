using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace FancyKlepto.GameObjects.MapObjects
{
    class Door : SpriteGameObject
    {
        protected SpriteSheet open_sprite;
        SoundEffect Door_Appear;
        public bool Timer;
        public Door (int x, int y): base("Map/Door_Close")
        {
            Reset();
            position = new Vector2(18 + x * (unitSize + unitSpacing), 10 + y * (unitSize + unitSpacing));
            defPos = position;

            Door_Appear = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Appear");
        }

        public override void Reset()
        {
            open_sprite = new SpriteSheet("Map/Door_Open", 0);
            open = false;
            base.Reset();
            position = defPos;
        }

        public  override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //door opening sound
            if (Timer)
            {
                Door_Appear.Play();
                Timer = false;
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //It changes the texture depending on open state
            if (open) {
                open_sprite.Draw(spriteBatch, this.GlobalPosition, origin);
            } else
            {
                sprite.Draw(spriteBatch, this.GlobalPosition, origin);
            }
        }
    }
}

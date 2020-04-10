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
        SoundEffect Door_Appear;
        public int Timer;
        public Door (int x, int y): base("Map/door")
        {
            Reset();
            position = new Vector2(18 + x * (unitSize + unitSpacing), 10 + 1 + y * (unitSize + unitSpacing));
            defPos = position;

            Door_Appear = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Appear");
        }

        public override void Reset()
        {
            base.Reset();
            position = defPos;
            visible = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Timer == 1)
            {
                Door_Appear.Play();
                Timer = 0;
            }
        }
    }
}

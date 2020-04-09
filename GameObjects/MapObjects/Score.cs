using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FancyKlepto.GameObjects.MapObjects
{
    class Score : TextGameObject
    {
        int timer;
        public int time;
        public int score;
        public Score(int Time) : base("Score")
        {
            time = Time;
            Reset();
        }

        public override void Reset()
        {
            base.Reset();
            position = new Vector2(0, 0);
            timer = 0;
            score = time * 33;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            text = score.ToString();

            timer++;
            score = time * 33;
            if (timer % 60 == 0)
            {
                time--;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(spriteFont, "UMU", new Vector2(GameEnvironment.Screen.X / 2, 50), Color.White);
        }
    }
}

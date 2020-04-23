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

        public Score(int x, int y, int Time) : base("Score")
        {
            position = new Vector2(18 + (x) * (unitSize + unitSpacing), y);
            time = Time;
            Reset();
        }

        public override void Reset()
        {
            base.Reset();
            timer = 0;
            score = 9900;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            text = "Score: " + score.ToString();

            timer++;
            if (timer % 60 == 0)
            {
                time--;
                score--;
            }
            if (score < 0)
            {
                score = 0;
            }
        }
    }
}

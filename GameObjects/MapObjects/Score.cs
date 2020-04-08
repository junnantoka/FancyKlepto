using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FancyKlepto.GameObjects.MapObjects
{
    class Score : TextGameObject
    {
        int timer;
        public int time;
        public int score;
        public Score(int Time) : base("Old_Englished_Boots")
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
    }
}

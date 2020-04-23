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
        private Vector2 openPos;
        private float screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

        public Score(int x, int y, int Time) : base("Score")
        {
            position = new Vector2(x + screenWidth, y);
            time = Time;
            // 352 is total width of the open window
            openPos.X = defPos.X - 352;
            Reset();
        }

        public override void Reset()
        {
            base.Reset();
            position = new Vector2(GameEnvironment.Screen.X / 2 - 70, 40);
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

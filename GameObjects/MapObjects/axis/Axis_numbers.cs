using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FancyKlepto.GameObjects.MapObjects.axis
{
    class Axis_numbers : GameObjectList
    {
        public Axis_numbers(int x, int y)
        {
            Reset();

            for (int i = 0; i < 30; i++)
            {
                TextGameObject xaxis_Numbers = new TextGameObject("Score");
                xaxis_Numbers.text = (i - x).ToString();
                xaxis_Numbers.position = new Vector2(i * (unitSize + unitSpacing), y * (unitSize + unitSpacing));
                Add(xaxis_Numbers);
            }
            for (int i = 0; i < 17; i++)
            {
                TextGameObject yaxis_Numbers = new TextGameObject("Score");
                yaxis_Numbers.text = (i - y).ToString();
                yaxis_Numbers.position = new Vector2(x * (unitSize + unitSpacing), i * (unitSize + unitSpacing));
                Add(yaxis_Numbers);
            }
        }

        public override void Reset()
        {
            base.Reset();
            velocity = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
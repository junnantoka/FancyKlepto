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
        TextGameObject xaxis_Numbers;
        TextGameObject yaxis_Numbers;
        public Axis_numbers(Vector2 position, Vector2 position2)
        {
            Reset();
            xaxis_Numbers = new TextGameObject("Score");
            yaxis_Numbers = new TextGameObject("Score");

            for (int i = 0; i < 30; i++)
            {
                this.Add(xaxis_Numbers);
                xaxis_Numbers.text = i.ToString();
                xaxis_Numbers.position = position - new Vector2(0, i * (unitSize + unitSpacing));
            }
            for (int i = 0; i < 17; i++)
            {
                this.Add(yaxis_Numbers);
                yaxis_Numbers.text = i.ToString();
                xaxis_Numbers.position = position - new Vector2(i, 0) * (unitSize + unitSpacing);
            }

            xaxis_Numbers.position = position * (unitSize + unitSpacing);
            yaxis_Numbers.position = position2 * (unitSize + unitSpacing);
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

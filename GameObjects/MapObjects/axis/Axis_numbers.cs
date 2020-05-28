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
            //Variables
            const int NEGATIVE = -1;
            const int OFFSET = 2;

            //Make 60 TextGameObjects for the X-axis
            for (int i = 0; i < 30; i++)
            {
                TextGameObject xaxis_Numbers = new TextGameObject("Axis_text");
                xaxis_Numbers.text = (i - x).ToString();
                xaxis_Numbers.position = new Vector2(i * (unitSize + unitSpacing), y * (unitSize + unitSpacing));
                xaxis_Numbers.Color = Color.Black;
                Add(xaxis_Numbers);
            }
            for (int i = 0; i < 30; i++)
            {
                TextGameObject xaxis_Numbers = new TextGameObject("Axis_text");
                xaxis_Numbers.text = (i - x).ToString();
                xaxis_Numbers.position = new Vector2(i * (unitSize + unitSpacing) - OFFSET, y * (unitSize + unitSpacing) - OFFSET);
                xaxis_Numbers.Color = Color.Yellow;
                Add(xaxis_Numbers);
            }
            //Make 34 TextGameObjects for Y-axis
            for (int i = 0; i < 17; i++)
            {
                TextGameObject yaxis_Numbers = new TextGameObject("Axis_text");
                yaxis_Numbers.text = ((i - y) * NEGATIVE).ToString();
                yaxis_Numbers.position = new Vector2(x * (unitSize + unitSpacing), i * (unitSize + unitSpacing));
                yaxis_Numbers.Color = Color.Black;
                Add(yaxis_Numbers);
            }
            for (int i = 0; i < 17; i++)
            {
                TextGameObject yaxis_Numbers = new TextGameObject("Axis_text");
                yaxis_Numbers.text = ((i - y) * NEGATIVE).ToString();
                yaxis_Numbers.position = new Vector2(x * (unitSize + unitSpacing) - OFFSET, i * (unitSize + unitSpacing) - OFFSET);
                yaxis_Numbers.Color = new Color(255, 255, 0);
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
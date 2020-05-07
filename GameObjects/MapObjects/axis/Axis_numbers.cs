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
        TextGameObject text;
        public Axis_numbers(int x, int y)
        {
            Reset();
            for (int i = 0; i < 29; i++)
            {
                this.Add(text);
                text.position.X = i;
                text.position.Y = y;
                text.text = (x - i).ToString();
            }
            for (int i = 0; i < 16; i++)
            {
                this.Add(text);
                text.position.X = x;
                text.position.Y = i;
                text.text = (y - i).ToString();
            }
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}

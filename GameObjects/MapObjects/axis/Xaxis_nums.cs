using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FancyKlepto.GameObjects.MapObjects.axis
{
    class Xaxis_nums : TextGameObject
    {
        public Xaxis_nums(int x) : base("Score")
        {
            position = new Vector2(18 + (x) * (unitSize + unitSpacing));
            Reset();
        }

        public override void Reset()
        {
            base.Reset();
            text = "";
        }
    }
}

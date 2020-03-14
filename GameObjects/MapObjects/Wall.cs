using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using FancyKlepto.GameManagement;
using FancyKlepto.GameObjects;

namespace FancyKlepto.GameObjects
{
    class Wall : GameObject
    {
        public Wall(int x, int y) : base("spr_black_wall")
        {
            Reset();
            position = new Vector2(x *( unitSize + unitSpacing), y * (unitSize + unitSpacing));
            pPosition =position ;
        }

        public override void Reset()
        {
            position = pPosition;
        }
    }
}

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
        private Vector2 pPosition;
        public Wall(int x, int y) : base("spr_black_wall")
        {
            position = new Vector2(x * unit, y * unit);
        }

        public override void Reset()
        {
            position = pPosition;
        }
    }
}

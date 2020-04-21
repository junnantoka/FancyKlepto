using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FancyKlepto.GameObjects
{
    class xAxis : SpriteGameObject
    {
        public int gridPos;
        public xAxis(int y) : base("Map/spr_horizontal_art_blue")
        {
            gridPos = y;
            position = new Vector2(18, 10 + (y - 2) * (unitSize + unitSpacing) - sprite.Height / 2);
        }
    }
}







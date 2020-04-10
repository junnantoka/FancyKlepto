using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FancyKlepto.GameObjects
{
    class yAxis : SpriteGameObject
    {
        public yAxis(int x) : base("Map/spr_vertical_art_blue")
        {
            position = new Vector2(18 + x * (unitSize + unitSpacing)-sprite.Width/2,10);
        }
    }
}
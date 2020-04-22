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
        public xAxis(int y, string assetName) : base(assetName)
        {
            gridPos = y;
            position = new Vector2(18, 10 + (y) * (unitSize + unitSpacing) - sprite.Height / 2);
        }
    }
}
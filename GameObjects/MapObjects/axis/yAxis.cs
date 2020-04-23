using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FancyKlepto.GameObjects
{
    class Yaxis : SpriteGameObject
    {
        public int gridPos;
        public Yaxis(int x, string assetName) : base(assetName)
        {
            gridPos = x;
            position = new Vector2(18 + (x) * (unitSize + unitSpacing) - sprite.Width / 2, 10);
        }
    }
}
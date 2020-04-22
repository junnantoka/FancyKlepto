﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FancyKlepto.GameObjects
{
    class yAxis : SpriteGameObject
    {
        public int gridPos;
        public yAxis(int x, string assetName) : base(assetName)
        {
            gridPos = x-2;
            position = new Vector2(18 + (x - 2) * (unitSize + unitSpacing) - sprite.Width / 2, 10);
        }
    }
}
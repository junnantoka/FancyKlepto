﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FancyKlepto.GameObjects
{
    class xAxis : SpriteGameObject
    {
        public xAxis(int y) : base("spr_horizontal_art_blue2")
        {
            position = new Vector2(18, 10 + y * (unitSize + unitSpacing)-sprite.Height/2);

        }
    }
}
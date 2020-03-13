﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using FancyKlepto.GameManagement;
using FancyKlepto.GameObjects;

namespace FancyKlepto
{
    class SwitchBoard : GameObject
    {
        private Vector2 pPosition;
        public SwitchBoard(int x,int y) : base("spr_switchboard_brown")
        {
            Reset();
            position = new Vector2(x * unit, y * unit);
            pPosition = position;
        }
        public override void Reset()
        {
            position = pPosition;
        }
    }
}

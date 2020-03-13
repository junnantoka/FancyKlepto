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
    class ExtraGoal : GameObject
    {
        public ExtraGoal (int x,int y) : base("spr_reward_2")
        {
            position = new Vector2(x* unit, y* unit);
        }
    }
}

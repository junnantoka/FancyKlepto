using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using FancyKlepto.GameManagement;
using FancyKlepto.GameObjects;

namespace FancyKlepto
{
    class MainGoal : GameObject
    {
        public MainGoal (int x,int y) : base("spr_maingoal")
        {
            Reset();
            position = new Vector2(2+x * (unitSize + unitSpacing), 2+y * (unitSize + unitSpacing));
            pPosition = position;
        }
        public override void Reset()
        {
            position = pPosition;
        }
    }
}

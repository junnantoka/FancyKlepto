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
        private Vector2 pPosition;
        public MainGoal (int x,int y) : base("spr_reward_1")
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

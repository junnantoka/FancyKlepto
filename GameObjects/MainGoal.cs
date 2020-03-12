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
        public MainGoal (string MainReward) : base(MainReward)
        {
            position = new Vector2(100, 100);
        }
    }
}

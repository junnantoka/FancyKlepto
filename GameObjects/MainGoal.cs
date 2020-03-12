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
        public MainGoal(Vector2 position, string MainReward) : base(MainReward)
        {
            this.position = position;
        }
    }
}

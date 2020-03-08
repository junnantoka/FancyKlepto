using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using FancyKlepto.GameManagement;

namespace FancyKlepto.GameObjects
{
    class Guard : GameObject
    {
        public Guard() : base("spr_circle")
        {
            Reset();
            position = new Vector2(400, 100);
            velocity = new Vector2(40, 40);
        }


    }
}

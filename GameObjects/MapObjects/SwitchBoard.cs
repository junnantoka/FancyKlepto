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
    class SwitchBoard : GameObject
    {
        public SwitchBoard(int x,int y) : base("spr_switchboard")
        {
            Reset();
            position = new Vector2(x *( unitSize + unitSpacing), y *( unitSize + unitSpacing));
            pPosition = position;
        }
        public override void Reset()
        {
            position = pPosition;
        }
    }
}

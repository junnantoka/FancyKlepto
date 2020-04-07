using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FancyKlepto.GameObjects.MapObjects
{
    class Door : SpriteGameObject
    {
        public Door (int x, int y): base("spr_door_art_light")
        {
            Reset();
            position = new Vector2(18 + x * (unitSize + unitSpacing), 10 + 1 + y * (unitSize + unitSpacing));
            defPos = position;
        }

        public override void Reset()
        {
            base.Reset();
            position = defPos;
            visible = false;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FancyKlepto.GameManagement;

namespace FancyKlepto.GameStates
{
    class StartState : GameObjectList
    {
        public StartState()
        {
            this.Add(new SpriteGameObject("spr_text"));
        }
    }
}
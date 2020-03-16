using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyKlepto.GameManagement;

namespace FancyKlepto.GameObjects
{
    class Map : SpriteGameObject
    {
        public Map(string LevelName) : base(LevelName)
        {
            position = new Vector2(0,0);
        }
    }
}

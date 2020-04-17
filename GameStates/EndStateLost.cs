using FancyKlepto.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace FancyKlepto.GameStates
{
    class EndStateLost : GameObjectList
    {
        SpriteGameObject background;
        public EndStateLost()
        {
            background = new SpriteGameObject("spr_background_end");
            this.Add(background);

            background.Position = GameEnvironment.Screen.ToVector2() / 2;
            background.Origin = new Vector2(background.Width / 2, background.Height / 2);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Keys.Enter))
            {
                GameEnvironment.GameStateManager.SwitchTo("StartState");
            }
        }
    }
}

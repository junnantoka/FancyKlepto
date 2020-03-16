using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using FancyKlepto.GameManagement;
using FancyKlepto.GameStates;


namespace FancyKlepto
{
    class Fancy_Klepto : GameEnvironment
    {
        int state = 0;

        protected override void LoadContent()
        {
            base.LoadContent();
            graphics.IsFullScreen = true;
            gameStateList.Add(new StartState());
            gameStateList.Add(new PlayingState());
            gameStateList.Add(new EndStateWon());
            gameStateList.Add(new EndStateLost());

            screen = new Point(1920, 1080);
            ApplyResolutionSettings();

            GameEnvironment.SwitchTo(state);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (inputHelper.KeyPressed(Keys.Enter))
            {
                state++;
                GameEnvironment.SwitchTo(state);
                state = 0;
            }
        }

    }
}

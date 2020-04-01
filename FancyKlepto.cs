using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FancyKlepto.GameStates;

namespace FancyKlepto
{
    class Fancy_Klepto : GameEnvironment
    {
        protected override void LoadContent()
        {
            base.LoadContent();


            screen = new Point(1920, 1080);
            ApplyResolutionSettings();

            graphics.IsFullScreen = false;
            GameStateManager.AddGameState("StartState", new StartState());
            GameStateManager.AddGameState("PlayingSate", new PlayingState());
            GameStateManager.AddGameState("EndStateWon", new EndStateWon());
            GameStateManager.AddGameState("EndStateLost", new EndStateLost());

            GameStateManager.SwitchTo("StartState");
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (inputHelper.KeyPressed(Keys.Enter))
            {
                GameStateManager.SwitchTo("PlayingState");
            }
        }

    }
}

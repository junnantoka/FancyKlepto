using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FancyKlepto.GameStates;
using FancyKlepto.GameObjects;
using FancyKlepto;

namespace FancyKlepto
{
    class Fancy_Klepto : GameEnvironment
    {
        protected override void LoadContent()
        {
            base.LoadContent();
            screen = new Point(1920, 1080);
            ApplyResolutionSettings();

            FullScreen = true;
            
            GameStateManager.AddGameState("StartState", new StartState());
            GameStateManager.AddGameState("Level1", new Level1());
            GameStateManager.AddGameState("Level2", new Level2());
            GameStateManager.AddGameState("EndStateWon", new EndStateWon());
            GameStateManager.AddGameState("EndStateLost", new EndStateLost());

            GameStateManager.SwitchTo("StartState");
        }
    }
}

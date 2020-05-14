using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FancyKlepto.GameStates
{
    class EndStateWon : GameObjectList
    {
        SpriteGameObject background;
        public EndStateWon()
        {
            background = new SpriteGameObject("WinState");
            this.Add(background);

            background.Position = GameEnvironment.Screen.ToVector2() / 2;
            background.Origin = new Vector2(background.Width / 2, background.Height / 2);
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Keys.Space))
            {
                GameEnvironment.GameStateManager.SwitchTo("StartState");
            }
        }
    }
}
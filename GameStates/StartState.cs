using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace FancyKlepto.GameStates
{
    class StartState : GameObjectList
    {
        SpriteGameObject background;
        public StartState()
        {
            background = new SpriteGameObject("spr_text");
            this.Add(background);

            background.Position = GameEnvironment.Screen.ToVector2() / 2;
            background.Origin = new Vector2(background.Width / 2, background.Height / 2);
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Keys.Enter))
            {
                GameEnvironment.GameStateManager.SwitchTo("PlayingState");
            }
        }
    }
}
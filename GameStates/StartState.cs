using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoSprites;

namespace FancyKlepto.GameStates
{
    class StartState : GameObjectList
    {
        SpriteGameObject background;
        TextGameObject start;
        int timer;
        int seconds;
        public StartState()
        {
            background = new SpriteGameObject("TitleScreen");
            start = new TextGameObject("Score");
            this.Add(background);
            this.Add(start);

            background.Position = GameEnvironment.Screen.ToVector2() / 2;
            background.Origin = new Vector2(background.Width / 2, background.Height / 2);

            start.position = GameEnvironment.Screen.ToVector2() / 2;
            start.text = "Press ENTER to start";
            start.Open = true;
            timer = 0;
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Keys.Enter))
            {
                GameEnvironment.GameStateManager.SwitchTo("Level1");
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timer++;
            seconds = timer / 60;
            if (seconds > 2)
            {
                start.Open = false;
                if (seconds > 4)
                {
                    start.Open = true;
                    timer = 0;
                }
            }
        }
    }
}
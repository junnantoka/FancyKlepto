using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace FancyKlepto.GameStates
{
    class StartState : GameObjectList
    {
        TextGameObject start;
        int timer;
        int seconds;
        public StartState()
        {
            start = new TextGameObject("Score");
            this.Add(start);

            start.position = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
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
                if(seconds > 4)
                {
                    start.Open = true;
                    timer = 0;
                }
            }
        }
    }
}
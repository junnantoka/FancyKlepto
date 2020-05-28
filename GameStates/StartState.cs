using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using MonoSprites;
using Microsoft.Xna.Framework.Audio;

namespace FancyKlepto.GameStates
{
    class StartState : GameObjectList
    {
        //Variables
        SpriteGameObject background;
        SpriteGameObject startButton;

        int frames;
        float seconds;
        int offset = 300;
        public StartState()
        {
            //Initialize
            background = new SpriteGameObject("TitleScreen");
            startButton = new SpriteGameObject("startButton");

            //Add to list
            this.Add(background);
            this.Add(startButton);

            //Background position and origin
            background.Position = GameEnvironment.Screen.ToVector2() / 2;
            background.Origin = new Vector2(background.Width / 2, background.Height / 2);

            //"startButton" position and origin
            startButton.position.X = GameEnvironment.Screen.X - startButton.Width / 2;
            startButton.position.Y = GameEnvironment.Screen.Y - startButton.Height / 2 + offset;
            startButton.Origin = new Vector2(background.Width / 2, background.Height / 2);
            
            //Default value(s)
            startButton.Open = true;
            frames = 0;
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            //Check for keyboard input
            if (inputHelper.KeyPressed(Keys.Enter))
            {
                GameEnvironment.GameStateManager.SwitchTo("ExplanationState");
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //Update frames every frame
            frames++;
            seconds = frames / 60;
            if (seconds > 0.5)
            {
                startButton.Open = false;
                if (seconds > 1)
                {
                    startButton.Open = true;
                    frames = 0;
                }
            }
        }
    }
}
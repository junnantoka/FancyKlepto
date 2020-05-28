using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using MonoSprites;

namespace FancyKlepto.GameStates
{
    class StartState : GameObjectList
    {
        //Variables
        SpriteGameObject background;
        SpriteGameObject startButton;

        const int OFFSET = 600;
        
        int frames;
        float seconds;
        int offset = 300;
        public StartState()
        {
            //Initialize
            background = new SpriteGameObject("TitleScreen");
            startButton = new SpriteGameObject("startButton");

            TextGameObject controls = new TextGameObject("Score");
            controls.text = "W, A, S, D om te bewegen. SPATIE voor interactie met objecten, ENTER om je formule te bevestigen";
            controls.position = new Vector2(GameEnvironment.Screen.X / 2 - OFFSET, GameEnvironment.Screen.Y / 2);

            //Shadow
            TextGameObject controls2 = new TextGameObject("Score");
            controls2.text = "W, A, S, D om te bewegen. SPATIE voor interactie met objecten, ENTER om je formule te bevestigen";
            controls2.position = new Vector2(GameEnvironment.Screen.X / 2 - OFFSET - 2, GameEnvironment.Screen.Y / 2 + 2);
            controls2.Color = Color.Black;

            //Add to list
            this.Add(background);
            this.Add(startButton);
            this.Add(controls2);
            this.Add(controls);

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
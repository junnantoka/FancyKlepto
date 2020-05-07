﻿using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using MonoSprites;

namespace FancyKlepto.GameStates
{
    class StartState : GameObjectList
    {
        SpriteGameObject background;
        SpriteGameObject startButton;

        
        int timer;
        int seconds;
        int offset = 300;
        public StartState()
        {
            background = new SpriteGameObject("TitleScreen");
            startButton = new SpriteGameObject("startButton");
            this.Add(background);
            this.Add(startButton);

            background.Position = GameEnvironment.Screen.ToVector2() / 2;
            background.Origin = new Vector2(background.Width / 2, background.Height / 2);

            startButton.position.X = GameEnvironment.Screen.X - startButton.Width / 2;
            startButton.position.Y = GameEnvironment.Screen.Y - startButton.Height / 2 + offset;
            startButton.Origin = new Vector2(background.Width / 2, background.Height / 2);
            
            startButton.Open = true;
            timer = 0;
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Keys.Enter))
            {
                GameEnvironment.GameStateManager.SwitchTo("ExplanationState");
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timer++;
            seconds = timer / 60;
            if (seconds > 0.5f)
            {
                startButton.Open = false;
                if (seconds > 1)
                {
                    startButton.Open = true;
                    timer = 0;
                }
            }
        }
    }
}
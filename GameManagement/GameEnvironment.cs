using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FancyKlepto.GameManagement
{
    class GameEnvironment : Game
    {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        static protected ContentManager content;
        protected static Point screen;
        protected static Random random;
        protected InputHelper inputHelper;
        static protected List<GameObject> gameStateList;
        static protected GameObject currentGameState;

        public static Point Screen
        {
            get { return screen; }
        }

        public static Random Random
        {
            get { return random; }
        }

        public static ContentManager ContentManager
        {
            get { return content; }
        }

        static public void SwitchTo(int gameStateIndex)
        {
            if (gameStateIndex >= 0 && gameStateIndex < gameStateList.Count)
                currentGameState = gameStateList[gameStateIndex];
        }

        public GameEnvironment()
        {
            graphics = new GraphicsDeviceManager(this);
            inputHelper = new InputHelper();
            Content.RootDirectory = "Content";
            content = Content;
            gameStateList = new List<GameObject>();
            random = new Random();
        }

        public void ApplyResolutionSettings()
        {
            graphics.PreferredBackBufferWidth = screen.X;
            graphics.PreferredBackBufferHeight = screen.Y;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected void HandleInput()
        {
            inputHelper.Update();
            if (inputHelper.KeyPressed(Keys.Escape))
            {
                Exit();
            }

            if (currentGameState != null)
                currentGameState.HandleInput(inputHelper);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (currentGameState != null)
                currentGameState.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            HandleInput();

            if (currentGameState != null)
                currentGameState.Update(gameTime);

            base.Update(gameTime);
        }
    }

}
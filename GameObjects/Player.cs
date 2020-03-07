using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FancyKlepto.GameManagement;

namespace FancyKlepto
{
    class Player : GameObject
    {
        private KeyboardState oldState = Keyboard.GetState();
        public Player() : base("player_brown")
        {
            Reset();
            position = new Vector2(150, 440);
            velocity = new Vector2(10, 10);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update()
        {
            base.Update();
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
            {
                position.X -= velocity.X;
                Console.WriteLine("heyo");
            }
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D) && oldState.IsKeyUp(Keys.D))
            { position.X += velocity.X; }
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.W) && oldState.IsKeyUp(Keys.W))
            { position.Y -= velocity.Y; }
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S))
            { position.Y += velocity.Y; }

            oldState = GameEnvironment.KeyboardState;
            //position.X = MathHelper.Clamp(position.X, 0, GameEnvironment.Screen.X - texture.Width);
            //position.Y = MathHelper.Clamp(position.Y, 0, GameEnvironment.Screen.Y - texture.Height);
        }
    }
}
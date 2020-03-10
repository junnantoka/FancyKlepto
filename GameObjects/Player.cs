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
        private KeyboardState key = GameEnvironment.KeyboardState;
        private Vector2 maxVelocity;
        private Vector2 zeroVelocity;
        private Vector2 minVelocity;
        private float velocityVelocity;
        public Player() : base("player_brown")
        {
            Reset();
            position = new Vector2(150, 440);

            velocity = new Vector2(0, 0);
            velocityVelocity = 0.1f;

            maxVelocity = new Vector2(10, 10);
            zeroVelocity = new Vector2(0, 0);
            minVelocity = -1 * maxVelocity;
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update()
        {
            base.Update();
            // && oldState.IsKeyUp(Keys.A)
            /*
             if (GameEnvironment.KeyboardState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
            {
                position.X -= velocity.X;
            }
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D) && oldState.IsKeyUp(Keys.D))
            {
                position.X += velocity.X;
            }
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.W) && oldState.IsKeyUp(Keys.W))
            {
                position.Y -= velocity.Y;
            }
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S))
            {
                position.Y += velocity.Y;
            }
            */
            Move();

            oldState = GameEnvironment.KeyboardState;
            key = GameEnvironment.KeyboardState;
            position.X = MathHelper.Clamp(position.X, 0, GameEnvironment.Screen.X - texture.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, GameEnvironment.Screen.Y - texture.Height);
        }

        public void Move()
        {
            position += velocity;
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.A) && velocity.X > minVelocity.X)
            {
                velocity.X -= velocityVelocity;
            }
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D) && velocity.X < maxVelocity.X)
            {
                velocity.X += velocityVelocity;
            }
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.W) && velocity.Y > minVelocity.Y)
            {
                velocity.Y -= velocityVelocity;
            }
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.S) && velocity.Y < maxVelocity.Y)
            {
                velocity.Y += velocityVelocity;
            }

            if (key.IsKeyUp(Keys.A)&& key.IsKeyUp(Keys.D) && key.IsKeyUp(Keys.W) && key.IsKeyUp(Keys.S))
            {
                if(velocity.X<0.5 && velocity.X>-0.5 && velocity.Y<0.5 && velocity.Y > -0.5)
                {
                    velocity = zeroVelocity;
                }

                if (velocity.X > zeroVelocity.X)
                {
                    velocity.X -= velocityVelocity;
                }
                if (velocity.X < zeroVelocity.X)
                {
                    velocity.X += velocityVelocity;
                }

                if (velocity.Y > zeroVelocity.Y)
                {
                    velocity.Y -= velocityVelocity;
                }
                if (velocity.Y < zeroVelocity.Y)
                {
                    velocity.Y += velocityVelocity;
                }
            }
        }
    }
}
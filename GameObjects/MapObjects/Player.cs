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
        private Vector2 pPosition;
        public Player(int x, int y) : base("spr_player")
        {
            Reset();
            velocityVelocity = 0.1f;
            position = new Vector2(x * unit, y * unit);
            pPosition = position;

            maxVelocity = new Vector2(10, 10);
            zeroVelocity = new Vector2(0, 0);
            minVelocity = -1 * maxVelocity;
        }

        public override void Reset()
        {
            position = pPosition;
            velocity = zeroVelocity;
        }

        public override void Update()
        {
            base.Update();
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
        public void checkForCollision(GameObject pObject)
        {
            if (Overlaps(pObject)) Reset();
        }
    }
}
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
<<<<<<< HEAD:GameObjects/Player.cs
        private KeyboardState oldState = Keyboard.GetState();
        private KeyboardState key = GameEnvironment.KeyboardState;
        private Vector2 maxVelocity;
        private Vector2 zeroVelocity;
        private Vector2 minVelocity;
        private float velocityVelocity;
        public Player() : base("player_brown")
        {
            Reset();
            position = new Vector2(140, 440);
=======
        public KeyboardState key = GameEnvironment.KeyboardState;
        public Vector2 maxVelocity;
        public Vector2 zeroVelocity;
        public Vector2 minVelocity;
        public int stopVelocity;
        public Vector2 velocityVelocity;
>>>>>>> master:GameObjects/MapObjects/Player.cs

        public bool moveRight, moveLeft, moveUp, moveDown;
        public Player(int x, int y) : base("spr_player")
        {
            velocityVelocity = new Vector2(0.2f,0.2f);
            stopVelocity=2;
            position = new Vector2(x * (unitSize + unitSpacing), y * (unitSize+ unitSpacing));
            pPosition = position;
            maxVelocity = new Vector2(5, 5);
            zeroVelocity = new Vector2(0, 0);
            minVelocity = -1 * maxVelocity;
            Reset();
        }

        public override void Reset()
        {
<<<<<<< HEAD:GameObjects/Player.cs
            base.Reset();
            position = new Vector2(140, 440);
=======
            moveRight = true;
            moveLeft = true;
            moveUp = true;
            moveDown = true;
            position = pPosition;
            velocity = zeroVelocity;
>>>>>>> master:GameObjects/MapObjects/Player.cs
        }

        public override void Update()
        {
            base.Update();
            Move();
            key = GameEnvironment.KeyboardState;
            position.X = MathHelper.Clamp(position.X, 0, GameEnvironment.Screen.X - texture.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, GameEnvironment.Screen.Y - texture.Height);
        }

        public void Move()
        {
            position += velocity;
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.A) && velocity.X > minVelocity.X && moveLeft)
            {
                velocity.X -= velocityVelocity.X;
            }
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D) && velocity.X < maxVelocity.X && moveRight)
            {
                velocity.X += velocityVelocity.X;
            }
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.W) && velocity.Y > minVelocity.Y && moveUp)
            {
                velocity.Y -= velocityVelocity.Y;
            }
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.S) && velocity.Y < maxVelocity.Y && moveDown)
            {
                velocity.Y += velocityVelocity.Y;
            }

            if (key.IsKeyUp(Keys.A)&& key.IsKeyUp(Keys.D) && key.IsKeyUp(Keys.W) && key.IsKeyUp(Keys.S))
            {
                if(velocity.X< stopVelocity && velocity.X>-stopVelocity && velocity.Y< stopVelocity && velocity.Y > -stopVelocity)
                {
                    velocity = zeroVelocity;
                }

                if (velocity.X > zeroVelocity.X)
                {
                    velocity.X -= velocityVelocity.X;
                }
                if (velocity.X < zeroVelocity.X)
                {
                    velocity.X += velocityVelocity.X;
                }

                if (velocity.Y > zeroVelocity.Y)
                {
                    velocity.Y -= velocityVelocity.Y;
                }
                if (velocity.Y < zeroVelocity.Y)
                {
                    velocity.Y += velocityVelocity.Y;
                }
            }
            if (key.IsKeyUp(Keys.A))
            {
                moveLeft = true;
            }
            if (key.IsKeyUp(Keys.D))
            {
                moveRight = true;
            }
            if (key.IsKeyUp(Keys.W))
            {
                moveUp = true;
            }
            if (key.IsKeyUp(Keys.S))
            {
                moveDown = true;
            }
        }
        public void checkForCollision(GameObject pObject)
        {
            if (Overlaps(pObject)) Reset();
        }
    }
}
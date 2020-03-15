﻿using System;
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
        private KeyboardState key = GameEnvironment.KeyboardState;
        private Vector2 maxVelocity;
        private Vector2 zeroVelocity;
        private Vector2 minVelocity;
        private int stopVelocity;
        private float velocityVelocity;

        public bool moveRight, moveLeft, moveUp, moveDown;
        public Player(int x, int y) : base("spr_player")
        {
            velocityVelocity = 0.1f;
            stopVelocity=2;
            position = new Vector2(x * (unitSize + unitSpacing), y * (unitSize+ unitSpacing));
            pPosition = position;
            maxVelocity = new Vector2(10, 10);
            zeroVelocity = new Vector2(0, 0);
            minVelocity = -1 * maxVelocity;
            Reset();
        }

        public override void Reset()
        {
            moveRight = true;
            moveLeft = true;
            moveUp = true;
            moveDown = true;
            position = pPosition;
            velocity = zeroVelocity;
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
            if (key.IsKeyDown(Keys.A) && velocity.X > minVelocity.X && moveLeft)
            {
                velocity.X -= velocityVelocity;
            }
            if (key.IsKeyDown(Keys.D) && velocity.X < maxVelocity.X && moveRight)
            {
                velocity.X += velocityVelocity;
            }
            if (key.IsKeyDown(Keys.W) && velocity.Y > minVelocity.Y && moveUp)
            {
                velocity.Y -= velocityVelocity;
            }
            if (key.IsKeyDown(Keys.S) && velocity.Y < maxVelocity.Y && moveDown)
            {
                velocity.Y += velocityVelocity;
            }

            if (key.IsKeyUp(Keys.A)&& key.IsKeyUp(Keys.D) && key.IsKeyUp(Keys.W) && key.IsKeyUp(Keys.S))
            {
                if(velocity.X< stopVelocity && velocity.X>-stopVelocity && velocity.Y< stopVelocity && velocity.Y > -stopVelocity)
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
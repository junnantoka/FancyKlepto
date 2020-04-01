﻿using Microsoft.Xna.Framework;
using System;

namespace FancyKlepto.GameObjects
{
    class Guard : SpriteGameObject
    {
        int frameCounter = 0;
        const int guards = 1;
        public Vector2 positionA, positionB;
        public Guard(Vector2 positionA, Vector2 positionB) : base("spr_guard");
        {
            for (int i = 0; i < guards; i++)
            {
                Add(guard);
                velocity = new Vector2(GameEnvironment.Random.Next(-100, 100), 0);
            }
            position = positionA;
            this.positionA = positionA;
            this.positionB = positionB;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            frameCounter++; //keep track of frames

            if (position.X > positionA.X && position.X < positionB.X && frameCounter > 60)
            {
                position += velocity;
                frameCounter = 0;
            }
            if (position.X < 0 || position.X > GameEnvironment.Screen.X - guard.texture.Width)
            {
                velocity = -velocity;
            }
        }

        public bool Overlaps(SpriteGameObject other)
        {
            return guard.Overlaps(other);
        }

        public override void Reset()
        {
            base.Reset();
            this.position = positionA;
            velocity = new Vector2(0, 0);
        }
    }
}
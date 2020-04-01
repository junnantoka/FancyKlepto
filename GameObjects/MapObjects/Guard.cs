using Microsoft.Xna.Framework;
using System;

namespace FancyKlepto.GameObjects
{
    class Guard : GameObjectList
    {
        int frameCounter = 0;
        const int guards = 1;
        public Vector2 positionA, positionB;
        SpriteGameObject guard = new SpriteGameObject("spr_guard");
        public Guard(Vector2 positionA, Vector2 positionB)
        {
            for (int i = 0; i < guards; i++)
            {
                Add(guard);
            }
            position = positionA;
            this.positionA = positionA;
            this.positionB = positionB;
            this.velocity.X = 65;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            frameCounter++; //keep track of frames

            if (position.X > positionA.X && position.X < positionB.X && frameCounter > 60)
            {
                Console.WriteLine(velocity);
                position += velocity;
                frameCounter = 0;
            }
            else
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
            guard.position = position;
        }
    }
}
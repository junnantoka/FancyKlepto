using Microsoft.Xna.Framework;
using System;

namespace FancyKlepto.GameObjects
{
    class Guard : SpriteGameObject
    {
        public Vector2 destination;
        public Vector2 posA, posB;

        public Guard(Vector2 positionA, Vector2 positionB) : base("agent")
        {
            position = new Vector2(18 + positionA.X * (unitSize + unitSpacing), 10 + positionA.Y * (unitSize + unitSpacing));

            positionA = position;
            positionB = new Vector2(18 + positionB.X * (unitSize + unitSpacing), 10 + positionB.Y * (unitSize + unitSpacing));

            posA = positionA;
            posB = positionB;

            Reset();
        }
        public override void Reset()
        {
            position = posA;
            this.destination = posB;
            velocity = new Vector2(3, 3);
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);

            Move();
            Rotate();
        }
        public void Rotate()
        {
            if (Math.Abs(destination.X - position.X) < sprite.Width && Math.Abs(destination.Y - position.Y) < sprite.Height)
            {
                DestChange();
            }
        }

        public void DestChange()
        {
            if(destination == posB)
            {
                destination = posA;
            } else if (destination == posA)
            {
                destination = posB;
            }
        }
        public void Move()
        {
            ////////////////////////////// to the destination X
            if (position.X < destination.X)
            {
                position.X += velocity.X;
            } else if (position.X > destination.X)
            {
                position.X -= velocity.X;
            }
            ////////////////////////////// to the destination Y
            if (position.Y < destination.Y)
            {
                position.Y += velocity.Y;
            }
            else if (position.X > destination.Y)
            {
                position.Y -= velocity.Y;
            }
        }
    }
}
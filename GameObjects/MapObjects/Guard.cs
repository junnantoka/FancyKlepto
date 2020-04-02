using Microsoft.Xna.Framework;
using System;

namespace FancyKlepto.GameObjects
{
    class Guard : SpriteGameObject
    {
        public Vector2 positionB;

        public bool Right, Left, Up, Down, Forward;

        public Guard(Vector2 positionA, Vector2 positionB) : base("spr_guard")
        {
            position = new Vector2(18 + positionA.X * (unitSize + unitSpacing), 10 + positionA.Y * (unitSize + unitSpacing));
            positionB = new Vector2(18 + positionB.X * (unitSize + unitSpacing), 10 + positionB.Y * (unitSize + unitSpacing));

            this.positionB = positionB;

            Reset();
            velocity = new Vector2(5, 0);
        }
        public override void Reset()
        {
            Forward = true;
            if (position.X > positionB.X)
            {
                Right = false;
                Left = true;
            }
            else
            {
                Right = true;
                Left = false;
            }

            if (position.Y > positionB.Y)
            {
                Up = true;
                Down = false;
            }
            else
            {
                Down = true;
                Up = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            position += velocity;
        }

        public void Collision(SpriteGameObject pObject)
        {
            if (CollidesWith(pObject))
            {
                //////////////////////////////////////////////////////////////////////                  horizontal
                if (pObject.Position.X > position.X || pObject.Position.X < position.X)
                {
                    velocity.X = 0;
                }
                ////////////////////////////////////////////////////////////////////                  vertical
                if (pObject.Position.Y < position.Y)
                {
                    position.Y += Math.Abs(velocity.Y);
                    Up = false;
                }
                else if (pObject.Position.Y > position.Y)
                {
                    position.Y -= Math.Abs(velocity.Y);
                    Down = false;
                }
            }
        }
    }
}
using Microsoft.Xna.Framework;
using System;

namespace FancyKlepto.GameObjects
{
    class Guard : SpriteGameObject
    {
        public Vector2 positionA, positionB;

        public bool Right, Left, Up, Down;
        bool forward;
        public Guard(Vector2 positionA, Vector2 positionB) : base("spr_guard")
        {

            position = new Vector2(18 + positionA.X * (unitSize + unitSpacing), 10 + positionA.Y * (unitSize + unitSpacing));

            this.positionA = positionA;
            this.positionB = positionB;

            Reset();
        }
        public override void Reset()
        {
            forward = true;
            Right = true;
            Left = true;
            Up = true;
            Down = true;
            velocity = new Vector2(5, 5);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Move();
        }

        public void Move()
        {
            if (forward)
            {
                if (position.X > positionB.X && Right)
                {
                    position.X -= velocity.X;
                } else if (!Right)
                {
                    position.X += velocity.X;
                }

                if (position.Y > positionB.Y && Up)
                {
                    position.Y -= velocity.Y;
                } else if (!Up)
                {
                    position.Y += velocity.Y;
                }
            } else if (!forward)
            {

            }
            
            /*
            if (forward)
            {
                if (positionB.X > position.X && Right)
                {
                    position.X += velocity.X;
                }
                else if (positionB.X < position.X && Left)
                {
                    position.X -= velocity.X;
                }

                if (positionB.Y > position.Y && Down)
                {
                    position.Y += velocity.Y;
                }
                else if (positionB.Y < position.Y && Up)
                {
                    position.X -= velocity.Y;
                }
            }
            else if (!forward)
            {
                if (positionA.X > position.X && Right)
                {
                    position.X += velocity.X;
                }
                else if (positionA.X < position.X && Left)
                {
                    position.X -= velocity.X;
                }

                if (positionA.Y > position.Y && Down)
                {
                    position.Y += velocity.Y;
                }
                else if (positionA.Y < position.Y && Up)
                {

                }
            }*/
        }
    }
}
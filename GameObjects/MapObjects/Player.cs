using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FancyKlepto.GameObjects;

class Player : SpriteGameObject
{
    protected KeyboardState currentKeyboardState;
    public Vector2 maxVelocity;
    public Vector2 zeroVelocity;
    public Vector2 minVelocity;
    public int stopVelocity;
    public Vector2 velocityVelocity;

    public bool moveRight, moveLeft, moveUp, moveDown;

    public Player(int x, int y) : base("spr_player")
    {
        velocityVelocity = new Vector2(0.6f, 0.6f);
        stopVelocity = 2;
        position = new Vector2(x * (unitSize + unitSpacing), y * (unitSize + unitSpacing));
        defPos = position;
        maxVelocity = new Vector2(5, 5);
        zeroVelocity = new Vector2(0, 0);
        minVelocity = -1 * maxVelocity;
        Reset();
    }

    public override void Reset()
    {
        base.Reset();
        moveRight = true;
        moveLeft = true;
        moveUp = true;
        moveDown = true;
        position = defPos;
        velocity = zeroVelocity;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        position.X = MathHelper.Clamp(position.X, 0, GameEnvironment.Screen.X - sprite.Width);
        position.Y = MathHelper.Clamp(position.Y, 0, GameEnvironment.Screen.Y - sprite.Height);
        currentKeyboardState = Keyboard.GetState();
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        position += velocity;
        if (inputHelper.IsKeyDown(Keys.A) && velocity.X > minVelocity.X && moveLeft)
        {
            velocity.X -= velocityVelocity.X;
        }
        if (inputHelper.IsKeyDown(Keys.D) && velocity.X < maxVelocity.X && moveRight)
        {
            velocity.X += velocityVelocity.X;
        }
        if (inputHelper.IsKeyDown(Keys.W) && velocity.Y > minVelocity.Y && moveUp)
        {
            velocity.Y -= velocityVelocity.Y;
        }
        if (inputHelper.IsKeyDown(Keys.S) && velocity.Y < maxVelocity.Y && moveDown)
        {
            velocity.Y += velocityVelocity.Y;
        }

        if (currentKeyboardState.IsKeyUp(Keys.W) &&
            currentKeyboardState.IsKeyUp(Keys.S))
        {
            if (velocity.Y > zeroVelocity.Y)
            {
                velocity.Y -= velocityVelocity.Y;
            }
            if (velocity.Y < zeroVelocity.Y)
            {
                velocity.Y += velocityVelocity.Y;
            }

            if (velocity.Y < stopVelocity &&
                velocity.Y > -stopVelocity)
            {
                velocity.Y = zeroVelocity.Y;
            }
        }
        if (currentKeyboardState.IsKeyUp(Keys.A) &&
            currentKeyboardState.IsKeyUp(Keys.D))
        {
            if (velocity.X > zeroVelocity.X)
            {
                velocity.X -= velocityVelocity.X;
            }
            if (velocity.X < zeroVelocity.X)
            {
                velocity.X += velocityVelocity.X;
            }

            if (velocity.X < stopVelocity &&
                velocity.X > -stopVelocity)
            {
                velocity.X = zeroVelocity.X;
            }
        }

        if (currentKeyboardState.IsKeyUp(Keys.A))
        {
            moveLeft = true;
        }
        if (currentKeyboardState.IsKeyUp(Keys.D))
        {
            moveRight = true;
        }
        if (currentKeyboardState.IsKeyUp(Keys.W))
        {
            moveUp = true;
        }
        if (currentKeyboardState.IsKeyUp(Keys.S))
        {
            moveDown = true;
        }
    }

    public void Collision(SpriteGameObject pObject)
    {
        Vector2 wallPos = pObject.Position;
        
        if (pObject is Wall)
        {
            if (wallPos.X > position.X && CollidesWith(pObject))
            {
                position.X -= Math.Abs(Velocity.X);
                velocity.X = 0;
            }

            if (wallPos.X < position.X && CollidesWith(pObject))
            {
                position.X += Math.Abs(velocity.X);
                velocity.X = 0;
            }
            //////////////////////////////////////////////////////////////////////                  vertical
            if (wallPos.Y < position.Y && CollidesWith(pObject))
            {
                position.Y += Math.Abs(velocity.Y);
                velocity.Y = 0;
            }
            if (wallPos.Y > position.Y && CollidesWith(pObject))
            {
                position.Y -= Math.Abs(velocity.Y);
                velocity.Y = 0;
            }
        }
    }
}
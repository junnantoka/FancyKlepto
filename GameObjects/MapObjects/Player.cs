using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FancyKlepto.GameObjects;

class Player : SpriteGameObject
{
    protected KeyboardState currentKeyboardState;
    public Vector2 maxVelocity;
    public Vector2 minVelocity;
    public int stopVelocity;
    public Vector2 velocityVelocity;

    public Player(int x, int y) : base("spr_thief")
    {
        velocityVelocity = new Vector2(0.6f, 0.6f);
        stopVelocity = 2;
        position = new Vector2(18 + x * (unitSize + unitSpacing), 10 + y * (unitSize + unitSpacing));
        defPos = position;
        maxVelocity = new Vector2(5, 5);
        minVelocity = -1 * maxVelocity;
        Reset();
    }

    public override void Reset()
    {
        base.Reset();
        position = defPos;
        velocity = Vector2.Zero;
    }

    public override void Update(GameTime gameTime) 
    {
        base.Update(gameTime);
        currentKeyboardState = Keyboard.GetState();
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        position += velocity;

        #region acceleration
        if (inputHelper.IsKeyDown(Keys.A) && velocity.X > minVelocity.X)
        {
            velocity.X -= velocityVelocity.X;
        }
        if (inputHelper.IsKeyDown(Keys.D) && velocity.X < maxVelocity.X)
        {
            velocity.X += velocityVelocity.X;
        }
        if (inputHelper.IsKeyDown(Keys.W) && velocity.Y > minVelocity.Y)
        {
            velocity.Y -= velocityVelocity.Y;
        }
        if (inputHelper.IsKeyDown(Keys.S) && velocity.Y < maxVelocity.Y)
        {
            velocity.Y += velocityVelocity.Y;
        }

        if (currentKeyboardState.IsKeyUp(Keys.W) &&
            currentKeyboardState.IsKeyUp(Keys.S))
        {
            if (velocity.Y > Vector2.Zero.Y)
            {
                velocity.Y -= velocityVelocity.Y;
            }
            if (velocity.Y < Vector2.Zero.Y)
            {
                velocity.Y += velocityVelocity.Y;
            }

            if (velocity.Y < stopVelocity &&
                velocity.Y > -stopVelocity)
            {
                velocity.Y = Vector2.Zero.Y;
            }
        }
        if (currentKeyboardState.IsKeyUp(Keys.A) &&
            currentKeyboardState.IsKeyUp(Keys.D))
        {
            if (velocity.X > Vector2.Zero.X)
            {
                velocity.X -= velocityVelocity.X;
            }
            if (velocity.X < Vector2.Zero.X)
            {
                velocity.X += velocityVelocity.X;
            }

            if (velocity.X < stopVelocity &&
                velocity.X > -stopVelocity)
            {
                velocity.X = Vector2.Zero.X;
            }
        }
        #endregion
    }
}
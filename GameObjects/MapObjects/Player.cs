using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FancyKlepto.GameObjects;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

class Player : RotatingSpriteGameObject
{
    SoundEffect Player_Walk1, Player_Walk2, Player_Walk3, Player_Walk4;
    protected KeyboardState currentKeyboardState;
    public Vector2 maxVelocity, minVelocity, Acceleration;
    public int stopVelocity;

    int degreeRotater = 3;
    double radius = 0.0174532925;

    public Player(int x, int y) : base("Player/idle")
    {
        position = new Vector2(18 + x * (unitSize + unitSpacing), 10 + y * (unitSize + unitSpacing));
        defPos = position;

        origin = sprite.Center;

        VelocitySetup();
        #region walk Sound
        Player_Walk1 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/pl_tile1");
        Player_Walk2 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/pl_tile2");
        Player_Walk3 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/pl_tile3");
        Player_Walk4 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/pl_tile4");
        #endregion
        Reset();
    }
    public void VelocitySetup()
    {
        Acceleration = new Vector2(25, 25);
        stopVelocity = 2;
        maxVelocity = new Vector2(5, 5);
        minVelocity = -1 * maxVelocity;
    }
    public override void Reset()
    {
        base.Reset();
        position = defPos;
        velocity = Vector2.Zero;
        offsetDegrees = 90;
    }

    public override void Update(GameTime gameTime)
    {
        currentKeyboardState = Keyboard.GetState();
        #region offset
        if (offsetDegrees > 360)
        {
            offsetDegrees = offsetDegrees % 360;
        }
        else if (offsetDegrees < 0)
        {
            offsetDegrees += 360;
        }
        #endregion

        #region velocity reseter
        if (velocity.X > 0)
        {
            velocity.X--;
        }
        else if (velocity.X < 0)
        {
            velocity.X++;
        }
        if (velocity.Y > 0)
        {
            velocity.Y--;
        }
        else if (velocity.Y < 0)
        {
            velocity.Y++;
        }
        velocity.X /= 5;
        velocity.Y /= 5;
        #endregion
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.IsKeyDown(Keys.Up))
        {
            position.X += (float)Math.Cos(offsetDegrees * radius) * velocity.X;
            position.Y -= (float)Math.Sin(offsetDegrees * radius) * velocity.Y;
            velocity += Acceleration;

        }
        if (inputHelper.IsKeyDown(Keys.Down))
        {
            position.X -= (float)Math.Cos(offsetDegrees * radius) * velocity.X;
            position.Y += (float)Math.Sin(offsetDegrees * radius) * velocity.Y;
            velocity += Acceleration;
        }

        if (inputHelper.IsKeyDown(Keys.Left))
        {
            offsetDegrees += degreeRotater;
        }
        if (inputHelper.IsKeyDown(Keys.Right))
        {
            offsetDegrees -= degreeRotater;
        }
    }

    public void LoadContent()
    {
    }
}
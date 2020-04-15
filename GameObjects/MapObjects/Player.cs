using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FancyKlepto.GameObjects;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Runtime.CompilerServices;

class Player : RotatingSpriteGameObject
{
    SoundEffect Player_Walk1, Player_Walk2, Player_Walk3, Player_Walk4;
    protected SpriteSheet idle, walk1, walk2, walk3, walk4, walk5, idle2, walk21, walk22, walk23, walk24, walk25;
    protected KeyboardState currentKeyboardState;
    public Vector2 maxVelocity, minVelocity, Acceleration;
    public int stopVelocity;
    public bool walk;
    public int walkTimer;
    int degreeRotater = 5;
    double radius = 0.0174532925;

    public Player(int x, int y) : base("Player/idle")
    {
        #region player_animation
        idle = new SpriteSheet("Player/idle", 0);
        walk1 = new SpriteSheet("Player/walk1", 0);
        walk2 = new SpriteSheet("Player/walk2", 0);
        walk3 = new SpriteSheet("Player/walk3", 0);
        walk4 = new SpriteSheet("Player/walk4", 0);
        walk5 = new SpriteSheet("Player/walk5", 0);
        idle2 = new SpriteSheet("Player/idle2", 0);
        walk21 = new SpriteSheet("Player/walk21", 0);
        walk22 = new SpriteSheet("Player/walk22", 0);
        walk23 = new SpriteSheet("Player/walk23", 0);
        walk24 = new SpriteSheet("Player/walk24", 0);
        walk25 = new SpriteSheet("Player/walk25", 0);
        #endregion
        #region walk Sound
        Player_Walk1 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/pl_tile1");
        Player_Walk2 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/pl_tile2");
        Player_Walk3 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/pl_tile3");
        Player_Walk4 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/pl_tile4");
        #endregion
        position = new Vector2(18 + x * (unitSize + unitSpacing), 10 + y * (unitSize + unitSpacing));
        defPos = position;

        origin = sprite.Center;

        VelocitySetup();
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
        if (walkTimer > 59)
        {
            walkTimer = 1;
        }
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
        if (inputHelper.IsKeyDown(Keys.W) && !inputHelper.IsKeyDown(Keys.S))
        {
            position.X += (float)Math.Cos(offsetDegrees * radius) * velocity.X;
            position.Y -= (float)Math.Sin(offsetDegrees * radius) * velocity.Y;
            velocity += Acceleration;
            walk = true;
            walkTimer++;
        }
        if (inputHelper.IsKeyDown(Keys.S) && !inputHelper.IsKeyDown(Keys.W))
        {
            position.X -= (float)Math.Cos(offsetDegrees * radius) * velocity.X;
            position.Y += (float)Math.Sin(offsetDegrees * radius) * velocity.Y;
            velocity += Acceleration;
        }
        if(!inputHelper.IsKeyDown(Keys.S)&& !inputHelper.IsKeyDown(Keys.W))
        {
            walk = false;
        }
        if (inputHelper.IsKeyDown(Keys.A))
        {
            offsetDegrees += degreeRotater;
        }
        if (inputHelper.IsKeyDown(Keys.D))
        {
            offsetDegrees -= degreeRotater;
        }
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        
        if (walk)
        {
            if (walkTimer > 0 && walkTimer<=5)
            {
                sprite = idle;
            }
            else if (walkTimer > 5 && walkTimer<=10)
            {
                sprite = walk1;
            }
            else if (walkTimer > 10 && walkTimer<=15)
            {
                sprite = walk2;
            }
            else if (walkTimer > 15 && walkTimer<=20)
            {
                sprite = walk3;
            }
            else if (walkTimer > 20 && walkTimer<=25)
            {
                sprite = walk4;
            }
            else if (walkTimer > 25 && walkTimer<=30)
            {
                sprite = walk5;
            }
            else if (walkTimer > 30 && walkTimer<=35)
            {
                sprite = idle2;
            }
            else if (walkTimer > 35 && walkTimer<=40)
            {
                sprite = walk21;
            }
            else if (walkTimer > 40 && walkTimer<=45)
            {
                sprite = walk22;
            }
            else if (walkTimer > 45 && walkTimer<=50)
            {
                sprite = walk23;
            }
            else if (walkTimer > 50 && walkTimer<=55)
            {
                sprite = walk24;
            }
            else if (walkTimer > 55)
            {
                sprite = walk25;
            }
        }
        else
        {
            sprite = idle;
            walkTimer = 0;
        }
        
    }
}
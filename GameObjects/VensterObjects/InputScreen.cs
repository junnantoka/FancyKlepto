using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using FancyKlepto.GameObjects.MapObjects;
class InputScreen : SpriteGameObject
{
    SoundEffect SlideSound;
    public int Timer;
    private Vector2 openPos;

    public InputScreen(int x, int y) : base("Map/spr_inputbar")
    {
        SlideSound = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Slide");
        velocity = new Vector2(0, 5);
        position = new Vector2(x, y);
        defPos = position;

        openPos.Y = defPos.Y - 65-30;
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (Timer == 1)
        {
            SlideSound.Play();
            Timer = 0;
        }
        Move();
    }

    public void Move()
    {
        if (open && position.Y > openPos.Y)
        {
            position.Y -= velocity.Y;
            if (position.Y < openPos.Y)
            {
                position.Y = openPos.Y;
            }
        }
        else if (!open && position.Y < defPos.Y)
        {
            position.Y += velocity.Y;
            if (position.Y > defPos.Y)
            {
                position.Y = defPos.Y;
            }
        }
    }
}
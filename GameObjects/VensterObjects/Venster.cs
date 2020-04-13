using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using FancyKlepto.GameObjects.MapObjects;
class Venster : SpriteGameObject
{
    SoundEffect SlideSound;
    public int Timer;
    private Vector2 openPos;
    private float screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

    public Venster(int x, int y, string pObject) : base(pObject)
    {
        SlideSound = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Slide");
        velocity = new Vector2(15, 0);
        position = new Vector2(x, y);
        position.X += screenWidth;
        defPos = position;
        // 352 is total width of the open window
        openPos.X = defPos.X - 352;
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
        if (open && position.X > openPos.X)
        {
            position.X -= velocity.X;
            if (position.X < openPos.X)
            {
                position.X = openPos.X;
            }
        }
        else if (!open && position.X < defPos.X)
        {
            position.X += velocity.X;
            if (position.X > defPos.X)
            {
                position.X = defPos.X;
            }
        }
    }
}
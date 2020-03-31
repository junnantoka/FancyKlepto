using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
class SpriteGameObject : GameObject
{
    protected Vector2 origin;
    public Vector2 position2;
    public SpriteGameObject(String assetName)
    {
        if (assetName.Length > 0)
            texture = GameEnvironment.ContentManager.Load<Texture2D>(assetName);
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        if (visible)
            spriteBatch.Draw(texture, GlobalPosition, Color.White);
    }

    public override Rectangle BoundingBox
    {
        get
        {
            int left = (int)(GlobalPosition.X - origin.X);
            int top = (int)(GlobalPosition.Y - origin.Y);
            return new Rectangle(left, top, Width, Height);
        }
    }
    public int Width
    {
        get
        {
            return texture.Width;
        }
    }

    public int Height
    {
        get
        {
            return texture.Height;
        }
    }
    public Vector2 Origin
    {
        get { return origin; }
        set { origin = value; }
    }
}
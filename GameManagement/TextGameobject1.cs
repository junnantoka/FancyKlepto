using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class TextGameobject : GameObject
{
    protected Vector2 origin;
    public Vector2 position2;
    public TextGameobject()
    {

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

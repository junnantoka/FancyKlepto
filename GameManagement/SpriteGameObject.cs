using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoSprites;
using System;

public class SpriteGameObject : GameObject
{
    protected SpriteSheet sprite;
    protected Vector2 origin;
    public bool PerPixelCollisionDetection = true;

    public SpriteGameObject(string assetName, int layer = 0, string id = "", int sheetIndex = 0)
        : base(layer, id)
    {
        if (assetName != "")
        {
            sprite = new SpriteSheet(assetName, sheetIndex);
        }
        else
        {
            sprite = null;
        }
        size.X = sprite.Width;
        size.Y = sprite.Height;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible || sprite == null)
        {
            return;
        }
        sprite.Draw(spriteBatch, this.GlobalPosition, origin);
    }

    public SpriteSheet Sprite
    {
        get { return sprite; }
    }

    public Vector2 Center
    {
        get { return new Vector2(Width, Height) / 2; }
    }

    public int Width
    {
        get
        {
            return sprite.Width;
        }
    }

    public int Height
    {
        get
        {
            return sprite.Height;
        }
    }

    public bool Mirror
    {
        get { return sprite.Mirror; }
        set { sprite.Mirror = value; }
    }

    public Vector2 Origin
    {
        get { return origin; }
        set { origin = value; }
    }
    public Vector2 Intersection(SpriteGameObject spriteGameObject)
    {
        Vector2 minDistance = new Vector2(sprite.Width + spriteGameObject.Width, sprite.Height + spriteGameObject.Height) / 2;

        Vector2 centerA = new Vector2(BoundingBox.Center.X, BoundingBox.Center.Y);
        Vector2 centerB = new Vector2(spriteGameObject.BoundingBox.Center.X, spriteGameObject.BoundingBox.Center.Y);
        Vector2 distance = centerA - centerB;
        Vector2 depth = Vector2.Zero;

        if (distance.X <= minDistance.X)
        {
            depth.X = minDistance.X - Math.Abs(distance.X);
        }

        if (distance.Y <= minDistance.Y)
        {
            depth.Y = minDistance.Y - Math.Abs(distance.Y);
        }
        return depth;
    }
    public void Xcol(SpriteGameObject spriteGameObject)
    {
        if (spriteGameObject.Position.X > position.X)
        {
            position.X -= Intersection(spriteGameObject).X;
        }

        if (spriteGameObject.Position.X < position.X)
        {
            position.X += Intersection(spriteGameObject).X;
        }
    }
    public void Ycol(SpriteGameObject spriteGameObject)
    {
        if (spriteGameObject.Position.Y < position.Y)
        {
            position.Y += Intersection(spriteGameObject).Y;
        }

        if (spriteGameObject.Position.Y > position.Y)
        {
            position.Y -= Intersection(spriteGameObject).Y;
        }
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
    public Vector3 Dist(SpriteGameObject obj)
    {
        Vector3 dist;
        dist.X = Math.Abs(obj.Center.X - Center.X);
        dist.Y = Math.Abs(obj.Center.Y - Center.Y);
        dist.Z = (float)Math.Sqrt((dist.X)*(dist.X)+(dist.Y)*(dist.Y));
        return dist;
        
    }
    public bool XaxisCol(SpriteGameObject spriteGameObject)
    {
        if (BoundingBox.Center.X < spriteGameObject.BoundingBox.Right &&
            BoundingBox.Center.X > spriteGameObject.BoundingBox.Left)
        {
            return true;
        }
        return false;
    }
    public bool YaxisCol(SpriteGameObject spriteGameObject)
    {
        if ((BoundingBox.Center.Y < spriteGameObject.BoundingBox.Bottom &&
            BoundingBox.Center.Y > spriteGameObject.BoundingBox.Top))
        {
            return true;
        }
        return false;
    }
    public bool CollidesWith(SpriteGameObject obj)
    {
        if (!visible || !obj.visible || !BoundingBox.Intersects(obj.BoundingBox))
        {
            return false;
        }
        if (!PerPixelCollisionDetection)
        {
            return true;
        }
        Rectangle b = Collision.Intersection(BoundingBox, obj.BoundingBox);
        for (int x = 0; x < b.Width; x++)
        {
            for (int y = 0; y < b.Height; y++)
            {
                int thisx = b.X - (int)(GlobalPosition.X - origin.X) + x;
                int thisy = b.Y - (int)(GlobalPosition.Y - origin.Y) + y;
                int objx = b.X - (int)(obj.GlobalPosition.X - obj.origin.X) + x;
                int objy = b.Y - (int)(obj.GlobalPosition.Y - obj.origin.Y) + y;
                if (sprite.IsTranslucent(thisx, thisy) && obj.sprite.IsTranslucent(objx, objy))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool PixelCollision(SpriteGameObject spriteGameObject)
    {
        Color[] colorData1 = new Color[sprite.Sprite.Width * sprite.Sprite.Height];
        Color[] colorData2 = new Color[spriteGameObject.sprite.Sprite.Width * spriteGameObject.sprite.Sprite.Height];

        sprite.Sprite.GetData<Color>(colorData1);
        spriteGameObject.sprite.Sprite.GetData<Color>(colorData2);

        int top, bottom, left, right;

        top = Math.Max(BoundingBox.Top, spriteGameObject.BoundingBox.Top);
        bottom = Math.Min(BoundingBox.Bottom, spriteGameObject.BoundingBox.Bottom);
        left = Math.Max(BoundingBox.Left, spriteGameObject.BoundingBox.Left);
        right = Math.Min(BoundingBox.Right, spriteGameObject.BoundingBox.Right);

        for (int y = top; y < bottom; y++)
        {
            for (int x = left; x < right; x++)
            {
                Color A = colorData1[(y - BoundingBox.Top) * (BoundingBox.Width) + (x - BoundingBox.Left)];
                Color B = colorData2[(y - spriteGameObject.BoundingBox.Top) * (spriteGameObject.BoundingBox.Width) + (x - spriteGameObject.BoundingBox.Left)];

                if (A.A != 0 && B.A != 0) return true;
            }
        }
        return false;
    }
}
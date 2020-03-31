using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
class GameObject
{
    public int unitSize;
    public int unitSpacing;
    public Vector2 position;
    public Vector2 velocity;
    public Vector2 defPos;
    public Texture2D texture;
    protected bool visible;
    public bool open;
    protected GameObject parent;

    public bool Visible
    {
        get { return visible; }
        set { visible = value; }
    }

    public GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }
    public GameObject()
    {
        unitSize = 64;
        unitSpacing = 1;
        visible = true;
    }

    public virtual void Update(GameTime gameTime)
    {
        position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public virtual void Reset() { }
    public virtual void HandleInput(InputHelper inputHelper) { }

    public virtual void Draw(SpriteBatch spriteBatch)
    { }

    public virtual Vector2 GlobalPosition
    {
        get
        {
            if (parent != null)
            {
                return parent.GlobalPosition + position;
            }
            else
            {
                return position;
            }
        }
    }
    public virtual Rectangle BoundingBox
    {
        get
        {
            return new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, 0, 0);
        }
    }
    public bool Overlaps(GameObject other)
    {
        if (visible)
        {
            float w0 = this.texture.Width,
                h0 = this.texture.Height,
                w1 = other.texture.Width,
                h1 = other.texture.Height,
                x0 = this.GlobalPosition.X,
                y0 = this.GlobalPosition.Y,
                x1 = other.GlobalPosition.X,
                y1 = other.GlobalPosition.Y;

            return !(x0 > x1 + w1 || x0 + w0 < x1 ||
              y0 > y1 + h1 || y0 + h0 < y1);
        }

        return false;
    }
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
class Venster_Object : SpriteGameObject
{
    private Vector2 openPos;
    private float screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
    public Venster_Object(int x, int y, String pObject) : base(pObject)
    {
        velocity = new Vector2(15, 0);
        position = new Vector2(x, y);
        position.X += screenWidth;
        defPos = position;
        // 353 is total width of the open window
        openPos.X = defPos.X - 352;
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
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
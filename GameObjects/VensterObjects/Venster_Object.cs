using System;
using Microsoft.Xna.Framework;
class Venster_Object : SpriteGameObject
{
    private Vector2 movingSpace;
    private Vector2 movedLeft;
    public Venster_Object(int x, int y, String pObject) : base(pObject)
    {
        velocity = new Vector2(15, 0);
        movingSpace = new Vector2(330, 0);
        position = new Vector2(x, y) + movingSpace;
        pPosition = position;
        movedLeft = new Vector2(0, 0);
    }
    public override void Update(GameTime gameTime)
    {
        Console.WriteLine(position);
        base.Update(gameTime);
        Move();
    }

    public void Move()
    {
        if (visible && position.X >= pPosition.X - movingSpace.X)
        {
            position.X -= velocity.X;
        }
        if (!visible && position.X < pPosition.X)
        {
            position.X += velocity.X;
        }
    }
}
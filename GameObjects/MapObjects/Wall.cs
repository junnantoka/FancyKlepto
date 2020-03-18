using Microsoft.Xna.Framework;

class Wall : SpriteGameObject
{
    public Wall(int x, int y) : base("spr_black_wall")
    {
        Reset();
        position = new Vector2(x * (unitSize + unitSpacing), y * (unitSize + unitSpacing));
        pPosition = position;
    }

    public override void Reset()
    {
        position = pPosition;
    }
}

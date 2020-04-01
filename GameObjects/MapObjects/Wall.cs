using Microsoft.Xna.Framework;

class Wall : SpriteGameObject
{
    public Wall(int x, int y) : base("spr_black_wall")
    {
        Reset();
        position = new Vector2(18 + x * (unitSize + unitSpacing), 10 + y * (unitSize + unitSpacing));
        defPos = position;
    }

    public override void Reset()
    {
        position = defPos;
    }
}
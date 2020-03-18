using Microsoft.Xna.Framework;

class Wall : SpriteGameObject
{
    public Wall(int x, int y) : base("spr_black_wall")
    {
        Reset();
        position = new Vector2(x * (unitSize + unitSpacing), y * (unitSize + unitSpacing));
        defPos = position;
    }

    public override void Reset()
    {
        position = defPos;
    }
}

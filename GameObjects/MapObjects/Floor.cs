using Microsoft.Xna.Framework;
class Floor : SpriteGameObject
{
    public Floor(int x, int y) : base("spr_floor")
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

using Microsoft.Xna.Framework;
class Floor : SpriteGameObject
{
    public Floor(int x, int y) : base("spr_floor")
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

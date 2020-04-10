using Microsoft.Xna.Framework;
class Floor : SpriteGameObject
{
    public Floor(int x, int y) : base("Map/floor_gray_black_light")
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

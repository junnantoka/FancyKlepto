using Microsoft.Xna.Framework;
class MainGoal : SpriteGameObject
{
    public MainGoal(int x, int y) : base("spr_maingoal")
    {
        Reset();
        position = new Vector2(2 + x * (unitSize + unitSpacing), 2 + y * (unitSize + unitSpacing));
        pPosition = position;
    }
    public override void Reset()
    {
        position = pPosition;
    }
}
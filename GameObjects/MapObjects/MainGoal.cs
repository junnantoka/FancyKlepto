using Microsoft.Xna.Framework;

class MainGoal : SpriteGameObject
{
    public bool hold;
    public MainGoal(int x, int y) : base("spr_maingoal")
    {
        Reset();
        position = new Vector2(2 + x * (unitSize + unitSpacing), 2 + y * (unitSize + unitSpacing));
        defPos = position;
    }
    public override void Reset()
    {
        position = defPos;
        hold = false;
    }
    public void Hold(SpriteGameObject pObject)
    {
        position = pObject.Center;
    }
}
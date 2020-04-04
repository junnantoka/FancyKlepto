using Microsoft.Xna.Framework;

class ExtraGoal : SpriteGameObject
{
    public bool hold;
    public ExtraGoal(int x, int y) : base("spr_diamond_small")
    {
        Reset();
        position = new Vector2(18 + 1 + x * (unitSize + unitSpacing), 10 + 1 + y * (unitSize + unitSpacing));
        defPos = position;
    }
    public override void Reset()
    {
        position = defPos;
        hold = false;
    }
    public void Hold(SpriteGameObject pObject)
    {
        position.X = pObject.Position.X + pObject.Sprite.Width / 2 - sprite.Width / 2;
        position.Y = pObject.Position.Y + pObject.Sprite.Height / 2 - sprite.Height / 2;
    }
}

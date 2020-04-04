using Microsoft.Xna.Framework;

class Wall : SpriteGameObject
{
    public bool Die;
    public Wall(int x, int y) : base("spr_wall_choco_dark_2")
    {
        Reset();
        position = new Vector2(18 + x * (unitSize + unitSpacing), 10 + y * (unitSize + unitSpacing));
        defPos = position;
    }

    public override void Reset()
    {
        position = defPos;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (Die)
        {
            position.X = -1000;
            Die = false;
        }
    }
}
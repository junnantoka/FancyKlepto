using Microsoft.Xna.Framework;

class Wall : GameObjectList
{
    SpriteGameObject wall = new SpriteGameObject("spr_black_wall");
    public Wall()
    {
        for (int i = 0; i < 1; i++)
        {
            this.Add(wall);
        }
        Reset();
    }

    public override void Reset()
    {
        base.Reset();
    }

    public bool Overlaps(SpriteGameObject other)
    {
        return wall.Overlaps(other);
    }
}

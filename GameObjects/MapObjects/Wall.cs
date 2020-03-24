using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FancyKlepto.GameObjects
{
    class Wall : GameObjectList
    {
        SpriteGameObject wall = new SpriteGameObject("spr_black_wall");
        public Wall()
        {
            const int walls = 1;
            for (int i = 0; i < walls; i++)
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
}
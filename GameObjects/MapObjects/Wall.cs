using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FancyKlepto.GameObjects
{
    class Wall : GameObjectList
    {
        SpriteGameObject wall;
        public Wall()
        {
            const int walls = 29;
            const int spacing = 65;
            for (int i = 0; i < walls; i++)
            {
                this.Add(wall = new SpriteGameObject("spr_black_wall"));
                Children[i].position.X = spacing * i;
            }
        }

        public bool Overlaps(SpriteGameObject other)
        {
            return wall.Overlaps(other);
        }
    }
}
using Microsoft.Xna.Framework;

namespace FancyKlepto.GameObjects
{
    class Map : SpriteGameObject
    {
        public Map(string LevelName) : base(LevelName)
        {
            position = new Vector2(0, 0);
        }
    }
}

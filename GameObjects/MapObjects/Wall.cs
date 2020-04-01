using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FancyKlepto.GameObjects
{
    class Wall : GameObjectList
    {
        SpriteGameObject wall;
        const int wallsHorizontal = 29;
        const int wallsVertical = 15;
        const int spacing = 65;

        public Wall()
        {
            for (int i = 0; i < wallsHorizontal; i++)//top row of walls
            {
                Add(wall = new SpriteGameObject("spr_black_wall"));
                wall.position.X = spacing * i;
                wall.position.Y = 0;
            }
            for (int i = 0; i < wallsHorizontal; i++)//bottom row of walls
            {
                Add(wall = new SpriteGameObject("spr_black_wall"));
                wall.position.X = spacing * i;
                wall.position.Y = 975;
            }
            for (int i = 1; i < wallsVertical; i++)//left row of walls
            {
                Add(wall = new SpriteGameObject("spr_black_wall"));
                wall.position.Y = spacing * i;
                wall.position.X = 0;
            }
            for (int i = 1; i < wallsVertical; i++)//right row of walls
            {
                Add(wall = new SpriteGameObject("spr_black_wall"));
                wall.position.Y = spacing * i;
                wall.position.X = 1820;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public bool Overlaps(SpriteGameObject other)
        {
            return wall.Overlaps(other);
        }
    }
}
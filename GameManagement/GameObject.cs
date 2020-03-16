using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancyKlepto.GameManagement
{
    class GameObject
    {
        public Vector2 position;
        public Vector2 velocity;
        protected bool visible;
        protected GameObject parent;

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public GameObject Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public GameObject()
        {
            visible = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Reset() { }
        public virtual void HandleInput(InputHelper inputHelper) { }

        public virtual void Draw(SpriteBatch spriteBatch)
        { }

        public virtual Vector2 GlobalPosition
        {
            get
            {
                if (parent != null)
                {
                    return parent.GlobalPosition + position;
                }
                else
                {
                    return position;
                }
            }
        }

    }

}
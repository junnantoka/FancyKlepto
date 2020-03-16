using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FancyKlepto.GameManagement;
using Microsoft.Xna.Framework.Graphics;
using FancyKlepto.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FancyKlepto.GameManagement
{
    class GameObject
    {
        public int unitSize;
        public int unitSpacing;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 pPosition;
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
            unitSize = 64;
            unitSpacing = 1;
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
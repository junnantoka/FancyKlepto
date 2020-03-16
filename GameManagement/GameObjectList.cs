using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancyKlepto.GameManagement
{
    class GameObjectList : GameObject
    {

        private List<GameObject> children;

        public List<GameObject> Children
        {
            get { return children; }
        }

        public GameObjectList()
        {
            children = new List<GameObject>();
        }

        public void Add(GameObject gameObject)
        {
            gameObject.Parent = this;
            children.Add(gameObject);
        }

        public void remove(GameObject gameObject)
        {
            gameObject.Parent = null;
            children.Remove(gameObject);
        }

        public override void Reset()
        {
            foreach (GameObject gameObject in children)
                gameObject.Reset();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (GameObject gameObject in children)
                gameObject.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
                foreach (GameObject gameObject in children)
                    gameObject.Draw(spriteBatch);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            foreach (GameObject gameObject in children)
                gameObject.HandleInput(inputHelper);
        }

    }

}
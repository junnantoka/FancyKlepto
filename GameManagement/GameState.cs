using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancyKlepto.GameManagement
{
    class GameState
    {
        protected List<GameObject> gameObjectList;
        public int unitSize;
        public int unitSpacing;

        public GameState()
        {
            unitSize = 64;
            unitSpacing = 1;
            gameObjectList = new List<GameObject>();
        }

        public virtual void Reset()
        {
            foreach (GameObject gameObject in gameObjectList)
                gameObject.Reset();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (GameObject gameObject in gameObjectList)
                gameObject.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject gameObject in gameObjectList)
                gameObject.Draw(spriteBatch);
        }
    }
}
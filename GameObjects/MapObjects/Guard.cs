using Microsoft.Xna.Framework;

namespace FancyKlepto.GameObjects
{
    class Guard : GameObjectList
    {
        int frameCounter = 0;
        const int guards = 1;
        public Vector2 B;
        SpriteGameObject guard = new SpriteGameObject("spr_guard");
        public Guard(float A, float B, float C, float D)
        {
            for (int i = 0; i < guards; i++)
            {
                Add(guard);
            }

            Reset();
            position = new Vector2(A, B);
            this.B = new Vector2(C, D);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            frameCounter++; //keep track of frames

            Movement();
            if ((!guard.position.Equals(position) || !guard.position.Equals(B)) && frameCounter > 40)
            {
                position.X += velocity.X;
                frameCounter = 0;
            }
        }

        public void Movement()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i].position.X < 0 || Children[i].position.X > GameEnvironment.Screen.X - Children[i].texture.Width)
                {
                    Children[i].velocity.X = -Children[i].velocity.X;
                }
            }
        }

        public bool Overlaps(SpriteGameObject other)
        {
            return guard.Overlaps(other);
        }

        public override void Reset()
        {
            base.Reset();
            guard.position = position;
        }
    }
}
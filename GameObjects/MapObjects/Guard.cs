using Microsoft.Xna.Framework;
using FancyKlepto.GameObjects;

class Guard : GameObjectList
{
    Map map = new Map("spr_1.3");
    int frameCounter = 0;
    SpriteGameObject guard = new SpriteGameObject("spr_guard");
    public Guard()
    {
        this.Add(guard);
        this.Add(guard);
        for (int i = 0; i < Children.Count; i++)
        {
            if (i == 0)
            {
                position.X = 250;
            }
            if (i == 1)
            {
                position.X = 780;
            }
            position.Y = 65 * i;
        }
        velocity = new Vector2(GameEnvironment.Random.Next(-20, 20), 0);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        frameCounter++; //keep track of frames
        Movement();
        BorderCollision();
    }

    public void Movement()
    {
        if (frameCounter > 20)
        {
            position.X += velocity.X;
            frameCounter = 0;
        }
    }

    public void BorderCollision()
    {
        //Collision with border of screen
        if (position.X >= GameEnvironment.Screen.X - texture.Width)
        {
            velocity.X = -velocity.X;
        }
        if (position.X <= texture.Width)
        {
            velocity.X = -velocity.X;
        }
    }
}

using Microsoft.Xna.Framework;
using FancyKlepto.GameObjects;

class Guard : SpriteGameObject
{
    Map map = new Map("spr_1.3");
    int frameCounter = 0;
    public Guard(Vector2 position) : base("spr_guard")
    {
        Reset();
        this.position = position;
        velocity = new Vector2(10, 0);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        frameCounter++; //keep track of frames
        if (frameCounter > 20)
        {
            position.X += velocity.X;
            frameCounter = 0;
        }

        //Collision with border of screen
        if (position.X >= map.texture.Width - (texture.Width * 4))
        {
            velocity.X = -velocity.X;
        }
        if (position.X <= texture.Width * 3)
        {
            velocity.X = -velocity.X;
        }
    }
}

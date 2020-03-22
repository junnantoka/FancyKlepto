using Microsoft.Xna.Framework;
using FancyKlepto.GameObjects;

class Guard : GameObjectList
{
    int frameCounter = 0;
    const int guards = 1;
    SpriteGameObject guard = new SpriteGameObject("spr_guard");
    SpriteGameObject guard1 = new SpriteGameObject("spr_guard");
    public Guard()
    {
        for (int i = 0; i < guards; i++)
        {
            Add(guard);
            Add(guard1);
        }

        Reset();

        guard.velocity.X = -150;
        guard1.velocity.X = 150;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        frameCounter++; //keep track of frames
        Movement();
    }

    public void Movement()
    {
        if (frameCounter > 40)
        {
            position += velocity;
            frameCounter = 0;
        }
    }

    public bool Overlaps(SpriteGameObject other)
    {
        return guard.Overlaps(other) || guard1.Overlaps(other);
    }

    public override void Reset()
    {
        base.Reset();
        guard.position.X = 500;
        guard.position.Y = 65;

        guard1.position.X = 780;
        guard1.position.Y = 130;
    }
}
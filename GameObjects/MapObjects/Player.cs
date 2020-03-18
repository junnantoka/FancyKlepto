using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
class Player : SpriteGameObject
{
    public Vector2 maxVelocity;
    public Vector2 zeroVelocity;
    public Vector2 minVelocity;
    public int stopVelocity;
    public Vector2 velocityVelocity;

    public bool moveRight, moveLeft, moveUp, moveDown;
    public Player(int x, int y) : base("spr_player")
    {
        velocityVelocity = new Vector2(0.2f, 0.2f);
        stopVelocity = 2;
        position = new Vector2(x * (unitSize + unitSpacing), y * (unitSize + unitSpacing));
        pPosition = position;
        maxVelocity = new Vector2(5, 5);
        zeroVelocity = new Vector2(0, 0);
        minVelocity = -1 * maxVelocity;
        Reset();
    }

    public override void Reset()
    {
        base.Reset();
        moveRight = true;
        moveLeft = true;
        moveUp = true;
        moveDown = true;
        position = pPosition;
        velocity = zeroVelocity;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        position.X = MathHelper.Clamp(position.X, 0, GameEnvironment.Screen.X - texture.Width);
        position.Y = MathHelper.Clamp(position.Y, 0, GameEnvironment.Screen.Y - texture.Height);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        position += velocity;
        if (inputHelper.KeyPressed(Keys.A) && velocity.X > minVelocity.X && moveLeft)
        {
            velocity.X -= velocityVelocity.X;
        }
        if (inputHelper.KeyPressed(Keys.D) && velocity.X < maxVelocity.X && moveRight)
        {
            velocity.X += velocityVelocity.X;
        }
        if (inputHelper.KeyPressed(Keys.W) && velocity.Y > minVelocity.Y && moveUp)
        {
            velocity.Y -= velocityVelocity.Y;
        }
        if (inputHelper.KeyPressed(Keys.S) && velocity.Y < maxVelocity.Y && moveDown)
        {
            velocity.Y += velocityVelocity.Y;
        }

        if (inputHelper.KeyPressed(Keys.A) &&
            inputHelper.KeyPressed(Keys.D) &&
            inputHelper.KeyPressed(Keys.W) &&
            inputHelper.KeyPressed(Keys.S))
        {
            if (velocity.X < stopVelocity &&
                velocity.X > -stopVelocity &&
                velocity.Y < stopVelocity &&
                velocity.Y > -stopVelocity)
            {
                velocity = zeroVelocity;
            }

            if (velocity.X > zeroVelocity.X)
            {
                velocity.X -= velocityVelocity.X;
            }
            if (velocity.X < zeroVelocity.X)
            {
                velocity.X += velocityVelocity.X;
            }

            if (velocity.Y > zeroVelocity.Y)
            {
                velocity.Y -= velocityVelocity.Y;
            }
            if (velocity.Y < zeroVelocity.Y)
            {
                velocity.Y += velocityVelocity.Y;
            }
        }
        if (inputHelper.KeyPressed(Keys.A))
        {
            moveLeft = true;
        }
        if (inputHelper.KeyPressed(Keys.D))
        {
            moveRight = true;
        }
        if (inputHelper.KeyPressed(Keys.W))
        {
            moveUp = true;
        }
        if (inputHelper.KeyPressed(Keys.S))
        {
            moveDown = true;
        }
    }
}
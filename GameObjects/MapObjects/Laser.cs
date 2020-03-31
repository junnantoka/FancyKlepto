using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
class Laser : SpriteGameObject
{
    private bool Active;
    private float angle;
    private float radius;

    public Laser(Vector2 position, Vector2 position2, string assetName) : base(assetName)
    {
        this.position = new Vector2(position.X * (unitSize + unitSpacing), position.Y * (unitSize + unitSpacing));
        this.position2 = new Vector2(position2.X * (unitSize + unitSpacing), position2.Y * (unitSize + unitSpacing));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Active = true;
        //Calculates the size of the line and the angle of the line based on the given position
        radius = (float)Math.Sqrt((position.X - position2.X) * (position.X - position2.X) + (position.Y - position2.Y) * (position.Y - position2.Y));
        angle = (float)Math.Atan2(position2.Y - position.Y, position2.X - position.X);
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        //Draws the laser 
        if (Active)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)radius, 7), new Rectangle(0, 0, 1, 1), Color.White, angle, new Vector2(0, 0), SpriteEffects.None, 1);
        }
    }
}
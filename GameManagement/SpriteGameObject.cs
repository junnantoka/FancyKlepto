using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
class SpriteGameObject : GameObject
{

    public SpriteGameObject(String assetName)
    {
        if (assetName.Length > 0)
            texture = GameEnvironment.ContentManager.Load<Texture2D>(assetName);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (visible)
            spriteBatch.Draw(texture, GlobalPosition, Color.White);
    }



}
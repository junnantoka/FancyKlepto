using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
class SwitchBoard : SpriteGameObject
{
    public Color color;
    private Texture2D light1, light2, light3;
    bool blink,solved;
    int blink_timer;

    private Vector2 lightpos1, lightpos2, lightpos3;

    public SwitchBoard(int x, int y, Color color) : base("Map/spr_switchboard_art")
    {
        this.color = color;
        position = new Vector2(18 + x * (unitSize + unitSpacing), 10 + y * (unitSize + unitSpacing));
        defPos = position;

        light1 = GameEnvironment.AssetManager.GetSprite("Map/spr_switchboard_light");
        light2 = GameEnvironment.AssetManager.GetSprite("Map/spr_switchboard_light");
        light3 = GameEnvironment.AssetManager.GetSprite("Map/spr_switchboard_light");
        solved = false;
        Reset();
    }
    public override void Reset()
    {
        position = defPos;

        blink_timer = 0;
        blink = false;

        lightpos1 = position;
        lightpos2 = position;
        lightpos3 = position;

        lightpos1.X += 8 + 4;
        lightpos2.X += 8 + 16 + 4;
        lightpos3.X += 8 + 16 + 16 + 4;

        lightpos1.Y += 2;
        lightpos2.Y += 2;
        lightpos3.Y += 2;

    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (!solved)
        {
            blink_timer++;
            if (blink_timer < 30)
            {
                blink = true;
            }
            else blink = false;

            if (blink_timer >= 60)
            {
                blink_timer = 0;
            }
        }
        else
        {
            blink_timer = 0;
            blink = true;
            color = Color.Gray;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);

        if (blink)
        {
            spriteBatch.Draw(light1, lightpos1, color);
            spriteBatch.Draw(light2, lightpos2, color);
            spriteBatch.Draw(light3, lightpos3, color);
        }
    }
}

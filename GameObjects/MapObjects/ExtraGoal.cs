using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

class ExtraGoal : SpriteGameObject
{
    SoundEffect Item_Collected1, Item_Collected2, Item_Collected3;
    public int Timer;
    public bool hold;
    public ExtraGoal(int x, int y) : base("spr_diamond_dark")
    {
        Item_Collected1 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Collected1");
        
        Item_Collected2 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Collected2");
        Item_Collected3 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Collected3");
        Reset();
        position = new Vector2(18 + 1 + x * (unitSize + unitSpacing), 10 + 1 + y * (unitSize + unitSpacing));
        defPos = position;
    }
    public override void Reset()
    {
        position = defPos;
        hold = false;
    }
    public void Hold(SpriteGameObject pObject)
    {
        position.X = pObject.Position.X + pObject.Sprite.Width / 2 - sprite.Width / 2;
        position.Y = pObject.Position.Y + pObject.Sprite.Height / 2 - sprite.Height / 2;
    }
    public override void Update(GameTime gameTime)
    {
        if (Timer == 1)
        {
            Item_Collected1.Play();
            Timer = 0;
        }
        if (Timer == 2)
        {
            Item_Collected2.Play();
            Timer = 0;
        }
        if (Timer == 3)
        {
            Item_Collected3.Play();
            Timer = 0;
        }
        base.Update(gameTime);
    }
}

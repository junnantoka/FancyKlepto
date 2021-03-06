﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

class MainGoal : SpriteGameObject
{
     SoundEffect Item_Collected1, Item_Collected2, Item_Collected3;
    public int Timer;
    public bool hold;
    public MainGoal(int x, int y) : base("Map/spr_nachtwacht_frame")
    {
        Item_Collected1 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Collected1");
        Item_Collected2 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Collected2");
        Item_Collected3 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Collected3");
        origin = sprite.Center;
        Reset();
        position = new Vector2(18 + 1 + x * (unitSize + unitSpacing), 10 + 1 + y * (unitSize + unitSpacing));
        defPos = position;
    }
    public override void Reset()
    {
        position = defPos;
        hold = false;
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
    public void Hold(SpriteGameObject pObject)
    {
        position.X = pObject.Position.X; //+ pObject.Sprite.Width / 2 - sprite.Width / 2;
        position.Y = pObject.Position.Y; //+ pObject.Sprite.Height / 2 - sprite.Height / 2;
    }
}
﻿using Microsoft.Xna.Framework;

class ExtraGoal : SpriteGameObject
{
    public bool hold;
    public ExtraGoal(int x, int y) : base("spr_secondgoal")
    {
        Reset();
        position = new Vector2(2 + x * (unitSize + unitSpacing), 2 + y * (unitSize + unitSpacing));
        defPos = position;
    }
    public override void Reset()
    {
        position = defPos;
        hold = false;
    }
    public void Hold(SpriteGameObject pObject)
    {
        position = pObject.Center;
    }
}

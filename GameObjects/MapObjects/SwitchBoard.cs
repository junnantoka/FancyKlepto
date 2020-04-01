using Microsoft.Xna.Framework;
    class SwitchBoard : SpriteGameObject
{
    public SwitchBoard(int x, int y) : base("spr_switchboard")
        {
            Reset();
            position = new Vector2(18 + x * ( unitSize + unitSpacing), 10 + y *( unitSize + unitSpacing));
            defPos = position;
        }
        public override void Reset()
        {
            position = defPos;
        }
    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FancyKlepto.GameManagement;

namespace FancyKlepto.GameObjects
{
    class Player : GameObject
    {
        const int PLAYERSPD = 40;
        public Player() : base("spr_circle")
        {
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update()
        {
            base.Update();
            velocity.X = 0;
            velocity.Y = 0;
            if (Fancy_Klepto.previousState.IsKeyDown(Keys.W) &&
                Fancy_Klepto.KeyboardState.IsKeyUp(Keys.W))
            {
                velocity.Y = -PLAYERSPD;
            }
            if (Fancy_Klepto.previousState.IsKeyDown(Keys.A) &&
                Fancy_Klepto.KeyboardState.IsKeyUp(Keys.A))
            {
                velocity.X = -PLAYERSPD;
            }
            if (Fancy_Klepto.previousState.IsKeyDown(Keys.S) &&
                Fancy_Klepto.KeyboardState.IsKeyUp(Keys.S))
            {
                velocity.Y = PLAYERSPD;
            }
            if (Fancy_Klepto.previousState.IsKeyDown(Keys.D) &&
                Fancy_Klepto.KeyboardState.IsKeyUp(Keys.D))
            {
                velocity.X = PLAYERSPD;
            }

            position.X = MathHelper.Clamp(position.X, 0, GameEnvironment.Screen.X - texture.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, GameEnvironment.Screen.Y - texture.Height);
        }
    }
}
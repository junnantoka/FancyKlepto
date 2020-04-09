
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FancyKlepto.GameObjects.MapObjects
{
    class InputAnwser : TextGameObject
    {
        public int length;
        private string _stringValue = string.Empty;

        private Vector2 openPos; private float screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public InputAnwser(float x, float y) : base("Input")
        {
            color = Color.Gray;
            velocity = new Vector2(15, 0); position = new Vector2(x, y);
            //position.X += screenWidth;
            defPos = position;
            // 352 is total width of the open window            
            openPos.X = defPos.X - 352;
            text = "";
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Move();
        }
        public void Move()
        {
            if (open && position.X > openPos.X)
            {
                position.X -= velocity.X;
                if (position.X < openPos.X)
                {
                    position.X = openPos.X;
                }
            }
            else if (!open && position.X < defPos.X)
            {
                position.X += velocity.X;
                if (position.X > defPos.X)
                {
                    position.X = defPos.X;
                }
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (length < 6)

            {
                
                if (inputHelper.KeyPressed(Keys.NumPad0) || inputHelper.KeyPressed(Keys.NumPad1) || inputHelper.KeyPressed(Keys.NumPad2) || inputHelper.KeyPressed(Keys.NumPad3) || inputHelper.KeyPressed(Keys.NumPad4)
                   || inputHelper.KeyPressed(Keys.NumPad5) || inputHelper.KeyPressed(Keys.NumPad6) || inputHelper.KeyPressed(Keys.NumPad7) || inputHelper.KeyPressed(Keys.NumPad8)
                   || inputHelper.KeyPressed(Keys.NumPad9) || inputHelper.KeyPressed(Keys.X) || inputHelper.KeyPressed(Keys.Y) || inputHelper.KeyPressed(Keys.OemPlus) || inputHelper.KeyPressed(Keys.OemMinus)
                   || inputHelper.KeyPressed(Keys.D0) || inputHelper.KeyPressed(Keys.D1) || inputHelper.KeyPressed(Keys.D2) || inputHelper.KeyPressed(Keys.D3) || inputHelper.KeyPressed(Keys.D4)
                   || inputHelper.KeyPressed(Keys.D5) || inputHelper.KeyPressed(Keys.D6) || inputHelper.KeyPressed(Keys.D7) || inputHelper.KeyPressed(Keys.D8) || inputHelper.KeyPressed(Keys.D9))
                { 
                    

                    var keyboardState = Keyboard.GetState(); var keys = keyboardState.GetPressedKeys();

                    if (keys.Length > 0)
                    {
                        _stringValue = keys[0].ToString();
                        text += _stringValue;
                        length++;
                    }
                } 
                
                else if (inputHelper.KeyPressed(Keys.Back))
                {
                    var keyboardState = Keyboard.GetState(); var keys = keyboardState.GetPressedKeys();
                    text.Remove(keys.Length - 1);
                }
            }
        }
    }
}

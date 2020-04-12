
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Security.AccessControl;
using System.Security.Cryptography;

namespace FancyKlepto.GameObjects.MapObjects
{
    class InputAnwser : TextGameObject
    {
        public int length;

        private string[] _OldText= new string[6];
        private string _stringValue = string.Empty;

        private Vector2 openPos; private float screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public InputAnwser(float x, float y) : base("Input")
        {
            color = Color.Gray;
            velocity = new Vector2(15, 0);
            position = new Vector2(x, y);
            position.X += screenWidth;
            defPos = position;
            // 352 is total width of the open window            
            openPos.X = defPos.X - 352;
            text = "";
            _OldText[0] = text;
            _OldText[1] = text;
            _OldText[2] = text;
            _OldText[3] = text;
            _OldText[4] = text;
            _OldText[5] = text;
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
            if (length < 6)
            {
                if (inputHelper.KeyPressed(Keys.NumPad0) || inputHelper.KeyPressed(Keys.D0))
                {
                    _stringValue = "0";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad1) || inputHelper.KeyPressed(Keys.D1))
                {
                    _stringValue = "1";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad2) || inputHelper.KeyPressed(Keys.D2))
                {
                    _stringValue = "2";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad3) || inputHelper.KeyPressed(Keys.D3))
                {
                    _stringValue = "3";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad4) || inputHelper.KeyPressed(Keys.D4))
                {
                    _stringValue = "4";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad5) || inputHelper.KeyPressed(Keys.D5))
                {
                    _stringValue = "5";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad6) || inputHelper.KeyPressed(Keys.D6))
                {
                    _stringValue = "6";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad7) || inputHelper.KeyPressed(Keys.D7))
                {
                    _stringValue = "7";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad8) || inputHelper.KeyPressed(Keys.D8))
                {
                    _stringValue = "8";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad9) || inputHelper.KeyPressed(Keys.D9))
                {
                    _stringValue = "9";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad9) || inputHelper.KeyPressed(Keys.D9))
                {
                    _stringValue = "9";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.X))
                {
                    _stringValue = "X";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.Y))
                {
                    _stringValue = "Y";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.OemPlus))
                {
                    _stringValue = "+";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.OemMinus))
                {
                    _stringValue = "-";
                    length++;
                }
                else
                {
                    _stringValue = "";
                }

                if (_stringValue != "")
                {
                    _OldText[length-1] = text;
                }

                text += _stringValue;
            }
            if (inputHelper.KeyPressed(Keys.Back)&&length>=1)
            {
                text = _OldText[length-1];
                length -= 1;
            }
        }
    }
}

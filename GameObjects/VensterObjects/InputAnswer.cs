using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;

namespace FancyKlepto.GameObjects
{
    class InputAnswer : TextGameObject
    {
        public SoundEffect Button_Enter, Button_Typing1, Button_Typing2, Button_Typing3;
        public int length;
        public int maxLength = 12;
        bool pressed;
        private string[] _OldText = new string[12];
        private string _stringValue = string.Empty;

        private Vector2 openPos; private float screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public InputAnswer(float x, float y) : base("Input")
        {
            Button_Typing1 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/typing1");
            Button_Typing2 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/typing2");
            Button_Typing3 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/typing3");
            Button_Enter = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Enter");
            color = Color.Gray;
            velocity = new Vector2(15, 0);

            position = new Vector2(x + screenWidth, y);
            defPos = position;
            // 352 is total width of the open window            
            openPos.X = defPos.X - 352;

            text = "";
            for (int i = 0; i < _OldText.Length; i++)
            {
                _OldText[i] = text;
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Move();
            Sound();
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
                    text = "";
                }
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (length < maxLength)
            {
                if (inputHelper.KeyPressed(Keys.NumPad0) || inputHelper.KeyPressed(Keys.D0))
                {
                    pressed = true;
                    _stringValue = "0";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad1) || inputHelper.KeyPressed(Keys.D1))
                {
                    pressed = true;
                    _stringValue = "1";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad2) || inputHelper.KeyPressed(Keys.D2))
                {
                    pressed = true;
                    _stringValue = "2";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad3) || inputHelper.KeyPressed(Keys.D3))
                {
                    pressed = true;
                    _stringValue = "3";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad4) || inputHelper.KeyPressed(Keys.D4))
                {
                    pressed = true;
                    _stringValue = "4";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad5) || inputHelper.KeyPressed(Keys.D5))
                {
                    pressed = true;
                    _stringValue = "5";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad6) || inputHelper.KeyPressed(Keys.D6))
                {
                    pressed = true;
                    _stringValue = "6";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad7) || inputHelper.KeyPressed(Keys.D7))
                {
                    pressed = true;
                    _stringValue = "7";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad8) || inputHelper.KeyPressed(Keys.D8))
                {
                    pressed = true;
                    _stringValue = "8";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad9) || inputHelper.KeyPressed(Keys.D9))
                {
                    pressed = true;
                    _stringValue = "9";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.NumPad9) || inputHelper.KeyPressed(Keys.D9))
                {
                    pressed = true;
                    _stringValue = "9";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.X))
                {
                    pressed = true;
                    _stringValue = "X";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.Y))
                {
                    pressed = true;
                    _stringValue = "Y";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.OemComma))
                {
                    pressed = true;
                    _stringValue = ",";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.Divide))
                {
                    pressed = true;
                    _stringValue = "/";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.OemPlus))
                {
                    pressed = true;
                    _stringValue = "+";
                    length++;
                }
                else if (inputHelper.KeyPressed(Keys.OemMinus))
                {
                    pressed = true;
                    _stringValue = "-";
                    length++;
                }
                else
                {
                    _stringValue = "";
                }

                if (_stringValue != "")
                {
                    _OldText[length - 1] = text;
                }

                text += _stringValue;
            }
            if (inputHelper.KeyPressed(Keys.Back) && length > 0)
            {
                text = _OldText[length - 1];
                length -= 1;
                pressed = true;
            }
        }
        public void Sound()
        {
            if (pressed)
            {
                int rndm = GameEnvironment.Random.Next(1, 3);

                if (rndm == 1)
                {
                    Button_Typing1.Play();
                }
                else if (rndm == 2)
                {
                    Button_Typing2.Play();
                }
                else if (rndm == 3)
                {
                    Button_Typing3.Play();
                }
                pressed = false;
            }
        }
    }
}

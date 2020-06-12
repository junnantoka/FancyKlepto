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
        public const int _MaxLength = 12;
        bool pressed;
        private string[] oldText = new string[12];
        private string _stringValue = string.Empty;

        private Vector2 openPos;
        private float screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        public InputAnswer(float x, float y) : base("Input")
        {
            //Load the sound effects
            Button_Typing1 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/typing1");
            Button_Typing2 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/typing2");
            Button_Typing3 = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/typing3");
            Button_Enter = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Enter");
            color = Color.Gray;

            
            velocity = new Vector2(0, 5);
            position = new Vector2(x+10, y);
            defPos = position;

            openPos.X = defPos.X;
            openPos.Y = defPos.Y - 975 - 15;

            text = "";
            for (int i = 0; i < oldText.Length; i++)
            {
                oldText[i] = text;
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
            if (open && position.Y > openPos.Y)
            {
                position.Y -= velocity.Y;
                if (position.Y < openPos.Y)
                {
                    position.Y = openPos.Y;
                }
            }
            else if (!open && position.Y < defPos.Y)
            {
                position.Y += velocity.Y;
                if (position.Y > defPos.Y)
                {
                    position.Y = defPos.Y;
                    text = "";
                }
            }
        }


        public override void HandleInput(InputHelper inputHelper)
        {
            if (open)
            {
                if (length < _MaxLength)
                {
                    string anwserInput = Input(inputHelper);
                    if (anwserInput != "")
                    {
                        oldText[length - 1] = text;
                    }

                    text += anwserInput;
                }
            }
            if (!open)
            {
                Reset();
            }
            if (inputHelper.KeyPressed(Keys.Back) && length > 0)
            {
                Remove();
            }
        }

        private string Input(InputHelper inputHelper)
        {
            string returnText;
            //If a key is pressed, return the stringvalue of the key.
            if (inputHelper.KeyPressed(Keys.NumPad0) || inputHelper.KeyPressed(Keys.D0))
            {
                pressed = true;
                returnText = "0";
                length++;
            }
            else if (inputHelper.KeyPressed(Keys.NumPad1) || inputHelper.KeyPressed(Keys.D1))
            {
                pressed = true;
                returnText = "1";
                length++;
            }
            else if (inputHelper.KeyPressed(Keys.NumPad2) || inputHelper.KeyPressed(Keys.D2))
            {
                pressed = true;
                returnText = "2";
                length++;
            }
            else if (inputHelper.KeyPressed(Keys.NumPad3) || inputHelper.KeyPressed(Keys.D3))
            {
                pressed = true;
                returnText = "3";
                length++;
            }
            else if (inputHelper.KeyPressed(Keys.NumPad4) || inputHelper.KeyPressed(Keys.D4))
            {
                pressed = true;
                returnText = "4";
                length++;
            }
            else if (inputHelper.KeyPressed(Keys.NumPad5) || inputHelper.KeyPressed(Keys.D5))
            {
                pressed = true;
                returnText = "5";
                length++;
            }
            else if (inputHelper.KeyPressed(Keys.NumPad6) || inputHelper.KeyPressed(Keys.D6))
            {
                pressed = true;
                returnText = "6";
                length++;
            }
            else if (inputHelper.KeyPressed(Keys.NumPad7) || inputHelper.KeyPressed(Keys.D7))
            {
                pressed = true;
                returnText = "7";
                length++;
            }
            else if (inputHelper.KeyPressed(Keys.NumPad8) || inputHelper.KeyPressed(Keys.D8))
            {
                pressed = true;
                returnText = "8";
                length++;
            }
            else if (inputHelper.KeyPressed(Keys.NumPad9) || inputHelper.KeyPressed(Keys.D9))
            {
                pressed = true;
                returnText = "9";
                length++;
            }
            else if (inputHelper.KeyPressed(Keys.NumPad9) || inputHelper.KeyPressed(Keys.D9))
            {
                pressed = true;
                returnText = "9";
                length++;
            }
            else if (inputHelper.KeyPressed(Keys.X))
            {
                pressed = true;
                returnText = "X";
                length++;
            }
            else if (inputHelper.KeyPressed(Keys.Y))
            {
                pressed = true;
                returnText = "Y";
                length++;
            }
            else if (inputHelper.KeyPressed(Keys.OemComma))
            {
                pressed = true;
                returnText = ",";
                length++;
            }
            else if (inputHelper.KeyPressed(Keys.Divide))
            {
                pressed = true;
                returnText = "/";
                length++;
            }
            else if (inputHelper.KeyPressed(Keys.OemPlus))
            {
                pressed = true;
                returnText = "+";
                length++;
            }
            else if (inputHelper.KeyPressed(Keys.OemMinus))
            {
                pressed = true;
                returnText = "-";
                length++;
            }
            else
            {
                returnText = "";
            }
            return returnText;
        }
        public override void Reset()
        {
            text = "";
            length = 0;
        }
        public void Remove()
        {
            text = oldText[length - 1];
            length -= 1;
            pressed = true;
        }
        public void Sound()
        {
            if (pressed)
            {
                int random = GameEnvironment.Random.Next(1, 3);

                if (random == 1)
                {
                    Button_Typing1.Play();
                }
                else if (random == 2)
                {
                    Button_Typing2.Play();
                }
                else if (random == 3)
                {
                    Button_Typing3.Play();
                }
                pressed = false;
            }
        }
    }
}

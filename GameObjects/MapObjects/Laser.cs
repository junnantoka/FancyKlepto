using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using Newtonsoft.Json;

class Laser : SpriteGameObject
{
    SoundEffect Lazer_Off, Lazer_Alert, Lazer_Col, Lazer_Col_Alarm, Lazer_Spark;
    public int Off, Alert, Col, Col_Alarm, Spark;
    public bool Active;
    private float angle, radius;
    private readonly Texture2D texture;
    public Color color;

    public Vector2 position2;
    public Vector2 formulPos, formulPos2;
    public Vector2 gridPos,gridPos2;

    public float slopeX, slopeY, slope, c, cTop, cBot;
    public string Formula;
    public string slope_string,slopeX_string,slopeY_string,c_string,cTop_string,cBot_string;
    public Laser(Vector2 position, Vector2 position2, Color color,int xaxis,int yaxis) : base("Laser/lazer")
    {
        Active = true;
        #region sound
        Lazer_Off = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Lazer_Off");
        Lazer_Alert = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/alert");
        Lazer_Col = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Lazer_Col");
        Lazer_Col_Alarm = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Lazer_Col_Alarm");
        Lazer_Spark = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/Lazer_Spark");
        #endregion
        texture = GameEnvironment.AssetManager.GetSprite("Laser/lazer");
        this.color = color;
        this.position = new Vector2(18 + position.X * (unitSize + unitSpacing), 10 + position.Y * (unitSize + unitSpacing));
        this.position2 = new Vector2(18 + position2.X * (unitSize + unitSpacing), 10 + position2.Y * (unitSize + unitSpacing));
        gridPos = position;
        gridPos2 = position2;

        formulPos.X = gridPos.X - xaxis;
        formulPos.Y = gridPos.Y - yaxis;
        formulPos2.X = gridPos2.X - xaxis;
        formulPos2.Y = gridPos2.Y - yaxis;

        slopeX = (formulPos.X - formulPos2.X);
        slopeY = (formulPos.Y - formulPos2.Y);
        slope = slopeX / slopeY;


        cTop = (gridPos.Y * slopeY - gridPos.X * slopeX);
        cBot = slopeY;
        c = cTop / cBot;

        if (slope % 1 == 0)
        {
            slope_string =slope.ToString();
            Formula = slope_string;
        }
        else
        {
            slopeX_string = Math.Abs(slopeX).ToString();
            slopeY_string = Math.Abs(slopeY).ToString();
            if (slopeX < 0 && slopeY < 0)
            {
                Formula = slopeX_string + "/" + slopeY_string;
            }
            else if (slopeX > 0 && slopeY < 0)
            {
                Formula = "-" + slopeX_string + "/" + slopeY_string;
            }
            else if (slopeX < 0 && slopeY > 0)
            {
                Formula = "-" + slopeX_string + "/" + slopeY_string;
            }
            else if (slopeX > 0 && slopeY > 0)
            {
                Formula = slopeX_string + "/" + slopeY_string;
            }
        }

        Formula += "X";


        if (c % 1 == 0)
        {
            if (c > 0)
            {
                c_string ="+" + c.ToString();
            } else if (c < 0)
            {
                c_string = c.ToString();
            }
            Formula += c_string;
        }
        else
        {
            cTop_string = Math.Abs(cTop).ToString();
            cBot_string = Math.Abs(cBot).ToString();
            if (cTop > 0 && cBot > 0)
            {
                Formula += "+" + cTop_string + "/" + cBot_string;
            }
            else if (cTop > 0 && cBot < 0)
            {
                Formula += "-" + cTop_string + "/" + cBot_string;

            }
            else if (cTop < 0 && cBot > 0)
            {
                Formula += "-" + cTop_string + "/" + cBot_string;

            }
            else if (cTop < 0 && cBot < 0)
            {
                Formula += "+" + cTop_string + "/" + cBot_string;
            }
        }
        Console.WriteLine(Formula);
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Spark--;
        //Calculates the size of the line and the angle of the line based on the given position
        radius = (float)Math.Sqrt((position.X - position2.X) * (position.X - position2.X) + (position.Y - position2.Y) * (position.Y - position2.Y));
        angle = (float)Math.Atan2(position2.Y - position.Y, position2.X - position.X);
        Sound();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //Draws the laser
        if (Active)
        {
            if (Spark <= 0)
            {
                Lazer_Spark.Play();
                Spark = GameEnvironment.Random.Next(420,600);
            }
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)radius, 7), new Rectangle(0, 0, 1, 1), color, angle, new Vector2(0, 0), SpriteEffects.None, 1);
        }
    }
    public void Sound()
    {
        if(Off == 1)
        {
            Lazer_Off.Play();
            Off = 0;
        }
        if(Alert == 1)
        {
            Lazer_Alert.Play();
            Alert = 0;
        }
        if (Col == 1)
        {
            Lazer_Col.Play();
            Col = 0;
        }
        if (Col_Alarm == 1)
        {
            Lazer_Col_Alarm.Play();
            Col_Alarm = 0;
        }
    }
}
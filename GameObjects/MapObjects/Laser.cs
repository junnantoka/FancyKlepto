using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using Newtonsoft.Json;
using Zds;
using FancyKlepto;

class Laser : RotatingSpriteGameObject
{
    protected SpriteSheet laser_corner1, laser_corner2,laser_corner_small1,laser_corner_small2;

    SoundEffect Lazer_Off, Lazer_Alert, Lazer_Col, Lazer_Col_Alarm, Lazer_Spark;
    public int Off, Alert, Col, Col_Alarm, Spark;
    public bool Active;
    private float angle, radius;
    private readonly Texture2D texture;
    public Color color;

    public Vector2 position2;
    public Vector2 formulPos, formulPos2;
    public Vector2 gridPos, gridPos2;

    public float slopeX, slopeY, slope, c, cTop, cBot;
    public string Formula;
    public string slope_string, slopeX_string, slopeY_string, c_string, cTop_string, cBot_string;
    public Laser(Vector2 position, Vector2 position2, Color color, int xaxis, int yaxis) : base("big_laser")
    {

        laser_corner1 = new SpriteSheet("Laser/laser_corner2_dirty", 0);
        laser_corner2 = new SpriteSheet("Laser/laser_corner2_dirty", 0);
        laser_corner_small1 = new SpriteSheet("Laser/laser_corner",0);
        laser_corner_small2 = new SpriteSheet("Laser/laser_corner", 0);

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

        //gridPosition from map (0,0) is topLeft of the screen
        gridPos = position;
        gridPos2 = position2;

        //Here you get the grid position depending on axises
        formulPos.X = gridPos.X - yaxis;
        formulPos.Y = gridPos.Y -  xaxis;
        formulPos2.X = gridPos2.X - yaxis;
        formulPos2.Y = gridPos2.Y -  xaxis;

        //variables of the slope of the formula is determined here
        slopeX = (formulPos.X - formulPos2.X);
        slopeY = (formulPos.Y - formulPos2.Y);
        slope = slopeY / slopeX;

        //variables of the c value of the formula is determined here
        cTop = slopeX*formulPos.Y-slopeY*formulPos.X;
        cBot = slopeX;
        c = cTop / cBot;


        //if slope is an integer I adds slope to the formula 
        if (slope % 1 == 0)
        {
            slope_string = slope.ToString();
            //creates the first step of the formula
            if (slope != 0)
                Formula = slope_string;
            //because we dont want -1X in our formula we must write exceptions for it
            if (slope == 1)
            {
                Formula = "";
            }
            if (slope == -1)
            {
                Formula = "-";
            }
        }
        else
        {
            // If slope isn't an integer It but be calculated separately for top and bottom variable of the slope 
            slopeX_string = Math.Abs(slopeX).ToString();
            slopeY_string = Math.Abs(slopeY).ToString();
            if (slopeX < 0 && slopeY < 0)
            {
                Formula = slopeY_string  + "/" + slopeX_string;
            }
            else if (slopeX > 0 && slopeY < 0)
            {
                Formula = "-" + slopeY_string + "/" + slopeX_string;
            }
            else if (slopeX < 0 && slopeY > 0)
            {
                Formula = "-" + slopeY_string + "/" + slopeX_string;
            }
            else if (slopeX > 0 && slopeY > 0)
            {
                Formula = slopeY_string + "/" + slopeX_string;
            }
        }
        //here it adds X string to the slope so it can be writen like -1/3X
        if(slope!=0)
        Formula += "X";
        //it is calculated here if c value is an separately
        if (c % 1 == 0)
        {
            if (c > 0&&slope!=0)
            {
                c_string = "+" + c.ToString();
            }
            else if (c < 0)
            {
                c_string = c.ToString();
            }
            if (c == 0)
            {
                c_string = "";
            }
            Formula += c_string;
        }
        else
        {
            //Like slope floats must be calculated separately
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
        Console.WriteLine(Formula + " " + this.color.ToString());
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //Spark soundeffect timer
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
                Spark = GameEnvironment.Random.Next(600, 1000);
            }
            //the lazer texture must be replaced depending on slope
            if (slope<0) {
                laser_corner1.Draw(spriteBatch, position - new Vector2(24, 24), origin);
                laser_corner2.Draw(spriteBatch, position2 - new Vector2(24, 24), origin);

                spriteBatch.Draw(texture, new Rectangle((int)position.X - 6, (int)position.Y - 6, (int)radius, 24), new Rectangle(0, 0, 1, 1), new Color(color, 50), angle, new Vector2(0, 0), SpriteEffects.None, 1);
                spriteBatch.Draw(texture, new Rectangle((int)position.X - 4, (int)position.Y - 4, (int)radius, 15), new Rectangle(0, 0, 1, 1), new Color(color, 50), angle, new Vector2(0, 0), SpriteEffects.None, 1);
                spriteBatch.Draw(texture, new Rectangle((int)position.X - 2, (int)position.Y - 2, (int)radius, 8), new Rectangle(0, 0, 1, 1), new Color(color, 50), angle, new Vector2(0, 0), SpriteEffects.None, 1);
                spriteBatch.Draw(texture, new Rectangle((int)position.X - 1, (int)position.Y - 1, (int)radius, 5), new Rectangle(0, 0, 1, 1), new Color(color, 50), angle, new Vector2(0, 0), SpriteEffects.None, 1);
                spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)radius, 4), new Rectangle(0, 0, 1, 1), new Color(color, 200), angle, new Vector2(0, 0), SpriteEffects.None, 1);

                laser_corner_small1.Draw(spriteBatch, position - new Vector2(12, 12), origin);
                laser_corner_small2.Draw(spriteBatch, position2 - new Vector2(12, 12), origin);
            } else if (slope>0)
            {
                laser_corner1.Draw(spriteBatch, position - new Vector2(24, 24), origin);
                laser_corner2.Draw(spriteBatch, position2 - new Vector2(24, 24), origin);

                spriteBatch.Draw(texture, new Rectangle((int)position.X + 6, (int)position.Y - 6, (int)radius, 24), new Rectangle(0, 0, 1, 1), new Color(color, 50), angle, new Vector2(0, 0), SpriteEffects.None, 1);
                spriteBatch.Draw(texture, new Rectangle((int)position.X + 4, (int)position.Y - 4, (int)radius, 15), new Rectangle(0, 0, 1, 1), new Color(color, 50), angle, new Vector2(0, 0), SpriteEffects.None, 1);
                spriteBatch.Draw(texture, new Rectangle((int)position.X + 2, (int)position.Y - 2, (int)radius, 8), new Rectangle(0, 0, 1, 1), new Color(color, 50), angle, new Vector2(0, 0), SpriteEffects.None, 1);
                spriteBatch.Draw(texture, new Rectangle((int)position.X + 1, (int)position.Y - 1, (int)radius, 5), new Rectangle(0, 0, 1, 1), new Color(color, 50), angle, new Vector2(0, 0), SpriteEffects.None, 1);
                spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)radius, 4), new Rectangle(0, 0, 1, 1), new Color(color, 200), angle, new Vector2(0, 0), SpriteEffects.None, 1);

                laser_corner_small1.Draw(spriteBatch, position - new Vector2(12, 12), origin);
                laser_corner_small2.Draw(spriteBatch, position2 - new Vector2(12, 12), origin);
            }
            //spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)radius, 2), new Rectangle(0, 0, 1, 1), color, angle, new Vector2(0, 0), SpriteEffects.None, 1);
        }
    }
    public void Sound()
    {
        if (Off == 1)
        {
            Lazer_Off.Play();
            Off = 0;
        }
        if (Alert == 1)
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

    public override void Reset()
    {
        base.Reset();
        Active = true;
    }
}
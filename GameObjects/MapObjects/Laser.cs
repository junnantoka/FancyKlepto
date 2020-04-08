using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
class Laser : SpriteGameObject
{
    SoundEffect Lazer_Off, Lazer_Alert, Lazer_Col, Lazer_Col_Alarm, Lazer_Spark;
    public int Off, Alert, Col, Col_Alarm, Spark;
    private bool Active;
    private float angle;
    private float radius;
    private readonly Texture2D texture;

    public Vector2 position2;
    Color color;
    public Laser(Vector2 position, Vector2 position2, Color color) : base("lazer")
    {
        Lazer_Off = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Lazer_Off");
        Lazer_Alert = GameEnvironment.AssetManager.Content.Load<SoundEffect>("alert");
        Lazer_Col = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Lazer_Col");
        Lazer_Col_Alarm = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Lazer_Col_Alarm");
        Lazer_Spark = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Lazer_Spark");
        this.color = color;
        this.position = new Vector2(18 + position.X * (unitSize + unitSpacing), 10 + position.Y * (unitSize + unitSpacing));
        this.position2 = new Vector2(18 + position2.X * (unitSize + unitSpacing), 10 + position2.Y * (unitSize + unitSpacing));

        texture = GameEnvironment.AssetManager.GetSprite("lazer");
        Active = true;
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
            if (Spark == 0)
            {
                Lazer_Spark.Play();
                Spark = GameEnvironment.Random.Next(360,480);
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
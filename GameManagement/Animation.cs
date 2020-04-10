using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Animation : SpriteSheet
{
    protected float frameTime;
    protected bool isLooping;
    protected float time;

    public Animation(string assetname, bool isLooping, float frameTime = 0.1f) : base(assetname)
    {
        this.frameTime = frameTime;
        this.isLooping = isLooping;
    }

    public void Play()
    {
        sheetIndex = 0;
        time = 0.0f;
    }

    public void Update(GameTime gameTime)
    {
        time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        while (time > frameTime)
        {
            time -= frameTime;
            if (isLooping)
            {
                sheetIndex = (sheetIndex + 1) % NumberSheetElements;
            }
            else
            {
                sheetIndex = Math.Min(sheetIndex + 1, NumberSheetElements - 1);
            }
        }
    }

    public float FrameTime
    {
        get { return frameTime; }
    }

    public bool IsLooping
    {
        get { return isLooping; }
    }

    public int CountFrames
    {
        get { return NumberSheetElements; }
    }

    public bool AnimationEnded
    {
        get { return !isLooping && sheetIndex >= NumberSheetElements - 1; }
    }
    public int CurrentFrame { get; set; }
    public int FrameCount { get; private set; }
    public int FrameHeight { get; { return Texture.Height; } }
    public int FrameSpeed { get; set; }
    public int FrameWidth { get; { return Texture.Width / FrameCount; } }
    public bool Islooping { get; set; }
    public Texture2D Texture { get; private set; }
    public Animation (Texture2D texture, int frameCount)
    {
        Texture = texture;

        FrameCount = frameCount;

        IsLooping = true;

        FrameSpeed = 0.2f;
    }
}


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FancyKlepto.GameStates;
using FancyKlepto.GameObjects;
using FancyKlepto;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace FancyKlepto
{
    class Fancy_Klepto : GameEnvironment
    {
        //SoundEffect
        SoundEffect bgMusic;
        SoundEffectInstance soundEffectInstance;
        protected override void LoadContent()
        {
            base.LoadContent(); //Call to base method
            bgMusic = AssetManager.Content.Load<SoundEffect>("Sound/Background_music"); //Initialize file
            soundEffectInstance = bgMusic.CreateInstance(); //Create instance
            soundEffectInstance.IsLooped = true; //Loop the song
            soundEffectInstance.Play(); //Play
            screen = new Point(1920, 1080); //Screen resolution
            ApplyResolutionSettings(); //Apply settings
            
            FullScreen = false;
            
            //Game States
            GameStateManager.AddGameState("StartState", new StartState());
            GameStateManager.AddGameState("Level1", new Level1());
            GameStateManager.AddGameState("Level2", new Level2());
            GameStateManager.AddGameState("Level3", new Level3());
            GameStateManager.AddGameState("Level4", new Level4());
            GameStateManager.AddGameState("Level5", new Level5());
            GameStateManager.AddGameState("EndStateWon", new EndStateWon());
            GameStateManager.AddGameState("EndStateLost", new EndStateLost());
            GameStateManager.AddGameState("ExplanationState", new ExplanationState());
            GameStateManager.AddGameState("ExplanationStateTwo", new ExplanationStateTwo());
            //Switch to another state
            GameStateManager.SwitchTo("StartState");
        }
    }
}

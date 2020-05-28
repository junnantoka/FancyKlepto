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
        SoundEffect bgMusic;
        SoundEffectInstance soundEffectInstance;
        protected override void LoadContent()
        {
            base.LoadContent();
            bgMusic = AssetManager.Content.Load<SoundEffect>("Sound/Background_music");
            soundEffectInstance = bgMusic.CreateInstance();
            soundEffectInstance.IsLooped = true;
            soundEffectInstance.Play();
            screen = new Point(1920, 1080);
            ApplyResolutionSettings();
            
            FullScreen = true;
            
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


            GameStateManager.SwitchTo("StartState");
        }
    }
}

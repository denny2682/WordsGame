using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using GameWords.Game;
using GameWords.Game.Utility;


namespace GameWords.Game
{
   public class GameManager
   {
      #region variables private

      private Level level;
      private CreateLevel director;
      private int currentLevel;
      private List<ISprite> sprites; 
      private bool endgame;
      private List<LevelSettings> settingsLevel;

      

      #endregion

      /// <summary>
      /// Start the "builder" pattern with the first level
      /// </summary>
      public GameManager(List<LevelSettings> settings)
      {
         endgame = false;

         currentLevel = -1;

         // Pattern Builder: Create Director
         director = new CreateLevel();

         settingsLevel = settings;

         // Istance a new Level 
         level = new Level(this);

         // Pattern Builder: set builder to director
         director.Builder = level;

         // Create a new level
         createLevel();
      }

      #region public variables
      public event EventHandler MouseOnPress;
      public event EventHandler MouseOnRelease;
      #endregion

      /// <summary>
      /// This method update the current level and the score.
      /// </summary>
      /// <param name="gameTime"></param>
      public void Update(GameTime gameTime)
      {
         if (level != null && level.Score >= getLevelSettings().MinScore && !endgame)
            getNextLevel();
      }

      /// <summary>
      /// It Returns sprites of the level 
      /// </summary>
      /// <returns></returns>
      public List<ISprite> GetCurrentSprite()
      {
         return sprites;
      }

      /// <summary>
      /// This method reproduces the sound effect
      /// </summary>
      public void NotifyCorrectWord()
      {
         try
         {
            MediaPlayer.IsRepeating = true;
            SoundEffect sound = WordsGame.Content.Load<SoundEffect>("Sounds/soundNewWord");
            sound.Play();
         }
         catch (Exception ex)
         {
            // it may happen that the sound does not work in MediaPlayer, this should not block the execution of the program
         }
      }


      #region Methods for mouse events
      public void MouseIsLeftDown()
      {
         if (MouseOnPress != null)
            MouseOnPress(this, new EventArgs());
      }

      public void MouseIsLeftRelease()
      {
         level.UpdateScore();
         if (MouseOnRelease != null)
         {
            MouseOnRelease(this, new EventArgs());
         }

      }
      #endregion


      #region Private methods for the level
      private LevelSettings getLevelSettings()
      {
         LevelSettings settings = null;

         if (settingsLevel?.FirstOrDefault() == null)
            throw new InvalidDataException(" Levels Setting is not found");
         else
         {
            currentLevel = (currentLevel == -1) ? settingsLevel.FirstOrDefault().LevelNumber : currentLevel;
            settings = settingsLevel?.FirstOrDefault(x => x.LevelNumber == currentLevel);

            if (settings == null)
               throw new InvalidDataException("Level Setting is not found for level number: " + currentLevel);
         }

         return settings;
      }

      /// <summary>
      /// Create a new level 
      /// </summary>
      private void createLevel()
      {
         try
         {
            // Detach all events that were associated
            detachEvents();

            // it creates the level starting from the current one
            LevelSettings settings = getLevelSettings();

            if (settings != null)
            {
               sprites = director.BuildLevel(settings);

            }
         }
         catch (Exception ex)
         {
            Console.WriteLine("Create Level is failed!");
         }
      }

      

      /// <summary>
      /// if the level has been passed, it goes to the next one, 
      /// otherwise it reloads the current one
      /// </summary>
      private void getNextLevel()
      {
         // If it is not the last level go to the next
         if (currentLevel < settingsLevel.Count())
         {
            // Increase the level number
            currentLevel++;

            // Create new level
            createLevel();
         }
         else
         {
            // if it is last level and the game is finished then 
            // it call the director for add sprite image of end game
            if (currentLevel == settingsLevel.Count && !endgame)
            {
               // detach the observers
               endgame = true;
               detachEvents();
               sprites = director.addWin();
            }

         }
      }

      /// <summary>
      /// This method reloads the current level
      /// </summary>
      public void ReloadLevel()
      {
         // this creates a new level
         createLevel();
      }

      /// <summary>
      /// This method reloads the current level
      /// </summary>
      public void ReloadGame()
      {
         try
         {
            endgame = false;
            currentLevel = settingsLevel.FirstOrDefault().LevelNumber;
            createLevel();
         }
         catch (Exception ex)
         {
            Console.WriteLine("Reload game is not execute: " + ex.Message);
         }
      }
      #endregion

      #region private Method for events 
      // Detach the events
      private void detachEvents()
      {
         try
         {
            // removes the methods that were registered in the MouseOnPress event
            if (MouseOnPress?.GetInvocationList() != null)
            {
               // Detach di tutti gli osservatori
               foreach (var item in MouseOnPress.GetInvocationList())
               {
                  var type = (ISprite)item.Target;
                  MouseOnPress -= (EventHandler)item;
               }
            }

            //removes the methods that were registered in the MouseOnPress event
            if (MouseOnRelease?.GetInvocationList() != null)
            {
               // Detach di tutti gli osservatori
               foreach (var item in MouseOnRelease.GetInvocationList())
               {
                  var type = (ISprite)item.Target;
                  if ((type.GetTypeSprite() != TypeSprite.BtnReloadGame) ||
                      (type.GetTypeSprite() == TypeSprite.BtnReloadGame && !endgame))
                  {
                     MouseOnRelease -= (EventHandler)item;
                  }
               }
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine("Non è riuscito a rimuovere gli eventi a causa di un errore imprevisto: " + ex.Message);

         }
      }
      #endregion

   }
}

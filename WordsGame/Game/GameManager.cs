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
using WordsGame.Game;
using WordsGame.Game.Utility;
using System.Diagnostics;

namespace WordsGame.Game
{
   /// <summary>
   /// Game Manager handles the interaction with the level and the sprites
   /// </summary>
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
      /// Create new level and start the game
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
      // Events call sprite-specific methods
      public event EventHandler MouseOnPress;
      public event EventHandler MouseOnRelease;
      #endregion

      /// <summary>
      /// This method update the current level and the score.
      /// </summary>
      /// <param name="gameTime">Xna GameTime</param>
      public void Update(GameTime gameTime)
      {
         if (level != null && level.Score >= getLevelSettings().MinScore && !endgame)
            getNextLevel();
      }

      /// <summary>
      /// It Returns sprites of the level 
      /// </summary>
      /// <returns>sprites</returns>
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
      /// <summary>
      /// Intercept the click of the mouse
      /// </summary>
      public void MouseIsLeftDown()
      {
         if (MouseOnPress != null)
            MouseOnPress(this, new EventArgs());
      }
      /// <summary>
      /// Intercept the release of the mouse
      /// </summary>
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
      /// <summary>
      /// Get the current level in Game
      /// </summary>
      /// <returns></returns>
      private LevelSettings getLevelSettings()
      {
         LevelSettings settings = null;

         if (settingsLevel?.FirstOrDefault() == null)
            throw new Exception(" Levels Setting is not found");
         else
         {
            currentLevel = (currentLevel == -1) ? settingsLevel.FirstOrDefault().LevelNumber : currentLevel;
            settings = settingsLevel?.FirstOrDefault(x => x.LevelNumber == currentLevel);

            if (settings == null)
               throw new Exception("Level Setting is not found for level number: " + currentLevel);
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
               sprites = director.BuildLevel(settings);
         }
         catch (Exception ex)
         {
            Debug.WriteLine("Create Level is failed!");
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
            // if it is the last level and the game is finished then 
            // it call the director for add sprite image of end game
            if (currentLevel == settingsLevel.Count && !endgame)
            {
               // detach all the sprite 
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
      /// This method reloads the game
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
            Debug.WriteLine("Game reload fails due to unexpected error " + ex.Message);
         }
      }
      #endregion

      #region private Method for events 
      /// <summary>
      /// Detatch all the events
      /// </summary>
      private void detachEvents()
      {
         try
         {
            // Removes the methods that were registered in the MouseOnPress event
            if (MouseOnPress?.GetInvocationList() != null)
            {
               //  Detach all the sprites subscribed to the event 
               foreach (var item in MouseOnPress.GetInvocationList())
               {
                  var type = (ISprite)item.Target;
                  MouseOnPress -= (EventHandler)item;
               }
            }

            // Removes the methods that were registered in the MouseOnPress event
            if (MouseOnRelease?.GetInvocationList() != null)
            {
               //  Detach all the sprites subscribed to the event 
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
            Debug.WriteLine("It was unable to remove events due to an unexpected error: " + ex.Message);
         }
      }
      #endregion

   }
}

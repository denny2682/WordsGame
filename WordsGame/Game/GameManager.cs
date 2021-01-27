﻿using System;
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

      // levels
      private Level level;
      private CreateLevel director;
      private int currentLevel;
      private List<LevelSettings> settingsLevel;
      private List<ISprite> sprites;

      // Presentation area
      private int widthPresentationArea;
      private int heightPresentationArea;

      // Finale state of the game
      private bool endgame;

      #endregion

      /// <summary>
      /// Create new level and start the game
      /// </summary>
      public GameManager(List<LevelSettings> settings, int widthScreen, int heightScreen)
      {
         endgame = false;

         currentLevel = -1;

         // Pattern Builder: Create Director
         director = new CreateLevel(widthScreen, heightScreen);

         widthPresentationArea = widthScreen;
         heightPresentationArea = heightScreen;

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
      public event EventHandler MouseOnPressed;
      public event EventHandler MouseOnReleased;
      public event EventHandler MouseOnOver;
      
      #endregion

      #region public methods

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


      #region public methods for event calls

      /// <summary>
      /// Intercept the click of the mouse
      /// </summary>
      public void MouseIsLeftDown()
      {
         if (MouseOnPressed != null)
            MouseOnPressed(this, new EventArgs());
      }

      /// <summary>
      /// Intercept the release of the mouse
      /// </summary>
      public void MouseIsLeftRelease()
      {
         // Updates the Score
         level.UpdateScore();
         if (MouseOnReleased != null)
            MouseOnReleased(this, new EventArgs());
      }

      /// <summary>
      /// intercepts the mouse hover
      /// </summary>
      public void MouseIsOver()
      {
         if (MouseOnOver != null)
            MouseOnOver(this, new EventArgs());
      }

      #endregion public methods for event calls

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
            throw new Exception("Game reload fails due to unexpected error " + ex.Message);
         }
      }


      #endregion public methods


      #region Private methods for the level

      /// <summary>
      /// Get the current level in Game
      /// </summary>
      /// <returns>settings</returns>
      private LevelSettings getLevelSettings()
      {
         LevelSettings settings = null;

         if (settingsLevel?.FirstOrDefault() == null)
            throw new Exception(" The Levels Setting was not founded");
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
            throw new Exception("Create Level is failed!");
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
            if (MouseOnPressed?.GetInvocationList() != null)
            {
               //  Detach all the sprites subscribed to the event 
               foreach (var item in MouseOnPressed.GetInvocationList())
               {
                  var type = (ISprite)item.Target;
                  MouseOnPressed -= (EventHandler)item;
               }
            }

            // Removes the methods that were registered in the MouseOnPress event
            if (MouseOnReleased?.GetInvocationList() != null)
            {
               //  Detach all the sprites subscribed to the event 
               foreach (var item in MouseOnReleased.GetInvocationList())
               {
                  var type = (ISprite)item.Target;
                  if ((type.GetTypeSprite() != TypeSprite.BtnReloadGame) ||
                      (type.GetTypeSprite() == TypeSprite.BtnReloadGame && !endgame))
                  {
                     MouseOnReleased -= (EventHandler)item;
                  }
               }
            }

            // Removes the methods that were registered in the MouseOnOver event
            if (MouseOnOver?.GetInvocationList() != null)
            {
               //  Detach all the sprites subscribed to the event 
               foreach (var item in MouseOnOver.GetInvocationList())
               {
                  var type = (ISprite)item.Target;
                  if ((type.GetTypeSprite() != TypeSprite.BtnReloadGame) ||
                      (type.GetTypeSprite() == TypeSprite.BtnReloadGame && !endgame))
                  {
                     MouseOnOver -= (EventHandler)item;
                  }
               }
            }
         }
         catch (Exception ex)
         {
            // This exception is not blocking
            Debug.WriteLine("It was unable to remove events due to an unexpected error: " + ex.Message);
         }
      }
      
      #endregion

   }
}

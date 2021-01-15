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
using GameWords.Game.Utility;

namespace GameWords.Game
{
   public class GameManager : Subject
   {
      #region variables private

      private Level level;
      private CreateLevel director;
      private int currentLevel = 0;
      private List<ISprite> sprites;
      private bool endgame = false;
      private List<LevelSettings> settingsLevel;

      #endregion

      /// <summary>
      /// Start the "builder" pattern with the first level
      /// </summary>
      public GameManager(List<LevelSettings> settings)
      {
         // Pattern Builder: Create Director
         director = new CreateLevel();

         settingsLevel = settings;

         // Set Level with first level
         currentLevel = settingsLevel.FirstOrDefault().LevelNumber;

         // Istance a new Level 
         level = new Level(this);

         // Pattern Builder: set builder to director
         director.Builder = level;

         // Create a new level
         createLevel();
      }


      /// <summary>
      /// This method update the current level, the time and the score.
      /// </summary>
      /// <param name="gameTime"></param>
      public void Update(GameTime gameTime)
      {
            if (level != null && level.Score >= getLevelSettings().MinScore)
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
         NotifySelected();
      }

      public void MouseIsLeftRelease()
      {
         level.UpdateScore();
         NotifyUnSelected();
      }
      #endregion


      #region Private methods
      private LevelSettings getLevelSettings()
      {
         LevelSettings setting = settingsLevel?.FirstOrDefault(x => x.LevelNumber == currentLevel);
         if (setting == null)
            throw new InvalidDataException("Setting Level not found for level number:" + currentLevel);

         return setting;
      }

      /// <summary>
      /// Create a new level 
      /// </summary>
      private void createLevel()
      {
         try
         {
            detachObservers();

            // it creates the level starting from the current one
            LevelSettings settings = getLevelSettings();
            if (settings != null)
               sprites = director.BuildLevel(settings);
         }
         catch (Exception ex)
         {
            Console.WriteLine("Create Level is failed!");
         }
      }


      private void detachObservers()
      {
         // Detach di tutti gli osservatori
         foreach (var item in level.GetSprites())
         {
            switch (item.GetTypeSprite())
            {
               case TypeSprite.Letter:
                  ((LetterTile)item).DetachLetter();
                  break;
               case TypeSprite.Image:
                  break;
               case TypeSprite.Text:
                  break;
               case TypeSprite.ButtonReload:
                  ((ButtonReload)item).DetachButtonReaload();
                  break;
               case TypeSprite.Score:
                  break;

            }
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
            // se è l'ultimo livello ha vinto
            if (currentLevel == settingsLevel.Count && !endgame)
            {
               // detach the observers
               detachObservers();
               sprites = director.addWin();
               endgame = true;
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

      #endregion

   }
}

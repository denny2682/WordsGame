using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WordsGame.Game.Utility;

namespace WordsGame.Game
{
   /// <summary>
   /// Pattern Builder 
   /// Role: Director
   /// It creates the level 
   /// </summary>
   class CreateLevel
   {
      #region private variables

      // Presentation Area
      int widthAreaPresentation;
      int heightAreaPresentation;

      // Level builder
      private ILevelBuilder builder;

      #endregion

      /// <summary>
      /// Costructor
      /// </summary>
      /// <param name="widthArea"></param>
      /// <param name="heightArea"></param>
      public CreateLevel(int widthArea, int heightArea)
      {
         widthAreaPresentation = widthArea;
         heightAreaPresentation = heightArea;
      }


      #region public variables

      // Sets builder
      public ILevelBuilder Builder {
         set { builder = value; }
      }

      #endregion

      #region public methods

      /// <summary>
      /// Builds Level 
      /// it creates a grid
      /// </summary>
      /// <param name="settings">Level Setting </param>
      /// <returns>sprites</returns>
      public List<ISprite> BuildLevel(LevelSettings settings)
      {
         if (settings == null)
            throw new InvalidDataException("The level settings were not found");

         // Resets Level 
         builder.Reset();

         // Builds Background image
         builder.buildBackground(0,0, new ColorRGB(255, 255, 255, 255));

         // Builds the Title 
         builder.buildTitle(widthAreaPresentation/2-150, 10, "LEVEL " + settings.LevelNumber, new ColorRGB(255, 255, 255, 255));

         // Builds the button to reload the level
         builder.buildReloadLevelBtn(widthAreaPresentation-200, 10, new ColorRGB(255, 255, 255, 255));

         // Builds the button to reload the game
         builder.buildReloadGameBtn(widthAreaPresentation-200, 100, new ColorRGB(255, 255, 255, 255));

         // Builds the letters grid and description 
         builder.buildGridAndDescription(
            settings.Rows, 
            settings.Columns, 
            widthAreaPresentation/2, 150, 
            new List<string>() {
               "Completa il livello ottendo almeno " + settings.MinScore + " punti.",
               "Componi parole di almeno 4 lettere.",
               "Se vuoi uscire dal gioco premi ESC"
         }, 
            new ColorRGB(255, 255, 255, 255), 
            TypeFont.Arial);

         // Builds the level score
         builder.buildScore(TypeFont.Arial, 100, 30, new ColorRGB(231, 125, 55, 255));

         // Returns the objects of type sprite
         return builder.GetSprites();
      }

      /// <summary>
      /// Method to add a new sprite with the winner's graphic
      /// </summary>
      /// <returns></returns>
      public List<ISprite> addWin()
      {
         if (builder != null)
            builder.buildWinner(widthAreaPresentation/2-200, 400, new ColorRGB(255, 255, 255, 255));

         return builder.GetSprites();
      }
      
      #endregion
   }
}

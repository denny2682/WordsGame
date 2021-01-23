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
      int widthAreaPresentation;
      int heightAreaPresentation;

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
      
      private ILevelBuilder builder;

      // Set builder
      public ILevelBuilder Builder {
         set { builder = value; }
      }

      /// <summary>
      /// Build Level 
      /// it creates a grid
      /// </summary>
      /// <param name="settings">Level Setting </param>
      /// <returns>sprites</returns>
      public List<ISprite> BuildLevel(LevelSettings settings)
      {
         if (settings == null)
            throw new InvalidDataException("The level settings were not found");

         // Reset Level 
         builder.Reset();
         // Title level
         builder.buildBackground(0,0, new ColorRGB(255, 255, 255, 255));

         builder.buildTitle(widthAreaPresentation/2-150, 10, "LEVEL " + settings.LevelNumber, new ColorRGB(255, 255, 255, 255));
        
         builder.buildReloadLevelBtn(widthAreaPresentation-200, 10, new ColorRGB(255, 255, 255, 255));
         
         builder.buildReloadGameBtn(widthAreaPresentation-200, 100, new ColorRGB(255, 255, 255, 255));

         // Centered letter grid
         builder.buildGridAndDescription(
            settings.Rows, 
            settings.Columns, 
            widthAreaPresentation/2, 170, 
            new List<string>() {
               "Completa il livello ottendo almeno " + settings.MinScore + " punti",
               "Non sono ammesse parole di 2 e 3 lettere"
         }, 
            new ColorRGB(255, 255, 255, 255), 
            TypeFont.Arial);

         // Level score
         builder.buildScore(TypeFont.Arial, 100, 30, new ColorRGB(231, 125, 55, 255));

         // Return the objects of type sprite
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
   }
}

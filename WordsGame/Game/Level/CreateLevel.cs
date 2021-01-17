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
      private ILevelBuilder builder;

      // Set builder
      public ILevelBuilder Builder {
         set { builder = value; }
      }


      /// <summary>
      /// Build Level 
      /// it creates a <paramref name="row"/>x<paramref name="coloumn"/> grid
      /// </summary>
      /// <param name="minScore"></param>
      /// <param name="row"></param>
      /// <param name="coloumn"></param>
      /// <returns></returns>
      public List<ISprite> BuildLevel(LevelSettings settings)
      {
         if (settings == null)
            throw new InvalidDataException("Settings Level not found");

         // Reset Level 
         builder.Reset();

         // Title level
         builder.buildText(TypeFont.Arial, "* LIVELLO " + settings.LevelNumber + " * ", 400, 30, new ColorRGB(255, 255, 255, 255));
         builder.buildBtnReloadLevel(680, 10, new ColorRGB(255, 255, 255, 255));
         
         builder.buildBtnReloadGame(770, 10, new ColorRGB(255, 255, 255, 255));

         // Level Description
         builder.buildText(TypeFont.Arial, "Completa il livello ottendo almeno " + settings.MinScore + " punti.", 100, 90, new ColorRGB(255, 255, 255, 255));
         builder.buildText(TypeFont.Arial, "Non sono ammesse parole di 2 e 3 lettere", 100, 120, new ColorRGB(255, 255, 255, 255));

         // Letters grid 
         builder.buildGrid(settings.Row, settings.Coloumn, 90, 200);

         // Level score
         builder.buildText(TypeFont.Arial, "Punteggio:", 100, 30, new ColorRGB(255, 255, 255, 255));
         builder.buildScore(TypeFont.Arial, 250, 30, new ColorRGB(255, 255, 255, 255));

         // Return the objects of type sprite
         return builder.GetSprites();
      }

      public List<ISprite> addWin()
      {
         if (builder != null)
            builder.buildWinner(250, 400, new ColorRGB(255, 255, 255, 255));

         return builder.GetSprites();
      }
   }
}

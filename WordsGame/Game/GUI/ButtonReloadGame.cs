﻿using System;
using System.Collections.Generic;
using System.Text;
using WordsGame.Game.Utility;

namespace WordsGame.Game
{
   
   /// <summary>
   /// Button new game
   /// </summary>
   class ButtonReloadGame : ImageProxy
   {
      private GameManager gameManager;
      private readonly ColorRGB colorSelected = new ColorRGB(161, 096, 255, 255);

      /// <summary>
      /// Costruttore di una nuova tessera lettera
      /// </summary>
      public ButtonReloadGame(string fileName, int x, int y, ColorRGB colorRGB, GameManager gameController) : base(fileName, x, y, colorRGB)
      {
         gameManager = gameController;

         // Bind the method to events
         gameManager.MouseOnRelease += this.Onclick;
      }

      /// <summary>
      /// returns the sprite type
      /// </summary>
      /// <returns></returns>
      public override TypeSprite GetTypeSprite()
      {
         return TypeSprite.BtnReloadGame;
      }

      // Method called on button click
      public void Onclick(object sender, EventArgs e)
      {
         if (IsSelectedArea())
            gameManager.ReloadGame();
      }
   }
}
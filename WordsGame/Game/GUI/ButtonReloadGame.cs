using System;
using System.Collections.Generic;
using System.Text;
using WordsGame.Game.Utility;

namespace WordsGame.Game
{
   // Todo: alla fine potrebbe essere necessario effettuare le seguenti modifiche
   // LetterTile potrebbe ereditare da image e text invece già lo è
   // LetterTile dunque estende image
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
         // add event
         gameManager.MouseOnRelease += this.Onclick;
      }

      public override TypeSprite GetTypeSprite()
      {
         return TypeSprite.BtnReloadGame;
      }

      // rilascio del click
      public void Onclick(object sender, EventArgs e)
      {
         if (IsSelectedArea())
            gameManager.ReloadGame();
      }
   }
}

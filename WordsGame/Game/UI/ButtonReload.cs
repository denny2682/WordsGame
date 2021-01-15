using System;
using System.Collections.Generic;
using System.Text;
using GameWords.Game.Utility;


namespace GameWords.Game
{
   // Todo: alla fine potrebbe essere necessario effettuare le seguenti modifiche
   // LetterTile potrebbe ereditare da image e text invece già lo è
   // LetterTile dunque estende image
   class ButtonReload : ImageProxy, IObserver
   {
      private GameManager gameManager;
      private readonly ColorRGB colorSelected = new ColorRGB(161, 096, 255, 255);

      /// <summary>
      /// Costruttore di una nuova tessera lettera
      /// </summary>
      public ButtonReload(string fileName, int x, int y, ColorRGB colorRGB, GameManager gameController) : base(fileName, x, y, colorRGB)
      {
         gameManager = gameController;
         gameManager.Attach(this);
      }

      public override TypeSprite GetTypeSprite()
      {
         return TypeSprite.ButtonReload;
      }

      public void DetachButtonReaload()
      {
         gameManager.Detach(this);
      }

      public void Selected()
      {
         
      }

      // rilascio del click
      public void UnSelected()
      {
         if (IsSelectedArea())
            gameManager.ReloadLevel();
      }
   }
}

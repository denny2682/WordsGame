using System;
using System.Collections.Generic;
using System.Text;
using WordsGame.Game.Utility;


namespace WordsGame.Game
{
   /// <summary>
   /// Button 
   /// </summary>
   class ButtonReloadLevel : ImageProxy
   {
      #region private variables
      private GameManager gameManager;
      private readonly ColorRGB colorSelected = new ColorRGB(161, 096, 255, 255);
      #endregion

      /// <summary>
      /// Costruttore di una nuova tessera lettera
      /// </summary>
      public ButtonReloadLevel(string fileName, int x, int y, ColorRGB colorRGB, GameManager gameController) : base(fileName, x, y, colorRGB)
      {
         gameManager = gameController;

         // Bind the method to events
         gameManager.MouseOnRelease += this.Onclick;
      }

      /// <summary>
      /// Returns the sprite type
      /// </summary>
      /// <returns></returns>
      public override TypeSprite GetTypeSprite()
      {
         return TypeSprite.BtnReloadLevel;
      }

     
      // Onclick
      public virtual void Onclick(object sender, EventArgs e)
      {
         if (IsSelectedArea())
            gameManager.ReloadLevel();
      }
   }
}

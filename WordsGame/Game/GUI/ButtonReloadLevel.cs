using System;
using System.Collections.Generic;
using System.Text;
using WordsGame.Game.Utility;


namespace WordsGame.Game
{
   /// <summary>
   /// Button to reload the level
   /// </summary>
   class ButtonReloadLevel : ImageProxy
   {
      #region private variables
      private GameManager gameManager;
      private readonly ColorRGB colorSelected = new ColorRGB(161, 096, 255, 255);
      #endregion

      /// <summary>
      /// Costructor
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


      /// <summary>
      /// This method is called on button click
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      public virtual void Onclick(object sender, EventArgs e)
      {
         if (IsSelectedArea())
            gameManager.ReloadLevel();
      }
   }
}

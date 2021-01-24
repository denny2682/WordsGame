using System;
using System.Collections.Generic;
using System.Text;
using WordsGame.Game.Utility;

namespace WordsGame.Game
{

   /// <summary>
   /// Game reload button
   /// </summary>
   class GameReloadButton : Button
   {
      #region private variables

      private readonly ColorRGB colorSelected = new ColorRGB(161, 096, 255, 255);

      #endregion

      /// <summary>
      /// Costructor
      /// </summary>
      /// <param name="settings"></param>
      /// <param name="game"></param>
      public GameReloadButton(GraphicImageInfo settings, GameManager game) : base(settings, game) { }
      
      #region public methods

      /// <summary>
      /// returns the sprite type
      /// </summary>
      /// <returns></returns>
      public override TypeSprite GetTypeSprite()
      {
         return TypeSprite.BtnReloadGame;
      }

      #endregion


      #region protected methods
      
      /// <summary>
      /// This method is called on button click
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      protected override void Onclick(object sender, EventArgs e)
      {
         if (this.IsSelectedArea())
            gameManager.ReloadGame();
      }

      /// <summary>
      /// This method is called on mouse is over
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      protected override void OnMouseOver(object sender, EventArgs e)
      {
         if (this.IsSelectedArea())
            highlight = true;
         else
            highlight = false;
      }

      #endregion
   }
}

using System;
using System.Collections.Generic;
using System.Text;
using WordsGame.Game.Utility;

namespace WordsGame.Game
{
   
   /// <summary>
   /// Button new game
   /// </summary>
   class ButtonReloadGame : Button
   {
      private readonly ColorRGB colorSelected = new ColorRGB(161, 096, 255, 255);

      /// <summary>
      /// Costructur
      /// </summary>
      public ButtonReloadGame(GraphicImageInfo settings, GameManager game) : base(settings, game)
      {
      }
      

      /// <summary>
      /// returns the sprite type
      /// </summary>
      /// <returns></returns>
      public override TypeSprite GetTypeSprite()
      {
         return TypeSprite.BtnReloadGame;
      }

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

      protected override void OnMouseOver(object sender, EventArgs e)
      {
         if (this.IsSelectedArea())
            highlight = true;
         else
            highlight = false;
      }
   }
}

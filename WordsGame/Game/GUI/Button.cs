using System;
using System.Collections.Generic;
using System.Text;
using WordsGame.Game.Utility;

namespace WordsGame.Game
{
   /// <summary>
   /// General Button Abastract
   /// </summary>
   abstract class Button : ImageProxy
   {
      protected GameManager gameManager;
      public Button(GraphicImageInfo settings, GameManager game):base(settings)
      {
        gameManager = game;
         addEventOnClick(game);
         addEventOnMouseOver(game);
      }

      protected virtual void  Onclick(object sender, EventArgs e) { }
      protected virtual void OnMouseOver(object sender, EventArgs e) { }
      private void addEventOnClick(GameManager game)
      {
         game.MouseOnReleased += this.Onclick;
      }

      private void addEventOnMouseOver(GameManager game)
      {
         game.MouseOnOver += this.OnMouseOver;
      }

      private void RemoveEventOnClick(GameManager game)
      {
         game.MouseOnReleased -= this.Onclick;
      }

      private void RemoveEventOnMouseOver(GameManager game)
      {
         game.MouseOnReleased -= this.Onclick;
      }
   }
}

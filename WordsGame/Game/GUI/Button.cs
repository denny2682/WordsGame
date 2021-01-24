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
      #region protected variables

      protected GameManager gameManager;

      #endregion

      public Button(GraphicImageInfo settings, GameManager game) : base(settings)
      {
         gameManager = game;
         addEventOnClick(game);
         addEventOnMouseOver(game);
      }

      #region protected methods 

      protected virtual void Onclick(object sender, EventArgs e) { }
      protected virtual void OnMouseOver(object sender, EventArgs e) { }

      protected void addEventOnClick(GameManager game)
      {
         game.MouseOnReleased += this.Onclick;
      }

      protected void addEventOnMouseOver(GameManager game)
      {
         game.MouseOnOver += this.OnMouseOver;
      }
      
      protected void RemoveEventOnClick(GameManager game)
      {
         game.MouseOnReleased -= this.Onclick;
      }

      protected void RemoveEventOnMouseOver(GameManager game)
      {
         game.MouseOnReleased -= this.Onclick;
      }

      #endregion
   }
}

using System;
using System.Collections.Generic;
using System.Text;
using WordsGame.Game.Utility;

namespace WordsGame.Game
{
   /// <summary>
   ///Button - Abstract class
   /// </summary>
   abstract class Button : ImageProxy
   {
      #region protected variables

      protected GameManager gameManager;

      #endregion

      public Button(GraphicImageInfo settings, GameManager game) : base(settings)
      {
         gameManager = game;
         AddEventOnClick(game);
         AddEventOnMouseOver(game);
      }

      #region protected methods 

      /// <summary>
      /// This method 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      protected virtual void Onclick(object sender, EventArgs e) { }
      
      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      protected virtual void OnMouseOver(object sender, EventArgs e) { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="game"></param>
      protected void AddEventOnClick(GameManager game)
      {
         game.MouseOnReleased += this.Onclick;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="game"></param>
      protected void AddEventOnMouseOver(GameManager game)
      {
         game.MouseOnOver += this.OnMouseOver;
      }
      
      /// <summary>
      /// 
      /// </summary>
      /// <param name="game"></param>
      protected void RemoveEventOnClick(GameManager game)
      {
         game.MouseOnReleased -= this.Onclick;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="game"></param>
      protected void RemoveEventOnMouseOver(GameManager game)
      {
         game.MouseOnReleased -= this.Onclick;
      }

      #endregion
   }
}

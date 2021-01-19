using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using WordsGame.Game.Utility;

namespace WordsGame.Game
{
   /// <summary>
   /// Pattern Proxy: role real subject
   /// </summary>
   class Image
   {
      #region private variables
      // Xna texture2D
      private Texture2D texture;
      private int posX;
      private int posY;
      #endregion region

      #region public variables
      public ColorRGB Color{ get; set; }
      #endregion

      /// <summary>
      /// Costructor
      /// </summary>
      /// <param name="fileName"></param>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="colorRGB"></param>
      public Image(string fileName, int x, int y, ColorRGB colorRGB)
      {
         posX = x; posY = y;
         texture = WordsGame.Content.Load<Texture2D>(fileName);
         Color = colorRGB;
      }

      /// <summary>
      /// get position x 
      /// </summary>
      public int PosX { 
         get { return posX;  } 
      }

      /// <summary>
      /// Get position y
      /// </summary>
      public int PosY
      {
         get { return posY; }
      }

      /// <summary>
      /// Get size real
      /// </summary>
      /// <returns></returns>
      public Size GetExtent()
      {
         return new Size(texture.Width, texture.Height);
      }

      /// <summary>
      ///  Draws in the spritebatch
      /// </summary>
      /// <param name="spriteBatch"></param>
      public void Draw(ref SpriteBatch spriteBatch)
      {
         spriteBatch.Draw(texture, new Vector2(PosX, PosY), null, Utils.GetColorXNA(Color), 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0f);
      }

      /// <summary>
      /// Draws in the spritebatch with the specific color
      /// </summary>
      /// <param name="spriteBatch"></param>
      /// <param name="color"></param>
      public void Draw(ref SpriteBatch spriteBatch, ColorRGB color)
      {
         spriteBatch.Draw(texture, new Vector2(PosX, PosY), null, Utils.GetColorXNA(color), 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0f);
      }
   }




}

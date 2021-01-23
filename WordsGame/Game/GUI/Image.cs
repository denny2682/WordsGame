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
      private Coordinate2D coordinate;
      #endregion region

      #region public variables
      public ColorRGB Color { get; set; }
      #endregion

      /// <summary>
      /// Costructor
      /// </summary>
      /// <param name="fileName"></param>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="colorRGB"></param>
      public Image(GraphicImageInfo settings)
      {
         coordinate = settings.Coordinate;
         texture = WordsGame.Content.Load<Texture2D>(settings.FileName);
         Color = settings.Color;
      }

      /// <summary>
      /// Gets coordinate 
      /// </summary>
      public Coordinate2D Coordinate
      {
         get { return coordinate; }
      }

      /// <summary>
      /// Sets coordinate 
      /// </summary>
      public void SetPosition(Coordinate2D coordinate2D)
      {
         coordinate = coordinate2D;
      }

      /// <summary>
      /// Sets Color 
      /// </summary>
      public void SetColor(ColorRGB color)
      {
         Color = color;
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
         spriteBatch.Draw(texture, new Vector2(Coordinate.X, Coordinate.Y), null, Utils.GetColorXNA(Color), 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0f);
      }

      /// <summary>
      /// Draws in the spritebatch with the specific color
      /// </summary>
      /// <param name="spriteBatch"></param>
      /// <param name="color"></param>
      public void Draw(ref SpriteBatch spriteBatch, ColorRGB color)
      {
         spriteBatch.Draw(texture, new Vector2(coordinate.X, coordinate.Y), null, Utils.GetColorXNA(color), 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0f);
      }
   }




}

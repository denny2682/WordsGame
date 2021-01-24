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
      
      // Coordinate
      private Coordinate2D coordinate;
      
      #endregion region

      /// <summary>
      /// Costructor
      /// </summary>
      /// <param name="settings"></param>
      public Image(GraphicImageInfo settings)
      {
         coordinate = settings.Coordinate;
         texture = WordsGame.Content.Load<Texture2D>(settings.FileName);
         Color = settings.Color;
      }

      #region public variables

      /// <summary>
      /// color attribute
      /// </summary>
      public ColorRGB Color { get; set; }

      /// <summary>
      /// Gets coordinate 
      /// </summary>
      public Coordinate2D Coordinate
      {
         get { return coordinate; }
      }

      #endregion

      #region public methods

      /// <summary>
      /// Sets coordinate 
      /// </summary>
      public void SetPosition(Coordinate2D coordinate2D)
      {
         coordinate = coordinate2D;
      }

      /// <summary>
      /// Sets the color 
      /// </summary>
      public void SetColor(ColorRGB color)
      {
         Color = color;
      }

      /// <summary>
      /// Get the real size
      /// </summary>
      /// <returns></returns>
      public Size GetExtent()
      {
         return new Size(texture.Width, texture.Height);
      }

      /// <summary>
      ///  Draw inside the spritebatch
      /// </summary>
      /// <param name="spriteBatch"></param>
      public void Draw(ref SpriteBatch spriteBatch)
      {
         spriteBatch.Draw(texture, new Vector2(Coordinate.X, Coordinate.Y), null, Utils.GetColorXNA(Color), 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0f);
      }

      /// <summary>
      /// Draw inside the spritebatch with the specific color
      /// </summary>
      /// <param name="spriteBatch"></param>
      /// <param name="color"></param>
      public void Draw(ref SpriteBatch spriteBatch, ColorRGB color)
      {
         spriteBatch.Draw(texture, new Vector2(coordinate.X, coordinate.Y), null, Utils.GetColorXNA(color), 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0f);
      }

      #endregion
   }




}

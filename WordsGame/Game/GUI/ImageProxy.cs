using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Globalization;
using WordsGame.Game.Utility;

namespace WordsGame.Game
{
   /// <summary>
   /// Pattern Proxy: role proxy
   /// </summary>
   class ImageProxy : ISprite, ISubject
   {
      #region private variables

      private Size extent;
      private ColorRGB color;
      private string fileName;

      #endregion

      #region protected variables

      protected Coordinate2D coordinate;
      protected bool highlight = false;
      protected Image Image;
      
      #endregion

      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="settings"></param>
      public ImageProxy(GraphicImageInfo settings)
      {
         coordinate = settings.Coordinate;
         fileName = settings.FileName;
         color = settings.Color;
      }

      #region public variables

      /// <summary>
      /// Return if the image was selected
      /// </summary>
      public bool Highlight
      {
         get { return highlight; }

      }

      /// <summary>
      /// Returns the x and y coordinates
      /// </summary>
      public Coordinate2D Coordinate
      {
         get { return coordinate; }
      }

      /// <summary>
      /// Returns color
      /// </summary>
      public ColorRGB Color
      {
         get { return color; }
      }

      #endregion

      #region public methods

      /// <summary>
      /// Returns the sprite type
      /// </summary>
      /// <returns></returns>
      public virtual TypeSprite GetTypeSprite()
      {
         return TypeSprite.Image;
      }

      /// <summary>
      /// Returns the size calculated
      /// </summary>
      /// <returns></returns>
      public Size GetExtent()
      {
         if (extent.Height == 0 && extent.Width == 0)
            extent = Utils.GetFileSize(fileName, WordsGame.Content.RootDirectory);
         return extent;
      }

      /// <summary>
      /// Draws in the spritebatch
      /// </summary>
      /// <param name="spriteBatch"></param>
      public void Draw(SpriteBatch spriteBatch)
      {
         if (highlight)
            GetImage().Draw(ref spriteBatch, new ColorRGB(161, 096, 255, 100));
         else
            GetImage().Draw(ref spriteBatch);
      }

      /// <summary>
      /// Draws in the spritebatch with the specific color
      /// </summary>
      /// <param name="spriteBatch"></param>
      /// <param name="color"></param>
      public void Draw(SpriteBatch spriteBatch, ColorRGB color)
      {
         GetImage().Draw(ref spriteBatch, color);
      }

      /// <summary>
      /// Returns true if it is in the pointer area
      /// </summary>
      /// <returns></returns>
      public bool IsSelectedArea()
      {
         return Utils.IntersectRectangle(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), new Rectangle(Coordinate.X, Coordinate.Y, GetExtent().Width, GetExtent().Height));
      }

      /// <summary>
      /// Sets the position
      /// </summary>
      /// <param name="coordinate2D"></param>
      public void SetPosition(Coordinate2D coordinate2D)
      {
         // Sets the position of the real image and current proxyimage 
         GetImage().SetPosition(coordinate2D);
         coordinate = coordinate2D;
      }

      /// <summary>
      /// Sets the color
      /// </summary>
      /// <param name="colorRGB"></param>
      public void SetColor(ColorRGB colorRGB)
      {
         // Sets the position of the real image and current proxyimage 
         GetImage().SetColor(color);
         color = colorRGB;
      }

      #endregion

      #region protected methods
      
      /// <summary>
      /// Gets the real image
      /// </summary>
      /// <returns></returns>
      protected Image GetImage()
      {
         if (Image == null)
            Image = new Image(new GraphicImageInfo(fileName, Coordinate, color));
         return Image;
      }

      #endregion

   }

}


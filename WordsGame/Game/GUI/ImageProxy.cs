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
   class ImageProxy :  ISprite, ISubject
   {
      
      private Size extent;
      private string fileNameImg;

      protected int posX;
      protected int posY;
      protected bool highlight = false;
      protected ColorRGB color;
      protected Image Image;

      public ImageProxy(string fileName, int x, int y, ColorRGB colorRGB) // constructor
      {
         fileNameImg = fileName; // local copy of filename
         posX = x; posY = y;
         color = colorRGB;
        
      }

      /// <summary>
      /// Return if selected element
      /// </summary>
      public bool Highlight
      {
         get { return highlight; }

      }

      /// <summary>
      /// return sprite type
      /// </summary>
      /// <returns></returns>
      public virtual TypeSprite GetTypeSprite()
      {
         return TypeSprite.Image;
      }

      /// <summary>
      /// Get real image
      /// </summary>
      /// <returns></returns>
      protected Image GetImage()
      {
         if (Image == null)
           Image = new Image(fileNameImg, posX, posY, color);
         return Image;
      }

      /// <summary>
      /// Return size calculate
      /// </summary>
      /// <returns></returns>
      public Size GetExtent()
      {
         // Todo:
         // trovare il modo di sapere in anticipo la larghezza per poterlo istanziare
         if (extent.Height == 0 && extent.Width == 0)
            extent = Utils.GetFileSize(fileNameImg, WordsGame.Content.RootDirectory);
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
      public void Draw(SpriteBatch spriteBatch, ColorRGB color)
      {
         GetImage().Draw(ref spriteBatch, color);
      }

      /// <summary>
      /// returns true if it is in the pointer area
      /// </summary>
      /// <returns></returns>
      public bool IsSelectedArea() 
      {
         return Utils.IntersectRectangle(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), new Rectangle(posX, posY, GetExtent().Width, GetExtent().Height));
      }

   }

}


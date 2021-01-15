using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Globalization;
using GameWords.Game.Utility;

namespace GameWords.Game
{
   class ImageProxy :  ISprite
   {
      
      private Size _extent;
      private string _fileName;
      protected int _x;
      protected int _y;
      protected bool highlight = false;
      protected ColorRGB color;
      protected Image Image;

      public ImageProxy(string fileName, int x, int y, ColorRGB colorRGB) // constructor
      {
         _fileName = fileName; // local copy of filename
         _x = x; _y = y;
         color = colorRGB;
        
      }

      public bool Highlight
      {
         get { return highlight; }

      }
      public virtual TypeSprite GetTypeSprite()
      {
         return TypeSprite.Image;
      }

      protected Image GetImage()
      {
         if (Image == null)
           Image = new Image(_fileName, _x, _y, color);
         return Image;
      }

      public Size GetExtent()
      {
         // Todo:
         // trovare il modo di sapere in anticipo la larghezza per poterlo istanziare
         if (_extent.Height == 0 && _extent.Width == 0)
            _extent = Utils.GetFileSize(_fileName, WordsGame.Content.RootDirectory);
         return _extent;
      }

      public void Draw(SpriteBatch spriteBatch) 
      {
         if (highlight)
            GetImage().Draw(ref spriteBatch, new ColorRGB(161, 096, 255, 100));
         else
            GetImage().Draw(ref spriteBatch);
      }

      public void Draw(SpriteBatch spriteBatch, ColorRGB color) 
      {
         GetImage().Draw(ref spriteBatch, color);
      }

      public bool IsSelectedArea() 
      {
         return Utils.IntersectRectangle(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), new Rectangle(_x, _y, GetExtent().Width, GetExtent().Height));
      }
      



   }

}


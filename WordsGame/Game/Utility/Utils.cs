using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WordsGame.Game.Utility
{
   /// <summary>
   /// groups the methods, structures and enums useful in the graphic context
   /// </summary>
   public static class Utils
   {
      /// <summary>
      /// Returns the Xna class for Color
      /// </summary>
      /// <param name="colorRGB"></param>
      /// <returns></returns>
      public static Color GetColorXNA(ColorRGB colorRGB)
      {
         return new Color(colorRGB.R, colorRGB.G, colorRGB.B, colorRGB.A);
      }

      /// <summary>
      /// Return the image size lettertile
      /// </summary>
      /// <param name="fileName"></param>
      /// <param name="rootDirectory"></param>
      /// <returns></returns>
      public static Size GetFileSize(string fileName, string rootDirectory)
      {
         return new Size(80, 80);
      }

      /// <summary>
      /// this method gets the vector identifying the mouse position and the rectangle in the x and y coordinates,
      /// it returns true if there is intersection.
      /// </summary>
      /// <param name="mouseLocation"></param>
      /// <param name="RectangleLocation"></param>
      /// <returns></returns>
      public static bool IntersectRectangle(Vector2 mouseLocation, Rectangle RectangleLocation)
      {
         if (mouseLocation.X >= RectangleLocation.Left && mouseLocation.X <= RectangleLocation.Right &&
            mouseLocation.Y >= RectangleLocation.Top && mouseLocation.Y <= RectangleLocation.Bottom)
            return true;
         else
            return false;
      }

    

   }

   #region enum

   /// <summary>
   /// The type of sprite in the graphic context
   /// </summary>
   public enum TypeSprite
   {
      Letter,
      Image,
      Text,
      BtnReloadLevel,
      BtnReloadGame,
      Score,
   }

   /// <summary>
   /// The type of font in the graphic context
   /// </summary>
   public enum TypeFont
   {
      Arial,
      Hud
   }

   #endregion

   #region struct
   /// <summary>
   /// Struct to define width and height
   /// </summary>
   public struct Size
   {
      public int Width;
      public int Height;

      public Size(int width, int height)
      {
         Width = width;
         Height = height;
      }
   }

   /// <summary>
   /// Struct to setting an image on the screen
   /// </summary>
   public struct GraphicImageInfo
   {
      public string FileName;
      public Coordinate2D Coordinate;
      public ColorRGB Color;

      public GraphicImageInfo(string file, Coordinate2D coordinate, ColorRGB color)
      {
         FileName = file;
         Color = color;
         Coordinate = coordinate;
      }
   }

   public struct GraphicFontInfo
   {
      public Coordinate2D Coordinate;
      public ColorRGB Color;
      public TypeFont Font;

      public GraphicFontInfo(TypeFont typeFont, Coordinate2D coordinate, ColorRGB color)
      {
         Color = color;
         Coordinate = coordinate;
         Font = typeFont;
      }
   }

   public struct Coordinate2D
   {  
      public int X;
      public int Y;

      public Coordinate2D(int x, int y)
      {
         X = x;
         Y = y;
      }
   }
   /// <summary>
   /// Struct to define a custom ColorRGB
   /// </summary>
   public struct ColorRGB
   {
      public byte R;
      public byte G;
      public byte B;
      public byte A;

      public ColorRGB(byte r, byte g, byte b, byte a)
      {
         R = r;
         G = g;
         B = b;
         A = a;
      }
   }
   #endregion

}

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WordsGame.Game.Utility
{
   public static class Utils
   {
      /// <summary>
      /// Restituisce la classe Color di Xna
      /// </summary>
      /// <param name="colorRGB"></param>
      /// <returns></returns>
      public static Color GetColorXNA(ColorRGB colorRGB)
      {
         return new Color(colorRGB.R, colorRGB.G, colorRGB.B, colorRGB.A);
      }

      public static Size GetFileSize(string fileName, string rootDirectory)
      {
         return new Size(90, 90);
      }

      
      public static bool IntersectRectangle(Vector2 mouseLocation, Rectangle RectangleLocation)
      {
         if (mouseLocation.X >= RectangleLocation.Left && mouseLocation.X <= RectangleLocation.Right &&
            mouseLocation.Y >= RectangleLocation.Top && mouseLocation.Y <= RectangleLocation.Bottom)
            return true;
         else
            return false;
      }

   }

   public enum TypeSprite
   {
      Letter,
      Image,
      Text,
      BtnReloadLevel,
      BtnReloadGame,
      Score,
   }

   public enum TypeFont
   {
      Arial,
      Hud
   }

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

}

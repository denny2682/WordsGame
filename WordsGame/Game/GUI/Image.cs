﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using WordsGame.Game.Utility;

namespace WordsGame.Game
{
   class Image
   {
      private Texture2D texture;
      private int posX;
      private int posY;
      public ColorRGB Color{ get; set; }

      public Image(string fileName, int x, int y, ColorRGB colorRGB)
      {
         posX = x; posY = y;
         texture = WordsGame.Content.Load<Texture2D>(fileName);
         Color = colorRGB;
      }

      public int PosX { 
         get { return posX;  } 
      }

      public int PosY
      {
         get { return posY; }
      }

      public Size GetExtent()
      {
         return new Size(texture.Width, texture.Height);
      }

      public void Draw(ref SpriteBatch spriteBatch)
      {
         spriteBatch.Draw(texture, new Vector2(PosX, PosY), null, Utils.GetColorXNA(Color), 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0f);
      }

      public void Draw(ref SpriteBatch spriteBatch, ColorRGB color)
      {
         spriteBatch.Draw(texture, new Vector2(PosX, PosY), null, Utils.GetColorXNA(color), 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0f);
      }
   }




}

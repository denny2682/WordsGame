using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameWords.Game.Utility;

namespace GameWords.Game
{
   class Image
   {
      private Texture2D texture;
      
      public int PosX;
      public int PosY;
      public ColorRGB Color{ get; set; }

      public Image(string fileName, int x, int y, ColorRGB colorRGB)
      {
         PosX = x; PosY = y;
         texture = WordsGame.Content.Load<Texture2D>(fileName);
         Color = colorRGB;
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

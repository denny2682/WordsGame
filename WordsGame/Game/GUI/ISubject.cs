using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WordsGame.Game.Utility;

namespace WordsGame.Game
{
   public interface ISubject
   {
      Size GetExtent();
      void Draw(SpriteBatch spriteBatch);
      void Draw(SpriteBatch spriteBatch, ColorRGB color);
   }
}

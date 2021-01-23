using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WordsGame.Game.Utility;

namespace WordsGame.Game
{
   /// <summary>
   /// Interface for all the objects of type sprite
   /// </summary>
   public interface ISprite
   {
      void Draw(SpriteBatch spriteBatch);
      TypeSprite GetTypeSprite();

   }
}

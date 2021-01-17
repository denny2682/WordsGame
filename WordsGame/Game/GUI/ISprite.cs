using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameWords.Game.Utility;

namespace GameWords.Game
{
   public interface ISprite
   {
      void Draw(SpriteBatch spriteBatch);
      TypeSprite GetTypeSprite();
   }
}

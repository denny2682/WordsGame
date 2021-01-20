using System;
using System.Collections.Generic;
using System.Text;
using WordsGame.Game.Utility;

namespace WordsGame.Game
{
   /// <summary>
   /// Interface pattern builder: role builder
   /// </summary>
   public interface ILevelBuilder
   {
      public void buildText(TypeFont typeFont, string text, int x, int y, ColorRGB colorRGB);
      public void buildGrid(int row, int column, int x, int y);
      public void buildScore(TypeFont typeFont, int x, int y, ColorRGB colorRGB);

      public void buildWinner(int x, int y, ColorRGB colorRGB);

      public void buildReloadLevelBtn (int x, int y, ColorRGB colorRGB);

      public void buildReloadGameBtn (int x, int y, ColorRGB colorRGB);

      public List<ISprite> GetSprites();
     
      public void Reset();
   }
}

using System;
using System.Collections.Generic;
using System.Text;
using WordsGame.Game.Utility;

namespace WordsGame.Game
{
   public interface ILevelBuilder
   {
      public void buildText(TypeFont typeFont, string text, int x, int y, ColorRGB colorRGB);
      public void buildGrid(int row, int coloumn, int x, int y);
      public void buildScore(TypeFont typeFont, int x, int y, ColorRGB colorRGB);

      public void buildWinner(int x, int y, ColorRGB colorRGB);

      public void buildBtnReloadLevel (int x, int y, ColorRGB colorRGB);

      public void buildBtnReloadGame(int x, int y, ColorRGB colorRGB);

      public List<ISprite> GetSprites();
     
      public void Reset();
   }
}

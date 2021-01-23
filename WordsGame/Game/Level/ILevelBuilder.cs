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
      public void buildBackground(int x, int y, ColorRGB color = new ColorRGB());
      public void buildTitle(int x, int y, string text, ColorRGB color = new ColorRGB());
      public void buildText(TypeFont typeFont, string text, int x, int y, ColorRGB colorRGB);
      public void buildGridAndDescription(int row, int column, int x, int y, List<string> description, ColorRGB colorRGB, TypeFont typeFont);
      public void buildScore(TypeFont typeFont, int x, int y, ColorRGB colorRGB);

      public void buildWinner(int x, int y, ColorRGB colorRGB);

      public void buildReloadLevelBtn (int x, int y, ColorRGB colorRGB);

      public void buildReloadGameBtn (int x, int y, ColorRGB colorRGB);

      public List<ISprite> GetSprites();
     
      public void Reset();
   }
}

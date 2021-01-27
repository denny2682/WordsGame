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
      public void BuildBackground(int x, int y, ColorRGB color = new ColorRGB());
      public void BuildTitle(int x, int y, string text, ColorRGB color = new ColorRGB());
      public void BuildText(TypeFont typeFont, string text, int x, int y, ColorRGB colorRGB);
      public void BuildGridAndDescription(int row, int column, int x, int y, List<string> description, ColorRGB colorRGB, TypeFont typeFont);
      public void BuildScore(TypeFont typeFont, int x, int y, ColorRGB colorRGB);

      public void BuildWinner(int x, int y, ColorRGB colorRGB);

      public void BuildReloadLevelBtn (int x, int y, ColorRGB colorRGB);

      public void BuildReloadGameBtn (int x, int y, ColorRGB colorRGB);

      public List<ISprite> GetSprites();
     
      public void Reset();
   }
}

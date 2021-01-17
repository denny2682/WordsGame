using System;
using System.Collections.Generic;
using System.Text;

namespace WordsGame.Game
{
   public class LevelSettings
   {
      private int levelNumber;
      private int minScore;
      private int row;
      private int coloumn;

      public LevelSettings(int settingLevelNumber, int settingMinScore, int settingRow, int SettingColoumn)
      {
         levelNumber = settingLevelNumber;
         minScore = settingMinScore;
         row = settingRow;
         coloumn = SettingColoumn;
      }

      public int LevelNumber
      {
         get { return levelNumber; }
      }

      public int MinScore
      {
         get { return minScore; }
      }

      public int Row
      {
         get { return row; }
      }

      public int Coloumn
      {
         get { return coloumn; }
      }

   }
}

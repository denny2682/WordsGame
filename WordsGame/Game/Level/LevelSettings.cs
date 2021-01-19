using System;
using System.Collections.Generic;
using System.Text;

namespace WordsGame.Game
{
   /// <summary>
   /// Class to set a level
   /// </summary>
   public class LevelSettings
   {
      private int levelNumber;
      private int minScore;
      private int rows;
      private int columns;

      public LevelSettings(int settingLevelNumber, int settingMinScore, int settingRow, int SettingColoumn)
      {
         levelNumber = settingLevelNumber;
         minScore = settingMinScore;
         rows = settingRow;
         columns = SettingColoumn;
      }

      /// <summary>
      /// Number of level
      /// </summary>
      public int LevelNumber
      {
         get { return levelNumber; }
      }

      /// <summary>
      /// Minimum score to go to the next level
      /// </summary>
      public int MinScore
      {
         get { return minScore; }
      }

      /// <summary>
      /// Rows for the grid
      /// </summary>
      public int Rows
      {
         get { return rows; }
      }

      /// <summary>
      /// Columns for the grid
      /// </summary>
      public int Columns
      {
         get { return columns; }
      }

   }
}

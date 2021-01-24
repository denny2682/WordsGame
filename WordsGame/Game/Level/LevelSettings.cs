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
      #region private variables

      private int levelNumber;
      private int minScore;
      private int rows;
      private int columns;

      #endregion

      /// <summary>
      /// Costructor
      /// </summary>
      /// <param name="settingLevelNumber"></param>
      /// <param name="settingMinScore"></param>
      /// <param name="settingRow"></param>
      /// <param name="settingColumn"></param>

      public LevelSettings(int settingLevelNumber, int settingMinScore, int settingRow, int settingColumn)
      {
         levelNumber = settingLevelNumber;
         minScore = settingMinScore;
         rows = settingRow;
         columns = settingColumn;
      }

      #region public variables

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

      #endregion
   }
}

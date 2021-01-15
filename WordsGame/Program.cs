using System;
using GameWords.Game;

namespace GameWords
{
   public static class Program
   {
      [STAThread]
      static void Main()
      {
         using (var game = new WordsGame())
            game.Run();
      }
   }
}

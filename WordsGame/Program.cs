using System;
using WordsGame.Game;

namespace WordsGame
{
   public static class Program
   {
      [STAThread]
      static void Main()
      {
         using (var game = new Game.WordsGame())
            game.Run();
      }
   }
}

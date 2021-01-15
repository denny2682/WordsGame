using System;
using System.Collections.Generic;
using System.Text;


namespace GameWords.Game
{
   public interface IObserver
   {
      public void Selected();
      public void UnSelected();
   }
}

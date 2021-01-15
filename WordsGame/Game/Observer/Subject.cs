using System;
using System.Collections.Generic;
using System.Text;

namespace GameWords.Game
{
   public abstract class Subject
   {
      private List<IObserver> observers = new List<IObserver>();
      public void Attach(IObserver obs) { observers.Add(obs); }
      public void Detach(IObserver obs) { observers.Remove(obs); }


      public void NotifySelected()
      {
         for (var i = 0; i < observers.Count; i++)
         {
            IObserver obs = observers[i];
            if (obs != null)
               obs.Selected();
         }

      }

      public void NotifyUnSelected()
      {
         for (var i = 0; i < observers.Count; i++)
         {
            IObserver obs = observers[i];
            if (obs != null)
               obs.UnSelected();
         }
      }


   }
}

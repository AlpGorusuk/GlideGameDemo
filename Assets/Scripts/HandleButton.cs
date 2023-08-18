using System.Collections.Generic;
using GlideGame.Interfaces;
using UnityEngine;

namespace GlideGame
{
    public class HandleButton : MonoBehaviour, IObservable
    {
        private List<IObserver> observers = new List<IObserver>();
        public virtual void Attach(IObserver observer)
        {
            if (observers.Contains(observer))
            {
                return;
            }
            observers.Add(observer);
        }

        public virtual void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public virtual void Notify()
        {
            foreach (var observer in observers)
            {
                observer.UpdateObserver(this);
            }
        }
    }
}

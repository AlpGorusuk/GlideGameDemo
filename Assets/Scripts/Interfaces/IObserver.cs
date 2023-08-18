using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlideGame.Interfaces
{
    public interface IObserver
    {
        void UpdateObserver(IObservable observable);
    }
}

using System;
using System.Collections.Generic;

public class Observable<T> : IObservable<T>
{
    private List<IObserver<T>> _observers;

    public Observable()
    {

    }

    public Observable(List<IObserver<T>> observers)
    {
        _observers = observers;
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);
        return new Unsubscriber(_observers, observer);
    }

    private class Unsubscriber : IDisposable
    {
        private List<IObserver<T>> _observers;
        private IObserver<T> _observer;

        public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }

    //ramas llaman a esta funcion
    public void TrackDimension(T observable) 
    {
        foreach (var observer in _observers)
        {
            if (observable == null)
                observer.OnError(new Exception());
            else
                observer.OnNext(observable);
        }
    }

    //cuando pierdo/gane
    public void EndTransmission()
    {
        foreach (var observer in _observers.ToArray())
            if (_observers.Contains(observer))
                observer.OnCompleted();

        _observers.Clear();
    }
}

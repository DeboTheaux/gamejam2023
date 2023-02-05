using System;
using System.Collections.Generic;

public class LifeManager : Observable<Life>
{
    public LifeManager(List<IObserver<Life>> lifeObservers) : base(lifeObservers)
    {

    }
}


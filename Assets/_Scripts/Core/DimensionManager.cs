using System;
using System.Collections.Generic;

public class DimensionManager : Observable<Dimension>
{
    private List<Dimension> _dimensions;
    private Dimension _currentDimension;

    public DimensionManager(List<IObserver<Dimension>> dimensionObservers) : base(dimensionObservers)
    {

    }

    public void Initialize(List<Dimension> dimensions)
    {
        _dimensions = dimensions;
        _currentDimension = _dimensions[0];

        _currentDimension.Start();
    }
}

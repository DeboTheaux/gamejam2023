public class ConsciousDimension : Dimension
{
    private Observable<Dimension> _dimensionObservable;
    private Observable<Life> _lifeObservable;

    public ConsciousDimension(Observable<Dimension> dimensionObservable, Observable<Life> lifeObservable)
    {
        _dimensionObservable = dimensionObservable;
        _lifeObservable = lifeObservable;
    }

    public void Start()
    {
        _dimensionObservable.TrackDimension(this);
    }
}

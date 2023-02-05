using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlobalInstaller : MonoBehaviour
{
    [SerializeField] private GameObject[] dimensionReactiveObjects;
    [SerializeField] private GameObject[] lifeReactiveObjects;
    [SerializeField] private List<PlayerAction> playerActions; //

    private IEnumerable<IObserver<Dimension>> _dimensionObservers; //estado del juego
    private IEnumerable<IObserver<Life>> _lifeObservers; //el progreso del juego, emisores de información para ser reaccionada

    private Observable<Dimension> _dimensionObservable;
    private Observable<Life> _lifeObservable;

    private List<Dimension> _dimensions;
    private RealityDimension _realityDimension; //
    private ConsciousDimension _consciousDimension; //

    private void Awake()
    {
        _dimensionObservers = dimensionReactiveObjects.ToList().Select(reactiveObject => reactiveObject.GetComponent<IObserver<Dimension>>());
        _lifeObservers = lifeReactiveObjects.ToList().Select(reactiveObject => reactiveObject.GetComponent<IObserver<Life>>());
    }

    private void Start()
    {
        _dimensionObservable = new DimensionManager(_dimensions, _dimensionObservers.ToList());
        _lifeObservable = new LifeManager(_lifeObservers.ToList());

        _realityDimension = new(playerActions, _dimensionObservable);
        _consciousDimension = new(_dimensionObservable, _lifeObservable);

        _dimensions.Add(_realityDimension);
        _dimensions.Add(_consciousDimension);


       // _realityDimension.ExecuteAction();
    }
}

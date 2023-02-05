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


    private IObservable<Dimension> _dimensionObservable;
    private IObservable<Life> _lifeObservable;

    private RealityDimension _realityDimension; //
    private ConsciousDimension _consciousDimension; //

    private void Awake()
    {
        _dimensionObservers = dimensionReactiveObjects.ToList().Select(reactiveObject => reactiveObject.GetComponent<IObserver<Dimension>>());
        _lifeObservers = lifeReactiveObjects.ToList().Select(reactiveObject => reactiveObject.GetComponent<IObserver<Life>>());
    }

    private void Start()
    {
        _dimensionObservable = new DimensionManager(_dimensionObservers.ToList());
        _lifeObservable = new LifeManager(_lifeObservers.ToList());

        _realityDimension = new(playerActions);
        _consciousDimension = new();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _realityDimension.ExecuteAction();
        }
    }

}

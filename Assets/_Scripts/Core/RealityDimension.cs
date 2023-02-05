using System;
using System.Collections.Generic;
using System.Linq;

public class RealityDimension : Dimension
{
    private List<PlayerAction> _playerActions;
    private Observable<Dimension> _dimensionObservable;
    private Queue<PlayerAction> _actionsSequence = new();

    private PlayerAction _activePlayerAction;

    public RealityDimension(List<PlayerAction> playerActions, Observable<Dimension> dimensionObservable)
    {
        _playerActions = playerActions;
        _dimensionObservable = dimensionObservable;
    }

    private void EnqueueActions()
    {
        _actionsSequence.Clear();

        _playerActions.ForEach(action =>
        {
            _actionsSequence.Enqueue(action);
            action.SetInactive();
        });

        _activePlayerAction = _playerActions[0];
        _activePlayerAction.SetActive();
    }

    public void Start()
    {
        EnqueueActions();
        _dimensionObservable.TrackDimension(this);
    }

    public void ExecuteAction()
    {
        if (_actionsSequence.Count is 0) return;

        var currentAction = _actionsSequence?.Dequeue();
        currentAction?.Execute(ExecuteAction);
    }
}

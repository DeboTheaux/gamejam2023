using System.Collections.Generic;
using System.Linq;

public class RealityDimension : Dimension
{
    private List<PlayerAction> _playerActions;
    private Queue<PlayerAction> _actionsSequence = new();

    public RealityDimension(List<PlayerAction> playerActions)
    {
        _playerActions = playerActions;
        EnqueueActions();
    }

    private void EnqueueActions()
    {
        _actionsSequence.Clear();

        _playerActions.ForEach(action =>
        {
            _actionsSequence.Enqueue(action);
        });
    }

    public void ExecuteAction()
    {
        if (_actionsSequence.Count is 0) return;

        var currentAction = _actionsSequence?.Dequeue();
        currentAction?.Execute(ExecuteAction);
    }
}

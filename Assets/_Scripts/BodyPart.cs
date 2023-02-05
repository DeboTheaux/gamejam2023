using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public enum State { NORMAL, PARTIAL_DAMAGE, DAMAGED }
    public bool isEnabled;
    public GameObject normalModel;
    public GameObject partialModel;
    public GameObject damagedModel;
    private State currentState = State.NORMAL;


    public void ChangeState(State state)
    {
        currentState = state;
        switch (currentState)
        {
            case State.NORMAL:
                normalModel.SetActive(true);
                partialModel.SetActive(false);
                damagedModel.SetActive(false);
                break;
            case State.PARTIAL_DAMAGE:
                normalModel.SetActive(false);
                partialModel.SetActive(true);
                damagedModel.SetActive(false);
                break;
            case State.DAMAGED:
                normalModel.SetActive(false);
                partialModel.SetActive(false);
                damagedModel.SetActive(true);
                break;
        }
    }
}

using UnityEngine;

public class InteractableFieldWithStateMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private StateMachineManager stateMachineManager;
    public void OnClickAction()
    {
        switch (stateMachineManager._currentState)
        {
            case EmptyState emptyState:
                Debug.Log("Empty state clicked!");
                stateMachineManager.SwitchState(stateMachineManager.GrowingStateInstance);
                break;
            case GrowingState growingState:
                Debug.Log("Growing state clicked!");
                stateMachineManager.SwitchState(stateMachineManager.HarvestStateInstance);
                break;
            case HarvestState harvestState:
                Debug.Log("Harvest state clicked!");
                stateMachineManager.SwitchState(stateMachineManager.EmptyStateInstance);
                break;
            default:
                Debug.LogWarning("Unknown state clicked!");
                break;
        }
        //if (stateMachineManager._currentState is EmptyState)
        //{
        //    Debug.Log("Empty state clicked!");
        //    stateMachineManager.SwitchState(stateMachineManager.GrowingStateInstance);
        //}
        //else if (stateMachineManager._currentState is GrowingState)
        //{
        //    Debug.Log("Growing state clicked!");
        //    stateMachineManager.SwitchState(stateMachineManager.HarvestStateInstance);
        //}
        //else if (stateMachineManager._currentState is HarvestState)
        //{
        //    Debug.Log("Harvest state clicked!");
        //    stateMachineManager.SwitchState(stateMachineManager.EmptyStateInstance);
        //}
    }

    private void OnDisable()
    {
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class GrowingState : State
{
    [SerializeField]
    private Renderer renderer;
    public GrowingState(StateMachineManager manager) : base(manager)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering the growing state
        //change the color of the plant to green
        Debug.Log("Entering Growing State");
    }
    public override void UpdateState()
    {
        // Implementation for updating the growing state
    }
    public override void ExitState()
    {
        // Implementation for exiting the growing state
        Debug.Log("Exiting Growing State");
    }
}

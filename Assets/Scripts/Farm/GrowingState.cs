using Unity.VisualScripting;
using UnityEngine;

public class GrowingState : State
{
    [SerializeField]
    private Renderer renderer;
    public GrowingState(FarmPlotState farmPlotState) : base(farmPlotState)
    {
    }
    public override void OnEnter()
    {
        // Implementation for entering the growing state
        //change the color of the plant to green
        Debug.Log("Entering Growing State");
        //tile.StartGrowing(tile.plantedSeed);
    }
    public override void OnUpdate()
    {
        // Implementation for updating the growing state
    }
    public override void OnExit()
    {
        // Implementation for exiting the growing state
        Debug.Log("Exiting Growing State");
    }
}

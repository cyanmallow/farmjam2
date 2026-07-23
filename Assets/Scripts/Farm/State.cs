using UnityEngine;

public abstract class State
{
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();

    protected FarmPlotState farmPlotState;

    public State(FarmPlotState state)
    {
        farmPlotState = state;
    }

    // no default
    public virtual void Plant(SeedData seed)
    {
        Debug.LogWarning("Plant action is not valid in the current state.");
    }
    public virtual void Water()
    {
        Debug.LogWarning("Water action is not valid in the current state.");
    }
    public virtual void Harvest()
    {
        Debug.LogWarning("Harvest action is not valid in the current state.");
    }
    public virtual void GrowUp()
    {
    }
}

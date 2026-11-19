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
    public abstract string StateName { get; } // 'growing', 'ready to harvest', 'empty', etc.
                                              // This is used to identify the state
                                              // and can be useful for debugging or for triggering specific behaviors
                                              // based on the state.
    public virtual bool CanPlant => false; // default to false, override in specific states where planting is allowed
    public virtual bool CanClear => false;

    public virtual bool CanWater => false;
    public virtual bool CanHarvest => false;
    // no default
    public virtual void Clear()
    {
        Debug.LogWarning("Clear action is not valid in the current state.");
    }
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

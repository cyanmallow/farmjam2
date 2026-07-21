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
}

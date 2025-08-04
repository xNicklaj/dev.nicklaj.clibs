using System;
using System.Collections.Generic;

public class StateMachine
{
    private StateNode current;
    private Dictionary<Type, StateNode> nodes = new();
    HashSet<ITransition> anyTransitions = new();
    
    /// <summary>
    /// Checks for possible transitions and calls the update loop on the current state.
    /// If a transition is available, the transition happens before updating the current state, so the update loop will be called
    /// directly in the new state.
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(float deltaTime)
    {
        var transition = GetTransition();
        if (transition != null)
            ChangeState(transition.To);
        
        current?.State.Update(deltaTime);
    }
    
    /// <summary>
    /// Calls the fixed update loop on the current state.
    /// </summary>
    /// <param name="fixedDeltaTime"></param>
    public void FixedUpdate(float fixedDeltaTime) => current?.State.FixedUpdate(fixedDeltaTime);

    /// <summary>
    /// Get a state or if it is not present, add it.
    /// </summary>
    /// <param name="state">The state to get.</param>
    /// <returns>The state node representing the state inside the state machine.</returns>
    StateNode GetOrAddNode(IState state)
    {
        var node = nodes.GetValueOrDefault(state.GetType());

        if (node == null)
        {
            node = new StateNode(state);
            nodes.Add(state.GetType(), node);
        }

        return node;
    }

    /// <summary>
    /// Add a transition that can happen from any state.
    /// </summary>
    /// <param name="to">State to transition to.</param>
    /// <param name="predicate">Condition to transition.</param>
    public void AddAnyTransition(IState to, IPredicate predicate)
    {
        anyTransitions.Add(new Transition(to, predicate));
    }

    /// <summary>
    /// Add a transition that can happen from a specific state.
    /// </summary>
    /// <param name="from">Starting state.</param>
    /// <param name="to">Ending state.</param>
    /// <param name="condition">Condition to transition.</param>
    public void AddTransition(IState from, IState to, IPredicate condition)
    {
        GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
    }

    /// <summary>
    /// Returns the first transition whose predicate is true.
    /// This prioritizes transition that can happen from any state to those that are state-specific.
    /// </summary>
    /// <returns>Returns the transition if any predicate is true, null otherwise.</returns>
    ITransition GetTransition()
    {
        foreach (var transition in anyTransitions)
            if (transition.Condition.Evaluate())
                return transition;

        if (current == null) return null;
        
        foreach (var transition in current.Transitions)
            if (transition.Condition.Evaluate())
                return transition;
            
        return null;
    }
    
    /// <summary>
    /// Transition the state of the state machine to the new state.
    /// This calls the OnExit function on the current state and the OnEnter function on the new state.
    /// </summary>
    /// <param name="state">State to transition to.</param>
    void ChangeState(IState state) {
        if (state == current.State) return;
            
        var previousState = current.State;
        var nextState = nodes[state.GetType()].State;
            
        previousState?.OnExit();
        nextState?.OnEnter();
        current = nodes[state.GetType()];
    }
    
    /// <summary>
    /// Sets the state of the state machine without transition.
    /// If any state is currently active, this doesn't call its OnExit function, however it calls the OnEnter function of the new state.
    /// </summary>
    /// <param name="state">State to enter.</param>
    public void SetState(IState state) {
        current = nodes[state.GetType()];
        current.State?.OnEnter();
    }

    public StateNode GetCurrentState() => current;
}

using System.Collections.Generic;

namespace dev.nicklaj.clibs.StateMachine
{
    public class StateNode
    {
        public IState State { get;  }
        /// <summary>
        /// Transitions that can happen from this state
        /// </summary>
        public HashSet<ITransition> Transitions { get; }

        public StateNode(IState state)
        {
            State = state;
            Transitions = new HashSet<ITransition>();
        }

        /// <summary>
        /// Add a transition from this state
        /// </summary>
        /// <param name="to">State to transition to.</param>
        /// <param name="condition">The condition to transition.</param>
        public void AddTransition(IState to, IPredicate condition) {
            Transitions.Add(new Transition(to, condition));
        }
    }
}
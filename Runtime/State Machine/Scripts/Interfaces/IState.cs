using UnityEngine;

namespace dev.nicklaj.clibs.StateMachine
{
    public interface IState
    {
        public void OnEnter();
        public void Update(float deltaTime);
        public void FixedUpdate(float fixedDeltaTime);
        public void OnExit();
    }
}

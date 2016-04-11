using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace KaizenEngine.States
{
    class StateMachine
    {
        public Dictionary<string, StateMachineField> Fields { get; set; }  // The values of the state machine varibles. Used to trigger state transitions
        protected List<State> states;
        public State EntryState;
        public State CurrentState;
        
        public StateMachine()
        {
            states = new List<State>();
            EntryState = new State();
            Fields = new Dictionary<string, StateMachineField>();
        }

        public virtual void AddState(State state)
        {
            states.Add(state);
        }

        public void SetEntryPoint(State state)
        {
            EntryState.AddTransition(new StateTransition(EntryState, state));
        }

        public virtual void Start()
        {
            CurrentState = EntryState.Transitions[0].To;
            CurrentState.Start();
        }

        public virtual void Update(GameTime gameTime, Vector2 position)
        {

        }

        protected virtual void ShouldChangeState()
        {
            foreach (StateTransition transition in CurrentState.Transitions)
            {
                if (transition.ShouldApplyTransition(Fields))
                {
                    ApplyTransition(transition);
                    break;
                }
            }
        }

        protected virtual void ApplyTransition(StateTransition transition)
        {
            CurrentState = transition.To;
            transition.Apply();
        }

        public void SetBoolField(string fieldName, bool value)
        {
            Fields[fieldName].Value = true ? 1 : 0;
            ShouldChangeState();
        }

        public void SetIntField(string fieldName, int value)
        {
            Fields[fieldName].Value = value;
            ShouldChangeState();
        }

        public void SetFloatField(string fieldName, float value)
        {
            Fields[fieldName].Value = value;
            ShouldChangeState();
        }

        public void SetTrigger(string fieldName)
        {
            Fields[fieldName].Value = 1.0f;
            ShouldChangeState();
        }
    }
}

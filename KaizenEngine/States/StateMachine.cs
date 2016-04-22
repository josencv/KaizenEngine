using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace KaizenEngine.States
{
    /// <summary>
    /// A generic state machine, similar to Unity's
    /// </summary>
    class StateMachine
    {
        public Dictionary<string, StateMachineField> Fields { get; set; }  // The values of the state machine varibles. Used to trigger state transitions
        protected List<State> states;
        public State EntryState;
        public State CurrentState;
        
        /// <summary>
        /// Initializes an instance of the StateMachine class
        /// </summary>
        public StateMachine()
        {
            states = new List<State>();
            EntryState = new State();
            Fields = new Dictionary<string, StateMachineField>();
        }

        /// <summary>
        /// Adds a state to the state machine.
        /// </summary>
        /// <param name="state"></param>
        public virtual void AddState(State state)
        {
            states.Add(state);
        }

        /// <summary>
        /// Sets the given state as the entry point (or start state) of the state machine.
        /// When the machine state starts, this states will be run first
        /// Note: in the future this might be useful for implementing sub-state machines
        /// </summary>
        /// <param name="state"></param>
        public void SetEntryPoint(State state)
        {
            EntryState.AddTransition(new StateTransition(EntryState, state));
        }

        /// <summary>
        /// Starts the state machine.
        /// </summary>
        public virtual void Start()
        {
            CurrentState = EntryState.Transitions[0].To;
            CurrentState.Start();
        }

        /// <summary>
        /// Update call to be used inside the game update loop
        /// </summary>
        /// <param name="delta">Time since last cycle</param>
        /// <param name="position">New position where to render the animation. TODO: change position to a transform component</param>
        public virtual void Update(GameTime gameTime, Vector2 position)
        {
            if (CurrentState != null)
            {
                CurrentState.Update();
            }
        }

        /// <summary>
        /// Checks if any transition of the current state meets the requirements to be applied
        /// </summary>
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

        /// <summary>
        /// Apply a transition of the current state and the state machine current state is changed to the one pointed by the transition
        /// </summary>
        /// <param name="transition"></param>
        protected virtual void ApplyTransition(StateTransition transition)
        {
            CurrentState = transition.To;
            transition.Apply();
        }

        /// <summary>
        /// Registers a boolean field to the state machine
        /// </summary>
        /// <param name="fieldName">The name of the field to register</param>
        /// <param name="startingValue">The value of the field when the state machine starts</param>
        public void RegisterBoolField(string fieldName, bool startingValue = false)
        {
            float fvalue = startingValue ? 1 : 0;
            StateMachineField field = new StateMachineField(StateMachineFieldType.Bool, fvalue);
            Fields.Add(fieldName, field);
        }

        /// <summary>
        /// Registers an integer field to the state machine
        /// </summary>
        /// <param name="fieldName">The name of the field to register</param>
        /// <param name="startingValue">The value of the field when the state machine starts</param>
        public void RegisterIntField(string fieldName, int startingValue = 0)
        {
            StateMachineField field = new StateMachineField(StateMachineFieldType.Int, startingValue);
            Fields.Add(fieldName, field);
        }

        /// <summary>
        /// Registers a float field to the state machine
        /// </summary>
        /// <param name="fieldName">The name of the field to register</param>
        /// <param name="startingValue">The value of the field when the state machine starts</param>
        public void RegisterFloatField(string fieldName, float startingValue = 0)
        {
            StateMachineField field = new StateMachineField(StateMachineFieldType.Float, startingValue);
            Fields.Add(fieldName, field);
        }

        /// <summary>
        /// Registers a trigger field to the state machine
        /// </summary>
        /// <param name="fieldName">The name of the field to register</param>
        /// <param name="startingValue">The value of the field when the state machine starts</param>
        public void RegisterTriggerField(string fieldName)
        {
            StateMachineField field = new StateMachineField(StateMachineFieldType.Trigger, 0);
            Fields.Add(fieldName, field);
        }

        /// <summary>
        /// Changes the value of a previously registered boolean field of the state machine
        /// </summary>
        /// <param name="fieldName">The name of the field to register</param>
        /// <param name="value">The new value to set to the field</param>
        public void SetBoolField(string fieldName, bool value)
        {
            Fields[fieldName].Value = value ? 1 : 0;
            ShouldChangeState();
        }

        /// <summary>
        /// Changes the value of a previously registered integer field of the state machine
        /// </summary>
        /// <param name="fieldName">The name of the field to register</param>
        /// <param name="value">The new value to set to the field</param>
        public void SetIntField(string fieldName, int value)
        {
            Fields[fieldName].Value = value;
            ShouldChangeState();
        }

        /// <summary>
        /// Changes the value of a previously registered float field of the state machine
        /// </summary>
        /// <param name="fieldName">The name of the field to register</param>
        /// <param name="value">The new value to set to the field</param>
        public void SetFloatField(string fieldName, float value)
        {
            Fields[fieldName].Value = value;
            ShouldChangeState();
        }

        /// <summary>
        /// Activates a previously registered trigger field of the state machine
        /// </summary>
        /// <param name="fieldName">The name of the field to register</param>
        /// <param name="value">The new value to set to the field</param>
        public void SetTrigger(string fieldName)
        {
            Fields[fieldName].Value = 1.0f;
            ShouldChangeState();
        }
    }
}

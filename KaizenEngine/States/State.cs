using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaizenEngine.States
{
    public delegate void StateHandler();

    /// <summary>
    /// A state of a state machine. It has transitions that points to other states than can be transited to
    /// if the transition conditions are met
    /// </summary>
    class State
    {
        event StateHandler StateStart;                          // event triggered when the state starts
        event StateHandler StateEnd;                            // event triggered when the state ends
        public List<StateTransition> Transitions { get; set; }  // List of all the transitions of the state

        /// <summary>
        /// Initializes an instance of the State class
        /// </summary>
        public State()
        {
            Transitions = new List<StateTransition>();
        }

        /// <summary>
        /// Adds a transition to the state
        /// </summary>
        /// <param name="transition"></param>
        public virtual void AddTransition(StateTransition transition)
        {
            Transitions.Add(transition);
        }

        /// <summary>
        /// Starts the state
        /// </summary>
        public virtual void Start()
        {
            if (StateStart != null)
            {
                StateStart.Invoke();
            }
        }

        /// <summary>
        /// Update call to be used inside the game update loop
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// Stops the state
        /// </summary>
        public virtual void End()
        {
            if (StateEnd != null)
            {
                StateEnd.Invoke();
            }
        }
    }
}

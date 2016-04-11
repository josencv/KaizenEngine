using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaizenEngine.States
{
    public delegate void StateHandler();

    class State
    {
        event StateHandler StateStart;
        event StateHandler StateEnd;
        public List<StateTransition> Transitions { get; set; }

        public State()
        {
            Transitions = new List<StateTransition>();
        }

        public virtual void AddTransition(StateTransition transition)
        {
            Transitions.Add(transition);
        }

        public virtual void Start()
        {
            if (StateStart != null)
            {
                StateStart.Invoke();
            }
        }

        public virtual void End()
        {
            if (StateEnd != null)
            {
                StateEnd.Invoke();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaizenEngine.States
{
    class StateTransition
    {
        public State From { get; set; }
        public State To { get; set; }
        private List<TransitionCondition> conditions;

        public StateTransition(State from, State to)
        {
            conditions = new List<TransitionCondition>();
            From = from;
            To = to;
        }

        public void AddCondition(TransitionCondition condition)
        {
            conditions.Add(condition);
        }

        public bool ShouldApplyTransition(Dictionary<string, StateMachineField> fields)
        {
            foreach (TransitionCondition condition in conditions)
            {
                if (!condition.IsConditionMet(fields[condition.FieldName].Value))
                {
                    return false;
                }
            }

            return true;
        }

        public void Apply()
        {
            To.Start();
        }
    }
}

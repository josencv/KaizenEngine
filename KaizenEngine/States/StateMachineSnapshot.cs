using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaizenEngine.States
{
    /// <summary>
    /// A snapshot of the fields of a stateMachine
    /// TODO: currently this class is not used anywhere, but it may be used in the future
    /// </summary>
    class StateMachineSnapshot
    {
        public List<StateMachineField> Fields { get; }

        public StateMachineSnapshot()
        {
            Fields = new List<StateMachineField>();
        }

        public void AddField(StateMachineField field)
        {
            Fields.Add(field);
        }
    }
}

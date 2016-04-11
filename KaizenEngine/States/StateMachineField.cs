using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaizenEngine.States
{
    enum StateMachineFieldType { Float, Int, Bool, Trigger }

    class StateMachineField
    {
        public StateMachineFieldType Type { get; set; }
        public float Value { get; set; }
    }
}

using System;

namespace KaizenEngine.States
{
    enum ConditionOperator { False = 0, True = 1, Greater, Less, Equal, NotEqual, None }

    class TransitionCondition
    {
        public StateMachineFieldType ConditionType { get; set; }
        public ConditionOperator ConditionOperator { get; set; }
        public string FieldName { get; set; }
        public float OperationValue { get; set; }

        public TransitionCondition(StateMachineFieldType conditionType, ConditionOperator conditionOperator, string fieldName, float operationValue)
        {
            ConditionType = conditionType;
            ConditionOperator = conditionOperator;
            FieldName = fieldName;
            OperationValue = operationValue;

            CheckOperationCorrectness();
        }

        /// <summary>
        /// Check if the TransitionCondition is met
        /// </summary>
        /// <param name="value">The value to compare (left operand of the operation for '')</param>
        /// <returns>True if the condition is met. False otherwise</returns>
        public bool IsConditionMet(float value)
        {
            bool conditionMet = false;

            switch (ConditionType)
            {
                case StateMachineFieldType.Float:
                    conditionMet = IsFloatConditionMet(value);
                    break;
                case StateMachineFieldType.Int:
                    conditionMet = IsIntConditionMet((int)value);
                    break;
                case StateMachineFieldType.Bool:
                    conditionMet = ((int)value == (int)ConditionOperator);
                    break;
                case StateMachineFieldType.Trigger:
                    conditionMet = ((int)value == 1);
                    break;
            }

            return conditionMet;
        }

        /// <summary>
        /// Check if the condition is met for a 'float' ConditionType
        /// </summary>
        /// <param name="value">The value to compare (left operand of the operation)</param>
        /// <returns>True if the condition is met. False otherwise</returns>
        private bool IsFloatConditionMet(float value)
        {
            bool conditionMet = false;
            float rightOperand = OperationValue;
            if (ConditionOperator == ConditionOperator.Greater)
            {
                conditionMet = (value > rightOperand);
            }
            else if (ConditionOperator == ConditionOperator.Less)
            {
                conditionMet = (value < rightOperand);
            }

            return conditionMet;
        }

        /// <summary>
        /// Check if the condition is met for an 'int' ConditionType
        /// </summary>
        /// <param name="value">The value to compare (left operand of the operation)</param>
        /// <returns>True if the condition is met. False otherwise</returns>
        private bool IsIntConditionMet(int value)
        {
            bool conditionMet = false;
            int rightOperand = (int)OperationValue;

            if (ConditionOperator == ConditionOperator.Greater)
            {
                conditionMet = (value > rightOperand);
            }
            else if (ConditionOperator == ConditionOperator.Less)
            {
                conditionMet = (value < rightOperand);
            }
            else if (ConditionOperator == ConditionOperator.Equal)
            {
                conditionMet = (value == rightOperand);
            }
            else
            {
                conditionMet = (value != rightOperand);
            }

            return conditionMet;
        }

        /// <summary>
        /// Checks if the TransitionCondition has been constructed correctly. For example, a ConditionType 'float' can not have
        /// a 'False' ConditionOperator because they are not strictly comparable
        /// </summary>
        private void CheckOperationCorrectness()
        {
            ArgumentException exception;
            switch (ConditionType)
            {
                case StateMachineFieldType.Float:
                    if (ConditionOperator == ConditionOperator.Equal ||
                        ConditionOperator == ConditionOperator.NotEqual ||
                        ConditionOperator == ConditionOperator.True ||
                        ConditionOperator == ConditionOperator.False)
                    {
                        exception = new ArgumentException("Invalid condition operator for float type (Hint: only 'Greater' and 'Less'  valid)");
                    }
                    break;
                case StateMachineFieldType.Int:
                    if (ConditionOperator == ConditionOperator.True ||
                        ConditionOperator == ConditionOperator.False)
                    {
                        exception = new ArgumentException("Invalid condition operator for int type (Hint: only 'Greater' and 'Less', 'Equal' and 'NotEqual' are valid)");
                    }
                    break;
                case StateMachineFieldType.Bool:
                    if (ConditionOperator == ConditionOperator.Equal ||
                        ConditionOperator == ConditionOperator.NotEqual ||
                        ConditionOperator == ConditionOperator.Greater ||
                        ConditionOperator == ConditionOperator.Less)
                    {
                        exception = new ArgumentException("Invalid condition operator for bool type (Hint: only 'True' and 'False' are valid)");
                    }
                    break;
                case StateMachineFieldType.Trigger:
                    if (ConditionOperator != ConditionOperator.None)
                    {
                        exception = new ArgumentException("Invalid condition operator for trigger type (only 'None' is valid)");
                    }
                    break;
                default:
                    break;
            }
        }

    }
}

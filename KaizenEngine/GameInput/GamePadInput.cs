using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace KaizenEngine.GameInput
{
    class GamePadInput : GameInput
    {
        private GamePadState currentState;
        private GamePadState previousState;
        private Dictionary<Buttons, GameInputButton> buttonMapper;          // GamePad button to GameInputButton mapper dictionary
        private Dictionary<GamePadThumbSticks, GameInputAxis> axisMapper;   // GamePad axis to GameInputAxis plus axis value mapper dictionary
        private int gamePadInputNumber;

        public GamePadInput(PlayerInputNumber playerInputNumber, int gamePadInputNumber)
        : base(GameInputType.GamePad, playerInputNumber)
        {
            buttonMapper = new Dictionary<Buttons, GameInputButton>();
            axisMapper = new Dictionary<GamePadThumbSticks, GameInputAxis>();
            this.playerInputNumber = playerInputNumber;
            this.gamePadInputNumber = gamePadInputNumber;
            RegisterInputs();
        }

        /// <summary>
        /// Maps all the game inputs to a gamepad button
        /// </summary>
        private void RegisterInputs()
        {
            buttonMapper.Add(Buttons.A, GameInputButton.A);
            buttonMapper.Add(Buttons.B, GameInputButton.B);
            buttonMapper.Add(Buttons.Y, GameInputButton.Y);
            buttonMapper.Add(Buttons.X, GameInputButton.X);
            buttonMapper.Add(Buttons.LeftShoulder, GameInputButton.L1);
            buttonMapper.Add(Buttons.RightShoulder, GameInputButton.R1);
            buttonMapper.Add(Buttons.LeftTrigger, GameInputButton.L2);
            buttonMapper.Add(Buttons.RightTrigger, GameInputButton.R2);
            buttonMapper.Add(Buttons.LeftStick, GameInputButton.LeftStick);
            buttonMapper.Add(Buttons.RightStick, GameInputButton.RightStick);
            buttonMapper.Add(Buttons.Start, GameInputButton.Start);
            buttonMapper.Add(Buttons.Back, GameInputButton.Back);
            buttonMapper.Add(Buttons.DPadUp, GameInputButton.DPadUp);
            buttonMapper.Add(Buttons.DPadLeft, GameInputButton.DPadLeft);
            buttonMapper.Add(Buttons.DPadDown, GameInputButton.DPadDown);
            buttonMapper.Add(Buttons.DPadRight, GameInputButton.DPadRight);
        }

        public override void UpdateState()
        {
            previousState = currentState;
            currentState = GamePad.GetState(gamePadInputNumber);

            // Update button states
            foreach (KeyValuePair<Buttons, GameInputButton> entry in buttonMapper)
            {
                if (!previousState.IsButtonDown(entry.Key) && currentState.IsButtonDown(entry.Key))
                {
                    currentButtonState[entry.Value] = GameInputButtonState.Pressed;
                }
                else if (previousState.IsButtonDown(entry.Key) && !currentState.IsButtonDown(entry.Key))
                {
                    currentButtonState[entry.Value] = GameInputButtonState.Released;
                }
                else if (currentState.IsButtonDown(entry.Key))
                {
                    currentButtonState[entry.Value] = GameInputButtonState.Down;
                }
                else
                {
                    currentButtonState[entry.Value] = GameInputButtonState.Up;
                }
            }

            // Update the axis values
            float[] leftStickValues = new float[2];
            float[] rightStickValues = new float[2];
            leftStickValues[0] = currentState.ThumbSticks.Left.X;
            leftStickValues[1] = currentState.ThumbSticks.Left.Y;
            rightStickValues[0] = currentState.ThumbSticks.Right.X;
            rightStickValues[1] = currentState.ThumbSticks.Right.Y;

            // Updates the GameInput stick (axis) states
            currentStickState[GameInputStick.Left] = leftStickValues;
            currentStickState[GameInputStick.Right] = rightStickValues;
        }
    }
}

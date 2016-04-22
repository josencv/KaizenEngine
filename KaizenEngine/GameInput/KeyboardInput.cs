using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace KaizenEngine.GameInput
{
    /// <summary>
    /// Represents the maximum and minimum posible values for an input axis. It is used to map the keyboard buttons
    /// to an axis with a value.
    /// </summary>
    public enum GameInputAxisValue { MaxValue = 1, MinValue = -1 }

    /// <summary>
    /// Struct to save a pair of GameInputAxis and a value to do the correct mapping
    /// from keyboard key to controller axis with value (positive means up or right, 
    /// negative left or down, depending on the axis)
    /// </summary>
    public struct GameInputAxisValuePair
    {
        public GameInputAxis GameInputAxis { get; set; }
        public float Value { get; set; }

        public GameInputAxisValuePair(GameInputAxis axis, float value)
        {
            this.GameInputAxis = axis;
            this.Value = value;
        }
    }

    /// <summary>
    /// Derived class of the GameInput class. Represents a keyboard input
    /// </summary>
    class KeyboardInput : GameInput
    {
        private Dictionary<Keys, GameInputButton> buttonMapper;          // Keyboard key to GameInputButton mapper dictionary
        private Dictionary<Keys, GameInputAxisValuePair> axisMapper;     // Keyboard key to GameInputAxis plus axis value mapper dictionary
        private KeyboardState currentState;
        private KeyboardState previousState;

        public KeyboardInput(PlayerIndex playerIndex)
        : base(GameInputType.Keyboard, playerIndex)
        {
            buttonMapper = new Dictionary<Keys, GameInputButton>();
            axisMapper = new Dictionary<Keys, GameInputAxisValuePair>();
            RegisterInputs();
        }

        /// <summary>
        /// Maps all the game inputs to a keyboard key
        /// </summary>
        private void RegisterInputs()
        {
            buttonMapper.Add(Keys.A, GameInputButton.A);
            buttonMapper.Add(Keys.S, GameInputButton.B);
            buttonMapper.Add(Keys.D, GameInputButton.Y);
            buttonMapper.Add(Keys.W, GameInputButton.X);
            buttonMapper.Add(Keys.Q, GameInputButton.L1);
            buttonMapper.Add(Keys.E, GameInputButton.R1);
            buttonMapper.Add(Keys.D1, GameInputButton.L2);
            buttonMapper.Add(Keys.D4, GameInputButton.R2);
            buttonMapper.Add(Keys.D2, GameInputButton.LeftStick);
            buttonMapper.Add(Keys.D3, GameInputButton.RightStick);
            buttonMapper.Add(Keys.Enter, GameInputButton.Start);
            buttonMapper.Add(Keys.Space, GameInputButton.Back);
            buttonMapper.Add(Keys.I, GameInputButton.DPadUp);
            buttonMapper.Add(Keys.J, GameInputButton.DPadLeft);
            buttonMapper.Add(Keys.K, GameInputButton.DPadDown);
            buttonMapper.Add(Keys.L, GameInputButton.DPadRight);

            axisMapper.Add(Keys.Up, new GameInputAxisValuePair(GameInputAxis.LeftStickY, (float)GameInputAxisValue.MaxValue));
            axisMapper.Add(Keys.Down, new GameInputAxisValuePair(GameInputAxis.LeftStickY, (float)GameInputAxisValue.MinValue));
            axisMapper.Add(Keys.Right, new GameInputAxisValuePair(GameInputAxis.LeftStickX, (float)GameInputAxisValue.MaxValue));
            axisMapper.Add(Keys.Left, new GameInputAxisValuePair(GameInputAxis.LeftStickX, (float)GameInputAxisValue.MinValue));
            axisMapper.Add(Keys.NumPad8, new GameInputAxisValuePair(GameInputAxis.RightStickY, (float)GameInputAxisValue.MaxValue));
            axisMapper.Add(Keys.NumPad4, new GameInputAxisValuePair(GameInputAxis.RightStickY, (float)GameInputAxisValue.MinValue));
            axisMapper.Add(Keys.NumPad5, new GameInputAxisValuePair(GameInputAxis.RightStickX, (float)GameInputAxisValue.MaxValue));
            axisMapper.Add(Keys.NumPad6, new GameInputAxisValuePair(GameInputAxis.RightStickX, (float)GameInputAxisValue.MinValue));
        }

        /// <summary>
        /// Updates the GmaeInput state based of this input state
        /// </summary>
        public override void UpdateState()
        {
            // Update the keyboard states
            previousState = currentState;
            currentState = Keyboard.GetState();
            // Triggers button down game events
            foreach (KeyValuePair<Keys, GameInputButton> entry in buttonMapper)
            {
                if (previousState.IsKeyUp(entry.Key) && currentState.IsKeyDown(entry.Key))
                {
                    currentButtonState[entry.Value] = GameInputButtonState.Pressed;
                }
                else if (previousState.IsKeyUp(entry.Key) && currentState.IsKeyDown(entry.Key))
                {
                    currentButtonState[entry.Value] = GameInputButtonState.Released;
                }
                else if (currentState.IsKeyDown(entry.Key))
                {
                    currentButtonState[entry.Value] = GameInputButtonState.Down;
                }
                else
                {
                    currentButtonState[entry.Value] = GameInputButtonState.Up;
                }
            }

            float[] leftStickValues = new float[2];
            float[] rightStickValues = new float[2];

            // Triggers axis actions
            foreach (KeyValuePair<Keys, GameInputAxisValuePair> entry in axisMapper)
            {
                if (currentState.IsKeyDown(entry.Key))
                {
                    switch (entry.Value.GameInputAxis)
                    {
                        case GameInputAxis.LeftStickX:
                            leftStickValues[0] = entry.Value.Value;
                            break;
                        case GameInputAxis.LeftStickY:
                            leftStickValues[1] = entry.Value.Value;
                            break;
                        case GameInputAxis.RightStickX:
                            rightStickValues[0] = entry.Value.Value;
                            break;
                        case GameInputAxis.RightStickY:
                            rightStickValues[1] = entry.Value.Value;
                            break;
                    }
                }
            }

            // Updates the GameInput stick (axis) states
            currentStickState[GameInputStick.Left] = leftStickValues;
            currentStickState[GameInputStick.Right] = rightStickValues;

        }
    }
}

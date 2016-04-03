using System;
using System.Collections.Generic;

namespace KaizenEngine.GameInput
{
    /// <summary>
    /// Enummerates the posible game input types, like an Xbox controller or the keyboard
    /// </summary>
    public enum GameInputType { Xbox, Keyboard, GamePad}

    /// <summary>
    /// Ennumerates the posibles contexts in which a GameInput can be in.
    /// For every context, the behavior of the GameInput can differ from another.
    /// Note: each GameInput context is independent from the others
    /// </summary>
    public enum GameInputContext { Loading, InGame, Pause, MainMenu }

    /// <summary>
    /// Enummerates the posible game button states
    /// </summary>
    enum GameInputButtonState { Up, Down, Pressed, Released }

    /// <summary>
    /// Enumerates all the game buttons available. Any input source should be mapped to these game buttons
    /// (like an xbox controller or a keyboard)
    /// </summary>
    public enum GameInputButton { A, B, X, Y, Start, Back, L1, L2, R1, R2, DPadUp, DPadLeft, DPadDown, DPadRight, LeftStick, RightStick, None }

    /// <summary>
    /// Enumerates all the game axis available. Any input source should be mapped to these game axis
    /// (like an xbox controller or a keyboard)
    /// </summary>
    public enum GameInputAxis { LeftStickX, LeftStickY, RightStickX, RightStickY, None }

    /// <summary>
    /// Represents a game signal that can be triggered by a GameInput button
    /// </summary>
    public delegate void ButtonSignal();

    /// <summary>
    /// Represents a game signal that can be triggered by a stick (axis)
    /// </summary>
    public delegate void StickSignal(float valueX, float valueY);

    /// <summary>
    /// Used to differentiate the left from the right stick from the controller
    /// </summary>
    public enum GameInputStick { Left, Right }

    /// <summary>
    /// The player input index in the game (Not necessarily related to the index of the GamePads connected)
    /// </summary>
    public enum PlayerInputNumber { Player1 = 1, Player2 = 2, Player3 = 3, Player4 = 4 }

    abstract class GameInput
    {
        protected GameInputType type;                                                   // Keyboard or Xbox
        protected PlayerInputNumber playerInputNumber;                                  // Player input number to differenciate from other player inputs
        protected GameInputContext currentContext;                                      // The current context in which the input is in
        protected Dictionary<GameInputButton, GameInputButtonState> currentButtonState; // Saves the current state of the GameInput buttons
        protected Dictionary<GameInputButton, float> holdTime;                          // Stores the time elapsed since every button was pressed
        protected Dictionary<GameInputStick, float[]> currentStickState;                // Stores the state of the GameInput sticks (composed axis)

        // List of all game events that can be triggered by the game input
        public event ButtonSignal PlayerAttackSignal;
        public event StickSignal PlayerMoveSignal;

        public GameInput(GameInputType type, PlayerInputNumber playerInputNumber)
        {
            this.type = type;
            this.playerInputNumber = playerInputNumber;
            this.currentContext = GameInputContext.InGame;  // To be changed to MainMenu or something like that
            currentButtonState = new Dictionary<GameInputButton, GameInputButtonState>();
            currentStickState = new Dictionary<GameInputStick, float[]>();
            holdTime = new Dictionary<GameInputButton, float>();

            InitializeControllerState();
        }

        /// <summary>
        /// Initializes the state for first time use
        /// </summary>
        private void InitializeControllerState()
        {
            currentButtonState.Add(GameInputButton.A, GameInputButtonState.Released);
            currentButtonState.Add(GameInputButton.B, GameInputButtonState.Released);
            currentButtonState.Add(GameInputButton.Y, GameInputButtonState.Released);
            currentButtonState.Add(GameInputButton.X, GameInputButtonState.Released);
            currentButtonState.Add(GameInputButton.L1, GameInputButtonState.Released);
            currentButtonState.Add(GameInputButton.L2, GameInputButtonState.Released);
            currentButtonState.Add(GameInputButton.R1, GameInputButtonState.Released);
            currentButtonState.Add(GameInputButton.R2, GameInputButtonState.Released);
            currentButtonState.Add(GameInputButton.Start, GameInputButtonState.Released);
            currentButtonState.Add(GameInputButton.Back, GameInputButtonState.Released);
            currentButtonState.Add(GameInputButton.LeftStick, GameInputButtonState.Released);
            currentButtonState.Add(GameInputButton.RightStick, GameInputButtonState.Released);
            currentButtonState.Add(GameInputButton.DPadUp, GameInputButtonState.Released);
            currentButtonState.Add(GameInputButton.DPadLeft, GameInputButtonState.Released);
            currentButtonState.Add(GameInputButton.DPadDown, GameInputButtonState.Released);
            currentButtonState.Add(GameInputButton.DPadRight, GameInputButtonState.Released);

            currentStickState.Add(GameInputStick.Left, new float[2]);
            currentStickState.Add(GameInputStick.Right, new float[2]);
        }

        /// <summary>
        /// Updates the GameInput button and axis states.
        /// </summary>
        public abstract void UpdateState();

        /// <summary>
        /// Gets the state of the button passed.
        /// </summary>
        /// <param name="button">GameInput button type to be checked</param>
        /// <returns></returns>
        public GameInputButtonState GetButtonState(GameInputButton button)
        {
            return currentButtonState[button];
        }

        public void FireButtonDownEvents()
        {
            if (currentContext == GameInputContext.InGame)
            {
                if (currentButtonState[GameInputButton.A] == GameInputButtonState.Down)
                {
                    if (PlayerAttackSignal != null)
                    {
                        PlayerAttackSignal.Invoke();
                    }
                }
            }
        }

        /// <summary>
        /// Sends the specified stick signal (X and Y axis values) to the subscribers, depending on the passed GameInputContext.
        /// </summary>
        /// <param name="stick">Type of stick to check</param>
        public void FireStickEvent(GameInputStick stick)
        {
            if (currentContext == GameInputContext.InGame)
            {
                if (stick == GameInputStick.Left)
                {
                    if (PlayerMoveSignal != null)
                        PlayerMoveSignal.Invoke(currentStickState[GameInputStick.Left][0], currentStickState[GameInputStick.Left][1]);
                }

                if (stick == GameInputStick.Right)
                {
                    if (PlayerMoveSignal != null)
                        PlayerMoveSignal.Invoke(currentStickState[GameInputStick.Right][0], currentStickState[GameInputStick.Right][1]);
                }
            }
        }

        /// <summary>
        /// Sends all signals to the different game components, depending on the current GameInput context
        /// </summary>
        public void SendAllSignals()
        {
            FireButtonDownEvents();
            FireStickEvent(GameInputStick.Left);
            FireStickEvent(GameInputStick.Right);
        }

        // Properties ==========================================================

        public PlayerInputNumber PlayerInputNumber { get { return playerInputNumber; } }

    }
}

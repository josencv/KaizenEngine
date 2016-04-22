using System.Collections.Generic;

namespace KaizenEngine.GameInput
{
    /// <summary>
    /// Responsible of listening and initializing new inputs to be used in the game
    /// </summary>
    class GameInputManager
    {
        private List<GameInput> gameInputs;     // List of currently connected / accepted game inputs in the game

        /// <summary>
        /// Initializes the game input manager
        /// </summary>
        public void InitializeGameInputs()
        {
            gameInputs = new List<GameInput>();

            GameInput gameInput = new KeyboardInput(PlayerIndex.Player1);
            gameInputs.Add(gameInput);
        }

        /// <summary>
        /// Gets a game input given a player index
        /// </summary>
        /// <param name="player">The index of the player to get the input for</param>
        /// <returns></returns>
        public GameInput GetGameInput(PlayerIndex player)
        {
            GameInput match = null;
            foreach (GameInput input in gameInputs)
            {
                if (input.PlayerIndex == player)
                {
                    match = input;
                    break;
                }
            }

            return match;
        }

        /// <summary>
        /// Updates the state for every registered game input
        /// </summary>
        public void ProcessGameInputs()
        {
            foreach (GameInput gameInput in gameInputs)
            {
                gameInput.UpdateState();
                gameInput.SendAllSignals();
            }
        }
    }
}

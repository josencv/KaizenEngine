using System.Collections.Generic;

namespace KaizenEngine.GameInput
{
    class GameInputManager
    {
        private List<GameInput> gameInputs;

        public void InitializeGameInputs()
        {
            gameInputs = new List<GameInput>();

            GameInput gameInput = new KeyboardInput(PlayerInputNumber.Player1);
            gameInputs.Add(gameInput);
        }

        /// <summary>
        /// Gets a game input given a player index
        /// </summary>
        /// <param name="player">The index of the player to get the input for</param>
        /// <returns></returns>
        public GameInput GetGameInput(PlayerInputNumber player)
        {
            GameInput match = null;
            foreach (GameInput input in gameInputs)
            {
                if (input.PlayerInputNumber == player)
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

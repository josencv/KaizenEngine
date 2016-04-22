using Microsoft.Xna.Framework;
using KaizenEngine.Animations;
using KaizenEngine.Sprites;

namespace KaizenEngine.States
{
    /// <summary>
    /// Specialization of the StateMachine class. Allows to use DrawableStates states instead of normal states.
    /// </summary>
    class DrawableStateMachine : StateMachine
    {
        private Animator animator;  // The animator of the state machine

        /// <summary>
        /// Initializes an instance of the DrawableStateMachine class
        /// </summary>
        /// <param name="spriteRenderer">The sprite renderer of the animator</param>
        public DrawableStateMachine(SpriteRenderer spriteRenderer) : base()
        {
            animator = new Animator(spriteRenderer);
        }

        /// <summary>
        /// Adds a state to the list of the states of the state machine
        /// </summary>
        /// <param name="state">The state to add</param>
        public void AddState(DrawableState state)
        {
            base.AddState(state);
            state.Animator = animator;
        }

        /// <summary>
        /// Update call to be used inside the game update loop
        /// </summary>
        /// <param name="delta">Time since last cycle</param>
        /// <param name="position">New position where to render the animation. TODO: change position to a transform component</param>
        public override void Update(GameTime gameTime, Vector2 position)
        {
            base.Update(gameTime, position);
            animator.Update(gameTime, position);
        }

        /// <summary>
        /// Checks if any transition of the current state meets the requirements to be applied
        /// </summary>
        public void Draw(Vector2 position)
        {
            if (CurrentState.GetType() == typeof(DrawableState))
            {
                animator.Draw();
            }
        }
    }
}

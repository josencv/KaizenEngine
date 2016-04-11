using Microsoft.Xna.Framework;
using KaizenEngine.Animations;
using KaizenEngine.Sprites;

namespace KaizenEngine.States
{
    class DrawableStateMachine : StateMachine
    {
        private Animator animator;

        public DrawableStateMachine(SpriteRenderer spriteRenderer) : base()
        {
            animator = new Animator(spriteRenderer);
        }

        public void AddState(DrawableState state)
        {
            base.AddState(state);
            state.Animator = animator;
        }

        public override void Update(GameTime gameTime, Vector2 position)
        {
            base.Update(gameTime, position);
            animator.Update(gameTime, position);
        }

        public void Draw(Vector2 position)
        {
            if (CurrentState.GetType() == typeof(DrawableState))
            {
                animator.Draw();
            }
        }
    }
}

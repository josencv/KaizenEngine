using System;
using Microsoft.Xna.Framework;
using KaizenEngine.Sprites;

namespace KaizenEngine.Animations
{
    enum AnimatorState { Playing, Stopped }

    /// <summary>
    /// Play animations (Animation2D class)
    /// </summary>
    class Animator
    {
        private const float animationBaseStep = (1000.0f / 10.0f);  // The animation speed base step. 10 frames per second
        private SpriteRenderer spriteRenderer;
        private Animation2D currentAnimation;
        private bool loop;
        private float timeEllapsed;                 // The time ellapsed since animation start
        public AnimatorState State { get; set; }
        public Vector2 Position { get; set; }   // TODO: move somewhere else (transform object?)

        /// <summary>
        /// Initializes a new instance of the Animator class
        /// </summary>
        /// <param name="spriteRenderer">The game sprite renderer</param>
        public Animator(SpriteRenderer spriteRenderer) 
        {
            this.spriteRenderer = spriteRenderer;
            State = AnimatorState.Stopped;
            timeEllapsed = 0;
            loop = false;
        }

        /// <summary>
        /// Plays an Animation2D instance
        /// </summary>
        /// <param name="animation">The animation to play</param>
        /// <param name="loop">Should loop animation</param>
        public void Play(Animation2D animation, bool loop = false)
        {
            this.loop = loop;
            currentAnimation = animation;
            State = AnimatorState.Playing;
            timeEllapsed = 0;
        }

        /// <summary>
        /// Changes the animation being played
        /// </summary>
        /// <param name="animation">The new animation to play</param>
        /// <param name="loop">Should loop animation</param>
        public void Change(Animation2D animation, bool loop = false)
        {
            Stop();
            Play(animation, loop);
        }

        /// <summary>
        /// Stops the current animation
        /// </summary>
        public void Stop()
        {
            State = AnimatorState.Stopped;
        }

        /// <summary>
        /// Update call to be used inside the game update loop
        /// </summary>
        /// <param name="delta">Time since last cycle</param>
        /// <param name="position">New position where to render the animation. TODO: change position to a transform component</param>
        public void Update(GameTime delta, Vector2 position)
        {
            if (State == AnimatorState.Playing)
            {
                Position = position;
                timeEllapsed += delta.ElapsedGameTime.Milliseconds;
            }
        }

        /// <summary>
        /// Animator draw call. Draws the current frame of the animation (if animation is being played).
        /// </summary>
        public void Draw()
        {
            if (State == AnimatorState.Playing)
            {
                int index = (int)(timeEllapsed / (animationBaseStep * currentAnimation.AnimationSpeed)) % currentAnimation.Count;
                spriteRenderer.Draw(currentAnimation.GetFrame(index), Position, scale: 3);

                if (!loop && index == currentAnimation.Count - 1)
                {
                    State = AnimatorState.Stopped;
                }
            }
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using KaizenEngine.Sprites;

namespace KaizenEngine.Animations
{
    enum AnimatorState { Playing, Stopped }

    class Animator
    {
        private SpriteRenderer spriteRenderer;
        private Animation2D currentAnimation;
        private bool loop;
        private float timeEllapsed;                 // The time ellapsed since animation start
        public AnimatorState State { get; set; }
        public Vector2 Position { get; set; }

        public Animator(SpriteRenderer spriteRenderer) 
        {
            this.spriteRenderer = spriteRenderer;
            State = AnimatorState.Stopped;
            timeEllapsed = 0;
            loop = false;
        }

        public void Play(Animation2D animation, bool loop = false)
        {
            this.loop = loop;
            currentAnimation = animation;
            State = AnimatorState.Playing;
            timeEllapsed = 0;
        }

        public void Stop()
        {
            State = AnimatorState.Stopped;
        }

        public void Update(GameTime delta, Vector2 position)
        {
            if (State == AnimatorState.Playing)
            {
                Position = position;
                timeEllapsed += delta.ElapsedGameTime.Milliseconds;
            }
        }

        public void Draw()
        {
            if (State == AnimatorState.Playing)
            {
                int index = (int)(timeEllapsed / ((1000.0f / 10.0f) * currentAnimation.AnimationSpeed)) % currentAnimation.Count;
                spriteRenderer.Draw(currentAnimation.GetFrame(index), Position, scale: 3);

                if (!loop && index == currentAnimation.Count - 1)
                {
                    State = AnimatorState.Stopped;
                }
            }
        }
    }
}

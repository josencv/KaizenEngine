using System;
using KaizenEngine.Animations;

namespace KaizenEngine.States
{
    class DrawableState : State
    {
        public Animation2D Animation { get; set; }
        public Animator Animator { get; set; }

        public DrawableState(Animation2D animation) : base()
        {
            Animation = animation;
        }

        public override void Start()
        {
            base.Start();
            Animator.Play(Animation, true);
        }
    }
}

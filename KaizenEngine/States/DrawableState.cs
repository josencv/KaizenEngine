using System.Collections.Generic;
using Microsoft.Xna.Framework;
using KaizenEngine.Animations;
using KaizenEngine.Helpers;

namespace KaizenEngine.States
{
    /// <summary>
    /// Type of animation selection criteria.
    /// </summary>
    enum AnimationSelectionType { None, Directional }

    /// <summary>
    /// Specialization of the State class. It adds animation per state support. Also support multiple animations per state
    /// that are selected using differente methods, as explained in this description later.
    /// 
    /// The animationSelectionFields field stores all the StateMachineField instances needed for the selected animation selection
    /// to work. Is responsability of the developer to manually add this fields after initialization.
    /// The number and the type of fields to store may vary depending on the animation selection method, as listed below:
    /// 
    /// None: no fields needed
    /// 
    /// Directional: chooses between 4 animation depending of the value of 2 values, x and y,
    /// that represents a coordinate in the cartesian plane. A field for x and for y values should be stored. (In that order),
    /// as well as 4 animations in the order: Right, Up, Left, Down.
    /// </summary>
    class DrawableState : State
    {
        private List<Animation2D> animations;                           // List of animations of the state
        private Animation2D currentAnimation;                           // The current animation being played       
        public Animator Animator { get; set; }                          // The animator. Needed to play animations.
        private AnimationSelectionType selectionType;                   // The animation selection type
        private int lastUsedAnimationIndex;                             // The index of the last animation used. Needed for some selection methods when an index cannot be resolved
        private List<StateMachineField> animationSelectionFields;       // List of fields used in the animation selection methods

        /// <summary>
        /// Initializes an instance of the DrawableState class. Use this constructor for multiple animation states.
        /// Animations and animation selection fields should be added manually after initialization.
        /// </summary>
        /// <param name="selectionType">The method to use to select animations</param>
        public DrawableState(AnimationSelectionType selectionType) : base()
        {
            this.selectionType = selectionType;
            animations = new List<Animation2D>();
            animationSelectionFields = new List<StateMachineField>();
            lastUsedAnimationIndex = 0;
        }

        /// <summary>
        /// Initializes an instance of the DrawableState class. Use this constructor for single animation states.
        /// </summary>
        /// <param name="animation"></param>
        public DrawableState(Animation2D animation) : base()
        {
            animations = new List<Animation2D>();
            animations.Add(animation);
            selectionType = AnimationSelectionType.None;
            lastUsedAnimationIndex = 0;
        }

        /// <summary>
        /// Adds an animation selection field to the state machine fields list
        /// </summary>
        /// <param name="field">State machine field to add</param>
        public void AddAnimationSelectionField(StateMachineField field)
        {
            animationSelectionFields.Add(field);
        }

        /// <summary>
        /// Adds an animation to the animations list
        /// </summary>
        /// <param name="animation"></param>
        public void AddAnimation(Animation2D animation)
        {
            animations.Add(animation);
        }

        /// <summary>
        /// Starts the state
        /// </summary>
        public override void Start()
        {
            base.Start();
            currentAnimation = SelectAnimation();
            Animator.Play(currentAnimation, true);
        }

        /// <summary>
        /// Update call to be used inside the game update loop
        /// </summary>
        public override void Update()
        {
            base.Update();
            Animation2D chosenAnimation = SelectAnimation();
            if (chosenAnimation != currentAnimation)
            {
                currentAnimation = chosenAnimation;
                Animator.Change(currentAnimation, true);
            }
        }

        /// <summary>
        /// Selects an animation based on the animation select type of the state
        /// </summary>
        /// <returns></returns>
        private Animation2D SelectAnimation()
        {
            int animationIndex = 0;
            switch (selectionType)
            {
                case AnimationSelectionType.None:
                    animationIndex = 0;
                    break;
                case AnimationSelectionType.Directional:
                    animationIndex = GetDirectionalAnimationIndex();
                    break;
            }

            lastUsedAnimationIndex = animationIndex;
            return animations[animationIndex];
        }

        /// <summary>
        /// Chooses the animation based on a (x, y) coordinate in the cartesian plane. These variables are read from a 
        /// list of StateMachineField instances.
        /// </summary>
        /// <returns></returns>
        private int GetDirectionalAnimationIndex()
        {
            double directionZone;

            Vector2 vector = new Vector2(animationSelectionFields[0].Value, animationSelectionFields[1].Value);
            directionZone = VectorMath.GetDirecionZoneFromVector(vector); // TODO: Change

            if (double.IsNaN(directionZone))
            {
                return lastUsedAnimationIndex;
            }
            else
            {
                return (int)directionZone;
            }
        }

    }
}

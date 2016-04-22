using System.Collections.Generic;
using Microsoft.Xna.Framework;
using KaizenEngine.Sprites;

namespace KaizenEngine.Animations
{
    /// <summary>
    /// Contains all the information related to a single 2D animation, such as the sprite list
    /// </summary>
    class Animation2D
    {
        private List<SpriteFrame> spriteList;
        public float AnimationSpeed { get; set; }   // Animation speed multiplier
        public Vector2 Position { get; set; }

        /// <summary>
        /// Initializes a new instance of the Animation2D class.
        /// </summary>
        /// <param name="frameList">The list of sprites that composes the animation</param>
        /// <param name="animationSpeed">The animation speed multiplier</param>
        public Animation2D(List<SpriteFrame> frameList, float animationSpeed = 1)
        {
            this.spriteList = frameList;
            AnimationSpeed = animationSpeed;
        }

        /// <summary>
        /// Gets a specific frame from the animation sprite list
        /// </summary>
        /// <param name="index">The index of the frame to get from the list</param>
        /// <returns>The frame requested</returns>
        public SpriteFrame GetFrame(int index)
        {
            return spriteList[index];
        }

        /// <summary>
        /// Count of the sprite list
        /// </summary>
        public int Count { get { return spriteList.Count; } }
    }
}

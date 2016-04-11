using System.Collections.Generic;
using Microsoft.Xna.Framework;
using KaizenEngine.Sprites;

namespace KaizenEngine.Animations
{
    class Animation2D
    {
        private List<SpriteFrame> spriteList;
        public float AnimationSpeed { get; set; }
        public Vector2 Position { get; set; }

        public Animation2D(List<SpriteFrame> frameList, float animationSpeed = 1)
        {
            this.spriteList = frameList;
            AnimationSpeed = animationSpeed;
        }

        public SpriteFrame GetFrame(int index)
        {
            return spriteList[index];
        }

        public int Count { get { return spriteList.Count; } }
    }
}

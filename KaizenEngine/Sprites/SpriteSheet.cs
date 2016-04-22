using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaizenEngine.Sprites
{
    /// <summary>
    /// Represents a sheet of SpriteFrames which can be accessed with the sprite name
    /// </summary>
    class SpriteSheet
    {
        private Dictionary<string, SpriteFrame> spriteList;

        /// <summary>
        /// Initializes an instance of the SpriteSheet class
        /// </summary>
        public SpriteSheet()
        {
            spriteList = new Dictionary<string, SpriteFrame>();
        }

        /// <summary>
        /// Adds a sprite frame into the sprite sheet
        /// </summary>
        /// <param name="name">Name of the frame added. Used as key to get the frame later</param>
        /// <param name="frame">The frame to add</param>
        public void Add(string name, SpriteFrame frame)
        {
            spriteList.Add(name, frame);
        }

        /// <summary>
        /// Gets the sprite from the sprite sheet dictionary
        /// </summary>
        /// <param name="spriteName">The sprite name of the sprite frame get</param>
        /// <returns></returns>
        public SpriteFrame GetSprite(string spriteName)
        {
            return spriteList[spriteName];
        }
    }
}

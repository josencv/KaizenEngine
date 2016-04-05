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

        public SpriteSheet()
        {
            spriteList = new Dictionary<string, SpriteFrame>();
        }

        public void Add(string name, SpriteFrame frame)
        {
            spriteList.Add(name, frame);
        }

        public SpriteFrame GetSprite(string spriteName)
        {
            return spriteList[spriteName];
        }
    }
}

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KaizenEngine.Sprites
{
    /// <summary>
    /// Represents a frame with sprite information that can be rendered by an SpriteRenderer instance.
    /// </summary>
    class SpriteFrame
    {
        /// <summary>
        /// Initializes an instance of the SpriteRenderer class.
        /// </summary>
        /// <param name="spriteName">The name of the sprite</param>
        /// <param name="texture">The texture of the sprite</param>
        /// <param name="sourceRectangle">The rectangle where is located the sprite in the texture</param>
        /// <param name="size">The size of the sprite in a Vector2 format</param>
        /// <param name="origin">The pivot point of the sprite</param>
        /// <param name="isRotated">Represents whether the sprite is rotated in 90 degrees clockwise</param>
        public SpriteFrame(string spriteName, Texture2D texture, Rectangle sourceRectangle, Vector2 size, Vector2 origin, bool isRotated = false)
        {
            this.Texture = texture;
            this.SourceRectangle = sourceRectangle;
            this.Origin = origin;
            this.Size = size;
            this.IsRotated = isRotated;
            this.SpriteName = spriteName;
        }

        public string SpriteName { get; }
        public Texture2D Texture { get; }
        public Rectangle SourceRectangle{ get; }
        public bool IsRotated{ get; }
        public Vector2 Origin { get; }
        public Vector2 Size { get; }

    }
}

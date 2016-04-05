using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KaizenEngine.Sprites
{
    class SpriteFrame
    {

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

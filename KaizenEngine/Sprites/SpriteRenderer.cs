using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KaizenEngine.Sprites
{
    /// <summary>
    /// Renders sprite frames using the Xna SpriteBatch class
    /// </summary>
    class SpriteRenderer
    {
        private const float ClockwiseNinetyDegreeRotation = (float)(Math.PI / 2.0f);    // Used to rotate sprites that were saved rotated in a spritesheet
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Initializes a new instance of the SpriteRenderer class.
        /// </summary>
        /// <param name="spriteBatch">The Xna SpriteBatch instance.</param>
        public SpriteRenderer(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        /// <summary>
        /// Draws an sprite
        /// </summary>
        /// <param name="sprite">The sprite frame to render</param>
        /// <param name="position">The position where to render the sprite</param>
        /// <param name="color">The color effect to apply to the sprite to render</param>
        /// <param name="rotation">The rotation ammount in radians to rotate the sprite</param>
        /// <param name="scale">The amount to scale the sprite</param>
        /// <param name="spriteEffects">The Xna SpriteEffect to apply to the sprite</param>
        public void Draw(SpriteFrame sprite, Vector2 position, Color? color = null, float rotation = 0, float scale = 1, SpriteEffects spriteEffects = SpriteEffects.None)
        {
            Vector2 origin = sprite.Origin;
            if (sprite.IsRotated)
            {
                rotation -= ClockwiseNinetyDegreeRotation;
                switch (spriteEffects)
                {
                    case SpriteEffects.FlipHorizontally: spriteEffects = SpriteEffects.FlipVertically; break;
                    case SpriteEffects.FlipVertically: spriteEffects = SpriteEffects.FlipHorizontally; break;
                }
            }
            switch (spriteEffects)
            {
                case SpriteEffects.FlipHorizontally: origin.X = sprite.SourceRectangle.Width - origin.X; break;
                case SpriteEffects.FlipVertically: origin.Y = sprite.SourceRectangle.Height - origin.Y; break;
            }

            this.spriteBatch.Draw(
                texture: sprite.Texture,
                position: position,
                sourceRectangle: sprite.SourceRectangle,
                color: color,
                rotation: rotation,
                origin: origin,
                scale: new Vector2(scale, scale),
                effects: spriteEffects);
        }
    }
}

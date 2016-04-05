using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KaizenEngine.Sprites
{
    class SpriteSheetLoader
    {
        private readonly ContentManager contentManager;
        private char[] invalidLineStartChars = { '#', '\n', '\t', '\r' };

        public SpriteSheetLoader(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        /// <summary>
        /// Creates a SpriteSheet instance loading the texture and the texture data with the information of each sprite.
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public SpriteSheet Load(string resourceName)
        {
            SpriteSheet sheet = new SpriteSheet();
            Texture2D texture = this.contentManager.Load<Texture2D>(resourceName);
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, this.contentManager.RootDirectory, Path.ChangeExtension(resourceName, "txt"));
            string[] lines = new string[0];

            if (File.Exists(fileName))
            {
                lines = File.ReadAllLines(fileName);
            }

            foreach (string line in lines)
            {
                if (line == "" || Array.IndexOf(invalidLineStartChars, line[0]) != -1)
                {
                    continue;
                }
                SpriteFrame frame = LoadSpriteFromLine(line, texture);
                sheet.Add(frame.SpriteName, frame);
            }

            return sheet;
        }

        public SpriteFrame LoadSpriteFromLine(string line, Texture2D texture)
        {
            string[] values = line.Split(';');

            string spriteName = values[0];
            bool isRotated = int.Parse(values[1]) == 1;

            Rectangle rectangle = new Rectangle(
                int.Parse(values[2]),
                int.Parse(values[3]),
                int.Parse(values[4]),
                int.Parse(values[5]));

            Vector2 size = new Vector2(
                int.Parse(values[6]),
                int.Parse(values[7]));

            Vector2 pivot = new Vector2(
                float.Parse(values[8]),
                float.Parse(values[9]));

            return new SpriteFrame(spriteName, texture, rectangle, size, pivot, isRotated);
        }
    }
}

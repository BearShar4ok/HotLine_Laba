using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace menu.UI
{
    class Bar
    {
        private Texture2D texture;
        private int size;
        private Rectangle sourseRectangle;

        public Bar(int size)
        {
            texture = null;
            this.size = size;
        }
        public void LoadContent(ContentManager manager, string name)
        {
            texture = manager.Load<Texture2D>(name);
        }
        public void Update(int size)
        {
            this.size = size;
        }
        public void Draw(SpriteBatch brush, Vector2 position)
        {
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y,
                texture.Width, texture.Height);
            sourseRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            for (int i = 0; i < size; i++)
            {
                brush.Draw(texture, destinationRectangle, null, Color.White);
                destinationRectangle.X += 15;
            }
        }
    }
}

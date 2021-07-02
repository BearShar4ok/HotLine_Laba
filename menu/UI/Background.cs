using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace menu.UI
{
    class Background
    {
        private Texture2D texture;
        private Rectangle rec;
        private string name;

        public Rectangle Rec { get { return rec; } set { rec = value; } }

        public Background(int width, int height, string name)
        {
            rec = new Rectangle(0, 0, width, height);
            this.name = name;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(name);
        }

        public void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, rec, Color.White);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using menu.Colitions;
using menu.SavesLoad;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace menu.PlayerPart
{
    class Map
    {
        private Texture2D map;
        private int width;
        private int height;
        private Vector2 position;
        public bool OpenNow { get; set; }
        public Map(Texture2D texture, Vector2 pos)
        {
            map = texture;
            position = pos;
            OpenNow = false;
        }
        public void LoadContent(ContentManager content, GraphicsDevice gd)
        {
            UpLoader.Load();
            UpLoader.makingMap(gd);
            width = UpLoader.width;
            height = UpLoader.height;
            map = UpLoader.textureCurrent;
        }
        public void Update(Vector2 position)
        {
            this.position = position;
            foreach (var key in UpLoader.new_dict.Values)
            {
                foreach (var value in key)
                {
                    if (value is Circle)
                    {
                        (value as Circle).Update(position);
                    }
                    else
                    {

                    }
                }
            }
        }
        public void Draw(SpriteBatch brushe)
        {
            brushe.Draw(map, position, Color.White);
            foreach (var key in UpLoader.new_dict.Values)
            {
                foreach (var value in key)
                {
                    if (value is Circle)
                    {
                        (value as Circle).DebugDraw(brushe);
                    }
                    else
                    {
                       
                    }
                }
            }
        }
    }
}

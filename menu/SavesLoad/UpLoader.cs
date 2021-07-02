
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using menu.Colitions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace menu.SavesLoad
{
    public enum Colisions { wall, glass, door, brokenWall, net, intererProhod, intererNoProhod, intererProhodCir, intererNoProhodCir }
    static class UpLoader
    {
        static private Dictionary<Colisions, List<menu.SavesLoad.BoundingBox>> dict;
        static public System.Drawing.Color[,] image2d;
        static public int width;
        static public int height;
        static public Dictionary<Colisions, List<object>> new_dict;
        static public Texture2D textureCurrent;

        static public void Load()
        {
            var loader = JsonConvert.DeserializeObject<List<dynamic>>(File.ReadAllText(@"C:\Users\bersh\Downloads\hotline_Miami\saves\S1.json"));
            //HotLine__Laba.Classes.BoundingBox
            dict = loader[0].ToObject<Dictionary<Colisions, List<menu.SavesLoad.BoundingBox>>>();
            image2d = ((JArray)loader[1]).ToObject<System.Drawing.Color[,]>();
            width = (int)loader[2];
            height = (int)loader[3];
        }
        static public void makingMap(GraphicsDevice gd)
        {
            new_dict = new Dictionary<Colisions, List<object>>();

           
            // width и height передавать через json
            Color[] frame = new Color[width * height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    byte A = Convert.ToByte(255);
                    frame[y * width + x] = new Color(image2d[x, y].R, image2d[x, y].G, image2d[x, y].B, A);
                }
            }
            textureCurrent = new Texture2D(gd, width, height);
            textureCurrent.SetData(frame);


            foreach (var key in dict.Keys)
            {
                new_dict.Add(key, new List<object>());
                foreach (var value in dict[key])
                {
                    if (value.isCircle)
                    {
                        //circle
                        new_dict[key].Add(new Circle(value.posX, value.posY, value.width, value.height));
                    }
                    else
                    {
                        //square
                        new_dict[key].Add(new Collider(new Rectangle(value.posX, value.posY, value.width, value.height)));
                    }
                }
            }
        }
    }

    public class BoundingBox
    {
        public int posX = 0, posY = 0;
        public int width, height;
        public bool isCircle = false;
        public BoundingBox(int posX, int posY, int width, int height, bool isCircle = false)
        {
            this.posX = posX;
            this.posY = posY;
            this.width = width;
            this.height = height;
            this.isCircle = isCircle;
        }
    }
}


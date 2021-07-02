using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace menu.Colitions
{
    class Collider : ICollidable
    {
        public Rectangle rec;
        public Vector2 pos;
        public Collider(Rectangle rec)
        {
            this.pos = new Vector2(rec.X, rec.Y);
            this.rec = rec;
        }

        public bool Intersects(ICollidable colliderObj)
        {
            if (colliderObj is Collider) return rec.Intersects((colliderObj as Collider).rec);
            if (colliderObj is Circle)
            {

            }
            return false;
        }

        public bool Intersects(Rectangle rect)
        {
            return rec.Intersects(rect);
        }

        public void Update(Vector2 mapPos)
        {
            rec.X = (int)(pos.X + mapPos.X);
            rec.Y = (int)(pos.Y + mapPos.Y);
        }
    }
}

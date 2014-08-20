using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sway_Chopter.Source.Obstacles
{
    public class Obstacles
    {
        List<Texture2D> textures;
        List<Vector2> locations;
        Rectangle src;
        bool flip;
        Vector2 size;
        Texture2D left;
        Texture2D right;

        Viewport viewport;

        Random r;
        public Obstacles(Viewport vp)
        {
            Initialize(vp);
        }

        private void Initialize(Viewport vp)
        {
            viewport = vp;

            size = new Vector2(viewport.Width * 0.35f, viewport.Width * 0.021875f);
            r = new Random();

            Vector2 vc1 = new Vector2(r.Next((int)(viewport.Width / 4), (int)(viewport.Width * 0.6f)), viewport.Height / 2);
            Vector2 vc2 = new Vector2(vc1.X + size.X + (viewport.Width / 7), vc1.Y);
            locations.Add(vc1);
            locations.Add(vc2);

            for (int i = 0; i < 7; i++)
            {
                //add the locations
            }
        }

        public void LoadContent(ContentManager content)
        {
            left = content.Load<Texture2D>("Steel Beam Left");
            right = content.Load<Texture2D>("Steel Beam");
        }

        public void Update(int number)
        {
            for (int i = 0; i < locations.Count; i++)
            {
                locations[i] = new Vector2(locations[i].X, locations[i].Y + number);
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
        }
    }
}


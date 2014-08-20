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

            size = new Vector2(viewport.Width, viewport.Width * 0.0625f);
            r = new Random();

            textures = new List<Texture2D>();
            locations = new List<Vector2>();

            Vector2 vc1 = new Vector2(r.Next((int)(viewport.Width / 4), (int)(viewport.Width * 0.6f)) - size.X, viewport.Height / 2);
            Vector2 vc2 = new Vector2(vc1.X + size.X + (viewport.Width / 7), vc1.Y);
            locations.Add(vc1);
            locations.Add(vc2);

            for (int i = 2; i < 7; i++)
            {
                Vector2 vc3 = new Vector2(r.Next((int)(viewport.Width / 4), (int)(viewport.Width * 0.6f)) - size.X, locations[locations.Count - 2].Y - (size.Y * 14));
                Vector2 vc4 = new Vector2(vc3.X + size.X + (viewport.Width / 3), locations[locations.Count - 2].Y - (size.Y * 14));

                locations.Add(vc3);
                locations.Add(vc4);
            }
        }

        public void LoadContent(ContentManager content)
        {
            left = content.Load<Texture2D>("Steel Beam Left");
            right = content.Load<Texture2D>("Steel Beam");

            for (int i = 0; i < 7; i++)
            {
                textures.Add(left);
                textures.Add(right);
            }
        }

        public void Update(int number)
        {
            for (int i = 0; i < locations.Count; i++)
            {
                locations[i] = new Vector2(locations[i].X, locations[i].Y + number);
            }

            if (locations[0].Y > viewport.Height)
            {
                locations.RemoveAt(0);
                locations.RemoveAt(0);

                Vector2 vc3 = new Vector2(r.Next((int)(viewport.Width / 4), (int)(viewport.Width * 0.6f)) - size.X, locations[locations.Count - 2].Y - (size.Y * 14));
                Vector2 vc4 = new Vector2(vc3.X + size.X + (viewport.Width / 3), locations[locations.Count - 2].Y - (size.Y * 14));

                locations.Add(vc3);
                locations.Add(vc4);
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            for (int i = 0; i < 9; i++)
            {
                spritebatch.Draw(textures[i], new Rectangle((int)locations[i].X, (int)locations[i].Y, (int)size.X, (int)size.Y), Color.White);
            }
        }
    }
}


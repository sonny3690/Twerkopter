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
    public class WreckingBall
    {
        Texture2D texture;
        Vector2 location;

        bool flip;
        Vector2 size;

        Viewport viewport;

        Random r;


        int cx;
        int cy;
        int x;
        int y;
        double time;

        const double speed = 1;
        const int ssize = 2;
        const int chainLength = 4;

        public WreckingBall(Viewport vp, int side, Vector2 loc, Vector2 ssize, ContentManager c)
        {
            Initialize(vp, side, loc, ssize, c);
        }

        private void Initialize(Viewport vp, int side, Vector2 loc, Vector2 ssizes, ContentManager c)
        {
            viewport = vp;

            size = new Vector2(viewport.Width / 8, viewport.Width / 4);

            if (side == 1)
            {
                float point = loc.X + ssizes.X;
                location = new Vector2(point - ((ssizes.X / 512) * 15), loc.Y + ((ssizes.Y / 32) * 10));
            }

            if (side == 2)
            {
                float point = loc.X;
                location = new Vector2(point + ((ssizes.X / 512) * 14), loc.Y + ((ssizes.Y / 32) * 10));
            }

            LoadContent(c);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Wrecking Ball");
        }

        public void Update(int number)
        {
            location = new Vector2(location.X, location.Y + number);
        }

        public void Draw(SpriteBatch spritebatch, float rotation)
        {
            spritebatch.Draw(texture, new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), null, Color.White, rotation, new Vector2(size.X / 2, 0), SpriteEffects.None, 0f);
        }

        /*
        bool Collides(Rectangle r)
        {
            double cdx = Math.Abs(x - r.x);
            double cdy = Math.Abs(y - r.y);

            if (cdx > (r.Width / 2 + circle.r)) return false;
            if (cdy > (r.Height / 2 + circle.r)) return false;

            if (cdx <= (r.Width / 2)) return true;
            if (cdy <= (r.Height / 2)) return true;

            double cornerDistance_sq = (cdx - r.wIdth / 2) ^ 2 +
                                 (cdy - r.Height / 2) ^ 2;

            return (cornerDistance_sq <= (circle.r ^ 2));
        }
         * */
    }
}


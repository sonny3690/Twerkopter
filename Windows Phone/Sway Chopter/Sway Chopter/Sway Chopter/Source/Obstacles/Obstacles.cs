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
        public List<Vector2> locations;
        public List<WreckingBall> WreckingBalls;
        public List<bool> didPass;

        bool flip;
        public Vector2 size;
        Texture2D left;
        Texture2D right;

        float passPoint;

        Viewport viewport;

        Random r;
        ContentManager c;

        float rotation = 1f;
        float rotationVelocity = 0.005f;
        float rotationVelocitysAdder = 0.002f;
        bool pos = true;

        public Obstacles(Viewport vp)
        {
            Initialize(vp);
        }

        private void Initialize(Viewport vp)
        {
            viewport = vp;

            passPoint = viewport.Height - (viewport.Width * 0.25f * 1.56f * 1.5f);

            size = new Vector2(viewport.Width, viewport.Width * 0.0625f);
            r = new Random();

            textures = new List<Texture2D>();
            locations = new List<Vector2>();
            didPass = new List<Boolean>();
            WreckingBalls = new List<WreckingBall>();
        }

        public void LoadContent(ContentManager content)
        {
            c = content;
            left = content.Load<Texture2D>("Steel Beam Left");
            right = content.Load<Texture2D>("Steel Beam");

            for (int i = 0; i < 7; i++)
            {
                textures.Add(left);
                textures.Add(right);
            }

            Vector2 vc1 = new Vector2(r.Next((int)(viewport.Width / 20), (int)(viewport.Width * 0.4f)) - size.X, size.Y * 3);
            Vector2 vc2 = new Vector2(vc1.X + size.X + (viewport.Width * 0.6f), vc1.Y);
            locations.Add(vc1);

            locations.Add(vc2);
            WreckingBalls.Add(new WreckingBall(viewport, 1, vc1, size, c));
            WreckingBalls.Add(new WreckingBall(viewport, 2, vc2, size, c));

            didPass.Add(false);
            didPass.Add(false);

            for (int i = 2; i < 7; i++)
            {
                Vector2 vc3 = new Vector2(r.Next((int)(viewport.Width / 20), (int)(viewport.Width * 0.4f)) - size.X, locations[locations.Count - 2].Y - (size.Y * 16));
                Vector2 vc4 = new Vector2(vc3.X + size.X + (viewport.Width * 0.6f), locations[locations.Count - 2].Y - (size.Y * 16));

                locations.Add(vc3);
                locations.Add(vc4);

                WreckingBalls.Add(new WreckingBall(viewport, 1, vc3, size, c));
                WreckingBalls.Add(new WreckingBall(viewport, 2, vc4, size, c));

                didPass.Add(false);
                didPass.Add(false);
            }
        }

        public void Update(int number)
        {
            for (int i = 0; i < locations.Count; i++)
            {
                locations[i] = new Vector2(locations[i].X, locations[i].Y + number);
                WreckingBalls[i].Update(number);
            }

            if (!didPass[0])
            {
                if (locations[0].Y > passPoint)
                {
                    GameState.me.score.score++;
                    didPass[0] = true;
                    didPass[1] = true;
                }
            }

            if (locations[0].Y > viewport.Height)
            {
                locations.RemoveAt(0);
                locations.RemoveAt(0);

                Vector2 vc3 = new Vector2(r.Next((int)(viewport.Width / 20), (int)(viewport.Width * 0.4f)) - size.X, locations[locations.Count - 2].Y - (size.Y * 16));
                Vector2 vc4 = new Vector2(vc3.X + size.X + (viewport.Width * 0.6f), locations[locations.Count - 2].Y - (size.Y * 16));

                locations.Add(vc3);
                locations.Add(vc4);

                WreckingBalls.RemoveAt(0);
                WreckingBalls.RemoveAt(0);

                WreckingBalls.Add(new WreckingBall(viewport, 1, vc3, size, c));
                WreckingBalls.Add(new WreckingBall(viewport, 2, vc4, size, c));

                didPass.RemoveAt(0);
                didPass.RemoveAt(0);

                didPass.Add(false);
                didPass.Add(false);
            }

            if (pos)
            {
                if (rotation >= .7f)
                {
                    rotationVelocity = 0.05f;
                    pos = false;
                }
                rotation += rotationVelocity;
                rotationVelocity += rotationVelocitysAdder;
            }

            else
            {
                if (rotation <= -.7f)
                {
                    rotationVelocity = 0.05f;
                    pos = true;
                }
                rotation -= rotationVelocity;
                rotationVelocity += rotationVelocitysAdder;
            }
            
        }

        public void Draw(SpriteBatch spritebatch)
        {
            for (int i = 0; i < 9; i++)
            {
                spritebatch.Draw(textures[i], new Rectangle((int)locations[i].X, (int)locations[i].Y, (int)size.X, (int)size.Y), Color.White);
                WreckingBalls[i].Draw(spritebatch, rotation);
            }
        }
    }
}


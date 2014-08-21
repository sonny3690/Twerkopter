using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Sway_Chopter.Source.Player;

namespace Sway_Chopter.Source.Obstacles
{
    public class WreckingBall
    {
        public static Texture2D texture;
        public Vector2 pos;
        public static float rotation;
        private int direction = 1;
        private bool flip = true;

        public WreckingBall(Vector2 position)
        {
            this.pos = position;
        }

        public void Update(GameTime gameTime, int moveUp)
        {
            float f = 1f - (float)Math.Abs(rotation) / MathHelper.PiOver2;
            f /= 2f;
            rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * MathHelper.PiOver2 * .4f * direction * f;
            if (flip && (rotation > MathHelper.PiOver4 || rotation < -MathHelper.PiOver4))
            {
                direction *= -1;
                flip = false;
            }
            if (Math.Abs(rotation) < MathHelper.PiOver4 / 2)
                flip = true;

            pos.Y += moveUp;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, pos, null, Color.White, rotation, new Vector2(32, 5), 1f, SpriteEffects.None, 0f);
        }

        public bool collidesWithMiley(Player.Player p)
        {
            float ballRadiusSquared = 32 * 32;
            Vector2 ballPos = this.pos;
            ballPos += new Vector2((float)Math.Cos(rotation + MathHelper.PiOver2), (float)Math.Sin(rotation + MathHelper.PiOver2)) * 93;

            for (int x = 0; x < p.texture.Width; x++)
                for (int y = 0; y < 160; y++)
                {
                    int X = x;
                    if (p.flip)
                        X = p.texture.Width - x;
                    if (p.textureData[x, y].A > 25) // transparency threshold
                    {
                        Vector2 pos = p.location + new Vector2(x, y);
                        if (Vector2.DistanceSquared(pos, ballPos) < ballRadiusSquared)
                        {
                            return true;
                        }
                    }
                }

            return false;
        }
    }
}


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
        public static Texture2D whiteText;
        public Vector2 pos;
        public static float rotation;
        private static int direction = 1;
        private static bool flip = true;

        public WreckingBall(Vector2 position)
        {
            this.pos = position;
        }

        public void Update(GameTime gameTime, int moveUp)
        {
            pos.Y += moveUp;
        }

        public static void UpdateRotation(GameTime gameTime)
        {
            float f = 1f - (float)Math.Abs(rotation) / MathHelper.PiOver2;
            f /= 2f;

            rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * MathHelper.PiOver4 * direction * f * 2f;
            if (flip && (rotation > MathHelper.PiOver4 || rotation < -MathHelper.PiOver4))
            {
                direction *= -1;
                flip = false;
            }
            if (Math.Abs(rotation) < MathHelper.PiOver4 / 2)
                flip = true;

        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, this.pos, null, Color.White, rotation, new Vector2(32, 5), 1f, SpriteEffects.None, 0f);
            Vector2 ballOffset = this.pos + new Vector2((float)Math.Cos(WreckingBall.rotation + MathHelper.PiOver2), (float)Math.Sin(WreckingBall.rotation + MathHelper.PiOver2)) * 65;
            spritebatch.Draw(whiteText, new Rectangle((int)ballOffset.X, (int)ballOffset.Y, 32, 4), null, Color.Red, rotation, new Vector2(0, 2), SpriteEffects.None, 0f);
            spritebatch.Draw(whiteText, new Rectangle((int)ballOffset.X, (int)ballOffset.Y, 32, 4), null, Color.Red, rotation + MathHelper.PiOver2, new Vector2(0, 2), SpriteEffects.None, 0f);
        }
    }
}


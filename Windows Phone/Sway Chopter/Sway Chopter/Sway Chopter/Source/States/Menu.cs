using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Sway_Chopter
{
    class Menu : State
    {
        Viewport viewport;
        public SpriteFont spriteFont;

        Vector2 ButtonSize;
        Texture2D btnPlay;
        Texture2D btnRanking;
        Texture2D btnRate;

        Vector2 playLocation;
        Vector2 rankingLocation;
        Vector2 rateLocation;

        public Menu(GraphicsDeviceManager g, ContentManager c, Viewport v) : base(g, c, v)
        {
            viewport = v;

            int temp = viewport.Width;
            viewport.Width = viewport.Height;
            viewport.Height = temp;

            spriteFont = content.Load<SpriteFont>("ScoreFont");

            ButtonSize = new Vector2(viewport.Width * 0.34f, viewport.Width * 0.17f);

            btnPlay = content.Load<Texture2D>("btnPlay");
            playLocation = new Vector2((viewport.Width - ButtonSize.X) / 2, viewport.Height * 0.6f);

            
        }

        public override State Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation tl in touchCollection)
            {
                if (tl.State == TouchLocationState.Pressed)
                {
                    if (new Rectangle((int)playLocation.X, (int)playLocation.Y, (int)ButtonSize.X, (int)ButtonSize.Y).Contains((int)tl.Position.X, (int)tl.Position.Y))
                    {
                        playLocation.Y += 5;
                    }
                }

                if (tl.State == TouchLocationState.Released)
                {
                    if (new Rectangle((int)playLocation.X, (int)playLocation.Y, (int)ButtonSize.X, (int)ButtonSize.Y).Contains((int)tl.Position.X, (int)tl.Position.Y))
                    {
                        playLocation.Y -= 5;
                        return new GameState(graphics, content, viewport);
                    }                    
                }
            }

            return this;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 size = spriteFont.MeasureString("Sway Chopter");
            spriteBatch.Begin();
            
            #region outline
            spriteBatch.DrawString(spriteFont, "Sway Chopter", new Vector2(MainGame.me.viewport.Width * .5f + 3, MainGame.me.viewport.Height * .4f), Color.Black, 0, size * .5f, 1f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(spriteFont, "Sway Chopter", new Vector2(MainGame.me.viewport.Width * .5f - 3, MainGame.me.viewport.Height * .4f), Color.Black, 0, size * .5f, 1f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(spriteFont, "Sway Chopter", new Vector2(MainGame.me.viewport.Width * .5f, MainGame.me.viewport.Height * .4f + 3), Color.Black, 0, size * .5f, 1f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(spriteFont, "Sway Chopter", new Vector2(MainGame.me.viewport.Width * .5f, MainGame.me.viewport.Height * .4f - 3), Color.Black, 0, size * .5f, 1f, SpriteEffects.None, 0f);
            #endregion

            spriteBatch.DrawString(spriteFont, "Sway Chopter", new Vector2(MainGame.me.viewport.Width * .5f, MainGame.me.viewport.Height * .4f), Color.White, 0, size * .5f, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(btnPlay, new Rectangle((int)playLocation.X, (int)playLocation.Y, (int)ButtonSize.X, (int)ButtonSize.Y), Color.White);

            spriteBatch.End();
        }
    }
}

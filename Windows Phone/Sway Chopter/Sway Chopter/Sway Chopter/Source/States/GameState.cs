using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Sway_Chopter.Source;
using Sway_Chopter.Source.Mechanics;

namespace Sway_Chopter
{
    public class GameState : State
    {
        public static GameState me;
        public Viewport viewport;
        public SpriteFont spriteFont;

        public bool GetReadE = true;
        Vector2 READEsize;

        Score score;

        public GameState(GraphicsDeviceManager g, ContentManager c, Viewport v) : base(g, c, v)
        {
            me = this;
            viewport = v;
            int temp = viewport.Width;
            viewport.Width = viewport.Height;
            viewport.Height = temp;

            score = new Score(v, c);
            score.display = true;

            spriteFont = content.Load<SpriteFont>("ScoreFont");
            READEsize = spriteFont.MeasureString("Get Ready");
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
        }

        public override State Update(GameTime gameTime)
        {
            if (GetReadE)
            {
            }

            else
            {
            }

            return this;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (GetReadE)
            {
                #region outline
                spriteBatch.DrawString(spriteFont, "Get Ready", new Vector2(MainGame.me.viewport.Width * .5f + 3, MainGame.me.viewport.Height * .4f), Color.White, 0, new Vector2(READEsize.X * .5f, 0), 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, "Get Ready", new Vector2(MainGame.me.viewport.Width * .5f - 3, MainGame.me.viewport.Height * .4f), Color.White, 0, new Vector2(READEsize.X * .5f, 0), 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, "Get Ready", new Vector2(MainGame.me.viewport.Width * .5f, MainGame.me.viewport.Height * .4f + 3), Color.White, 0, new Vector2(READEsize.X * .5f, 0), 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, "Get Ready", new Vector2(MainGame.me.viewport.Width * .5f, MainGame.me.viewport.Height * .4f - 3), Color.White, 0, new Vector2(READEsize.X * .5f, 0), 1f, SpriteEffects.None, 0f);
                #endregion

                spriteBatch.DrawString(spriteFont, "Get Ready", new Vector2(MainGame.me.viewport.Width * .5f, MainGame.me.viewport.Height * .4f), Color.Green, 0, new Vector2(READEsize.X * .5f, 0), 1f, SpriteEffects.None, 0f);
            }

            else
            {
                score.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
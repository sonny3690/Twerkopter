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
using Sway_Chopter.Source;
using Sway_Chopter.Source.Mechanics;
using Sway_Chopter.Source.Player;
using Sway_Chopter.Source.Obstacles;

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

        Player player;
        Obstacles obstacles;

        public GameState(GraphicsDeviceManager g, ContentManager c, Viewport v) : base(g, c, v)
        {
            me = this;
            viewport = v;

            score = new Score(v, c);
            score.display = true;

            player = new Player(v);
            player.LoadContent(c);

            obstacles = new Obstacles(viewport);
            obstacles.LoadContent(content);

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
                TouchCollection touchCollection = TouchPanel.GetState();
                foreach (TouchLocation tl in touchCollection)
                {
                    if (tl.State == TouchLocationState.Released)
                    {
                        GetReadE = false;
                    }
                }
            }

            else //The actual game loop
            {
                TouchCollection touchCollection = TouchPanel.GetState();
                foreach (TouchLocation tl in touchCollection)
                {
                    if (tl.State == TouchLocationState.Released)
                    {
                        player.TapUpdate();
                    }
                }
                obstacles.Update(4);
                player.Update(gameTime);
            }

            return this;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (GetReadE)
            {
                #region outline
                spriteBatch.DrawString(spriteFont, "Get Ready", new Vector2(MainGame.me.viewport.Width * .5f + 3, MainGame.me.viewport.Height * .25f), Color.White, 0, new Vector2(READEsize.X * .5f, 0), 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, "Get Ready", new Vector2(MainGame.me.viewport.Width * .5f - 3, MainGame.me.viewport.Height * .25f), Color.White, 0, new Vector2(READEsize.X * .5f, 0), 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, "Get Ready", new Vector2(MainGame.me.viewport.Width * .5f, MainGame.me.viewport.Height * .25f + 3), Color.White, 0, new Vector2(READEsize.X * .5f, 0), 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, "Get Ready", new Vector2(MainGame.me.viewport.Width * .5f, MainGame.me.viewport.Height * .25f - 3), Color.White, 0, new Vector2(READEsize.X * .5f, 0), 1f, SpriteEffects.None, 0f);
                #endregion

                spriteBatch.DrawString(spriteFont, "Get Ready", new Vector2(MainGame.me.viewport.Width * .5f, MainGame.me.viewport.Height * .25f), Color.Green, 0, new Vector2(READEsize.X * .5f, 0), 1f, SpriteEffects.None, 0f);
            }

            else
            {
                score.Draw(spriteBatch);
            }

            //From here, draw no matter what

            obstacles.Draw(spriteBatch);
            player.Draw(spriteBatch);
            
            spriteBatch.End();
        }
    }
}
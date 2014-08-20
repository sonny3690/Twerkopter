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

        #region Menu
        Vector2 ButtonSize;
        Texture2D btnPlay;
        Texture2D btnRanking;
        Texture2D btnRate;

        Vector2 playLocation;
        Vector2 rankingLocation;
        Vector2 rateLocation;
        bool Menu = true;
        Vector2 Menusize;
        #endregion

        #region GameOver
        Texture2D Scoreboard;
        Vector2 ScoreboardLocation;
        Vector2 ScoreboardSize;
        Vector2 DestScoreboard;
        bool IsGameOver = true;
        Vector2 GameOverSize;
        #endregion

        public bool GetReadE = true;
        Vector2 READEsize;

        public Score score;

        Player player;
        Obstacles obstacles;

        public GameState(GraphicsDeviceManager g, ContentManager c, Viewport v) : base(g, c, v)
        {
            me = this;
            viewport = v;

            ButtonSize = new Vector2(viewport.Width * 0.34f, viewport.Width * 0.17f);
            btnPlay = content.Load<Texture2D>("btnPlay");
            playLocation = new Vector2((viewport.Width - ButtonSize.X) / 2, viewport.Height * 0.6f);

            score = new Score(v, c);
            score.display = true;

            player = new Player(v);
            player.LoadContent(c);

            obstacles = new Obstacles(viewport);
            obstacles.LoadContent(content);

            spriteFont = content.Load<SpriteFont>("ScoreFont");
            READEsize = spriteFont.MeasureString("Get Ready");
            Menusize = spriteFont.MeasureString("Sway Copter");

            Scoreboard = content.Load<Texture2D>("Dashboard");
            ScoreboardSize = new Vector2(viewport.Width * 7f, viewport.Width * 0.328125f);
            DestScoreboard = new Vector2((viewport.Width - ScoreboardSize.X) / 2, viewport.Height / 2);
            ScoreboardLocation = new Vector2((viewport.Width - ScoreboardSize.X) / 2, viewport.Height);
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
        }

        public override State Update(GameTime gameTime)
        {
            if (Menu)
            {
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
                            Menu = false;
                        }
                    }
                }

                obstacles.Update(0);
            }

            else
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
                    obstacles.Update(0);
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

                    if (PlayerIsWrecked())
                    {
                        GetReadE = true;
                    }
                }
            }
            return this;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (Menu)
            {
                #region outline
                spriteBatch.DrawString(spriteFont, "Sway Chopter", new Vector2(MainGame.me.viewport.Width * .5f + 3, MainGame.me.viewport.Height * .25f), Color.Black, 0, Menusize * .5f, 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, "Sway Chopter", new Vector2(MainGame.me.viewport.Width * .5f - 3, MainGame.me.viewport.Height * .25f), Color.Black, 0, Menusize * .5f, 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, "Sway Chopter", new Vector2(MainGame.me.viewport.Width * .5f, MainGame.me.viewport.Height * .25f + 3), Color.Black, 0, Menusize * .5f, 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont, "Sway Chopter", new Vector2(MainGame.me.viewport.Width * .5f, MainGame.me.viewport.Height * .25f - 3), Color.Black, 0, Menusize * .5f, 1f, SpriteEffects.None, 0f);
                #endregion

                spriteBatch.DrawString(spriteFont, "Sway Chopter", new Vector2(MainGame.me.viewport.Width * .5f, MainGame.me.viewport.Height * .25f), Color.White, 0, Menusize * .5f, 1f, SpriteEffects.None, 0f);

                obstacles.Draw(spriteBatch);
                player.Draw(spriteBatch);

                spriteBatch.Draw(btnPlay, new Rectangle((int)playLocation.X, (int)playLocation.Y, (int)ButtonSize.X, (int)ButtonSize.Y), Color.White);
            }

            else
            {
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
            }
            spriteBatch.End();
        }

        public bool PlayerIsWrecked()
        {
            for (int i = 0; i < 2; i++)
            {
                if (!obstacles.didPass[i])
                {
                    if (new Rectangle(
                        (int)obstacles.locations[i].X, 
                        (int)obstacles.locations[i].Y, 
                        (int)obstacles.size.X, 
                        (int)obstacles.size.Y).Intersects(
                            new Rectangle(
                                (int)player.location.X, 
                                (int)player.location.Y, 
                                (int)player.size.X, 
                                (int)player.size.Y)
                                )
                        )
                    {
                        return true;
                    }


                }
            }

            return false;
        }
    }
}
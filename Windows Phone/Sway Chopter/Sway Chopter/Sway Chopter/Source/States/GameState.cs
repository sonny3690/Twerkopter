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
        }

        public override void Initialize()
        {
            score.saveScore();
        }

        public override void LoadContent()
        {
        }

        public override State Update(GameTime gameTime)
        {
            return this;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            score.Draw(spriteBatch);
            
            spriteBatch.End();
        }
    }
}
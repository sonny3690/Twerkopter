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

namespace Sway_Chopter
{
    public class GameState : State
    {
        public static GameState me;

        public Viewport viewport;

        public GameState(GraphicsDeviceManager g, ContentManager c, Viewport v) : base(g, c, v)
        {
            me = this;
        }

        public override void Initialize()
        {
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
            spriteBatch.End();
        }
    }
}
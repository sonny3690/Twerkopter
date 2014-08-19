using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Sway_Chopter
{
    class GetReady : State
    {

        public GetReady(GraphicsDeviceManager g, ContentManager c, Viewport v) : base(g, c, v)
        {
        }

        public override State Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Enter))
                return new GameState(graphics, content, viewport);

            return this;
        }

        public override void Draw(GameTime gameTime, SpriteBatch s)
        {
            s.Begin();
            s.End();
        }
    }
}

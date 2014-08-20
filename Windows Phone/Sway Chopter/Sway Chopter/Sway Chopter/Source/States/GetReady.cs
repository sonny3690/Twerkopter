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
    class GetReady : State
    {
        Viewport viewport;
        
        Vector2 size;

        public GetReady(GraphicsDeviceManager g, ContentManager c, Viewport v) : base(g, c, v)
        {
            viewport = v;
            int temp = viewport.Width;
            viewport.Width = viewport.Height;
            viewport.Height = temp;

            //spriteFont = content.Load<SpriteFont>("ScoreFont");
            //size = spriteFont.MeasureString("Get Ready ");
        }

        public override State Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation tl in touchCollection)
            {
                if (tl.State == TouchLocationState.Released)
                {
                    return new GameState(graphics, content, viewport);
                }
            }               

            return this;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            
            
            spriteBatch.End();
        }
    }
}

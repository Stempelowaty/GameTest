using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burucki.Utils
{
    public static class GlobalResources
    {
        public static SpriteFont Font { get; private set; }
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }
        public static GraphicsDevice GraphicsDevice { get; private set; }

        public static bool IsFullScreen { get; set; }

        public static void LoadContent(Game game)
        {
            Font = game.Content.Load<SpriteFont>("Font");

            ScreenWidth = game.GraphicsDevice.Viewport.Width;
            ScreenHeight = game.GraphicsDevice.Viewport.Height;
            GraphicsDevice = game.GraphicsDevice;
        }
    }

}

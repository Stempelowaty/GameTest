using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace Burucki.Utils
{
    public static class GlobalResources
    {
        public static SpriteFont Font { get; private set; }
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }
        public static float ScaleFactorX { get; set; }
        public static float ScaleFactorY {  get; set; }
        public static GraphicsDevice GraphicsDevice { get; private set; }

        public static Texture2D entityTexture;
       

        public static bool IsFullScreen { get; set; }

        public static void LoadContent(Game game)
        {
            Font = game.Content.Load<SpriteFont>("Font");
            entityTexture = game.Content.Load<Texture2D>("defaultSprite");

            ScreenWidth = game.GraphicsDevice.Viewport.Width;
            ScreenHeight = game.GraphicsDevice.Viewport.Height;
            GraphicsDevice = game.GraphicsDevice;
     

        }

        public static void UpdateScreenSize(int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;

            ScaleFactorX = (float)width / 1280f;
            ScaleFactorY = (float)height / 720f;
        }
    }

}

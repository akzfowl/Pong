using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;

namespace PongClone
{
    public class PlayerPaddle
    {
        public Vector2 position;
        public Texture2D paddleTex;
        public int width, height;
        public PlayerPaddle(Vector2 startPos, ContentManager content)
        {
            position = startPos;
            paddleTex = content.Load<Texture2D>(@"theTexture");
            width = 64;
            height = 32;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(paddleTex, new Rectangle((int)position.X, (int)position.Y, width, height), Color.White);
        }
    }
}
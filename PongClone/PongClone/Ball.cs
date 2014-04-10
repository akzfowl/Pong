using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
namespace PongClone
{
    public class Ball
    {
        public Vector2 position, velocity;
        public Texture2D paddleTex;
        public int width, height;
        Random rand;

        public Ball(Vector2 startPos, ContentManager content)
        {
            paddleTex = content.Load<Texture2D>(@"theTexture");
            width = height = 32;
            rand = new Random();
            Reset(startPos);
        }

        public void Reset(Vector2 startPos)
        {
            position = startPos;
            int direction;
            do //start in a random direction
            {
                direction = rand.Next(2);
                if (direction == 0)
                    velocity.X = (float)rand.NextDouble();
                else velocity.X = (float)rand.NextDouble() * -1;

                direction = rand.Next(2);
                if (direction == 0)
                    velocity.Y = (float)rand.NextDouble();
                else
                    velocity.Y = (float)rand.NextDouble() * -1; 

            } while (velocity.X != 0 && velocity.Y == 0); //don't want balls going completely horizontal
            velocity.Normalize();
            velocity *= 3;
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(paddleTex, new Rectangle((int)position.X, (int)position.Y, width, height), Color.White);
        }
    }
}
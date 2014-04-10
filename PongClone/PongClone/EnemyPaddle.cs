using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PongClone
{
    public class EnemyPaddle
    {
        public Vector2 position, velocity;
        public Texture2D paddleTex;
        public int width, height;

        public EnemyPaddle(Vector2 startPos, ContentManager content)
        {
            position = startPos;
            paddleTex = content.Load<Texture2D>(@"theTexture");
            width = 64;
            height = 32;
            velocity = Vector2.Zero;
        }

        public void Update(GameTime gameTime, Vector2 ballPos, Vector2 ballVel)
        {           
			Vector2 goalX = new Vector2(ballPos.X, position.Y);
			velocity = goalX - position;

			if (velocity.Length() < 20)
			{
				velocity.Normalize();
				velocity *= 2;
			}
			else //need to speed up a little to reach the ball
			{
				velocity.Normalize();
				velocity *= 4;
			}

			if (ballVel.X < 0.1f && Math.Abs(ballPos.X - position.X) < 0.01f) //if the ball is going mostly up straight toward the enemy, don't bother moving
			{
				velocity = Vector2.Zero;
			}  
		
		position += velocity;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(paddleTex, new Rectangle((int)position.X, (int)position.Y, width, height), Color.White);
        }
    }
}
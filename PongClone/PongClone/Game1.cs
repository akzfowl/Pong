using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
namespace PongClone
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        int winCount, lossCount;
        Vector2 winCountPos, lossCountPos;
        PlayerPaddle player;
        EnemyPaddle enemy;
        Ball ball;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }

        protected override void Initialize()
        {
            base.Initialize();
            TouchPanel.EnabledGestures = GestureType.FreeDrag;
            winCount = lossCount = 0;
            winCountPos = new Vector2(32, 450);
            lossCountPos = new Vector2(32, 16);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>(@"default");
            player = new PlayerPaddle(new Vector2(400, 448), Content);
            enemy = new EnemyPaddle(new Vector2(400, 0), Content);
            ball = new Ball(new Vector2(400, 240), Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            while (TouchPanel.IsGestureAvailable)
            {
                //user drags their paddle
                GestureSample gesture = TouchPanel.ReadGesture();
                if (gesture.GestureType == GestureType.FreeDrag)
                {
                    player.position.X += gesture.Delta.X;
                }
            }

            //player bounds check
            if (player.position.X < 0)
                player.position.X = 0;
            if (player.position.X > 736)
                player.position.X = 736;   

            ball.Update(gameTime);
            enemy.Update(gameTime, ball.position, ball.velocity);

            //enemy bounds check
            if (enemy.position.X < 0)
                enemy.position.X = 0;
            if (player.position.X > 736)
                enemy.position.X = 736;

            //ball collision with the wall
            if (ball.position.X < 0 || ball.position.X > 768)
            {
                ball.velocity.X = ball.velocity.X * -1.1f;
                ball.velocity.Y = ball.velocity.Y * 1.1f;
            }

            //ball collision with player paddle
            if (ball.position.Y + ball.height >= player.position.Y)
            {
                if (ball.position.X <= player.position.X + player.width)
                {
                    if (ball.position.X + ball.width >= player.position.X)
                    {
                        ball.velocity.X = ball.velocity.X * 1.1f;
                        ball.velocity.Y = ball.velocity.Y * -1.1f;
                    }
                }   
            }

            //ball collision with enemy paddle
            if (ball.position.Y <= enemy.position.Y + enemy.height)
            {
                if (ball.position.X <= enemy.position.X + enemy.width)
                {
                    if (ball.position.X + ball.width >= enemy.position.X)
                    {
                        ball.velocity.X = ball.velocity.X * 1.1f;
                        ball.velocity.Y = ball.velocity.Y * -1.1f;
                    }
                }
            }

            //ball collision with goals
            if (ball.position.Y > 448)
            {
                lossCount++;
                ball.Reset(new Vector2(400, 240));
            }
            if (ball.position.Y < 0)
            {
                winCount++;
                ball.Reset(new Vector2(400, 240));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            player.Draw(gameTime, spriteBatch);
            enemy.Draw(gameTime, spriteBatch);
            ball.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, "" + winCount, winCountPos, Color.White);
            spriteBatch.DrawString(font, "" + lossCount, lossCountPos, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

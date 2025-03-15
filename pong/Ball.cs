using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Ball
    {
        Texture2D texture;
        Game game;
        Vector2 position;
        Vector2 velocity;
        bool outOfBounds = false;

        public bool OutOfBounds { get => outOfBounds; }
        public Vector2 Velocity { get => velocity; }

        public const float BALL_VELOCITY = 2.5f;

        public Ball(Game game, Texture2D texture)
        {
            this.game = game;
            this.texture = texture;
        }

        public void SetInStartPosition()
        {
            var viewport = game.GraphicsDevice.Viewport;

            position.Y = (viewport.Height / 2) - (texture.Height / 2);
            position.X = (viewport.Width / 2) - (texture.Width / 2);

            velocity = new Vector2(BALL_VELOCITY);
            outOfBounds = false;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void SetPosition(float x)
        {
            position.X = x;
        }

        public void InvertVelocity()
        {
            velocity.X += velocity.X * 0.2F;
            velocity.X *= -1;
        }

        public void Update()
        {
            var viewport = game.GraphicsDevice.Viewport;

            if (position.Y < 0)
            {
                position.Y = 0;
                velocity.Y *= -1;
            }

            if (position.Y + texture.Height > viewport.Height)
            {
                position.Y = viewport.Height - texture.Height;
                velocity.Y *= -1;
            }

            position += velocity;

            if (position.X + texture.Width < 0 || position.X > viewport.Width)
            {
                outOfBounds = true;
                velocity = Vector2.Zero;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}

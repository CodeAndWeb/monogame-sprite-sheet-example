namespace TexturePackerLoader
{
    using Microsoft.Xna.Framework;

    public class Sprite
    {
        public Sprite(Rectangle rectangle, bool isRotated)
        {
            this.Rectangle = rectangle;
            this.Origin = new Vector2 (rectangle.Width / 2, rectangle.Height / 2);
            this.IsRotated = isRotated;
        }

        public Rectangle Rectangle { get; private set; }

        public bool IsRotated { get; private set; }

        public Vector2 Origin { get; private set; }
    }
}

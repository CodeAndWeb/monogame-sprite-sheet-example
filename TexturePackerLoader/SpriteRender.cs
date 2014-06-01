namespace TexturePackerLoader
{
	using System;

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	public class SpriteRender
	{
		private const float AntiClockwiseNinetyDegreeRotation = (float)(-Math.PI / 2.0f);

		private const float ClockwiseNinetyDegreeRotation = (float)(Math.PI / 2.0f);

		private SpriteBatch spriteBatch;

		public SpriteRender (SpriteBatch spriteBatch)
		{
			this.spriteBatch = spriteBatch;
		}

		// <param name="position">This should be where you want the CENTRE of the sprite image to be rendered.</param>
		public void Draw(Texture2D texture, Sprite sprite, Vector2 position, Color? color = null, SpriteEffects spriteEffects = SpriteEffects.None) {
			this.spriteBatch.Draw(
				texture: texture,
				position: position,
				sourceRectangle: sprite.Rectangle,
				origin: sprite.Origin,
				color: color,
				effect: spriteEffects,
				rotation: sprite.IsRotated ? (spriteEffects == SpriteEffects.None ? AntiClockwiseNinetyDegreeRotation : ClockwiseNinetyDegreeRotation) : 0f);
		}
	}
}
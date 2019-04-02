using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace PhonePong.GameScreens
{
    class SinglePlayerTitleScreen : TitleScreen
    {
	  public override void Initialize()
	  {
		base.Initialize();
	  }

	  public override void Update(GameTime gameTime)
	  {
		base.Update(gameTime);

		UpdateBallPosition();

		CheckNaturalBallCollisions();

		TouchCollection tc = TouchPanel.GetState();
		Rectangle bounds = ScreenManager.PongGame.GraphicsDevice.Viewport.Bounds;
		foreach (TouchLocation tl in tc)
		{
		    if (tl.Position.X < bounds.Width - 150 && tl.Position.X > bounds.Width - 250
			  && tl.Position.Y > 300 && tl.Position.Y < 600)
		    {
			  this.CurrStatus = ScreenStatus.Hidden;
			  ScreenManager.AddScreen(new SinglePlayerGameScreen());
			  this.ExitScreen();
		    }
		    else if (tl.Position.X < bounds.Width - 250 && tl.Position.X > bounds.Width - 350
			&& tl.Position.Y > 300 && tl.Position.Y < 600)
		    {
			  this.CurrStatus = ScreenStatus.Hidden;
			  ScreenManager.AddScreen(new TwoPlayerGameScreen());
			  this.ExitScreen();
		    }
		}
	  }

	  public override void Draw(GameTime gameTime)
	  {
		Game game = ScreenManager.PongGame;
		Rectangle ballSrc = new Rectangle(64, 0, 32, 32);
		game.GraphicsDevice.Clear(Color.Black);
		ScreenManager.Batch.Begin();
		ScreenManager.Batch.Draw(ScreenManager.SpriteSheet, this._ball, ballSrc, Color.White);
		ScreenManager.Batch.DrawString(ScreenManager.ChoiceFont, "PONG", new Vector2(game.GraphicsDevice.Viewport.Bounds.Width - 50, 300), Color.Red,
		    (float)Math.PI / 2, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0.0f);
		ScreenManager.Batch.DrawString(ScreenManager.ChoiceFont, "One Player", new Vector2(game.GraphicsDevice.Viewport.Bounds.Width - 150, 300), Color.Red,
		    (float)Math.PI / 2, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0.0f);
		ScreenManager.Batch.DrawString(ScreenManager.ChoiceFont, "Two Players", new Vector2(game.GraphicsDevice.Viewport.Bounds.Width - 250, 300), Color.Red,
		    (float)Math.PI / 2, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0.0f);
		ScreenManager.Batch.End();
		base.Draw(gameTime);
	  }
    }
}

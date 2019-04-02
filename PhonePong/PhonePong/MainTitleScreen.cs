using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace PhonePong.GameScreens
{
    class MainTitleScreen : TitleScreen
    {
	  public override void Initialize()
	  {
		base.Initialize();
		int height = ScreenManager.PongGame.GraphicsDevice.Viewport.Bounds.Height;
		int width = ScreenManager.PongGame.GraphicsDevice.Viewport.Bounds.Width;
		this._ball = new Rectangle(width / 2 - 16, height / 2 - 16, 32, 32);
	  }

	  public override void Update(GameTime gameTime)
	  {
		base.Update(gameTime);
		InitTitleBall();

		UpdateBallPosition();

		CheckNaturalBallCollisions();

		TouchCollection tc = TouchPanel.GetState();
		Rectangle bounds = ScreenManager.PongGame.GraphicsDevice.Viewport.Bounds;
		foreach (TouchLocation tl in tc)
		{
		    if (tl.Position.X < bounds.Width - 200 && tl.Position.X > bounds.Width - 300
			  && tl.Position.Y > 300 && tl.Position.Y < 600)
		    {
			  this.CurrStatus = ScreenStatus.Hidden;
			  SinglePlayerTitleScreen nextScreen = new SinglePlayerTitleScreen();
			  nextScreen.Ball = this._ball;
			  nextScreen.BallVelocity = this._ballVelocity;
			  ScreenManager.AddScreen(nextScreen);
			  this.ExitScreen();
		    }
		    else if (tl.Position.X < bounds.Width - 300 && tl.Position.X > bounds.Width - 400
			&& tl.Position.Y > 300 && tl.Position.Y < 600)
		    {
			  this.CurrStatus = ScreenStatus.Hidden;
			  ScreenManager.AddScreen(new TwoPlayerGameScreen());
			  this.ExitScreen();
		    }
		}
	  }

	  private void InitTitleBall()
	  {
		if (this._ballVelocity.Equals(Vector2.Zero))
		{
		    Random rnd = new Random();
		    this._ballVelocity.X = rnd.Next(4) + 4;
		    this._ballVelocity.Y = rnd.Next(4) + 4;
		}
	  }

	  public override void Draw(GameTime gameTime)
	  {
		Game game = ScreenManager.PongGame;
		Rectangle ballSrc = new Rectangle(64, 0, 32, 32);
		game.GraphicsDevice.Clear(Color.Black);
		ScreenManager.Batch.Begin();
		ScreenManager.Batch.Draw(ScreenManager.SpriteSheet, this._ball, ballSrc, Color.White);
		ScreenManager.Batch.DrawString(ScreenManager.ChoiceFont, "PONG", new Vector2(game.GraphicsDevice.Viewport.Bounds.Width - 100, 300), Color.Red,
		    (float)Math.PI / 2, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0.0f);
		ScreenManager.Batch.DrawString(ScreenManager.ChoiceFont, "One Player", new Vector2(game.GraphicsDevice.Viewport.Bounds.Width - 200, 300), Color.Red,
		    (float)Math.PI / 2, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0.0f);
		ScreenManager.Batch.DrawString(ScreenManager.ChoiceFont, "Two Players", new Vector2(game.GraphicsDevice.Viewport.Bounds.Width - 300, 300), Color.Red,
		    (float)Math.PI / 2, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0.0f);
		ScreenManager.Batch.End();
		base.Draw(gameTime);
	  }
    }
}

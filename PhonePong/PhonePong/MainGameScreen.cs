using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;

namespace PhonePong.GameScreens
{
    abstract class MainGameScreen : GameScreen
    {

	  protected Rectangle _topBar, _bottomBar, _ball;
	  protected Vector2 _ballVelocity;
	  protected const int SPEEDINCPERIOD = 2;
	  protected int _currSpeedIncNum = 0;
	  protected int _score1, _score2, _ballSpeed;

	  public override void Initialize()
	  {
		int height = ScreenManager.PongGame.GraphicsDevice.Viewport.Bounds.Height;
		int width = ScreenManager.PongGame.GraphicsDevice.Viewport.Bounds.Width;
		this._topBar = new Rectangle(width / 2 - 64, 0, 128, 32);
		this._bottomBar = new Rectangle(width / 2 - 64, height - 32, 128, 32);
		this._ball = new Rectangle(width / 2 - 16, height / 2 - 16, 32, 32);
		this._ballVelocity = Vector2.Zero;
		this._ballSpeed = 5;
		base.Initialize();
	  }

	  public override void LoadContent()
	  {
		base.LoadContent();
	  }

	  public override void UnloadContent()
	  {
		base.UnloadContent();
	  }

	  protected void UpdateBallPosition()
	  {
		_ball.X += (int)_ballVelocity.X;
		_ball.Y += (int)_ballVelocity.Y;
	  }

	  protected void IncreaseSpeed()
	  {
		if (this._currSpeedIncNum == SPEEDINCPERIOD)
		{
		    if (this._ballVelocity.X > 0)
		    {
			  this._ballVelocity.X += 1;
		    }
		    else
		    {
			  this._ballVelocity.X -= 1;
		    }
		    if (this._ballVelocity.Y > 0)
		    {
			  this._ballVelocity.Y += 1;
		    }
		    else
		    {
			  this._ballVelocity.Y -= 1;
		    }
		    this._currSpeedIncNum = 0;
		}
	  }

	  protected int CheckTopInput(int topBarMovement)
	  {
		TouchCollection tc = TouchPanel.GetState();
		foreach (TouchLocation tl in tc)
		{
		    if (tl.Position.Y < 100 && tl.Position.X >= 0 && tl.Position.X <= ScreenManager.PongGame.GraphicsDevice.Viewport.Width - _topBar.Width)
		    {
			  topBarMovement = _topBar.X - (int)tl.Position.X;
			  _topBar.X = (int)tl.Position.X;
		    }

		    if (tl.Position.X < 270 && tl.Position.X > 210 &&
			  tl.Position.Y < 430 && tl.Position.Y > 370 && _ballVelocity.Equals(Vector2.Zero))
		    {
			  InitBall();
		    }
		}
		return topBarMovement;
	  }

	  protected int CheckBottomInput(int bottomBarMovement)
	  {
		TouchCollection tc = TouchPanel.GetState();
		foreach (TouchLocation tl in tc)
		{
		    if (tl.Position.Y > ScreenManager.PongGame.GraphicsDevice.Viewport.Height - 100 && tl.Position.X >= 0 && tl.Position.X <= ScreenManager.PongGame.GraphicsDevice.Viewport.Width - this._bottomBar.Width)
		    {
			  bottomBarMovement = this._bottomBar.X - (int)tl.Position.X;
			  this._bottomBar.X = (int)tl.Position.X;
		    }

		    if (tl.Position.X < 270 && tl.Position.X > 210 &&
			  tl.Position.Y < 430 && tl.Position.Y > 370 && _ballVelocity.Equals(Vector2.Zero))
		    {
			  InitBall();
		    }
		}
		return bottomBarMovement;
	  }

	  protected void CheckGameBallCollisions(int topBarMovement, int bottomBarMovement)
	  {
		if (_ball.X < 0)
		{
		    _ballVelocity.X = -_ballVelocity.X;
		    _ball.X = 0;
		}
		else if (_ball.Right > ScreenManager.PongGame.GraphicsDevice.Viewport.Width)
		{
		    _ballVelocity.X = -_ballVelocity.X;
		    _ball.X = ScreenManager.PongGame.GraphicsDevice.Viewport.Width - _ball.Width;
		}
		else if (_ball.X >= _bottomBar.Left && _ball.X < _bottomBar.Right && _ball.Bottom >= _bottomBar.Top)
		{
		    _ballVelocity.Y = -_ballVelocity.Y;
		    _ball.Y = _bottomBar.Top - _ball.Height;
		    if (bottomBarMovement > 0)
		    {
			  _ballVelocity.X += 3;
			  this._currSpeedIncNum++;
		    }
		    else if (bottomBarMovement < 0)
		    {
			  _ballVelocity.X -= 3;
			  this._currSpeedIncNum++;
		    }
		}
		else if (_ball.X >= _topBar.Left && _ball.X < _topBar.Right && _ball.Y <= _topBar.Bottom)
		{
		    _ballVelocity.Y = -_ballVelocity.Y;
		    _ball.Y = _topBar.Bottom;
		    if (topBarMovement > 0)
		    {
			  _ballVelocity.X += 3;
			  this._currSpeedIncNum++;
		    }
		    else if (topBarMovement < 0)
		    {
			  _ballVelocity.X -= 3;
			  this._currSpeedIncNum++;
		    }
		}
		else if (_ball.Y < 0)
		{
		    _score1++;
		    _ball.X = 220;
		    _ball.Y = 380;
		    _ballVelocity.X = 0;
		    _ballVelocity.Y = 0;
		}
		else if (_ball.Bottom > ScreenManager.PongGame.GraphicsDevice.Viewport.Height)
		{
		    _score2++;
		    _ball.X = 220;
		    _ball.Y = 380;
		    _ballVelocity.X = 0;
		    _ballVelocity.Y = 0;
		}
		else if (_ball.Left < _topBar.Right && _ball.Right > _topBar.Left + _topBar.Width / 2 && _ball.Y <= _topBar.Bottom)
		{
		    _ballVelocity.X = -_ballVelocity.X;
		    _ball.X = _topBar.Right;
		    this._currSpeedIncNum++;
		}
		else if (_ball.Right > _topBar.Left && _ball.Left < _topBar.Right - _topBar.Width / 2 && _ball.Y <= _topBar.Bottom)
		{
		    _ballVelocity.X = -_ballVelocity.X;
		    _ball.X = _topBar.Left - _ball.Width;
		    this._currSpeedIncNum++;
		}
	  }

	  protected int determineAIMove(ref Rectangle bar)
	  {
		float ballHeadingForBar = this._ballVelocity.Y * (bar.Y - ScreenManager.PongGame.GraphicsDevice.Viewport.Bounds.Height / 2);
		if (ballHeadingForBar > 0)
		{
		    int projectedPos = ((int)(Math.Floor(Math.Abs((bar.Y - this._ball.Top) / this._ballVelocity.Y)) * this._ballVelocity.X + this._ball.X));
		    if (projectedPos < 0 || projectedPos > ScreenManager.PongGame.GraphicsDevice.Viewport.Bounds.Width)
		    {
			  return 0;
		    }
		    else if (projectedPos > bar.X + bar.Width - 10)
		    {
			  bar.X += 5;
			  return 5;
		    }
		    else if (projectedPos < bar.X + 10)
		    {
			  bar.X -= 5;
			  return -5;
		    }
		    else
		    {
			  return 0;
		    }
		}
		else
		{
		    if (bar.X < (ScreenManager.PongGame.GraphicsDevice.Viewport.Bounds.Width - bar.Width) / 2)
		    {
			  bar.X += 5;
		    }
		    else if (bar.X > (ScreenManager.PongGame.GraphicsDevice.Viewport.Bounds.Width - bar.Width) / 2)
		    {
			  bar.X -= 5;
		    }
		    return 0;
		}
	  }

	  protected void InitBall()
	  {
		Random rand = new Random();
		switch (rand.Next(4))
		{
		    case 0:
			  this._ballVelocity.X = _ballSpeed;
			  this._ballVelocity.Y = _ballSpeed;
			  break;
		    case 1:
			  _ballVelocity.X = -_ballSpeed;
			  _ballVelocity.Y = _ballSpeed;
			  break;
		    case 2:
			  _ballVelocity.X = _ballSpeed;
			  _ballVelocity.Y = -_ballSpeed;
			  break;
		    case 3:
			  _ballVelocity.X = -_ballSpeed;
			  _ballVelocity.Y = -_ballSpeed;
			  break;
		}
		_ball.X = ScreenManager.PongGame.GraphicsDevice.Viewport.Bounds.Width / 2 - _ball.Width / 2;
		_ball.Y = ScreenManager.PongGame.GraphicsDevice.Viewport.Bounds.Height / 2 - _ball.Height / 2;
		this._currSpeedIncNum = 0;
	  }

	  protected void CheckNaturalBallCollisions()
	  {
		Viewport viewport = ScreenManager.PongGame.GraphicsDevice.Viewport;
		if (_ball.X < 0)
		{
		    _ballVelocity.X = -_ballVelocity.X;
		    _ball.X = 0;
		}
		else if (_ball.Right > viewport.Width)
		{
		    _ballVelocity.X = -_ballVelocity.X;
		    _ball.X = viewport.Width - _ball.Width;
		}
		else if (_ball.Y < 0)
		{
		    _ballVelocity.Y = -_ballVelocity.Y;
		    _ball.Y = 0;
		}
		else if (_ball.Bottom > viewport.Height)
		{
		    _ballVelocity.Y = -_ballVelocity.Y;
		    _ball.Y = viewport.Height - _ball.Height;
		}
	  }

	  public override void Draw(GameTime gameTime)
	  {
		ScreenManager.PongGame.GraphicsDevice.Clear(Color.Black);
		SpriteBatch spriteBatch = ScreenManager.Batch;
		Texture2D spriteSheet = ScreenManager.SpriteSheet;
		Rectangle rect1Src = new Rectangle(0, 0, 32, 128);
		Rectangle rect2Src = new Rectangle(32, 0, 32, 128);
		Rectangle ballSrc = new Rectangle(64, 0, 32, 32);
		Viewport view = ScreenManager.PongGame.GraphicsDevice.Viewport;

		spriteBatch.Begin();
		spriteBatch.Draw(spriteSheet, this._topBar, rect1Src, Color.White);
		spriteBatch.Draw(spriteSheet, this._bottomBar, rect2Src, Color.White);
		spriteBatch.Draw(spriteSheet, this._ball, ballSrc, Color.White);
		spriteBatch.DrawString(ScreenManager.DebugFont, "Speed: (" + this._ballVelocity.X + ", " + this._ballVelocity.Y + ") Speed Ratio: " + String.Format("{0:0.00}",
		    (this._ballVelocity.X / this._ballVelocity.Y)), new Vector2(0.0f, 0.0f), Color.Red);
		spriteBatch.DrawString(ScreenManager.ScoreFont, this._score2.ToString(), new Vector2(view.Bounds.Width - 50, 50), Color.Blue, (float)Math.PI / 2,
		    new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0.0f);
		spriteBatch.DrawString(ScreenManager.ScoreFont, this._score1.ToString(), new Vector2(view.Bounds.Width - 50, view.Bounds.Height - 100),
		    Color.Red, (float)Math.PI / 2, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0.0f);
		spriteBatch.End();
	  }

    }
}

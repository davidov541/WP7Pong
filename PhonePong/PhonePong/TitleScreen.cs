using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;

namespace PhonePong.GameScreens
{
    abstract class TitleScreen : GameScreen
    {
	  protected Rectangle _ball;
	  protected Vector2 _ballVelocity;

	  public Rectangle Ball
	  {
		get;
		set;
	  }

	  public Vector2 BallVelocity
	  {
		get;
		set;
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace PhonePong.GameScreens
{
    class SinglePlayerGameScreen : MainGameScreen
    {
	  private bool _aiistop;

	  public SinglePlayerGameScreen()
	  {

	  }

	  public SinglePlayerGameScreen(bool AIIsTop)
	  {
		this._aiistop = AIIsTop;
	  }

	  public override void Update(GameTime gameTime)
	  {
		base.Update(gameTime);

		int topBarMovement = 0;
		int bottomBarMovement = 0;

		IncreaseSpeed();

		UpdateBallPosition();

		bottomBarMovement = determineAIMove(ref this._bottomBar);

		topBarMovement = CheckTopInput(topBarMovement);

		CheckGameBallCollisions(topBarMovement, bottomBarMovement);
	  }
    }
}

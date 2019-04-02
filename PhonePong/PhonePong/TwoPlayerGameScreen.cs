using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PhonePong.GameScreens
{
    class TwoPlayerGameScreen : MainGameScreen
    {

	  public override void Update(GameTime gameTime)
	  {
		base.Update(gameTime);

		int topBarMovement = 0;
		int bottomBarMovement = 0;

		IncreaseSpeed();

		UpdateBallPosition();

		bottomBarMovement = CheckBottomInput(bottomBarMovement);

		topBarMovement = CheckTopInput(topBarMovement);

		CheckGameBallCollisions(topBarMovement, bottomBarMovement);
	  }
    }
}

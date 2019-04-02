using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace PhonePong.GameScreens
{
    public enum ScreenStatus
    {
	  Visible,
	  Hidden
    }
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public abstract class GameScreen
    {
	  private ScreenStatus _currStatus = ScreenStatus.Visible;

	  public ScreenStatus CurrStatus
	  {
		get;
		set;
	  }

	  public virtual void Initialize() { }

	  public virtual void LoadContent() { }

	  public virtual void UnloadContent() { }

	  public virtual void Update(GameTime gameTime)
	  {
	  }

	  public virtual void Draw(GameTime gameTime) { }

	  public virtual void PostUIDraw(GameTime gameTime) { }

	  public void ExitScreen()
	  {
		ScreenManager.RemoveScreen(this);
	  }

    }
}

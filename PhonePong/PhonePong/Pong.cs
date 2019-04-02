using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using PhonePong.GameScreens;

namespace PhonePong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Pong : Microsoft.Xna.Framework.Game
    {
	  private GraphicsDeviceManager _graphics;
	  private static ScreenManager _sm = null;

	  public Pong()
	  {
		_graphics = new GraphicsDeviceManager(this);
		Content.RootDirectory = "Content";

		// Frame rate is 30 fps by default for Windows Phone.
		TargetElapsedTime = TimeSpan.FromTicks(333333);

		// Pre-autoscale settings.
		_graphics.PreferredBackBufferWidth = 480;
		_graphics.PreferredBackBufferHeight = 800;

		_sm = new ScreenManager(this);
		MainTitleScreen ts = new MainTitleScreen();
		ts.CurrStatus = ScreenStatus.Visible;
		ScreenManager.AddScreen(ts);
		this.Components.Add(_sm);
	  }

	  /// <summary>
	  /// Allows the game to perform any initialization it needs to before starting to run.
	  /// This is where it can query for any required services and load any non-graphic
	  /// related content.  Calling base.Initialize will enumerate through any components
	  /// and initialize them as well.
	  /// </summary>
	  protected override void Initialize()
	  {
		_sm.Initialize();
	  }

	  /// <summary>
	  /// LoadContent will be called once per game and is the place to load
	  /// all of your content.
	  /// </summary>
	  protected override void LoadContent()
	  {
	  }

	  /// <summary>
	  /// UnloadContent will be called once per game and is the place to unload
	  /// all content.
	  /// </summary>
	  protected override void UnloadContent()
	  {
	  }

	  /// <summary>
	  /// Allows the game to run logic such as updating the world,
	  /// checking for collisions, gathering input, and playing audio.
	  /// </summary>
	  /// <param name="gameTime">Provides a snapshot of timing values.</param>
	  protected override void Update(GameTime gameTime)
	  {
		_sm.Update(gameTime);
		// Allows the game to exit
		if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
		{
		    this.Exit();
		}
	  }

	  /// <summary>
	  /// This is called when the game should draw itself.
	  /// </summary>
	  /// <param name="gameTime">Provides a snapshot of timing values.</param>
	  protected override void Draw(GameTime gameTime)
	  {
		_sm.Draw(gameTime);
	  }
    }
}

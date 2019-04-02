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

using PhonePong.GameScreens;


namespace PhonePong
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ScreenManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
	  private static List<GameScreen> _screens = new List<GameScreen>();
	  private static SpriteBatch _batch;
	  private static bool _initialized = false;
	  private static Texture2D _grass, _spriteSheet;
	  private static SoundEffect _ballBounce, _playerScored;
	  private static SpriteFont _debugFont, _scoreFont, _titlefont, _choicefont;
	  private bool _traceenabled = true;
	  private static Game _ponggame;
	  private static List<GameScreen> _removedscreens = new List<GameScreen>();
	  private static List<GameScreen> _addedscreens = new List<GameScreen>();

	  public static SpriteBatch Batch
	  {
		get
		{
		    return _batch;
		}
	  }

	  public static bool Initialized
	  {
		get
		{
		    return _initialized;
		}
	  }

	  public bool TraceEnabled
	  {
		get
		{
		    return _traceenabled;
		}
	  }

	  public static Game PongGame
	  {
		get
		{
		    return _ponggame;
		}
	  }

	  public static Texture2D SpriteSheet
	  {
		get
		{
		    return _spriteSheet;
		}
	  }

	  public static SpriteFont ChoiceFont
	  {
		get
		{
		    return _choicefont;
		}
	  }

	  public static SpriteFont ScoreFont
	  {
		get
		{
		    return _scoreFont;
		}
	  }

	  public static SpriteFont DebugFont
	  {
		get
		{
		    return _debugFont;
		}
	  }

	  public ScreenManager(Game game)
		: base(game)
	  {
		this.Enabled = true;
		_ponggame = game;
	  }

	  protected override void LoadContent()
	  {
		base.LoadContent();
		_batch = new SpriteBatch(_ponggame.GraphicsDevice);
		_grass = _ponggame.Content.Load<Texture2D>(@"Textures/tennis");
		_spriteSheet = _ponggame.Content.Load<Texture2D>(@"Textures/sprites");
		_ballBounce = _ponggame.Content.Load<SoundEffect>(@"Sounds/bounce");
		_playerScored = _ponggame.Content.Load<SoundEffect>(@"Sounds/goal");
		_debugFont = _ponggame.Content.Load<SpriteFont>(@"Fonts/DebugFont");
		_scoreFont = _ponggame.Content.Load<SpriteFont>(@"Fonts/ScoreFont");
		_titlefont = _ponggame.Content.Load<SpriteFont>(@"Fonts/TitleFont");
		_choicefont = _ponggame.Content.Load<SpriteFont>(@"Fonts/ChoiceFont");

		foreach (GameScreen screen in _screens)
		{
		    if (screen.CurrStatus == ScreenStatus.Visible)
		    {
			  screen.Initialize();
			  screen.LoadContent();
		    }
		}
	  }

	  protected override void UnloadContent()
	  {
		base.UnloadContent();

		foreach (GameScreen screen in _screens)
		{
		    if (screen.CurrStatus == ScreenStatus.Visible)
		    {
			  screen.UnloadContent();
		    }
		}
	  }

	  /// <summary>
	  /// Allows the game component to perform any initialization it needs to before starting
	  /// to run.  This is where it can query for any required services and load content.
	  /// </summary>
	  public override void Initialize()
	  {
		base.Initialize();

		_initialized = true;
	  }

	  public static void AddScreen(GameScreen gs)
	  {
		_addedscreens.Add(gs);
		if (_initialized)
		{
		    gs.LoadContent();
		}
	  }

	  public static void RemoveScreen(GameScreen gs)
	  {
		_removedscreens.Add(gs);
		if (_initialized)
		{
		    gs.UnloadContent();
		}
	  }

	  /// <summary>
	  /// Allows the game component to update itself.
	  /// </summary>
	  /// <param name="gameTime">Provides a snapshot of timing values.</param>
	  public override void Update(GameTime gameTime)
	  {
		foreach (GameScreen gs in _screens)
		{
		    if (gs.CurrStatus == ScreenStatus.Visible)
		    {
			  gs.Update(gameTime);
		    }
		}
		foreach (GameScreen gs in _removedscreens)
		{
		    _screens.Remove(gs);
		}
		_removedscreens.Clear();
		foreach (GameScreen gs in _addedscreens)
		{
		    gs.Initialize();
		    _screens.Add(gs);
		}
		_addedscreens.Clear();
	  }

	  public override void Draw(GameTime gameTime)
	  {
		foreach (GameScreen gs in _screens)
		{
		    if (gs.CurrStatus == ScreenStatus.Visible)
		    {
			  gs.Draw(gameTime);
		    }
		}
	  }
    }
}

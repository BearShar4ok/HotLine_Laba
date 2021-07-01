using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using menu.UI;
using System;
using System.Collections.Generic;

namespace menu
{
    public enum GameState
    {
        Menu, Play, Exit, Info, Settings, Extensions, FullScreen, Window, WindowFrame, Sound, GameMenu
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Menu menu;
        private Random r;
        private Background bg;
        private List<string> mainMenu = new List<string>() {
            "New game","continue","settings","Developer`s info","Exit"
        };
        private List<string> settingsMenu = new List<string> { "extensions", "Sound", "back" };
        private List<string> infoMenu = new List<string> { "back" };
        private List<string> soundMenu = new List<string> { "music", "sound", "back" };
        private List<string> extensionsMenu = new List<string> { "full screen", "window", "window without a frame", "back" };
        private List<string> gameMenu = new List<string> { "continue", "restart level", "settings", "quit to menu" };
        public static GameState gameState;
        public static GameState prevGameState;

        private SpriteFont spriteFont;
        private string info = "We are game developers and we made this game.";


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            menu = new Menu();

            gameState = GameState.Menu;
            prevGameState = GameState.Menu;

            r = new Random();
            bg = new Background(_graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight, "back" + r.Next(0, 10));


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bg.LoadContent(Content);

            menu.LoadContent(Content);

            spriteFont = Content.Load<SpriteFont>("infofont");
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gameState = GameState.GameMenu;
                prevGameState = GameState.GameMenu;
            }

            // TODO: Add your update logic here
            switch (gameState)
            {
                case GameState.Menu:
                    menu.Update(mainMenu);
                    break;
                case GameState.Play:
                    break;
                case GameState.Exit:
                    Exit();
                    break;
                case GameState.Info:
                    menu.Update(infoMenu);
                    break;
                case GameState.Settings:
                    menu.Update(settingsMenu);
                    break;
                case GameState.Extensions:
                    menu.Update(extensionsMenu);
                    break;
                case GameState.FullScreen:
                    //System.Drawing.Size res = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
                    //_graphics.PreferredBackBufferWidth = res.Width;
                    //_graphics.PreferredBackBufferHeight = res.Height;
                    //_graphics.IsFullScreen = true;
                    Window.IsBorderless = true;
                    Window.Position = new Point(0, 0);
                    _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                    _graphics.ApplyChanges();
                    gameState = GameState.Extensions;
                    break;
                case GameState.Window:
                    Window.IsBorderless = false;
                    _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;
                    _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2;
                    _graphics.ApplyChanges();
                    gameState = GameState.Extensions;
                    break;
                case GameState.WindowFrame:
                    Window.IsBorderless = false;
                    System.Drawing.Size res2 = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
                    _graphics.PreferredBackBufferWidth = res2.Width / 4;
                    _graphics.PreferredBackBufferHeight = res2.Height / 4;
                    _graphics.ApplyChanges();
                    gameState = GameState.Extensions;
                    break;
                case GameState.Sound:
                    menu.Update(soundMenu);
                    break;
                case GameState.GameMenu:
                    menu.Update(gameMenu);
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            bg.Draw(_spriteBatch);

            switch (gameState)
            {
                case GameState.Menu:
                    menu.Draw(_spriteBatch, mainMenu, new Vector2(_graphics.PreferredBackBufferWidth / 2,
                        _graphics.PreferredBackBufferHeight / 2));
                    break;
                case GameState.Play:
                    break;
                case GameState.Info:
                    _spriteBatch.DrawString(spriteFont, info, new Vector2(_graphics.PreferredBackBufferWidth / 2 - info.Length * 3,
                        _graphics.PreferredBackBufferHeight / 2), Color.MediumPurple);
                    menu.Draw(_spriteBatch, infoMenu, new Vector2(_graphics.PreferredBackBufferWidth / 2,
                        _graphics.PreferredBackBufferHeight / 2 + _graphics.PreferredBackBufferHeight / 5));
                    break;
                case GameState.Settings:
                    menu.Draw(_spriteBatch, settingsMenu, new Vector2(_graphics.PreferredBackBufferWidth / 2,
                        _graphics.PreferredBackBufferHeight / 2));
                    break;
                case GameState.Extensions:
                    menu.Draw(_spriteBatch, extensionsMenu, new Vector2(_graphics.PreferredBackBufferWidth / 2,
                        _graphics.PreferredBackBufferHeight / 2));
                    break;
                case GameState.FullScreen:
                    Rectangle rec = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                        GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
                    bg.Rec = rec;
                    bg.Draw(_spriteBatch);
                    menu.Draw(_spriteBatch, extensionsMenu, new Vector2(_graphics.PreferredBackBufferWidth / 2,
                        _graphics.PreferredBackBufferHeight / 2));
                    break;
                case GameState.Window:
                    Rectangle rec1 = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2,
                        GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2);
                    bg.Rec = rec1;
                    bg.Draw(_spriteBatch);
                    menu.Draw(_spriteBatch, extensionsMenu, new Vector2(_graphics.PreferredBackBufferWidth / 2,
                        _graphics.PreferredBackBufferHeight / 2));
                    break;
                case GameState.WindowFrame:
                    Rectangle rec2 = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 4,
                        GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 4);
                    bg.Rec = rec2;
                    bg.Draw(_spriteBatch);
                    menu.Draw(_spriteBatch, extensionsMenu, new Vector2(_graphics.PreferredBackBufferWidth / 2,
                        _graphics.PreferredBackBufferHeight / 2));
                    break;
                case GameState.Sound:
                    menu.Draw(_spriteBatch, soundMenu, new Vector2(_graphics.PreferredBackBufferWidth / 2,
                        _graphics.PreferredBackBufferHeight / 2));
                    break;
                case GameState.GameMenu:
                    menu.Draw(_spriteBatch, gameMenu, new Vector2(_graphics.PreferredBackBufferWidth / 2,
                        _graphics.PreferredBackBufferHeight / 2));
                    break;
                default:
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

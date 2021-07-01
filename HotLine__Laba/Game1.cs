using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HotLine__Laba.Classes;
using HotLine__Laba.Classes.Connectors;
using System;
using System.Collections.Generic;
using System.Text;
namespace HotLine__Laba
{
    public class Game1 : Game
    {
        static Random r = new Random();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        GamePlay play;
       
        //MapLoader loader;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            play = new GamePlay(Content,_graphics);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            play.Initialize();
            base.Initialize();
           
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            play.LoadContent(_spriteBatch);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            play.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            play.Draw();
            base.Draw(gameTime);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Media;
using Microsoft.Xna.Framework.Media;
using WMPLib;

namespace menu.UI
{
    class Menu
    {
        private int selected = 0;
        private KeyboardState keyboard;
        private KeyboardState prevKeyboard;
        private Vector2 size = new Vector2(0, 0);

        private static int msize = 10;
        private static int ssize = 10;
        private Bar musicBar = new Bar(msize);
        private Bar soundBar = new Bar(10);
        private Bar backBar = new Bar(ssize);

        //private Song selectSound;
        private WindowsMediaPlayer WMP = new WindowsMediaPlayer();
        private WindowsMediaPlayer enterWMP = new WindowsMediaPlayer();

        private SpriteFont spriteFont;

        public Vector2 Size
        {
            get { return size; }
        }

        public void LoadContent(ContentManager Content)
        {
            spriteFont = Content.Load<SpriteFont>("MenuFont");
            //selectSound = Content.Load<Song>("selectsound");
            WMP.URL = @"G:\PROGTIME\Программирование\C#2\menu\menu\Content\selectsound.mp3";
            enterWMP.URL = @"G:\PROGTIME\Программирование\C#2\menu\menu\Content\entersound.mp3";
            backBar.LoadContent(Content, "lowbar");
            musicBar.LoadContent(Content, "highbar");
            soundBar.LoadContent(Content, "highbar");
            WMP.settings.volume = 10;
            enterWMP.settings.volume = 10;
        }

        public void Draw(SpriteBatch brush, List<string> glist, Vector2 pos)
        {

            Color color;
            for (int i = 0; i < glist.Count; i++)
            {
                if (selected == i)
                {
                    color = Color.Aqua;
                }
                else
                {
                    color = Color.HotPink;
                }
                brush.DrawString(spriteFont, glist[i], new Vector2(pos.X - 12 * glist.Count + 20 * i, pos.Y - 12 * glist.Count + 20 * i), color);
            }
            if (Game1.gameState == GameState.Sound)
            {
                musicBar.Draw(brush, new Vector2(pos.X - 12 * glist.Count + 50, pos.Y - 12 * glist.Count - 3));
                backBar.Draw(brush, new Vector2(pos.X - 12 * glist.Count + 50, pos.Y - 12 * glist.Count - 3));
                soundBar.Draw(brush, new Vector2(pos.X - 12 * glist.Count + 65, pos.Y - 12 * glist.Count + 18));
                backBar.Draw(brush, new Vector2(pos.X - 12 * glist.Count + 65, pos.Y - 12 * glist.Count + 18));
            }
        }

        public void Update(List<string> glist)
        {
            keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.S) && (keyboard.IsKeyDown(Keys.S) != prevKeyboard.IsKeyDown(Keys.S)))
            {
                if (selected < glist.Count - 1)
                {
                    selected++;
                    WMP.controls.play();
                    //MediaPlayer.Play(selectSound);
                }

            }
            if (keyboard.IsKeyDown(Keys.W) && (keyboard.IsKeyDown(Keys.W) != prevKeyboard.IsKeyDown(Keys.W)))
            {
                if (selected > 0)
                {
                    selected--;
                    WMP.controls.play();
                    //MediaPlayer.Play(selectSound);
                }

            }

            if (Game1.gameState == GameState.Menu && prevKeyboard != keyboard)
            {
                if (keyboard.IsKeyDown(Keys.Enter))
                {
                    enterWMP.controls.play();
                    switch (selected)
                    {
                        case 0:
                            Game1.gameState = GameState.Play;
                            break;
                        case 1:
                            Game1.gameState = GameState.Play;
                            break;
                        case 2:
                            Game1.gameState = GameState.Settings;
                            selected = 0;
                            break;
                        case 3:
                            Game1.gameState = GameState.Info;
                            selected = 0;
                            break;
                        case 4:
                            Game1.gameState = GameState.Exit;
                            break;
                        default:
                            break;
                    }
                }
                prevKeyboard = keyboard;
            }
            else if (Game1.gameState == GameState.Settings && prevKeyboard != keyboard)
            {
                if (keyboard.IsKeyDown(Keys.Enter))
                {
                    enterWMP.controls.play();
                    switch (selected)
                    {
                        case 0:
                            Game1.gameState = GameState.Extensions;
                            selected = 0;
                            break;
                        case 1:
                            Game1.gameState = GameState.Sound;
                            selected = 0;
                            break;
                        case 2:
                            if (Game1.prevGameState == GameState.Menu)
                            {
                                Game1.gameState = GameState.Menu;
                                selected = 0;
                            }
                            else if (Game1.prevGameState == GameState.GameMenu)
                            {
                                Game1.gameState = GameState.GameMenu;
                                selected = 0;
                            }
                            break;
                        default:
                            break;
                    }
                }
                prevKeyboard = keyboard;
            }
            else if (Game1.gameState == GameState.Extensions && prevKeyboard != keyboard)
            {
                if (keyboard.IsKeyDown(Keys.Enter))
                {
                    enterWMP.controls.play();
                    switch (selected)
                    {
                        case 0:
                            Game1.gameState = GameState.FullScreen;
                            break;
                        case 1:
                            Game1.gameState = GameState.Window;
                            break;
                        case 2:
                            Game1.gameState = GameState.Settings;
                            selected = 0;
                            break;
                        default:
                            break;
                    }
                }
                prevKeyboard = keyboard;
            }
            else if (Game1.gameState == GameState.Info && prevKeyboard != keyboard)
            {
                if (keyboard.IsKeyDown(Keys.Enter))
                {
                    enterWMP.controls.play();
                    switch (selected)
                    {
                        case 0:
                            Game1.gameState = GameState.Menu;
                            selected = 0;
                            break;
                        default:
                            break;
                    }
                }
                prevKeyboard = keyboard;
            }
            else if (Game1.gameState == GameState.Sound && prevKeyboard != keyboard)
            {
                switch (selected)
                {
                    case 0:
                        if (keyboard.IsKeyDown(Keys.Left) && msize > 0)
                        {
                            musicBar.Update(msize -= 1);
                        }
                        if (keyboard.IsKeyDown(Keys.Right) && msize < 10)
                        {
                            musicBar.Update(msize += 1);
                        }
                        break;
                    case 1:
                        if (keyboard.IsKeyDown(Keys.Left) && ssize > 0)
                        {
                            soundBar.Update(ssize -= 1);
                            WMP.settings.volume = ssize;
                            enterWMP.settings.volume = ssize;
                        }
                        if (keyboard.IsKeyDown(Keys.Right) && ssize < 10)
                        {
                            soundBar.Update(ssize += 1);
                            WMP.settings.volume = ssize;
                            enterWMP.settings.volume = ssize;
                        }
                        break;
                    case 2:
                        if (keyboard.IsKeyDown(Keys.Enter))
                        {
                            enterWMP.controls.play();
                            Game1.gameState = GameState.Settings;
                            selected = 0;
                        }
                        break;
                    default:
                        break;
                }
                prevKeyboard = keyboard;
            }
            else if (Game1.gameState == GameState.GameMenu && prevKeyboard != keyboard)
            {
                if (keyboard.IsKeyDown(Keys.Enter))
                {
                    enterWMP.controls.play();
                    switch (selected)
                    {
                        case 0:

                            break;
                        case 1:

                            break;
                        case 2:
                            Game1.gameState = GameState.Settings;
                            selected = 0;
                            break;
                        case 3:
                            Game1.gameState = GameState.Menu;
                            selected = 0;
                            break;
                        default:
                            break;
                    }
                }
                prevKeyboard = keyboard;
            }
        }

        /*public void SetMenuPosition(Vector2 position,List<string> glist)

        {
            Position = position;
            for (int i = 0; i < glist.Count; i++)
            {
                glist[i].Position = position;
                position.Y += 30;
            }
        }
        */
        /*private void SetSizeWidth()
        {
            string biggestWord = "";
            int max = 0;
            foreach (var txt in )
            {
                if (txt.Length > max)
                {
                    biggestWord = txt;
                    max = txt.Length;
                }
            }
            Console.WriteLine(biggestWord);
            size.X = biggestWord.Length * 8;
        }
        */
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

namespace MonoBlob
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static int gameState = 0;

        #region Variables
        int ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        int wWidth = 0;
        int wHeight = 0;
        int started = 0;
        const int buttonPad = 5;
        const float windowScale = 0.8f;
        const float planeScaleW = 0.75f;
        const float planeScaleH = 0.66f;
        const float lineThick = 2f;
        Color planeColor = Color.Cornsilk;
        Color backColor = Color.CornflowerBlue;
        Color lineColor = Color.Blue;
        public static Random rnd = new Random();
        KeyboardState oldKeyState;
        MouseState oldMouseState;
     
        List<Blob> blobs = new List<Blob>();
        List<Button> buttons = new List<Button>();
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public void Reset()
        {
            started = 0;
            blobs.Clear();
        }

        public void StartSimulation()
        {
            if (started == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Blob b = new Blob(50 * (i+5), 250, this.Content);
                    blobs.Add(b);
                }
            }
            started = 1;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            this.IsMouseVisible = true;
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = (int)(ScreenWidth * windowScale); wWidth = graphics.PreferredBackBufferWidth;
            graphics.PreferredBackBufferHeight = (int)(ScreenHeight * windowScale); wHeight = graphics.PreferredBackBufferHeight;
            this.Window.Position = new Point((int)(ScreenWidth * (1 - windowScale) / 2), (int)(ScreenHeight * (1 - windowScale)/4));
            graphics.ApplyChanges();

            Button start = new Button(wWidth * planeScaleW + buttonPad, 0 + buttonPad, "Start", Color.Green);
            buttons.Add(start);
            Button reset = new Button(wWidth * planeScaleW + buttonPad + (60 + buttonPad), 0 + buttonPad, "Reset", Color.Black);
            buttons.Add(reset);
            Button simSpeedMinus = new Button(wWidth * planeScaleW + buttonPad, 30 + buttonPad * 2, "Speed Minus", Color.Blue);
            buttons.Add(simSpeedMinus);
            Button simSpeedPlus = new Button(wWidth - buttonPad - 60, 30 + buttonPad * 2, "Speed Plus", Color.Blue);
            buttons.Add(simSpeedPlus);
        }

        #region Stuff
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        #endregion

        protected override void Update(GameTime gameTime)
        {
            KeyboardState newKeyState = Keyboard.GetState();
            MouseState newMouseState = Mouse.GetState();
            Rectangle mousePos = new Rectangle(Mouse.GetState().Position, new Point(1, 1));

            if (newKeyState.IsKeyDown(Keys.Escape))
            {
                gameState = 0;
                Exit();
            }

            if (newKeyState.IsKeyUp(Keys.Space) && oldKeyState.IsKeyDown(Keys.Space))
            {
                if (gameState == 0) gameState = 1;
                else gameState = 0;
            }

            if (mousePos.Intersects(buttons[0].getRect()))
            {
                if (newMouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (gameState == 0) {gameState = 1; buttons[0].setColor(Color.Red);}
                    else { gameState = 0; buttons[0].setColor(Color.Green); }
                }
            }

            if (mousePos.Intersects(buttons[1].getRect()))
            {
                if (newMouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed)
                {
                    gameState = 0;
                    buttons[0].setColor(Color.Green);
                    Reset();
                }
            }

            if (mousePos.Intersects(buttons[2].getRect()))
            {
                if (newMouseState.LeftButton == ButtonState.Pressed)// && oldMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (BlobManager.delay < 990) BlobManager.delay += 10f;
                }
            }

            if (mousePos.Intersects(buttons[3].getRect()))
            {
                if (newMouseState.LeftButton == ButtonState.Pressed)// && oldMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (BlobManager.delay > 0) BlobManager.delay -= 10f;
                }
            }

            if (gameState == 1)
            {
                if (started == 0) StartSimulation();
                foreach (Blob b in blobs)
                {
                    b.Update(gameTime);
                }

                base.Update(gameTime);
            }

            oldKeyState = newKeyState;
            oldMouseState = newMouseState;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backColor);
            spriteBatch.Begin();

            spriteBatch.FillRectangle(0, 0, wWidth * planeScaleW, wHeight * planeScaleH, planeColor);

            foreach (Blob b in blobs)
            {
                //if (b.getX() > 5 && b.getX() < wWidth * planeScaleW - 5 && b.getY() > 5 && b.getY() < wHeight * planeScaleH - 5)
                b.Draw(spriteBatch);
            }
            spriteBatch.FillRectangle(wWidth * planeScaleW, 0, wWidth * planeScaleW, wHeight, backColor);
            spriteBatch.FillRectangle(0, wHeight * planeScaleH, wWidth, wHeight * planeScaleH, backColor);

            spriteBatch.FillRectangle(wWidth * planeScaleW + buttonPad * 2 + 60, 30 + buttonPad * 2, ((wWidth - buttonPad * 2 - 60) - (wWidth * planeScaleW + buttonPad * 2 + 60)) * (1000f-BlobManager.delay)/1000f, 30, Color.Green);

            foreach (Button b in buttons)
            {
                b.Draw(spriteBatch);
            }

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}

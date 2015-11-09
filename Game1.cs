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
        const float windowScale = 0.8f;
        const float planeScaleW = 0.75f;
        const float planeScaleH = 0.66f;
        const float lineThick = 2f;
        Color planeColor = Color.Cornsilk;
        Color backColor = Color.CornflowerBlue;
        Color lineColor = Color.Blue;
        public static Random rnd = new Random();
        KeyboardState oldState;
     
        List<Blob> blobs = new List<Blob>();
        List<Button> buttons = new List<Button>();
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            #region Debug
            for (int i = 0; i < 10; i++)
            {
                Blob b = new Blob(50 * (i+5), 250, this.Content);
                blobs.Add(b);
            }

            Button speedMinus = new Button(wWidth * planeScaleW, 0);
            buttons.Add(speedMinus);
            #endregion
        }

        #region
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
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Escape))
            {
                gameState = 0;
                Exit();
            }

            if (newState.IsKeyUp(Keys.Space) && oldState.IsKeyDown(Keys.Space))
            {
                if (gameState == 0) gameState = 1;
                else gameState = 0;
            }

            if (gameState == 1)
            {
                foreach (Blob b in blobs)
                {
                    b.Update(gameTime);
                }

                base.Update(gameTime);
            }

            oldState = newState;
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

            foreach (Button b in buttons)
            {
                b.Draw(spriteBatch);
            }

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}

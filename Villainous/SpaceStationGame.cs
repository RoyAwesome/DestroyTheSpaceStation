#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.IO;
#endregion

namespace Villainous
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SpaceStationGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Station station;
        EntityManager manager;

        public SpaceStationGame()
            : base()
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
            manager = new EntityManager();
            Console.WriteLine(typeof(PlayerEntity));
            base.Initialize();
            UserInterface.width = GraphicsDevice.Viewport.Width;
            UserInterface.height = GraphicsDevice.Viewport.Height;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ArtManager.LoadAllTexture(this.Content);
           
           
            // TODO: use this.Content to load your game content here

            station = new Station(ArtManager.GetTexture("spacestation"));
            using (StreamReader reader = new StreamReader("Content\\metadata.txt"))
            {
                String[] data = reader.ReadToEnd().Split('\n');
                TileMap.ParseMetadata(data);
            }
            
            UserInterface.Message("Game is running!");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
        /// 
       protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            manager.update((float)gameTime.ElapsedGameTime.TotalSeconds);

            UserInterface.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            station.Draw(spriteBatch, (float)gameTime.ElapsedGameTime.TotalSeconds);
            manager.Draw(spriteBatch, (float)gameTime.ElapsedGameTime.TotalSeconds);

            UserInterface.Draw(spriteBatch, (float)gameTime.ElapsedGameTime.TotalSeconds);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

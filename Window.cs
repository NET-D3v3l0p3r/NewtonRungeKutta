using CelestialMechanicSimulatorV2MG.Core;
using CelestialMechanicSimulatorV2MG.Mathematics;
using Lines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CelestialMechanicSimulatorV2MG
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Window : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        

        public Window()
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
            IsFixedTimeStep = false;
            Globals.Device = GraphicsDevice;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            new Camera(0.007f, 1.0f);


            CelestialBody.InfluencingBodies.Add(new Sun() { Texture = Content.Load<Texture2D>(@"SunColor") });
            CelestialBody.InfluencingBodies.Add(new Mercury() { Texture = Content.Load<Texture2D>(@"MerkurColor") });
            CelestialBody.InfluencingBodies.Add(new Venus() { Texture = Content.Load<Texture2D>(@"VenusColor") });
            CelestialBody.InfluencingBodies.Add(new Earth() { Texture = Content.Load<Texture2D>(@"ErdeColor") });
            CelestialBody.InfluencingBodies.Add(new Mars() { Texture = Content.Load<Texture2D>(@"MarsColor") });
            CelestialBody.InfluencingBodies.Add(new Jupiter() { Texture = Content.Load<Texture2D>(@"JupiterColor") });
            CelestialBody.InfluencingBodies.Add(new Saturn() { Texture = Content.Load<Texture2D>(@"SaturnColor") });
            CelestialBody.InfluencingBodies.Add(new Uranus() { Texture = Content.Load<Texture2D>(@"UranusColor") });
            CelestialBody.InfluencingBodies.Add(new Neptun() { Texture = Content.Load<Texture2D>(@"NeptunColor") });
            CelestialBody.InfluencingBodies.Add(new Pluto() { Texture = Content.Load<Texture2D>(@"PlutoColor") });

            for (int i = 1; i < CelestialBody.InfluencingBodies.Count; i++)
                CelestialBody.InfluencingBodies[i].PreSimulate();


            CelestialBody.InitializePaths();
            CelestialBody.Model = Content.Load<Model>(@"celestialbody");

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
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!IsActive)
                return;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Camera.Move(new Vector3(0, 0, 1));
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                Camera.Move(new Vector3(0, 0, -1));

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Camera.Move(new Vector3(-1, 0, 0));
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Camera.Move(new Vector3(1, 0, 0));
            Camera.Update();
            // TODO: Add your update logic here

            foreach (var cb in CelestialBody.InfluencingBodies)
            {
                cb.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            foreach (var cb in CelestialBody.InfluencingBodies)
            {
                cb.Render();
            }
            // TODO: Add your drawing code here
            CelestialBody.RenderPaths();
            base.Draw(gameTime);
        }
    }
}

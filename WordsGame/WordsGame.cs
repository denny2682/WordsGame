using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;

namespace WordsGame.Game
{
   public class WordsGame : Microsoft.Xna.Framework.Game
   {
      // Static classes of Xna for the graphic manager
      public static GraphicsDeviceManager Graphics;
      public static SpriteBatch SpriteBatch;

      // Static class of Xna for graphics content
      public static ContentManager Content;

      // Display settings
      Vector2 baseScreenSize = new Vector2(1200, 1200);
      private Matrix globalTransformation;
      int backbufferWidth = 1200;
      int backbufferHeight = 1000;

      // Get state button
      bool mousePressed = false;
      bool mouseRelease = false;

      // game manager operate on the levels and game logic
      private GameManager gameManager;

      /// <summary>
      /// Define the main class of the game
      /// </summary>
      public WordsGame()
      {
         // Istance Graphic Device Manager
         Graphics = new GraphicsDeviceManager(this);

         // Set the screen not to full screen
         Graphics.IsFullScreen = false;

         // Set the resource directory
         base.Content.RootDirectory = "Content";
         Content = base.Content;

         // Set the mouse visible
         IsMouseVisible = true;
      }

      /// <summary>
      /// Initialize Game
      /// </summary>
      protected override void Initialize()
      {
         // Set preference screen width and height
         Graphics.PreferredBackBufferWidth = backbufferWidth;
         Graphics.PreferredBackBufferHeight = backbufferHeight;
         Graphics.ApplyChanges();

         // game manager 
         gameManager = new GameManager(
            new List<LevelSettings>() {
               new LevelSettings(1, 30, 5, 5),
               new LevelSettings(2, 60, 6, 6),
               new LevelSettings(3, 120, 7, 7)
         }, backbufferWidth,
            backbufferHeight
       );

         // Call method initialize XNA
         base.Initialize();
      }

      /// <summary>
      /// Load asset and presentation area
      /// </summary>
      protected override void LoadContent()
      {
         SpriteBatch = new SpriteBatch(GraphicsDevice);
         base.Content.RootDirectory = "Content";

         // Resize the presentation area for the device
         ScalePresentationArea();

         // Load Content in XNA
         base.LoadContent();
      }


      /// <summary>
      ///  Update method between 30 and 60 times per second on a normal situation (based on the device).
      /// </summary>
      /// <param name="gameTime">gameTime supplier from XNA </param>
      protected override void Update(GameTime gameTime)
      {
         // Call Update from manager game
         gameManager.Update(gameTime);

         // Handles user input events
         HandlesEventsInput(gameTime);

         // Call method update XNA
         base.Update(gameTime);
      }

      /// <summary>
      /// Draw
      /// Call 60 update per seconds
      /// </summary>
      /// <param name="gameTime"></param>
      protected override void Draw(GameTime gameTime)
      {
         // Get all sprites for the screen
         List<ISprite> sprites = gameManager.GetCurrentSprite();

         // Clear device screen
         Graphics.GraphicsDevice.Clear(new Color(63, 184, 175, 255));

         // Begin sprite batch
         SpriteBatch.Begin();

         // Draw all screen sprites
         if (sprites != null)
         {
            foreach (var sprite in sprites)
               sprite.Draw(SpriteBatch);
         }

         // End Draw
         SpriteBatch.End();

         // Call method base XNA
         base.Draw(gameTime);
      }

      // Call Events associated to Mouse Input
      private void HandlesEventsInput(GameTime gameTime)
      {
         // Update the current state of the mouse

         MouseState mouseState = Mouse.GetState();
         // Left button clicked
         if (mouseState.LeftButton == ButtonState.Pressed)
         {
            mousePressed = true;
            mouseRelease = false;
            gameManager.MouseIsLeftDown();
         }

         // Mouse Released 
         if (mouseState.LeftButton == ButtonState.Released && mousePressed && !mouseRelease)
         {
            mouseRelease = true;
            mousePressed = false;
            gameManager.MouseIsLeftRelease();
         }

         gameManager.MouseIsOver();

         // Poll for current keyboard state
         KeyboardState state = Keyboard.GetState();

         // If they hit esc, exit
         if (state.IsKeyDown(Keys.Escape))
         {
            // graphic resource free
            Content.Unload();

            // Exit from the game
            Exit();
         }


      }
      /// <summary>
      /// Redefine presentation area 
      /// </summary>
      public void ScalePresentationArea()
      {
         //Work out how much we need to scale our graphics to fill the screen
         backbufferWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
         backbufferHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;
         float horScaling = backbufferWidth / baseScreenSize.X;
         float verScaling = backbufferHeight / baseScreenSize.Y;
         Vector3 screenScalingFactor = new Vector3(horScaling, verScaling, 1);
         globalTransformation = Matrix.CreateScale(screenScalingFactor);
      }
   }
}

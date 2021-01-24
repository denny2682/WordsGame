using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using WordsGame.Game.Utility;
using System.Diagnostics;

namespace WordsGame.Game
{

   /// <summary>
   /// Defines a class to show text in the screen
   /// </summary>
   public class Text : ISprite
   {
      #region public variables
      public string ViewText { get; set; }
      public ColorRGB ColorText { get; set; }
      public Coordinate2D Coordinate { get; set; }
      public TypeFont Font { get; set; }

      #endregion

      /// <summary>
      /// Costructor
      /// Defines a text sprite 
      /// </summary>
      /// <param name="graphicFontInfo"></param>
      /// <param name="text"></param>
      public Text(GraphicFontInfo settings, string text)
      {
         Coordinate = settings.Coordinate;
         ViewText = text;
         Font = settings.Font;
         ColorText = settings.Color;
      }

      #region public methods

      /// <summary>
      /// Sprite type
      /// </summary>
      /// <returns></returns>
      public TypeSprite GetTypeSprite()
      {
         return TypeSprite.Text;
      }

      /// <summary>
      /// Updates the text
      /// </summary>
      /// <param name="text"></param>
      public void UpdateText(string text)
      {
         ViewText = text;
      }

      /// <summary>
      /// Draw in spriteBatch
      /// </summary>
      /// <param name="spriteBatch"></param>
      public void Draw(SpriteBatch spriteBatch)
      {
         try
         {
            SpriteFont spriteFont = WordsGame.Content.Load<SpriteFont>("Fonts/" + Font.ToString());
            spriteBatch.DrawString(spriteFont, ViewText, new Vector2(Coordinate.X, Coordinate.Y), Utils.GetColorXNA(ColorText));
         }
         catch (Exception ex)
         {
            throw new Exception("Unexpected error draw detail message: " + ex.Message);
         }
      }

      #endregion

   }





}
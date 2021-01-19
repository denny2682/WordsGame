using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using WordsGame.Game.Utility;

namespace WordsGame.Game
{
  
   /// <summary>
   /// Defines a class to show text in the screen
   /// </summary>
   public class Text : ISprite
   {
      #region private variables
      // Color and type font
      private ColorRGB colorText; 
      private TypeFont font;
      #endregion

      #region public variables
      public int PosX;
      public int PosY;
      public string ViewText;
      #endregion


      /// <summary>
      /// Defines a text sprite 
      /// </summary>
      /// <param name="typeFont"></param>
      /// <param name="text"></param>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="color"> color is optional </param>
      public Text(TypeFont typeFont, string text, int x, int y, ColorRGB color = new ColorRGB())
      {
         font = typeFont;
         ViewText = text;
         PosX = x;
         PosY = y;
         colorText = color;
      }

      /// <summary>
      /// Type sprite
      /// </summary>
      /// <returns></returns>
      public TypeSprite GetTypeSprite()
      {
         return TypeSprite.Text;
      }

      /// <summary>
      /// Draw in spriteBatch
      /// </summary>
      /// <param name="spriteBatch"></param>
      public void Draw(SpriteBatch spriteBatch)
      {
         try
         {
            SpriteFont spriteFont = WordsGame.Content.Load<SpriteFont>("Fonts/" + font.ToString());
            spriteBatch.DrawString(spriteFont, ViewText, new Vector2(PosX, PosY), Utils.GetColorXNA(colorText));
         }
         catch (Exception ex)
         {
            Console.WriteLine("Unexpected error draw detail message: " + ex.Message);
         }




      }

      public void UpdateText(string text)
      {
         ViewText = text;
      }

     
   }


   


}
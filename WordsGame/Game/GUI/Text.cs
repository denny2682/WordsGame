using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameWords.Game.Utility;

namespace GameWords.Game
{
   // Todo:
   // Si potrebbe prevedere un erditarietà con estensione di un particolare effetto su testo
   // Solo alla fine e solo se necessario
   /// <summary>
   /// 
   /// </summary>
   public class Text : ISprite
   {
      // Colore di Default
      private ColorRGB colorText;
      public int PosX;
      public int PosY;
      public string ViewText;
      private TypeFont font;
      private readonly TypeSprite type = TypeSprite.Text;


      //L'ultimo parametro è opzionale
      /// <summary>
      /// 
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

      public TypeSprite GetTypeSprite()
      {
         return type;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="content"></param>
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
            Console.WriteLine("Si è verificato un problema durante il draw del font: " + ex.Message);
         }




      }

      public void UpdateText(string text)
      {
         ViewText = text;
      }

     
   }


   


}
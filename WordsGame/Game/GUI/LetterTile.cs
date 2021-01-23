using System;
using System.Collections.Generic;
using System.Text;
using WordsGame.Game.Utility;


namespace WordsGame.Game
{
   /// <summary>
   /// LetterTile is a screen sprite
   /// </summary>
   class LetterTile : ImageProxy
   {
      #region private variables
      private int value;
      private long sequenceSelected;
      private GameManager gameManager;
      private char letter;
      private readonly ColorRGB colorSelected = new ColorRGB(161, 096, 255, 255);
      #endregion

      /// <summary>
      /// Costructor
      /// </summary>
      public LetterTile(char charLetter, GraphicImageInfo settings, GameManager gameController) : base(settings)
      {
         sequenceSelected = 0;
         letter = charLetter;
         value = getValue(letter);

         // Bind the method to events
         gameManager = gameController;
         gameManager.MouseOnPressed += this.OnSelected;
         gameManager.MouseOnReleased += this.OnReleaseSelection;
      }

      /// <summary>
      /// return value of letter
      /// </summary>
      public int Value {
         get { return value; }
      }

      /// <summary>
      /// return DateTime.Now.Ticks
      /// </summary>
      public long SequenceSelected
      {
         get { return sequenceSelected; }
      }

      /// <summary>
      /// returns the letter of the alphabet
      /// </summary>
      public char Letter
      {
         get { return letter; }
      }

      /// <summary>
      /// return Type sprite
      /// </summary>
      /// <returns></returns>
      public override TypeSprite GetTypeSprite()
      {
         return TypeSprite.Letter;
      }
 
      /// <summary>
      /// Return the value of the letter
      /// </summary>
      /// <param name="letter"></param>
      /// <returns></returns>
      private int getValue(char letter)
      {
         int value = 0;

         switch (char.ToUpper(letter))
         {
            case 'B':
            case 'D':
            case 'F':
            case 'G':
            case 'U':
            case 'V':
               value = 4;
               break;
            case 'H':
            case 'Z':
               value = 8;
               break;
            case 'P':
               value = 3;
               break;
            case 'L':
            case 'M':
            case 'N':
               value = 2;
               break;
            case 'Q':
               value = 10;
               break;
            default:
               value = 1;
               break;
         }
         return value;
      }

      /// <summary>
      /// return the frequency 
      /// </summary>
      /// <param name="letter"></param>
      /// <returns></returns>
      public static double getAnalisysExtractLangueIT(char letter)
      {
         double frequence = 0;
         switch (char.ToUpper(letter))
         {
            case 'A':
               frequence = 11.74;
               break;
            case 'B':
               frequence = 0.92;
               break;
            case 'C':
               frequence = 4.50;
               break;
            case 'D':
               frequence = 3.73;
               break;
            case 'E':
               frequence = 11.79;
               break;
            case 'F':
               frequence = 0.95;
               break;
            case 'G':
               frequence = 1.64;
               break;
            case 'H':
               frequence = 1.54;
               break;
            case 'I':
               frequence = 6.51;
               break;
            case 'L':
               frequence = 11.28;
               break;
            case 'M':
               frequence = 2.51;
               break;
            case 'N':
               frequence = 6.88;
               break;
            case 'O':
               frequence = 9.83;
               break;
            case 'P':
               frequence = 3.05;
               break;
            case 'Q':
               frequence = 0.51;
               break;
            case 'R':
               frequence = 6.37;
               break;
            case 'S':
               frequence = 4.98;
               break;
            case 'T':
               frequence = 5.62;
               break;
            case 'U':
               frequence = 3.01;
               break;
            case 'V':
               frequence = 2.10;
               break;
            case 'Z':
               frequence = 0.49;
               break;
         }
         return frequence;
      }

      /// <summary>
      /// This method is called when the letter is pressed
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      public void OnSelected(object sender, EventArgs e)
      {
         if (this.IsSelectedArea())
         {
            base.highlight = true;
            
            long date = DateTime.Now.Ticks;
            if (sequenceSelected == 0)
               sequenceSelected = date;
            

         }
      }

      /// <summary>
      /// This method is called when the letter is released
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      public void OnReleaseSelection(object sender, EventArgs e)
      {
         base.highlight = false;
         sequenceSelected = 0;
      }

   }
}

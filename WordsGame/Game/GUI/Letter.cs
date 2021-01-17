using System;
using System.Collections.Generic;
using System.Text;
using GameWords.Game.Utility;


namespace GameWords.Game
{
   // Todo: alla fine potrebbe essere necessario effettuare le seguenti modifiche
   // LetterTile potrebbe ereditare da image e text invece già lo è
   // LetterTile dunque estende image
   class LetterTile : ImageProxy
   {
      private int value;
      private long sequenceSelected = 0;
      private GameManager gameManager;
      private char letter;
      private readonly ColorRGB colorSelected = new ColorRGB(161, 096, 255, 255);


      public bool justSelected = true;
      
      /// <summary>
      /// Costruttore di una nuova tessera lettera
      /// </summary>
      public LetterTile(char charLetter, string fileName, int x, int y, ColorRGB colorRGB, GameManager gameController) : base(fileName, x, y, colorRGB)
      {
         
         letter = charLetter;
         value = getValue(letter);
         justSelected = false;
         gameManager = gameController;
         gameManager.MouseOnPress += this.OnSelected;
         gameManager.MouseOnRelease += this.OnReleaseSelection;

         //gameManager.Attach(this);
      }

      public int Value {
         get { return value; }
      }

      public long SequenceSelected
      {
         get { return sequenceSelected; }
      }

      public char Letter
      {
         get { return letter; }
      }

      public override TypeSprite GetTypeSprite()
      {
         return TypeSprite.Letter;
      }
 
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

      // prova eventi c#
      public virtual void OnSelected(object sender, EventArgs e)
      {
         // da tenere un elenco di elementi aggiornabili, che non è detto siano delle lettere
         // Può essere anche un bottone per andare al secondo livello.
         
         if (this.IsSelectedArea())
         {
            base.highlight = true;
            long date = DateTime.Now.Ticks;
            sequenceSelected = date;
            
         }
      }

      public virtual void OnReleaseSelection(object sender, EventArgs e)
      {
         // da tenere un elenco di elementi aggiornabili, che non è detto siano delle lettere
         // Può essere anche un bottone per andare al secondo livello.
         base.highlight = false;
         sequenceSelected = 0;
      }

   }
}

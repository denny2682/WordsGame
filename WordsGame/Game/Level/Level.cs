using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GameWords.Game.Utility;

namespace GameWords.Game
{

   // Pattern builder: role concrete builder
   public sealed class Level : ILevelBuilder
   {
      #region private variables

      private int score = 0;
      private List<ISprite> sprites = new List<ISprite>();
      // list of words selected in the level
      private List<string> selectedWordList = new List<string>();
      private ISprite[,] spriteGridLetter;

      // Italian alphabet
      private const string letters = "abcdefghilmnopqrstuvz";
      private const string vowels = "aeiou";

      private GameManager game;

      private Text spriteTime = null;
      private ISprite scoreSprite = null;

      #endregion

      /// <summary>
      /// Costructor
      /// </summary>
      /// <param name="gameManager">gameManager</param>
      public Level(GameManager gameManager)
      {
         game = gameManager;
      }

      #region public variables

      /// <summary>
      /// Return current score
      /// </summary>
      public int Score
      {
         get { return score; }
      }
      #endregion

      #region pattern builder: public methods 

      /// <summary>
      /// Builds a generic text sprite
      /// </summary>
      /// <param name="typeFont">type font</param>
      /// <param name="textView">text to show</param>
      /// <param name="x">position x</param>
      /// <param name="y">position y</param>
      /// <param name="color">color in Rgb</param>
      public void buildText(TypeFont typeFont, string textView, int x, int y, ColorRGB color = new ColorRGB())
      {
         Text text = new Text(typeFont, textView, x, y, color);
         sprites.Add(text);
      }

      /// <summary>
      /// Builds a letters grid
      /// </summary>
      /// <param name="row">number row</param>
      /// <param name="coloumn">number coloumn</param>
      /// <param name="posRelx">position x</param>
      /// <param name="posRelY">position y</param>
      public void buildGrid(int row, int coloumn, int posRelx, int posRelY)
      {
         Random rnd = new Random();
         spriteGridLetter = new ISprite[row, coloumn];
         int posX = posRelx;
         int width = 0;
         int height = 0;

         // Visual distance between the letters
         int distance = 20;

         // Create a grid with with the letters of the alphabet
         string[,] letterGrid = createLetterGrid(row, coloumn);

         // 
         for (int x = 0; x < letterGrid.GetLength(0); x++)
         {
            posX += width + distance;
            int posY = posRelY;
            for (int y = 0; y < letterGrid.GetLength(1); y++)
            {
               string letter = letterGrid[x, y];
               // Todo:
               // va rivisto il modo di assegnare la posizione
               ColorRGB colorWhite = new ColorRGB(255, 255, 255, 255);
               LetterTile letterTile = new LetterTile(letter,"Letters/letter-" + letter, posX, posY, colorWhite, game);
               Size size = letterTile.GetExtent();
               width = size.Width;
               height = size.Height;
               posY += height + distance;
               spriteGridLetter[x, y] = letterTile;
               sprites.Add(spriteGridLetter[x, y]);
            }
         }
      }

      /// <summary>
      /// Builds the level score
      /// </summary>
      /// <param name="typeFont">type font</param>
      /// <param name="x">position x</param>
      /// <param name="y">position y</param>
      /// <param name="color">color in Rgb</param>
      public void buildScore(TypeFont typeFont, int x, int y, ColorRGB color = new ColorRGB())
      {
         Text text = new Text(typeFont, Score.ToString(), x, y, color);
         scoreSprite = text;
         sprites.Add(text);
      }

      /// <summary>
      /// Builds the button reload
      /// </summary>
      /// <param name="typeFont">type font</param>
      /// <param name="x">position x</param>
      /// <param name="y">position y</param>
      /// <param name="color">color in Rgb</param>
      public void buildBtnReloadLevel(int x, int y, ColorRGB color = new ColorRGB())
      {
         ButtonReloadLevel btn = new ButtonReloadLevel("images/reload", x, y, color, game);
         sprites.Add(btn);
      }

      /// <summary>
      /// Builds the button reload
      /// </summary>
      /// <param name="typeFont">type font</param>
      /// <param name="x">position x</param>
      /// <param name="y">position y</param>
      /// <param name="color">color in Rgb</param>
      public void buildBtnReloadGame(int x, int y, ColorRGB color = new ColorRGB())
      {
         ButtonReloadGame btn = new ButtonReloadGame("images/home-icon", x, y, color, game);
         sprites.Add(btn);
      }

      /// <summary>
      /// Builds the level score
      /// </summary>
      /// <param name="typeFont">type font</param>
      /// <param name="x">position x</param>
      /// <param name="y">position y</param>
      /// <param name="color">color in Rgb</param>
      public void buildWinner(int x, int y, ColorRGB color = new ColorRGB())
      {
         ImageProxy image = new ImageProxy("images/overlay_win", x, y, color);
         sprites.Add(image);
      }


      /// <summary>
      /// Update sprite of type time
      /// </summary>
      /// <param name="text">string to show</param>
      public void updateTime(string text)
      {
         if (spriteTime != null)
            spriteTime.ViewText = text;
      }

      /// <summary>
      /// Update sprite of type time
      /// </summary>
      /// <param name="text">string to show</param>
      public void UpdateScore()
      {
         // Carica il dizionario in una lista
         var globalWordsContainer = GlobalWordsContainer.Istance;
         var wordsDictionary = globalWordsContainer.Load("Content/Dictionary/660000_parole_italiane.txt");

         // Ordina le lettere selezionate in base al tempo del mouseDown sulla lettera
         var spriteOrdered = sprites.Where(x => x.GetTypeSprite().Equals(TypeSprite.Letter) && ((LetterTile)x).Highlight);
         // 
         var spritesDistinct = spriteOrdered.OrderBy(l => ((LetterTile)l).SequenceSelected).Distinct<ISprite>(); 

         var str = "";
         int value = 0;
         foreach (var sprite in spritesDistinct)
         {
            var letterTile = (LetterTile)sprite;
            str += letterTile.Letter;
            value += letterTile.Value;
         }

         // Verifica la lunghezza della parola e la sua presenza nel dizionario
         // Sono accettate solo parole lunghe più di 3 caratteri e parole non precedentemente selezionate nel precedente livello
         if (str.Length >= 4 && wordsDictionary.IndexOf(str) >= 0 && selectedWordList.IndexOf(str) == -1)
         {
            // Aggiunge la parola ad una lista
            // per poter essere escluse 
            selectedWordList.Add(str);

            // Esegue il suono associato alla parola corretta
            game.NotifyCorrectWord();

            // Aggiorna lo score
            score += value;
            ((Text)scoreSprite).UpdateText(Score.ToString());
         }
      }

      // Ritorna gli sprites
      public List<ISprite> GetSprites()
      {
         return sprites;
      }


      public void Reset()
      {
         // Inizializza il livello con il punteggio uguale a zero
         score = 0;
         scoreSprite = null;
         selectedWordList = new List<string>();
         sprites = new List<ISprite>();
      }
      #endregion

      #region privates methods
      private string[,] createLetterGrid(int row, int coloumn)
      {
         Random rnd = new Random();
         string[,] letterGrid = new string[row, coloumn];
         Dictionary<string, int> frequencyLetter = new Dictionary<string, int>();

         for (int x = 0; x < row; x++)
         {
            for (int y = 0; y < coloumn; y++)
            {
               string letter = "";
               if (rnd.Next(2) == 1)
               {
                  letter = extractactLetter(frequencyLetter, rnd, row * coloumn, letters);
               }
               else
                  letter = extractactLetter(frequencyLetter, rnd, row * coloumn, vowels);

               if (letter != "")
               {
                  if (!frequencyLetter.ContainsKey(letter))
                     frequencyLetter.Add(letter, 1);
                  else
                     frequencyLetter[letter] = frequencyLetter[letter] + 1;
               }
               else
               {
                  // Se non è riuscito ad estrapolare nessuna lettera mette una consonante
                  letter = vowels.Substring(rnd.Next(vowels.Length), 1);
               }

               letterGrid[x, y] = letter;
            }
         }

         return letterGrid;
      }

      private string extractactLetter(Dictionary<string, int> frequencyLetter, Random rnd, int LetterTotal, string letters)
      {
         string letterExtracted = "";
         int count = 0;

         while (letterExtracted == "" && count <= 21)
         {
            // Esce dal ciclo quando ha trovato la lettera o ha eseguito 21 cicli
            // Affinché l'algoritmo possa sempre uscire
            count++;
            string letter = letters.Substring(rnd.Next(letters.Length), 1);
            // Calcola per ogni lettera estratta la percentuale massima di estrazione
            double extractionmax = (LetterTile.getAnalisysExtractLangueIT(letter) * LetterTotal / 100);

            // se l'estrazione della lettera è inferiore alla sua massima estrazione allora l'aggiunge 
            if (frequencyLetter.ContainsKey(letter) && frequencyLetter[letter] < extractionmax)
               letterExtracted = letter;
            else if (!frequencyLetter.ContainsKey(letter))
               letterExtracted = letter;
         }

         return letterExtracted;
      }

      #endregion 





   }
}

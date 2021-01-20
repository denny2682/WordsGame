using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WordsGame.Game.Utility;

namespace WordsGame.Game
{

   /// <summary>
   /// Pattern builder: role concrete builder
   /// </summary>
   public sealed class Level : ILevelBuilder
   {
      #region private variables

      private int score = 0;
      private List<ISprite> sprites;
      // list of words selected in the level
      private List<string> selectedWordList;
      private ISprite[,] spriteGridLetter;

      // Italian alphabet
      private const string letters = "abcdefghilmnopqrstuvz";
      private const string vowels = "aeiou";

      private GameManager game;
      private ISprite scoreSprite = null;

      #endregion

      /// <summary>
      /// Costructor Level
      /// </summary>
      /// <param name="gameManager">gameManager</param>
      public Level(GameManager gameManager)
      {
         game = gameManager;
         sprites = new List<ISprite>();
         selectedWordList = new List<string>();
      }

      #region public variables

      /// <summary>
      /// Returns current score
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
      /// <param name="column">number column</param>
      /// <param name="posRelx">position x</param>
      /// <param name="posRelY">position y</param>
      public void buildGrid(int row, int column, int posRelx, int posRelY)
      {
         Random rnd = new Random();
         spriteGridLetter = new ISprite[row, column];
         int posX = posRelx;
         int width = 0;
         int height = 0;

         // Visual distance between the letters
         int distance = 20;

         // Creates a grid with with the letters of the alphabet
         char[,] letterGrid = createLetterGrid(row, column);

         // Builds the grid 
         for (int x = 0; x < letterGrid.GetLength(0); x++)
         {
            posX += width + distance;
            int posY = posRelY;
            for (int y = 0; y < letterGrid.GetLength(1); y++)
            {
               char letter = letterGrid[x, y];
               
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
      /// Builds Level reload button
      /// </summary>
      /// <param name="typeFont">type font</param>
      /// <param name="x">position x</param>
      /// <param name="y">position y</param>
      /// <param name="color">color in Rgb</param>
      public void buildReloadLevelBtn(int x, int y, ColorRGB color = new ColorRGB())
      {
         ButtonReloadLevel btn = new ButtonReloadLevel("images/reload", x, y, color, game);
         sprites.Add(btn);
      }

      /// <summary>
      /// Builds game reload button
      /// </summary>
      /// <param name="typeFont">type font</param>
      /// <param name="x">position x</param>
      /// <param name="y">position y</param>
      /// <param name="color">color in Rgb</param>
      public void buildReloadGameBtn(int x, int y, ColorRGB color = new ColorRGB())
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
      /// Update Score
      /// </summary>
      public void UpdateScore()
      {
         // Load the dictionary into a list
         GlobalWordsContainer globalWordsContainer = GlobalWordsContainer.Istance;
         List<string> wordsDictionary = globalWordsContainer.Load("Content/Dictionary/660000_parole_italiane.txt");

         // Sorts the selected letters according to the time of the mouseDown on the letter
         // prevents the same letter from being selected multiple times by hovering over it
         var spriteOrdered = sprites.Where(x => x.GetTypeSprite().Equals(TypeSprite.Letter) && ((LetterTile)x).Highlight);
         var spritesDistinct = spriteOrdered.OrderBy(l => ((LetterTile)l).SequenceSelected).Distinct<ISprite>(); 

         // reset string
         string str = "";
         int value = 0;

         // concatenate the letters selected
         foreach (var sprite in spritesDistinct)
         {
            // Read the letter from the sprite
            var letterTile = (LetterTile)sprite;
            str += letterTile.Letter;
            // Sums value of the letter selected
            value += letterTile.Value;
         }

         // Check the length of the word,  if the word exists in the dictionary and the word is not previously selected
         // Accept word lenght of characters four or more
         if (str.Length >= 4 && wordsDictionary.IndexOf(str) >= 0 && selectedWordList.IndexOf(str) == -1)
         {
            // Add new word
            selectedWordList.Add(str);

            // Call the method that plays the sound effect from the GameManager
            game.NotifyCorrectWord();

            // Updates the score value and update scoreSprite
            score += value;
            if (scoreSprite != null)
               ((Text)scoreSprite).UpdateText(Score.ToString());
         }
      }

      // Returns all sprites instantiated in the level
      public List<ISprite> GetSprites()
      {
         return sprites;
      }

      /// <summary>
      /// Resets level
      /// </summary>
      public void Reset()
      {
         // reset score, sprite and selectWordList 
         score = 0;
         scoreSprite = null;
         selectedWordList = new List<string>();
         sprites = new List<ISprite>();
      }
      #endregion

      #region privates methods
      /// <summary>
      /// Create a new letters grid
      /// </summary>
      /// <param name="row"></param>
      /// <param name="column"></param>
      /// <returns></returns>
      private char[,] createLetterGrid(int row, int column)
      {
         Random rnd = new Random();
         char[,] letterGrid = new char[row, column];
         Dictionary<char, int> frequencyLetter = new Dictionary<char, int>();

         for (int x = 0; x < row; x++)
         {
            for (int y = 0; y < column; y++)
            {
               char letter = '\0';
               if (rnd.Next(2) == 1)
                  letter = extractactLetter(frequencyLetter, rnd, row * column, letters);
               else
                  letter = extractactLetter(frequencyLetter, rnd, row * column, vowels);

               if (letter != '\0')
               {
                  if (!frequencyLetter.ContainsKey(letter))
                     frequencyLetter.Add(letter, 1);
                  else
                     frequencyLetter[letter] = frequencyLetter[letter] + 1;
               }
               else
               {
                  // Se non è riuscito ad estrapolare nessuna lettera mette una consonante
                  letter = vowels.Substring(rnd.Next(vowels.Length), 1).ToCharArray().FirstOrDefault();
               }

               letterGrid[x, y] = letter;
            }
         }

         return letterGrid;
      }

      /// <summary>
      /// Extracts a letter from the alphabet
      /// </summary>
      /// <param name="frequencyLetter"></param>
      /// <param name="rnd"></param>
      /// <param name="LetterTotal"></param>
      /// <param name="letters"></param>
      /// <returns></returns>
      private char extractactLetter(Dictionary<char, int> frequencyLetter, Random rnd, int LetterTotal, string letters)
      {
         char letterExtracted = '\0';
         int count = 0;

         while (letterExtracted == '\0' && count <= 21)
         {
            // Exits the loop when it has found the letter or has performed 21 loops
            // So that the algorithm can always go out
            count++;
            char letter = letters.Substring(rnd.Next(letters.Length), 1).ToCharArray().FirstOrDefault();
            // It Calculates the maximum extraction percentage for each letter extracted
            double extractionmax = (LetterTile.getAnalisysExtractLangueIT(letter) * LetterTotal / 100);

            // If the letter extraction is less than its maximum extraction then it adds it
            if (frequencyLetter.ContainsKey(letter) && frequencyLetter[letter] < extractionmax)
               letterExtracted = letter;
            else if (!frequencyLetter.ContainsKey(letter))
               letterExtracted = letter;
         }

         // returns a new letter
         return letterExtracted;
      }

      #endregion 





   }
}

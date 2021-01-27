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
      private ISprite scoreSprite = null;
      
      // Llist of words selected in the level
      private List<string> selectedWordList;
      private ISprite[,] spriteGridLetter;
    
      // Italian alphabet
      private const string letters = "abcdefghilmnopqrstuvz";
      private const string vowels = "aeiou";

      // Game manager
      private GameManager game;

      // List of sprites
      private List<ISprite> sprites;
      
      // Defines the white color
      readonly ColorRGB colorWhite = new ColorRGB(255, 255, 255, 255);

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
      public void BuildText(TypeFont typeFont, string textView, int x, int y, ColorRGB color = new ColorRGB())
      {
         GraphicFontInfo settings = new GraphicFontInfo(typeFont, new Coordinate2D(x, y), color);
         Text text = new Text(settings, textView);
         sprites.Add(text);
      }

      /// <summary>
      /// Build Title
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="color"></param>
      /// <param name="text"></param>
      public void BuildTitle(int x, int y, string text, ColorRGB color = new ColorRGB())
      {
         // Adds an image
         GraphicImageInfo settings = new GraphicImageInfo("images/title", new Coordinate2D(x, y), color);
         ImageProxy title = new ImageProxy(settings);
         sprites.Add(title);

         // Adds an text
         BuildText(TypeFont.Arial, text, x + 110, y + 10, new ColorRGB(255, 255, 255, 255));

      }

      /// <summary>
      /// Builds Backgrund
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="color"></param>
      public void BuildBackground(int x, int y, ColorRGB color = new ColorRGB())
      {
         GraphicImageInfo settings = new GraphicImageInfo("images/bg_alternative", new Coordinate2D(x, y), color);
         ImageProxy title = new ImageProxy(settings);
         sprites.Add(title);
      }


      /// <summary>
      /// Build the letter grid and description
      /// </summary>
      /// <param name="row"></param>
      /// <param name="column"></param>
      /// <param name="centralPoint"></param>
      /// <param name="y"></param>
      /// <param name="description"></param>
      /// <param name="color"></param>
      /// <param name="font"></param>
      public void BuildGridAndDescription(int row, int column, int centralPoint, int y, List<string> description, ColorRGB color, TypeFont font)
      {
         int distance = 20;
         int posY = y;

         // Gets a letterTile to calculate the width to show the grid centrally
         LetterTile ltr = new LetterTile('a',new GraphicImageInfo("Letters/letter-" + 'a', new Coordinate2D(0, 0), new ColorRGB()), game);
         int posX = centralPoint-(((ltr.GetExtent().Width+distance) * row)/2);

         // Adds text sprites
         foreach (var text in description)
         {
            // Level Description
            BuildText(font, text, posX, posY, new ColorRGB(255, 255, 255, 255));
            // Adds spaces between elements
            posY += 30;
         }

         // Add distance between description and grid
         posY += 30;

         // inizialite width and height
         int width = 0;
         int height = 0;

         // Creates a grid with the letters of the alphabet
         char[,] letterGrid = createLetterGrid(row, column);

         // Builds the grid 
         for (int r = 0; r < letterGrid.GetLength(0); r++)
         {
            // Assigns the position of the letter on the y-coordinate axis
            int posYIncremental = posY;

            // Read the array grid
            for (int c = 0; c < letterGrid.GetLength(1); c++)
            {
               // Sets the letter to load
               char letter = letterGrid[r, c];
               
               GraphicImageInfo settings = new GraphicImageInfo("Letters/letter-" + letter, new Coordinate2D(posX, posYIncremental), colorWhite);
               LetterTile letterTile = new LetterTile(letter, settings, game);

               // Reads the size
               Size size = letterTile.GetExtent();
               width = size.Width;
               height = size.Height;

               // Increments the position set in the y axis
               posYIncremental += height + distance;

               // Sets the letter that was just created
               sprites.Add(letterTile);
            }

            // Increments the position set in the x axis
            posX += width + distance;
         }
      }

      /// <summary>
      /// Builds the level score
      /// </summary>
      /// <param name="typeFont">type font</param>
      /// <param name="x">position x</param>
      /// <param name="y">position y</param>
      /// <param name="color">color in Rgb</param>
      public void BuildScore(TypeFont typeFont, int x, int y, ColorRGB color = new ColorRGB())
      {
        
         GraphicImageInfo settingsimg = new GraphicImageInfo("images/score", new Coordinate2D(x-50, y-10), new ColorRGB(255,255,255,255));
         LevelReloadButton img = new LevelReloadButton(settingsimg, game); 
         
         GraphicFontInfo settings = new GraphicFontInfo(typeFont, new Coordinate2D(x, y), color);
         Text text = new Text(settings, Score.ToString());
         
         scoreSprite = text;
         
         sprites.Add(img);
         sprites.Add(text);
      }

      /// <summary>
      /// Builds Level reload button
      /// </summary>
      /// <param name="typeFont">type font</param>
      /// <param name="x">position x</param>
      /// <param name="y">position y</param>
      /// <param name="color">color in Rgb</param>
      public void BuildReloadLevelBtn(int x, int y, ColorRGB color = new ColorRGB())
      {
         GraphicImageInfo settings = new GraphicImageInfo("images/reload", new Coordinate2D(x, y), color);
         LevelReloadButton btn = new LevelReloadButton(settings, game);
         sprites.Add(btn);
      }


      /// /// <summary>
      /// Builds game reload button
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="color"></param>
      public void BuildReloadGameBtn(int x, int y, ColorRGB color = new ColorRGB())
      {
         GraphicImageInfo settings = new GraphicImageInfo("images/home-icon", new Coordinate2D(x,y), color);
         GameReloadButton btn = new GameReloadButton(settings, game);
         sprites.Add(btn);
      }

      /// <summary>
      /// Shows a completed game image
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="color"></param>
      public void BuildWinner(int x, int y, ColorRGB color = new ColorRGB())
      {
         GraphicImageInfo settings = new GraphicImageInfo("images/overlay_win", new Coordinate2D(x, y), color);
         ImageProxy image = new ImageProxy(settings);
         sprites.Add(image);
      }


      /// <summary>
      /// Update the current score
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

         // Reset string
         string str = "";
         int value = 0;

         // Concatenate the letters selected
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
            // Adds new word
            selectedWordList.Add(str);

            // Calls the method that plays the sound effect from the GameManager
            game.NotifyCorrectWord();

            // Updates the score value and update scoreSprite
            score += value;
            if (scoreSprite != null)
               ((Text)scoreSprite).ViewText = Score.ToString();
         }
      }

      /// <summary>
      /// Returns all sprites instantiated in the level
      /// </summary>
      /// <returns></returns>
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
                  letter = extractLetter(frequencyLetter, rnd, row * column, letters);
               else
                  letter = extractLetter(frequencyLetter, rnd, row * column, vowels);

               if (letter != '\0')
               {
                  if (!frequencyLetter.ContainsKey(letter))
                     frequencyLetter.Add(letter, 1);
                  else
                     frequencyLetter[letter] = frequencyLetter[letter] + 1;
               }
               else
               {
                  // If he failed to extract any letters he puts a consonant
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
      private char extractLetter(Dictionary<char, int> frequencyLetter, Random rnd, int LetterTotal, string letters)
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
            double extractionmax = (LetterTile.GetFrequencyItalianLanguage(letter) * LetterTotal / 100);

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

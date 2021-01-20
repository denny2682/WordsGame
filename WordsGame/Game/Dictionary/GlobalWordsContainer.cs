using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace WordsGame.Game
{
   /// <summary>
   /// Pattern: This is a singleton 
   /// Istances the dictionary 
   /// </summary>
   public class GlobalWordsContainer
   {
      private static GlobalWordsContainer istance = null;
      private List<string> wordsDictionary = null;

      // Costruttore
      protected GlobalWordsContainer() { }
      public static GlobalWordsContainer Istance
      {
         // instance can only be returned, but not assigned from outside
         get
         {
            if (istance == null)
               istance = new GlobalWordsContainer();

            return istance;
         }
      }

      /// <summary>
      /// Returns the dictionary of words
      /// </summary>
      public List<string> WordsDictionary
      {
         get { return wordsDictionary; }

      }

      /// <summary>
      /// Load the dictionary
      /// </summary>
      /// <param name="openStream"></param>
      public List<string> Load(string openStream)
      {
         if (wordsDictionary == null)
            wordsDictionary = getWordsDictionary(openStream);

         return wordsDictionary;
      }


      /// <summary>
      /// Returns the dictionary words
      /// </summary>
      /// <param name="fileStream">fileStream</param>
      /// <returns>List<string></returns>
      private List<string> getWordsDictionary(string fileStream)
      {
         try
         {
            List<string> lines = new List<string>();

            if (fileStream != null && File.Exists(fileStream))
            {
               using (StreamReader reader = new StreamReader(fileStream))
               {
                  string line = reader.ReadLine();
                  while (line != null)
                  {
                     lines.Add(line);
                     line = reader.ReadLine();
                  }
               }
            }
            else
            {
               throw new Exception("Dictionary file was not found");
               // todo: inserire throw exeption}
            }
            return lines;
         }
         catch (Exception ex)
         {
            throw new Exception("File Dictionary is not readable");
            return null;
         }
      }

      /// <summary>
      /// Returns a random word
      /// </summary>
      /// <param name="minLenght"></param>
      /// <param name="maxLenght"></param>
      /// <param name="maxWords"></param>
      /// <returns></returns>
      public List<string> GetExtractWords(int minLenght, int maxLenght, int maxWords)
      {
         try
         {
            Random rnd = new Random();
            List<string> lines = new List<string>();
            if (wordsDictionary != null && maxWords < wordsDictionary.Count())
            {
               var words = wordsDictionary.ToArray();
               int lenght = wordsDictionary.Count();
               int count = 0;

               while (count <= maxWords)
               {
                  string word = words[rnd.Next(lenght)];
                  if (!lines.Contains(word) && word.Length >= minLenght && word.Length <= maxLenght)
                  {
                     lines.Add(word);
                     count++;
                  }
               }
            }
            return lines;
         }
         catch (Exception ex)
         {
           Debug.WriteLine("unexpected error: " + ex.Message);
            return null;
         }
      }
   }



}

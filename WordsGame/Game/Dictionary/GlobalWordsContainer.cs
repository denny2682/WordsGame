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
      #region private variables

      private static GlobalWordsContainer istance = null;
      private List<string> wordsDictionary = null;
      
      #endregion

      /// <summary>
      /// Costructor
      /// </summary>
      protected GlobalWordsContainer() { }

      #region public methods

      /// <summary>
      /// Returns the GlobalWordsContainer Istance
      /// </summary>
      public static GlobalWordsContainer Istance
      {
         get
         {
            if (istance == null)
               istance = new GlobalWordsContainer();

            // instance can only be returned, but not assigned from outside
            return istance;
         }
      }

      /// <summary>
      /// Returns the words dictionary
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

      #endregion


      #region private methods

      /// <summary>
      /// Loads all words in the dictionary file into a list
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
               // exception File not found
               throw new FileNotFoundException("Dictionary file was not found");
            }
            return lines;
         }
         catch (Exception ex)
         {
            throw new Exception("An exception occurred while reading the file " + ex.Message);
         }
      }

      #endregion
   }



}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

// Operazione su i/o
// Singleton 
// lo apre e lo mantiene in memoria con un singleton
namespace WordsGame.Game
{

   // This is a singleton class
   public class GlobalWordsContainer
   {
      private static GlobalWordsContainer istance = null;
      private List<string> wordsDictionary = null;

      // Costruttore
      protected GlobalWordsContainer() { }
      public static GlobalWordsContainer Istance
      {
         // istance può solo essere restituita, ma non assegnata dall'esterno
         get
         {
            if (istance == null)
               istance = new GlobalWordsContainer();

            return istance;
         }
      }

      /// <summary>
      /// Resituisce il dizionario delle parole
      /// </summary>
      public List<string> WordsDictionary
      {
         get { return wordsDictionary; }

      }

      /// <summary>
      /// Metodo pubblico per ottenere le parole
      /// // todo: valutare se implementare unload
      /// </summary>
      /// <param name="openStream"></param>
      public List<string> Load(string openStream)
      {
         if (wordsDictionary == null)
            wordsDictionary = getWordsDictionary(openStream);

         return wordsDictionary;
      }


      /// <summary>
      /// restituisce le parole del dizionario
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
               throw new Exception("File Dictionary not is founded");
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

      public List<string> GetExtractWords(int minLenght, int maxLenght, int maxWords)
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
   }



}

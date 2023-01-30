using System;
using System.IO;
using System.Linq;

namespace PoemWriter
{
    class Poem
    {
        private string _name;
        private string _poem;
        private int _maxLineLength;
        private string _wrappedPoem;
        private string[] _words;

        public Poem(string name, string poem, int maxLineLength)
        {
            _name = name;
            _poem = poem;
            _maxLineLength = maxLineLength;
        }

        public void WrapPoem()
        {
            _words = _poem.Split(' ');

            string line = "";
            _wrappedPoem = "";
            foreach (string word in _words)
            {
                if ((line + word).Length > _maxLineLength)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        _wrappedPoem += line + "\n";
                    }
                    line = "";
                }
                if (line.Length > 0)
                {
                    line += " " + word;
                }
                else
                {
                    line = word;
                }
            }
            if (!string.IsNullOrEmpty(line))
            {
                _wrappedPoem += line;
            }
        }

        public string GetFinalPoem()
        {
            return $"This poem was written by the great and talented {_name}!\n\n{_wrappedPoem}";
        }

        public void WriteToFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                Console.WriteLine("File already exists. Please enter a different name: ");
                string filePrompt = Console.ReadLine();
                fileName = $"{filePrompt}.txt";
            }

            StreamWriter writer = new StreamWriter(fileName);
            writer.WriteLine(GetFinalPoem());
            writer.Close();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Annnnnnd...");
            Console.WriteLine("What would you like your poem to say?!");
            string poem = Console.ReadLine();

            int maxLineLength = 20;
            Poem wrappedPoem = new Poem(name, poem, maxLineLength);
            wrappedPoem.WrapPoem();
            Console.WriteLine(wrappedPoem.GetFinalPoem());

            Console.WriteLine("Please enter your desired file name: ");
            string fileName = Console.ReadLine() + ".txt";
            wrappedPoem.WriteToFile(fileName);
        }
    }
}

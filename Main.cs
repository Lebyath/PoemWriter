using System;
using System.IO;
using System.Linq;

namespace PoemWriter
{
    class Poem
    {
        // Private fields to store information about the poem
        private string _name;
        private string _poem;
        private int _maxLineLength;
        private string _wrappedPoem;
        private string[] _words;

        // Constructor for the Poem
        public Poem(string name, string poem, int maxLineLength)
        {
            _name = name;
            _poem = poem;
            _maxLineLength = maxLineLength > 0 ? maxLineLength : 20;
        }

        // Method to wrap the poem text 
        public void WrapPoem()
        {
            _words = _poem.Split(' ');
            string line = "";
            _wrappedPoem = "";
            foreach (string word in _words)
            {
                if ((line + word).Length > _maxLineLength)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        _wrappedPoem += line + "\n";
                    }
                    line = "";
                }
                line += line.Length > 0 ? " " + word : word;
            }
            _wrappedPoem += !string.IsNullOrWhiteSpace(line) ? line : "";
        }

        // Method to get the final poem text, including the author's name
        public string GetFinalPoem()
        {
            return $"This poem was written by the great and talented {_name}!\n\n{_wrappedPoem}";
        }

        //Method to write the final poem text to a file
        public void WriteToFile(string fileName)
        {
            while (File.Exists(fileName))
            {   // if file exists, prompt the user for a different file name
                Console.WriteLine("File already exists. Please enter a different name: ");
                string filePrompt = Console.ReadLine();
                fileName = $"{filePrompt}.txt";
            }

            //Use 'StreamWritor' to write the final poem a the file
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(GetFinalPoem());
            }
        }
    }

    class Program
    {
        static void Main(string[] args) // Bring it all together
        {
            //Get name
            Console.WriteLine("Please enter your name: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("ERROR: Name cannot be empty nor only contain whitespace.");
                return;
            }


            //Get poem
            Console.WriteLine("Annnnnnd...");
            Console.WriteLine("What would you like your poem to say?!");
            string poem = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(poem))
            {
                Console.WriteLine("ERROR: Poem cannot be empty nor only contain whitespace.");
                return;
            }


            //Get max line length and wrap poem
            Console.WriteLine("At how many characters would you like it to wrap? (20 is Recommended): ");
            int maxLineLength = 0;
            bool isValidMaxLineLength = false;
            while (!isValidMaxLineLength)
            {
                if (Int32.TryParse(Console.ReadLine(), out maxLineLength))
                {
                    if (maxLineLength <= 0)
                    {
                        Console.WriteLine("Max line length must be a positive integer. Please enter a valid value: ");
                    }
                    else
                    {
                        isValidMaxLineLength = true;
                    }
                }
                else
                {
                    Console.WriteLine("Max line length must be a positive integer. Please enter a valid value: ");
                }
            }
            Poem wrappedPoem = new Poem(name, poem, maxLineLength);
            wrappedPoem.WrapPoem();
            Console.WriteLine(wrappedPoem.GetFinalPoem());

            //Write to .txt file
            Console.WriteLine("Please enter your desired file name: ");
            string fileName = Console.ReadLine() + ".txt";
            wrappedPoem.WriteToFile(fileName);
        }
    }
}

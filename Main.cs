using System;
using System.IO;
using System.Linq;

namespace PoemWriter
{
    class Poem //creating the poem's class method.
    {
        private string _name; // defining the instance variables
        private string _poem;
        private int _maxLineLength;
        private string _wrappedPoem;
        private string[] _words; //define array of strings

        public Poem(string name, string poem, int maxLineLength) //create constructor and set parameters
        {
            _name = name;
            _poem = poem;
            _maxLineLength = maxLineLength;
        }

        public void WrapPoem() //object where we use algorithm to wrap poem
        {
            _words = _poem.Split(' '); //split the user's input into an array of strings

            string line = "";
            _wrappedPoem = "";
            foreach (string word in _words) //iterate over each word in the array
            {
                if ((line + word).Length > _maxLineLength) // if the length of line + word is greater than the max length
                {
                    if (!string.IsNullOrEmpty(line))//if line is empty 
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
            if (!string.IsNullOrEmpty(line)) //after loop is finished check if line is not empty
            {
                _wrappedPoem += line; // if not 
            }
        }

        public string GetFinalPoem() //concatinates the wrapped poem with name presentation
        {
            return $"This poem was written by the great and talented {_name}!\n\n{_wrappedPoem}";
        }

        public void WriteToFile(string fileName) //checks if f_name already exists then writes to file  
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
        static void Main(string[] args) //bring everything together and prompt the user
        {
            //Get name
            Console.WriteLine("Please enter your name: ");
            string name = Console.ReadLine();

            //Get poem
            Console.WriteLine("Annnnnnd...");
            Console.WriteLine("What would you like your poem to say?!");
            string poem = Console.ReadLine();

            //ask for amount of characters to wrap each line and wrap the poem
            Console.WriteLine("At how many characters would you like it to wrap? (20 is Recommended): ");
            int maxLineLength = Convert.ToInt16(Console.ReadLine());
            if (maxLineLength <= 0){ //this part is tricky, see if you can make this better
                maxLineLength = 20;
            }
            Poem wrappedPoem = new Poem(name, poem, maxLineLength);
            wrappedPoem.WrapPoem();//call WrapPoem
            Console.WriteLine(wrappedPoem.GetFinalPoem());//call GetFinalPoem and print it out

            //Get file name
            Console.WriteLine("Please enter your desired file name: ");
            string fileName = Console.ReadLine() + ".txt";
            wrappedPoem.WriteToFile(fileName); //call WriteToFile
        }
    }
}

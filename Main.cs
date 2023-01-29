using System;
using System.IO; 
using System.Linq;


namespace PoemWriter;
class Program
{
    static void Main(string[] args)
    {

        string name, poem, line, wrappedPoem = ""; 
        string[] words;
        int maxLineLength;

        //Get name. 
        Console.WriteLine("Please enter your name: ");
        name = Console.ReadLine();


        //get poem
        Console.WriteLine("Annnnnnd...");
        Console.WriteLine("What would you like your poem to say?!");
        poem = Console.ReadLine();

        //take a string and return a list of strings no longer than 20
        maxLineLength = 20;

        words = poem.Split(' ');
        
        line = "";
        if (wrappedPoem == null) wrappedPoem ="";
        wrappedPoem += line;
        foreach (string word in words){
           if ((line + word).Length > maxLineLength)
            {   
                if (!string.IsNullOrEmpty(line)){
                    wrappedPoem += line + "\n";
                }
                line ="";
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
        if (!string.IsNullOrEmpty(line)){
            wrappedPoem += line;
            }

        //create the final product 
        string finalPoem = $"This poem was written by the great and talented {name}!\n\n{wrappedPoem}";

        Console.WriteLine(finalPoem);


        //Write poem to .txt file
        Console.WriteLine("Please enter your desired file name: ");
        string filePrompt = Console.ReadLine(); 
        string fileName = $"{filePrompt}.txt";

        //Check if file already exists
        if (File.Exists(fileName))
        {
            Console.WriteLine("File already exists. Please enter a different name: ");
            filePrompt = Console.ReadLine();
            fileName = $"{filePrompt}.txt";
        }

        //create file and write poem to it
        StreamWriter writer;
        writer = new StreamWriter(fileName);
        writer.WriteLine(finalPoem);
        writer.Close();

    }
}

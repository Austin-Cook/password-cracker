using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // open dictionary

        string path = "Resources/2of12.txt";

        if (!File.Exists(path))
        {
            Console.WriteLine("Doesn't exist");
            Environment.Exit(0);
        }

        // hash all words in dictionary

        string[] dictionary = File.ReadAllLines(path);
        List<byte[]> dictionaryHashed = [];
        foreach (string word in dictionary)
        {
            dictionaryHashed.Add(SHA256.HashData(Encoding.UTF8.GetBytes(word)));
        }

        // get user input

        Console.Write("Please enter a password to check: ");
        string? input = Console.ReadLine();

        if (input == null)
        {
            Console.WriteLine("Error: Terminal input was null");
            Environment.Exit(0);
        }

        Console.WriteLine("input: " + input);

        // hash user input

        byte[] userInputBytes = Encoding.UTF8.GetBytes(input.ToString());
        byte[] userInputHashed = SHA256.HashData(userInputBytes);

        // check if hashed input in hashed dictionary

        foreach (byte[] wordHashed in dictionaryHashed)
        {
            {
                if (userInputHashed.SequenceEqual(wordHashed))
                {
                    Console.WriteLine("Your password is in the dictionary");
                    Environment.Exit(0);
                }
            }
        }
        Console.WriteLine("No match found in dictionary");
    }
}

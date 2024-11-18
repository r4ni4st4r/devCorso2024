using System.Security.Cryptography;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Docker;

class Program
{
    static void Main(string[] args)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "attempts.json");
        Random rnd = new Random();
        //Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int num = rnd.Next(1, 101);
        int attempts = 0;
        int attempt;
        Console.WriteLine("try to catch a number from 1 to 100");

        if(File.Exists(filePath))
        {   
            try
            {
                string attemptString = File.ReadAllText(filePath);
                if(string.IsNullOrWhiteSpace(attemptString)){
                    attempts = JsonConvert.DeserializeObject<int>(attemptString);
                }           
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Reading file error: {ex.Message}");
            }
        }

        do
        {
            string? stringTemp = Console.ReadLine();
            attempt = int.TryParse(stringTemp, out int result) ? result : 0;
            attempts++;

            if (attempt < num)
                Console.WriteLine("too small");
            else if (attempt > num)
                Console.WriteLine("too big");

            try
            {
                File.WriteAllText(filePath, JsonConvert.SerializeObject(attempts));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Writing file error: {ex.Message}");
            }

        } while (attempt != num);

        Console.WriteLine($"you win in {attempts}!");
    }
}

string path=@"source.txt"; //legge nella stessa cartella del programma
Random random= new Random();

string[] lines = File.ReadAllLines(path); //legge le righe del file

Console.WriteLine(lines[random.Next(lines.Length)]);

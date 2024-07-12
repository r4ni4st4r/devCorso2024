string path=@"source.txt"; //legge nella stessa cartella del programma
Random random= new Random();
string secondPath=@"secondSource.txt";

string[] lines = File.ReadAllLines(path); //legge le righe del file

string temp = lines[random.Next(lines.Length)];

Console.WriteLine(temp);

if (!File.Exists(secondPath))
    File.Create(secondPath);

File.AppendAllText(secondPath, temp + "\n");

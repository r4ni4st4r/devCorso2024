﻿string path=@"source.txt"; //legge nella stessa cartella del programma
Random random= new Random();
string secondPath=@"secondSource.txt";

string[] lines = File.ReadAllLines(path); //legge le righe del file

string temp = lines[random.Next(lines.Length)];

Console.WriteLine(temp);

if (!File.Exists(secondPath)){
    File.Create(secondPath).Close();
    File.AppendAllText(secondPath, temp + "\n");
}else{
    string[] secondLines = File.ReadAllLines(secondPath);

    if(!secondLines.Contains<string>(temp))
        File.AppendAllText(secondPath, temp + "\n");
    else
        Console.WriteLine("Il file contiene già questo nome!");
}
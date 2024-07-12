string path=@"source.txt"; //legge nella stessa cartella del programma
string[] lines = File.ReadAllLines(path); //legge le righe del file

lines[lines.Length - 2] += " ciao";

File.WriteAllLines(path, lines);


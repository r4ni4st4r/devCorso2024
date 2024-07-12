string path=@"source.txt"; //legge nella stessa cartella del programma
string[] lines = File.ReadAllLines(path); //legge le righe del file

foreach (string line in lines){
    Console.WriteLine(line);
}

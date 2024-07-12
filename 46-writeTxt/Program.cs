string path=@"source.txt"; //legge nella stessa cartella del programma

string secondPath=@"secondSource.txt";

string[] lines = File.ReadAllLines(path); //legge le righe del file
string[] names = new string[lines.Length];   

for(int i=0;  i<lines.Length; i++){
    names[i] = lines[i];
}

File.Create(secondPath).Close();

bool noNamesStartsWithA = true;

foreach(string name in names){

    if(name.StartsWith("a")){
        File.AppendAllText(secondPath, name + "\n");
        Console.WriteLine(name);
        noNamesStartsWithA = false;
    }
}

if(noNamesStartsWithA){
    Console.WriteLine("No names starts with a");
}
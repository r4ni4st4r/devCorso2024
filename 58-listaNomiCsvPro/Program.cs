using System.ComponentModel;
using Spectre.Console;

class Program{
    static void Main(string[] args){
        const string PATH = @"test.csv";

        if(!File.Exists(PATH))
            File.Create(PATH).Close();

        List<string> fileRecords = new  List<string>();

        using (StreamReader sr = new StreamReader(PATH)){
            string line;
            while((line = sr.ReadLine()) != null){
                if(line!="")
                    fileRecords.Add(line);
            }
                
        }

        while(true){
            bool verify = true;

            Console.Clear();
            string selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\nManipolare file .csv")
                .PageSize(14)
                .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                .AddChoices(new string[] {
                    "Inserisci record", "Rimuovi record", "Salva sul file", "Esci",
                }));


            switch (selection)
            {
                case "Inserisci partecipante":

                    Console.WriteLine("Inserisci nome cognome ed età\n\n");
                    Console.Write("Inserisci nome: ");
                    string nome = Console.ReadLine();
                    Console.Write("Inserisci cognome: ");
                    string cognome = Console.ReadLine();
                    Console.Write("Inserisci età: ");
                    string eta = Console.ReadLine();

                    foreach(string s in fileRecords){
                        if(s.Split(",").First() == nome){
                            Console.WriteLine("Il file contiene già un record con questo nome...\n\nPremere un tasto..");
                            Console.ReadLine();
                            verify = false;
                            break;
                        }
                    }

                    if(verify){
                        fileRecords.Add(nome + "," + cognome + "," + eta);
                        Console.WriteLine("Il record è stato inserito");
                    }

                    break;
                    
                case "Visualizza partecipanti": 

                    break;

                case "Salva sul file": 
                    using (StreamWriter sw = new StreamWriter(PATH)){
                        foreach(string s in fileRecords){
                            sw.WriteLine(s);
                        }
                    }

                    Console.WriteLine("File salvato!\n\nPremere un tasto");
                    Console.ReadKey();
                    break;

                case "Esci":

                    break;
            }
            

            

            Console.WriteLine("Vuoi inserire un altro nome? (s/n)");
            string risposta =  Console.ReadLine();
            
            if(risposta == "n"){
                break;
            }
        }

    }
}

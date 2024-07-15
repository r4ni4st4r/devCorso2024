// versione con console clear in modo da mantenere il menu nella stessa posizione dopo ogni scelta 
using Spectre.Console;

List<string> partecipanti = new List<string>();

string path = @"elenco.txt";
Random random = new Random();
string nome;
string selection;
int split;
List<string> squadra1 = new List<string>();
List<string> squadra2 = new List<string>();
bool teamsExist = false;

if(File.Exists(path)){
    using (StreamReader sr = new StreamReader(path)){
        string line;
        while ((line = sr.ReadLine()) != null){
            partecipanti.Add(line);
        }
    }
}

do
{
    Console.Clear();
    selection = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("\nGestionale [green]Squadre[/]")
        .PageSize(14)
        .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
        .AddChoices(new string[] {
            "Inserisci partecipante", "Visualizza partecipanti", "Ordina partecipanti", 
            "Cerca partecipante", "Elimina partecipante", "Modifica partecipante",
            "Divisione in squadre random", "Divisione in squadre manuale", 
            "Gestione dati","Esci",
        }));

    switch (selection)
    {
        case "Inserisci partecipante":
            Console.Write("Nome partecipante: ");
            nome = Console.ReadLine();
            if(nome != ""){
                if(partecipanti.Contains(nome)){
                    Console.WriteLine("La lista contiene già questo nome\n\nPremere un tasto...");
                    Console.ReadLine();
                    break; 
                }else
                    partecipanti.Add(nome);
            }else{
                Console.WriteLine("Si prega di inserire un nome valido\n\nPremere un tasto...");
                Console.ReadLine(); 
            }

            break;
        case "Visualizza partecipanti": 
            Console.WriteLine("Elenco partecipanti:");
            foreach (string partecipante in partecipanti){
                Console.WriteLine(partecipante);
            }
            if(partecipanti.Count == 0)
                Console.WriteLine("Lista vuota");
            Console.WriteLine("Premere un tasto...");
            Console.ReadLine(); 
            break;
        case "Ordina partecipanti":
            string sel = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("\nI che modo vuoi ordinarli?")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                .AddChoices(new string[] {
                "Ordine crescente","Ordine decrescente","Indietro",
            }));
            switch (sel){
                case "Ordine crescente":
                    partecipanti.Sort();
                    break;
                case "Ordine decrescente":
                    partecipanti.Sort();
                    partecipanti.Reverse();
                    break;
                case "Indietro":
                    break;          
            }
            break;
        case "Cerca partecipante":
            Console.Write("Nome partecipante: ");
            nome = Console.ReadLine();
            if (partecipanti.Contains(nome)){
                Console.WriteLine("Il partecipante è presente nella lista\nPremere un tasto...");
                Console.ReadLine();
            }
            else if (nome == ""){
                Console.WriteLine("Il nome cercato non è valido\nPremere un tasto...");
                Console.ReadLine();
            }else{
                Console.WriteLine("Il partecipante non è presente nella lista\nPremere un tasto...");
                Console.ReadLine();
            }  
            break;
        case "Elimina partecipante":
            Console.Write("Nome partecipante: ");

            nome = Console.ReadLine();
            
            if (partecipanti.Contains(nome)){
                partecipanti.Remove(nome);
                Console.WriteLine("Il partecipante è stato rimosso dalla lista\nPremere un tasto...");
                Console.ReadLine();
            }else if (nome == ""){
                Console.WriteLine("Il nome cercato non è valido\nPremere un tasto...");
                Console.ReadLine();
            }else{
                Console.WriteLine("Il partecipante non è presente nella lista\nPremere un tasto...");
                Console.ReadLine();
            }                
            break;
        case "Modifica partecipante":
            Console.Write("Nome partecipante: ");
            nome = Console.ReadLine();
            if(nome==""){
                Console.WriteLine("Il nome cercato non è valido\nPremere un tasto...");
                Console.ReadLine();
            }else if (partecipanti.Contains(nome)){
                Console.Write("Nuovo nome: ");
                string nuovoNome = Console.ReadLine();
                if (nuovoNome!=""){
                    partecipanti[partecipanti.IndexOf(nome)] = nuovoNome;
                    Console.WriteLine("Il partecipante è stato modificato\nPremere un tasto...");
                    Console.ReadLine();
                }else{
                    Console.WriteLine("Il nome cercato non è valido\nPremere un tasto...");
                    Console.ReadLine();
                }
            }else{
                Console.WriteLine("Il partecipante non è presente nella lista\nPremere un tasto...");
                Console.ReadLine();
            }
            break;
        case "Divisione in squadre random":
            Table squadreTable = new Table();
            squadreTable.AddColumn("[blue]SQUADRA 1[/]");
            squadreTable.AddColumn("[red]SQUADRA 2[/]");

            if (partecipanti.Count<2){
                Console.WriteLine("Non ci sono abbastanza partecipanti per creare due squadre\nPremere un tasto");
                Console.ReadLine();
            }else{
                foreach (string s in partecipanti){

                    if(random.Next(0, 2) == 0){
                        if(partecipanti.Count%2 == 0){
                            if(squadra1.Count < partecipanti.Count/2)
                                squadra1.Add(s);
                            else
                                squadra2.Add(s);
                        }else{
                            if(squadra1.Count <= partecipanti.Count/2)
                                squadra1.Add(s);
                            else
                                squadra2.Add(s);
                        }
                    }else{
                        if(partecipanti.Count%2 == 0){
                            if(squadra2.Count < partecipanti.Count/2)
                                squadra2.Add(s);
                            else
                                squadra1.Add(s); 
                        }else{
                            if(squadra2.Count <= partecipanti.Count/2)
                                squadra2.Add(s);
                            else
                                squadra1.Add(s);
                        }
                    }

                }
                int numberRow = 0;

                if(squadra1.Count>squadra2.Count)
                    numberRow = squadra1.Count;
                else    
                    numberRow = squadra2.Count;

                for(int i = 0;i<numberRow;i++){
                    squadreTable.AddRow(squadra1[i],squadra2[i]);
                }

                AnsiConsole.Write(squadreTable);

                AnsiConsole.WriteLine("Premi un tasto per continuare...");

                Console.ReadKey();
            }

            break;

        case "Divisione in squadre manuale":
            if(partecipanti.Count<2){
                Console.WriteLine("Non ci sono abbastanza partecipanti per creare due squadre\nPremere un tasto");
                Console.ReadLine();
            }else{
                do{

                }while(partecipanti.Count>0);
            }

            Console.WriteLine("Squadra 1:");

            foreach(string s in squadra1)
                Console.WriteLine(s);

            Console.WriteLine("Squadra 2:");

            foreach(string s in squadra2)
                Console.WriteLine(s);

            break;
/////////////////////////////////////////////////////////////////
            //crea le due squadre
            split = partecipanti.Count/2;
            List<string> squadra1Random = partecipanti.GetRange(0, split);
            List<string> squadra2Random = partecipanti.GetRange(split, partecipanti.Count - split);
            
            while(partecipanti.Count > 0){
                int index = random.Next(partecipanti.Count);
                string partecipante = partecipanti[index];

                partecipanti.RemoveAt(index);

                if(squadra1Random.Count<squadra2Random.Count){
                    squadra1Random.Add(partecipante);
                }else{
                    squadra2Random.Add(partecipante);
                }
            }

            Console.WriteLine("Elenco squadra 1:");

            foreach (string s1 in squadra1Random)
            {
                Console.WriteLine(s1);
            }

            Console.WriteLine("Elenco squadra 2:");

            foreach (string s2 in squadra2Random)
            {
                Console.WriteLine(s2);
            }
            
            partecipanti.Clear();
            break;
////////////////////////////////////////////////////////////////////////
        case "Gestione dati":
            sel = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("\nAccedi al filesystem")
                .PageSize(4)
                .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                .AddChoices(new string[] {
                "Salva", "Formatta File Salvato","Indietro"
            }));
            switch (sel){
                case "Salva":
                    using (StreamWriter sw = File.CreateText(path)){
                        if(partecipanti.Count != 0){
                            for(int i = 0;i<partecipanti.Count;i++)
                                sw.WriteLine(partecipanti[i]);
                            Console.WriteLine("Lista salvata\nPremere un tasto...");
                            Console.ReadLine();    
                        }else{
                            Console.WriteLine("La lista dei partecipanti è vuota...\nPremere un tasto...");
                            Console.ReadLine();
                        }
                    }
                    break;
                case "Formatta File Salvato":
                    using (StreamWriter sw = File.CreateText(path)){
                        Console.WriteLine("Il file di salvataggio è stato formattato...\nPremere un tasto...");
                        Console.ReadLine();
                    }
                    break;
                case "Indietro":
                    break;          
            }
            break;
        case "Esci":
            sel = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("\nVuoi salvare la lista e le squadre?")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                .AddChoices(new string[] {
                "Si","No",
            }));
            switch (sel){
                case "Si":
                    using (StreamWriter sw = File.CreateText(path)){
                        if(partecipanti.Count != 0){
                            for(int i = 0;i<partecipanti.Count;i++)
                                sw.WriteLine(partecipanti[i]); 
                    }else{
                        Console.WriteLine("La lista dei partecipanti è vuota...\nPremere un tasto...");
                        Console.ReadLine();
                    }
            }
                    break;
                case "No":
                    break;          
            }
            Console.WriteLine("Arrivederci!");
            break;
    }
} while (selection != "Esci");
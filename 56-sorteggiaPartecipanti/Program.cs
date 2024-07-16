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
}else{
    AnsiConsole.WriteLine("[red]Nessun file di salvataggio trovato[/]\n\nPremere un tasto...");
    Console.ReadKey();
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
                Console.WriteLine("Il partecipante è già presente nella lista\n\nPremere un tasto...");
                Console.ReadLine();
            }
            else if (nome == ""){
                Console.WriteLine("Il nome cercato non è valido\n\nPremere un tasto...");
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
                Console.WriteLine("Il partecipante è stato rimosso dalla lista\n\nPremere un tasto...");
                Console.ReadLine();
            }else if (nome == ""){
                Console.WriteLine("Il nome cercato non è valido\n\nPremere un tasto...");
                Console.ReadLine();
            }else{
                Console.WriteLine("Il partecipante non è presente nella lista\n\nPremere un tasto...");
                Console.ReadLine();
            }                
            break;
        case "Modifica partecipante":
            Console.Write("Nome partecipante: ");
            nome = Console.ReadLine();
            if(nome==""){
                Console.WriteLine("Il nome cercato non è valido\n\nPremere un tasto...");
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
                    if(i>=squadra1.Count){
                        squadreTable.AddRow("",squadra2[i]);
                    }else if(i>=squadra1.Count){
                        squadreTable.AddRow(squadra1[i],"");
                    }else
                        squadreTable.AddRow(squadra1[i],squadra2[i]);
                }

                AnsiConsole.Write(squadreTable);

                AnsiConsole.WriteLine("Premi un tasto per continuare...");

                Console.ReadKey();
            }

            break;

        case "Divisione in squadre manuale":
            if(partecipanti.Count<2){
                Console.WriteLine("Non ci sono abbastanza partecipanti per creare due squadre\n\nPremere un tasto");
                Console.ReadLine();
            }else{
                squadra1.Clear();
                squadra2.Clear();

                foreach(string s in partecipanti){
                    Console.Clear();
                    Console.WriteLine("In che squadra vuoi mettere "+s+"?\n");

                    string sele = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("\nSelezione squadre")
                        .PageSize(3)
                        .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                        .AddChoices(new string[] {
                        "Squadra 1","Squadra 2",
                    }));
                    

                    switch (sele){
                        case "Squadra 1":
                            squadra1.Add(s);
                        break;
                        case "Squadra 2":
                            squadra2.Add(s);
                        break;         
                    }
                }
                Table squadreManualTable = new Table();
                squadreManualTable.AddColumn("[blue]SQUADRA 1[/]");
                squadreManualTable.AddColumn("[red]SQUADRA 2[/]");
                int numberRow = 0;

                if(squadra1.Count>squadra2.Count)
                    numberRow = squadra1.Count;
                else    
                    numberRow = squadra2.Count;

                for(int i = 0;i<numberRow;i++){
                    if(i>=squadra1.Count){
                        squadreManualTable.AddRow("",squadra2[i]);
                    }else if(i>=squadra2.Count){
                        squadreManualTable.AddRow(squadra1[i],"");
                    }else
                        squadreManualTable.AddRow(squadra1[i],squadra2[i]);
                }

                AnsiConsole.Write(squadreManualTable);

                AnsiConsole.WriteLine("Premi un tasto per continuare...");

                Console.ReadKey();
            }
            break;
/////////////////////////////////////////////////////////////////

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
                        if(squadra1.Count != 0){
                            sw.WriteLine("######");
                            for(int i = 0;i<squadra1.Count;i++)
                                sw.WriteLine(squadra1[i]);
                            Console.WriteLine("Squadra 1 salvata\n\nPremere un tasto...");
                            Console.ReadLine();    
                        }else{
                            Console.WriteLine("La squadra 1 è vuota...\nPremere un tasto...");
                            Console.ReadLine();
                        }
                        if(squadra2.Count != 0){
                            sw.WriteLine("######");
                            for(int i = 0;i<squadra2.Count;i++)
                                sw.WriteLine(squadra2[i]);
                            Console.WriteLine("Squadra 2 salvata\n\nPremere un tasto...");
                            Console.ReadLine();    
                        }else{
                            Console.WriteLine("La squadra 2 è vuota...\nPremere un tasto...");
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

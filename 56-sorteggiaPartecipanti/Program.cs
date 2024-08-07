﻿// versione con console clear in modo da mantenere il menu nella stessa posizione dopo ogni scelta 
using Spectre.Console;

public class Program{

    const string PATH = @"elenco.txt";
    static Random random = new Random();
    static string nome;
    static string selection;
    static int split;
    static List<string> squadra1 = new List<string>();
    static List<string> squadra2 = new List<string>();
    bool teamsExist = false;
    static List<string> partecipanti = new List<string>();
    static Table squadreTable = new Table();
    static Table partecipantiTable = new Table();
                    


    static void Main(string[] args){

        squadreTable.AddColumn("[blue]SQUADRA 1[/]");
        squadreTable.AddColumn("[red]SQUADRA 2[/]");
        partecipantiTable.AddColumn("[Fuchsia]ELENCO[/]");
        
        VerificaFileCaricaListaSquadre();

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

                    InserisciPartecipante();

                    break;
                case "Visualizza partecipanti": 
                    
                    VisualizzaPartecipanti();

                    break;
                case "Ordina partecipanti":
                    
                    OrdinaPartecipanti();

                    break;
                case "Cerca partecipante":

                    CercaPartecipante();

                    break;
                case "Elimina partecipante":

                    EliminaPartecipante();
                                
                    break;
                case "Modifica partecipante":
                    
                    ModificaPartecipanti();
                    
                    break;

                case "Divisione in squadre random":
                    
                    DivisioneSquadreRandom();

                    break;

                case "Divisione in squadre manuale":
                    
                    DivisioneSquadreManuale();

                    break;

                case "Gestione dati":

                    GestioneDati();
                    
                    break;
                case "Esci":
                    UscireSalvare();
                    break;
            }
        } while (selection != "Esci");
    }


    private static void VerificaFileCaricaListaSquadre(){
        if(File.Exists(PATH)){
            using (StreamReader sr = new StreamReader(PATH)){
                string line;
                while ((line = sr.ReadLine()) != null){
                    if(line=="######")
                        break;
                    partecipanti.Add(line);
                }
                while ((line = sr.ReadLine()) != null){
                    if(line=="######")
                        break;
                    squadra1.Add(line);
                }
                while ((line = sr.ReadLine()) != null){
                    if(line=="######")
                        break;
                    squadra2.Add(line);
                }
            }
        }else{
            File.Create(PATH);
            AnsiConsole.WriteLine("[red]Nessun file di salvataggio trovato[/]\n\nPremere un tasto...");
            Console.ReadKey();
        }
    }

    private static void InserisciPartecipante(){
        Console.Write("Nome partecipante: ");
                    nome = Console.ReadLine();
                    if(nome != ""){
                        if(partecipanti.Contains(nome)){
                            Console.WriteLine("La lista contiene già questo nome\n\nPremere un tasto...");
                            Console.ReadKey(); 
                            return; 
                        }else
                            partecipanti.Add(nome);
                    }else{
                        Console.WriteLine("Si prega di inserire un nome valido\n\nPremere un tasto...");
                        Console.ReadKey(); 
                    }
    }

    private static void VisualizzaPartecipanti(){
        
        partecipantiTable.Rows.Clear();

        foreach (string partecipante in partecipanti)
            partecipantiTable.AddRow(partecipante);
        

        if(partecipanti.Count == 0)
            Console.WriteLine("Lista vuota");

        AnsiConsole.Write(partecipantiTable);

        Console.WriteLine("Premere un tasto...");
        Console.ReadKey(); 
    }

    private static void OrdinaPartecipanti(){
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
    }

    private static void CercaPartecipante(){
        Console.Write("Nome partecipante: ");
        nome = Console.ReadLine();
        
        if (partecipanti.Contains(nome)){
            Console.WriteLine("Il partecipante è presente nella lista\n\nPremere un tasto...");
            Console.ReadKey();
        }
        else if (nome == ""){
            Console.WriteLine("Il nome cercato non è valido\n\nPremere un tasto...");
            Console.ReadKey();
        }else{
            Console.WriteLine("Il partecipante non è presente nella lista\n\nPremere un tasto...");
            Console.ReadKey();
        }  
    }

    private static void DivisioneSquadreRandom(){
        if (partecipanti.Count<2){
            Console.WriteLine("Non ci sono abbastanza partecipanti per creare due squadre\n\nPremere un tasto");
            Console.ReadKey();
        }else{
            squadra1.Clear();
            squadra2.Clear();
            squadreTable.Rows.Clear();

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
                }else if(i>=squadra2.Count){
                    squadreTable.AddRow(squadra1[i],"");
                }else
                    squadreTable.AddRow(squadra1[i],squadra2[i]);
            }

            AnsiConsole.Write(squadreTable);

            AnsiConsole.WriteLine("Premi un tasto per continuare...");

            Console.ReadKey();
        }
    }

    static  void DivisioneSquadreManuale(){
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

            int numberRow = 0;

            squadreTable.Rows.Clear();

            if(squadra1.Count>squadra2.Count)
                numberRow = squadra1.Count;
            else    
                numberRow = squadra2.Count;

            for(int i = 0;i<numberRow;i++){
                if(i>=squadra1.Count){
                    squadreTable.AddRow("",squadra2[i]);
                }else if(i>=squadra2.Count){
                    squadreTable.AddRow(squadra1[i],"");
                }else
                    squadreTable.AddRow(squadra1[i],squadra2[i]);
            }

            AnsiConsole.Write(squadreTable);

            AnsiConsole.WriteLine("Premi un tasto per continuare...");

            Console.ReadKey();
        }
    }

    static void EliminaPartecipante(){

        string sele = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("\nSeleziona partecipante\nda rimuovere")
            .PageSize(partecipanti.Count)
            .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
            .AddChoices(partecipanti));
                   
        partecipanti.Remove(sele);
        Console.WriteLine($"\nL'utente {sele} è stato rimosso\n\nPremere un tasto per continuare...");
        Console.ReadKey();        
    }

    static void GestioneDati(){
        string sel = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("\nAccedi al filesystem")
            .PageSize(4)
            .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
            .AddChoices(new string[] {
            "Salva", "Formatta File Salvato","Indietro",
        }));
        switch (sel){
            case "Salva":
                using (StreamWriter sw = File.CreateText(PATH)){
                    if(partecipanti.Count != 0){
                        for(int i = 0;i<partecipanti.Count;i++)
                                sw.WriteLine(partecipanti[i]);
                            Console.WriteLine("Lista salvata\nPremere un tasto...");
                            Console.ReadKey(); 
                        }else{
                            Console.WriteLine("La lista dei partecipanti è vuota...\nPremere un tasto...");
                            Console.ReadKey();
                        }
                    if(squadra1.Count != 0){
                        sw.WriteLine("######");
                        for(int i = 0;i<squadra1.Count;i++)
                            sw.WriteLine(squadra1[i]);
                        Console.WriteLine("Squadra 1 salvata\n\nPremere un tasto...");
                        Console.ReadKey();    
                    }else{
                        Console.WriteLine("La squadra 1 è vuota...\nPremere un tasto...");
                        Console.ReadKey();
                    }
                    if(squadra2.Count != 0){
                        sw.WriteLine("######");
                        for(int i = 0;i<squadra2.Count;i++)
                            sw.WriteLine(squadra2[i]);
                        Console.WriteLine("Squadra 2 salvata\n\nPremere un tasto...");
                        Console.ReadKey();    
                    }else{
                        Console.WriteLine("La squadra 2 è vuota...\nPremere un tasto...");
                        Console.ReadKey();
                    }
                }
                break;
                case "Formatta File Salvato":
                    using (StreamWriter sw = File.CreateText(PATH)){
                        Console.WriteLine("Il file di salvataggio è stato formattato...\nPremere un tasto...");
                        Console.ReadKey();
                    }
                    break;
                case "Indietro":
                    break;          
        }
    }

    static void UscireSalvare(){
        string sel = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("\nVuoi salvare la lista e le squadre?")
                        .PageSize(3)
                        .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                        .AddChoices(new string[] {
                        "Si","No",
                    }));
        switch (sel){
            case "Si":
                using (StreamWriter sw = File.CreateText(PATH)){
                    if(partecipanti.Count != 0){
                        for(int i = 0;i<partecipanti.Count;i++)
                            sw.WriteLine(partecipanti[i]); 
                    }else{
                        Console.WriteLine("La lista dei partecipanti è vuota...\nPremere un tasto...");
                        Console.ReadLine();
                    }
                    if(squadra1.Count != 0){
                        sw.WriteLine("######");
                        for(int i = 0;i<squadra1.Count;i++)
                            sw.WriteLine(squadra1[i]);
                        Console.WriteLine("Squadra 1 salvata\n\nPremere un tasto...");
                        Console.ReadKey();    
                    }else{
                        Console.WriteLine("La squadra 1 è vuota...\nPremere un tasto...");
                        Console.ReadKey();
                    }
                    if(squadra2.Count != 0){
                        sw.WriteLine("######");
                        for(int i = 0;i<squadra2.Count;i++)
                            sw.WriteLine(squadra2[i]);
                        Console.WriteLine("Squadra 2 salvata\n\nPremere un tasto...");
                        Console.ReadKey();    
                    }else{
                        Console.WriteLine("La squadra 2 è vuota...\nPremere un tasto...");
                        Console.ReadKey();
                    }
                }
                break;
            case "No":
                break;          
        }
        Console.WriteLine("Arrivederci!");
    }

    static void ModificaPartecipanti(){
        List<string> tempList = new List<string>();

            foreach(string s in partecipanti){
                if(s!="######")
                    tempList.Add(s);
                else
                    break;
            }

            string sele = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("\nSeleziona partecipante\nda modificare")
                .PageSize(tempList.Count)
                .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                .AddChoices(tempList));
        
        Console.WriteLine("Inserire il nome modificato: \n");

        string nuovoNome = Console.ReadLine();

        if(nuovoNome!=""){
            partecipanti[partecipanti.IndexOf(sele)] = nuovoNome;
            AnsiConsole.Markup($"\nL'utente [blue]{sele}[/] è stato modificato in [red]{nuovoNome}[/]\n\nPremere un tasto per continuare...");
            Console.ReadKey();
        }else{
            Console.WriteLine("\nIl nome inserito non è valido...\n\nPremere un tasto per continuare...");
            Console.ReadKey();
        }

    }
    
}



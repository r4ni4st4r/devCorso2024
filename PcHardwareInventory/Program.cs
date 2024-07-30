using Spectre.Console;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

class Program{
    //const string CSVPATH = "C:\\Users\\francesco\\Documents\\workspace\\devCorso2024\\PcHardwareInventory\\data\\csvFiles";
    //const string CATPATH = "C:\\Users\\francesco\\Documents\\workspace\\devCorso2024\\PcHardwareInventory\\data\\productsCategories";

    const string CSVPATH = @".\data\csvFiles";                                      // Path per i files .csv da importare
    const string CATPATH = @".\data\productsCategories";                            //  Path per le cartelle corrispondenti alle varie categorie di prodotto
                                                                                    //  cpu - mother board - video card - ram
    const string PROPERTIESFILE = "data.txt";                                       // file all'interno di ogni cartella categoria con le coppie proprietà/tipo di dato 
    static List<string> propertiesDataType = new List<string>();                    // Lista per i tipi di dati delle categorie
    static int cpuFileName = GetFileName(Path.Combine(CATPATH, "cpu"));             // File intero con il successivo nome file per la categoria cpu
    static int videoCardFileName = GetFileName(Path.Combine(CATPATH, "video card"));// File intero con il successivo nome file per la categoria video card
    static int ramFileName = GetFileName(Path.Combine(CATPATH, "ram"));             // File intero con il successivo nome file per la categoria ram
    static int mbFileName = GetFileName(Path.Combine(CATPATH, "mother board"));     // File intero con il successivo nome file per la categoria mother board

    // variabile di tutte le proprietà delle varie categorie
    static string brand = "";
    static string model = "";
    static string type = "";
    static int mhz = new int();
    static int socket = new int();
    static int size = new int();
    static int ram = new int();
    //******************************************************

    static void Main(string[] args){
        string selection = "";
        List<string> categories = CategoryList();
        
        string[] addProductSelection = new string[] {"Insert manually", "Insert from .csv file", "Back",};                              //array di tringhe con le parti fisse dei menu
        string[] manageWarehouseSelection = new string[] {"View Products", "Add Product", "Modify Product", "Remove Product", "Exit",}; //array di tringhe con le parti fisse dei menu

        while(true){
            Console.Clear();
            selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\n[red]Manage Warehouse[/]")
                .PageSize(manageWarehouseSelection.Count() + 1)
                .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                .AddChoices(manageWarehouseSelection));
            
            switch(selection){
                case "View Products":                                           // Selezione che permette di visualizzare i vari prodotti di ogni categoria
                    if(categories.Count > 0){                                   // all'interno di una tabella di Spertre.Console
                        categories.Add("Back");
                        selection = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("\n[red]Select a category to visualize[/]")
                            .PageSize(categories.Count + 1)
                            .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                            .AddChoices(categories));

                        switch(selection){
                            case "Back":
                                break;
                            default:
                                ViewProducts(selection);
                                break;
                        }

                    }else{
                        Console.WriteLine("You have to add a Product Category\n\nPress a key...");
                        Console.ReadKey();
                    }
                    categories.Remove("Back");
                    break;

                case "Add Product":                                         // Selezione che permette di inserire un prodotto manualmente selezionando la categoria
                    selection = AnsiConsole.Prompt(                         // o tramite la lettura di un file.csv
                    new SelectionPrompt<string>()
                        .Title("\n[red]Product insertion[/]")
                        .PageSize(addProductSelection.Length + 1)
                        .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                        .AddChoices(addProductSelection));

                    switch(selection){
                        case "Insert manually":
                            if(categories.Count > 0){
                                categories.Add("Back");
                                selection = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("\n[red]Select Product Category[/]")
                                    .PageSize(categories.Count + 1)
                                    .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                    .AddChoices(categories));

                                switch(selection){
                                    case "Back":
                                        break;
                                    default:
                                        InsertProductManually(PropertiesList(selection), selection);
                                        break;
                                }
                            }else{
                                Console.WriteLine("You have to add a Product Category\n\nPress a key...");
                                Console.ReadKey();
                            }
                            categories.Remove("Back");
                            break; 
                        case "Insert from .csv file":

                            List<string> csvList = GetCsvList(CSVPATH); // lista di file .csv all'interno di una cartella predefinita
                            
                            if(csvList.Count > 0){
                                csvList.Add("Back");

                                selection = AnsiConsole.Prompt(
                                                new SelectionPrompt<string>()
                                                .Title("\n[red]Select .csv file to load[/]")
                                                .PageSize(csvList.Count + 1)
                                                .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                                .AddChoices(csvList));

                                switch(selection){
                                    default:
                                        InsertProductCsv(selection);
                                        break;
                                    case "Back":

                                        break; 
                                }
                            }else{
                                Console.WriteLine("There aren't .csv to load!\n\nPress e key...");
                                Console.ReadKey();
                            }
                            break;
                    }

                    break;
                case "Modify Product":  // Voce che permette di modificare un prodotto selezionandolo all'interno della sua categoria
                    if(categories.Count > 0){
                        categories.Add("Back");
                        selection = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("\n[red]Select a product to remove[/]")
                            .PageSize(categories.Count + 1)
                            .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                            .AddChoices(categories));

                        switch(selection){
                            case "Back":
                                break;
                            default:
                                BuildRemoveOrModifyMenu(selection, false);
                                break;
                        }
                    }else{
                        Console.WriteLine("You have to add a Product Category\n\nPress a key...");
                        Console.ReadKey();
                    }
                    categories.Remove("Back");

                    break;
                case "Remove Product":           // Voce che permette di rimuovere un prodotto selezionandolo all'interno della sua categoria
                    if(categories.Count > 0){
                        categories.Add("Back");
                        selection = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("\n[red]Select a product to remove[/]")
                            .PageSize(categories.Count + 1)
                            .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                            .AddChoices(categories));

                        switch(selection){
                            case "Back":
                                break;
                            default:
                                BuildRemoveOrModifyMenu(selection, true);
                                break;
                        }
                    }else{
                        Console.WriteLine("You have to add a Product Category\n\nPress a key...");
                        Console.ReadKey();
                    }
                    categories.Remove("Back");
                    break;

                case "Exit":
                    return;
            }
        }
    }
    private static List<string> CategoryList(){
        List<string> productsCategories = new List<string>(Directory.GetDirectories(CATPATH)); 

            if(productsCategories.Count > 0){
                for(int i = 0; i < productsCategories.Count; i++){
                    productsCategories[i] = productsCategories[i].Remove(0, CATPATH.Length+1);
                }
                
                return productsCategories;
            }else
                return new List<string>();
    }
    private static List<string> PropertiesList(string category){   // funzione che restituisce una lista di categorie se non è presente
        List<string> properties = new List<string>();
        string line;
            
        using(StreamReader sr = new StreamReader(Path.Combine(CATPATH, category, PROPERTIESFILE))){
            while((line = sr.ReadLine())!= null){
                propertiesDataType.Add(line.Split(",").Last());
                properties.Add(line.Split(",").First());
            }
        }

            return properties;
        
    }

    private static void InsertProductManually(List<string> properties, string item){    // Funzione che prende la lista delle proprietà del prodotto e la categoria 
        bool success;                                                                   // e permette l'inserimento dei campi e salva un nuovo file .json nella cartella
                                                                                        // corretta
        switch(item){
            case "cpu":
                cpuFileName = GetFileName(Path.Combine(CATPATH, item));

                mhz = new int();
                brand = "";
                model = "";

                for(int i = 0;i < properties.Count; i++){
                    if(properties[i] == "mhz"){

                        do{
                            Console.Clear();
                            Console.WriteLine($"Insert an integer number for the {properties[i]} property: \n");
                            Console.Write($"{properties[i]} --> ");
                            success = int.TryParse(Console.ReadLine(), out int result);
                        
                            if(success){
                                mhz = result;
                            }else{
                                Console.WriteLine("Please enter a valid integer\n\nPress a key...");
                                Console.ReadKey();
                            }
                        }while(!success);

                    }
                    if(properties[i] == "brand"){
                        do{
                            success = true;
                            Console.Clear();
                            Console.WriteLine($"Insert a value for the {properties[i]} property: \n");
                            Console.Write($"{properties[i]} --> ");
                            brand = Console.ReadLine();
                            if(brand == ""){
                                success = false;
                                Console.WriteLine("Please enter a valid string\n\nPress a key...");
                                Console.ReadKey();
                            }
                        }while(!success);
                    }
                    if(properties[i] == "model"){
                        do{
                            success = true;
                            Console.Clear();
                            Console.WriteLine($"Insert a value for the {properties[i]} property: \n");
                            Console.Write($"{properties[i]} --> ");
                            model = Console.ReadLine();
                            if(model == ""){
                                success = false;
                                Console.WriteLine("Please enter a valid string\n\nPress a key...");
                                Console.ReadKey();
                            }
                        }while(!success);

                    }
                }

                string file = cpuFileName.ToString();

                string path = Path.Combine(CATPATH, item, file + ".json");

                File.Create(path).Close();

                using (StreamWriter sw = new StreamWriter(path)){                                           // creazione del file .json e generazione di un nome file
                    sw.Write(JsonConvert.SerializeObject(new {brand, model, mhz}, Formatting.Indented));    // numerico progressivo e univoco
                }

                File.WriteAllText(Path.Combine(CATPATH, item, "fileName.txt"), (cpuFileName+1).ToString());

                break;
            case "mother board":
                mbFileName = GetFileName(Path.Combine(CATPATH, item));

                brand = "";
                model = "";
                socket = new int();

                for(int i = 0;i < properties.Count; i++){
                    if(properties[i] == "socket"){

                        do{
                            Console.Clear();
                            Console.WriteLine($"Insert an integer number for the {properties[i]} property: \n");
                            Console.Write($"{properties[i]} --> ");
                            success = int.TryParse(Console.ReadLine(), out int result);
                        
                            if(success){
                                socket = result;
                            }else{
                                Console.WriteLine("Please enter a valid integer\n\nPress a key...");
                                Console.ReadKey();
                            }
                        }while(!success);

                    }
                    if(properties[i] == "brand"){
                        do{
                            success = true;
                            Console.Clear();
                            Console.WriteLine($"Insert a value for the {properties[i]} property: \n");
                            Console.Write($"{properties[i]} --> ");
                            brand = Console.ReadLine();
                            if(brand == ""){
                                success = false;
                                Console.WriteLine("Please enter a valid string\n\nPress a key...");
                                Console.ReadKey();
                            }
                        }while(!success);
                    }
                    if(properties[i] == "model"){
                        do{
                            success = true;
                            Console.Clear();
                            Console.WriteLine($"Insert a value for the {properties[i]} property: \n");
                            Console.Write($"{properties[i]} --> ");
                            model = Console.ReadLine();
                            if(model == ""){
                                success = false;
                                Console.WriteLine("Please enter a valid string\n\nPress a key...");
                                Console.ReadKey();
                            }
                        }while(!success);

                    }
                }

                string mbFile = mbFileName.ToString();

                string mbPath = Path.Combine(CATPATH, item, mbFile + ".json");

                File.Create(mbPath).Close();

                using (StreamWriter sw = new StreamWriter(mbPath)){ 
                    sw.Write(JsonConvert.SerializeObject(new {brand, model, socket}, Formatting.Indented));
                }

                File.WriteAllText(Path.Combine(CATPATH, item, "fileName.txt"), (mbFileName+1).ToString());

                break;
            case "video card":
                success = true;
                videoCardFileName = GetFileName(Path.Combine(CATPATH, item));


                brand = "";
                model = "";
                ram = new int();

                for(int i = 0;i < properties.Count; i++){
                    if(properties[i] == "brand"){
                        do{
                            success = true;
                            Console.Clear();
                            Console.WriteLine($"Insert a value for the {properties[i]} property: \n");
                            Console.Write($"{properties[i]} --> ");
                            brand = Console.ReadLine();
                            if(brand == ""){
                                success = false;
                                Console.WriteLine("Please enter a valid string\n\nPress a key...");
                                Console.ReadKey();
                            }
                        }while(!success);

                    }else if(properties[i] == "model"){
                        do{
                            success = true;
                            Console.Clear();
                            Console.WriteLine($"Insert a value for the {properties[i]} property: \n");
                            Console.Write($"{properties[i]} --> ");
                            model = Console.ReadLine();
                            if(model == ""){
                                success = false;
                                Console.WriteLine("Please enter a valid string\n\nPress a key...");
                                Console.ReadKey();
                            }
                        }while(!success);

                    }else if(properties[i] == "ram"){
                         do{
                            Console.Clear();
                            Console.WriteLine($"Insert an integer number for the {properties[i]} property: \n");
                            Console.Write($"{properties[i]} --> ");
                            success = int.TryParse(Console.ReadLine(), out int result);
                        
                            if(success){
                                ram = result;
                            }else{
                                Console.WriteLine("Please enter a valid integer\n\nPress a key...");
                                Console.ReadKey();
                            }
                        }while(!success);

                    }
                }

                string videoCardFile = videoCardFileName.ToString();

                string videoCardPath = Path.Combine(CATPATH, item, videoCardFile + ".json");

                File.Create(videoCardPath).Close();

                using (StreamWriter sw = new StreamWriter(videoCardPath)){ 
                    sw.Write(JsonConvert.SerializeObject(new {brand, model, ram}, Formatting.Indented));
                }

                File.WriteAllText(Path.Combine(CATPATH, item, "fileName.txt"), (videoCardFileName + 1).ToString());

                break;
            case "ram":
                success = true;
                ramFileName = GetFileName(Path.Combine(CATPATH, item));

                brand = "";
                size = new int();
                type = "";
                mhz = new int();

                for(int i = 0;i < properties.Count; i++){
                    if(properties[i] == "brand"){
                        do{
                            success = true;
                            Console.Clear();
                            Console.WriteLine($"Insert a value for the {properties[i]} property: \n");
                            Console.Write($"{properties[i]} --> ");
                            brand = Console.ReadLine();
                            if(brand == ""){
                                success = false;
                                Console.WriteLine("Please enter a valid string\n\nPress a key...");
                                Console.ReadKey();
                            }
                        }while(!success);

                    }else if(properties[i] == "size"){
                        do{
                            Console.Clear();
                            Console.WriteLine($"Insert an integer number for the {properties[i]} property: \n");
                            Console.Write($"{properties[i]} --> ");
                            success = int.TryParse(Console.ReadLine(), out int result);
                        
                            if(success){
                                size = result;
                            }else{
                                Console.WriteLine("Please enter a valid integer\n\nPress a key...");
                                Console.ReadKey();
                            }
                        }while(!success);

                    }else if(properties[i] == "type"){
                         do{
                            Console.Clear();
                            Console.WriteLine($"Insert an integer number for the {properties[i]} property: \n");
                            Console.Write($"{properties[i]} --> ");
                            type = Console.ReadLine();
                        
                            if(type == ""){
                                success = false;
                                Console.WriteLine("Please enter a valid string\n\nPress a key...");
                                Console.ReadKey();
                            }
                        }while(!success);

                    }else if(properties[i] == "mhz"){
                         do{
                            Console.Clear();
                            Console.WriteLine($"Insert an integer number for the {properties[i]} property: \n");
                            Console.Write($"{properties[i]} --> ");
                            success = int.TryParse(Console.ReadLine(), out int result);
                        
                            if(success){
                                mhz = result;
                            }else{
                                Console.WriteLine("Please enter a valid integer\n\nPress a key...");
                                Console.ReadKey();
                            }
                        }while(!success);

                    }
                }

                string ramFile = ramFileName.ToString();

                string ramPath = Path.Combine(CATPATH, item, ramFile + ".json");

                File.Create(ramPath).Close();

                using (StreamWriter sw = new StreamWriter(ramPath)){ 
                    sw.Write(JsonConvert.SerializeObject(new {brand, model, ram}, Formatting.Indented));
                }

                File.WriteAllText(Path.Combine(CATPATH, item, "fileName.txt"), (ramFileName + 1).ToString());
                break;
        }
    }

    private static void ViewProducts(string category){ // deserializzazione dei file .json -> popolamentio di una tabella Spectre.Console e visualizzazione
        Table viewTable = new Table();
        
        List<string> filesList = new List<string>(Directory.GetFiles(Path.Combine(CATPATH, category)));

        switch(category){
            case "cpu":
                if(filesList.Count!=0){
                    viewTable.AddColumns("Brand", "Model", "Mhz");

                    foreach(string s in filesList){

                        if(!s.Contains("data.txt")&&!s.Contains("fileName.txt")){

                            string json = File.ReadAllText(s);

                            dynamic obj = JsonConvert.DeserializeObject(json);

                            string brand = obj.brand;
                            string model = obj.model;
                            string mhz = obj.mhz.ToString();
        
                            viewTable.AddRow(brand, model, mhz);
                        }
                    }

                    AnsiConsole.Write(viewTable);

                    AnsiConsole.WriteLine("Press a key to continue...");

                    Console.ReadKey();
                }else{
                    Console.WriteLine("There aren't Products to visualize!!!\n\n");
                    Console.ReadKey();
                }
                break;
            case "mother board":
                if(filesList.Count>2){
                    viewTable.AddColumns("Brand", "Model", "Socket");

                    foreach(string s in filesList){

                        if(!s.Contains("data.txt")&&!s.Contains("fileName.txt")){

                            string json = File.ReadAllText(s);

                            dynamic obj = JsonConvert.DeserializeObject(json);

                            string brand = obj.brand;
                            string model = obj.model;
                            string socket = obj.socket.ToString();
        
                            viewTable.AddRow(brand, model, socket);
                        }
                    }

                    AnsiConsole.Write(viewTable);

                    AnsiConsole.WriteLine("Press a key to continue...");

                    Console.ReadKey();
                }else{
                    Console.WriteLine("There aren't Products to visualize!!!\n\n");
                    Console.ReadKey();
                }
                break;

            case "ram":
                if(filesList.Count>2){
                    viewTable.AddColumns("Brand", "Size", "Type", "Mhz");  

                    foreach(string s in filesList){

                        if(!s.Contains("data.txt")&&!s.Contains("fileName.txt")){

                            string json = File.ReadAllText(s);

                            dynamic obj = JsonConvert.DeserializeObject(json);

                            string brand = obj.brand;
                            string size = obj.size.ToString();
                            string type = obj.type;
                            string mhz = obj.mhz.ToString();
        
                            viewTable.AddRow(brand, size + " Gb", type, mhz);//brand, *size, type, *mhz
                        }
                    }

                    AnsiConsole.Write(viewTable);

                    AnsiConsole.WriteLine("Press a key to continue...");

                    Console.ReadKey();
                }else{
                    Console.WriteLine("There aren't Products to visualize!!!\n\n");
                    Console.ReadKey();
                }
                break;
            case "video card":
                if(filesList.Count > 2){
                    viewTable.AddColumns("Brand", "Model", "Ram");  

                    foreach(string s in filesList){

                        if(!s.Contains("data.txt")&&!s.Contains("fileName.txt")){

                            string json = File.ReadAllText(s);

                            dynamic obj = JsonConvert.DeserializeObject(json);

                            string brand = obj.brand;
                            string model = obj.model;
                            int ram = obj.ram;
        
                            viewTable.AddRow(brand, model, ram.ToString() + " Gb");
                        }
                    }

                    AnsiConsole.Write(viewTable);

                    AnsiConsole.WriteLine("Press a key to continue...");

                    Console.ReadKey();
                }else{
                    Console.WriteLine("There aren't Products to visualize!!!\n\n");
                    Console.ReadKey();
                }
                break;
        }
    }

    private static int GetFileName(string path) {  //legge il nome del file da un .txt e lo ritorna in intero

        string tmp = File.ReadAllText(Path.Combine(path, "fileName.txt"));

        return Convert.ToInt32(tmp);
    }

    private static void BuildRemoveOrModifyMenu(string category, bool delete){  // deserializza i files .json e popola un menu di stringhe leggibile per selezionare il
        List<string> filesList = new List<string>(Directory.GetFiles(Path.Combine(CATPATH, category))); // il prodotto/file da cancellare
        List<string> menuList = new List<string>();

        switch(category){
            case "cpu":

                foreach(string s in filesList){

                    if(!s.Contains("data.txt")&&!s.Contains("fileName.txt")){

                        string tmp = Path.GetFileName(s)+"   ->  ";
                        string json = File.ReadAllText(s);

                        dynamic obj = JsonConvert.DeserializeObject(json);

                        string s1 = obj.brand;
                        tmp += " [green]Brand[/]: "+s1;

                        string s2 = obj.model;
                        tmp += " [green]Model[/]: "+s2;
                        
                        string s3 = obj.mhz.ToString();
                        tmp += " [green]Mhz[/]: "+s3;

                        menuList.Add(tmp);

                    }
                }
                if(menuList.Count > 0){
                    menuList.Add("Back");
                    string selection = AnsiConsole.Prompt(
                                        new SelectionPrompt<string>()
                                        .Title("\n[red]Select Product to remove[/]")
                                        .PageSize(menuList.Count + 4)
                                        .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                        .AddChoices(menuList));

                    switch(selection){
                        case "Back":
                            break;
                        default:
                            if(delete)
                                DeleteFile(selection, category);
                            else
                                ModifyProduct(selection, category);
                            break;
                    }
                }else{
                    Console.WriteLine("CPU category is empty!!!\n\nPress a key...");
                    Console.ReadKey();
                }
                menuList.Remove("Back");
                break;

            case "mother board":
                foreach(string s in filesList){

                    if(!s.Contains("data.txt")&&!s.Contains("fileName.txt")){

                        string tmp = Path.GetFileName(s)+"   ->  ";
                        string json = File.ReadAllText(s);

                        dynamic obj = JsonConvert.DeserializeObject(json);

                        string s1 = obj.brand;
                        tmp += " [green]Brand[/]: "+s1;
                        
                        string s2 = obj.model;
                        tmp += " [green]Model[/]: "+s2;
                        
                        string s3 = obj.socket.ToString();
                        tmp += " [green]Socket[/]: "+s3;

                        menuList.Add(tmp);

                    }
                }
                if(menuList.Count > 0){
                    menuList.Add("Back");
                    string selection = AnsiConsole.Prompt(
                                        new SelectionPrompt<string>()
                                        .Title("\n[red]Select a product to delete[/]")
                                        .PageSize(menuList.Count + 4)
                                        .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                        .AddChoices(menuList));

                    switch(selection){
                        case "Back":
                            break;
                        default:
                            if(delete)
                                DeleteFile(selection, category);
                            else
                                ModifyProduct(selection, category);
                            break;
                    }
                }else{
                    Console.WriteLine("Motherboard category is empty!!!\n\nPress a key...");
                    Console.ReadKey();
                }
                menuList.Remove("Back");
                break;
            case "ram":
                foreach(string s in filesList){

                    if(!s.Contains("data.txt")&&!s.Contains("fileName.txt")){

                        string tmp = Path.GetFileName(s)+"   ->  ";
                        string json = File.ReadAllText(s);

                        dynamic obj = JsonConvert.DeserializeObject(json);

                        string s1 = obj.brand;
                        tmp += " [green]Brand[/]: "+s1;
                        
                        string s2 = obj.size.ToString();
                        tmp += " [green]Size[/]: "+s2+"Gb";           
                        
                        string s3 = obj.type;
                        tmp += " [green]Type[/]: "+s3;

                        string s4 = obj.mhz.ToString();
                        tmp += " [green]Mhz[/]: "+s4;

                        menuList.Add(tmp);

                    }
                }
                if(menuList.Count > 0){
                    menuList.Add("Back");
                    string selection = AnsiConsole.Prompt(
                                        new SelectionPrompt<string>()
                                        .Title("\n[red]Select a product to delete[/]")
                                        .PageSize(menuList.Count + 4)
                                        .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                        .AddChoices(menuList));

                    switch(selection){
                        case "Back":
                            break;
                        default:
                            if(delete)
                                DeleteFile(selection, category);
                            else
                                ModifyProduct(selection, category);
                            break;
                    }
                }else{
                    Console.WriteLine("Ram category is empty!!!\n\nPress a key...");
                    Console.ReadKey();
                }

                menuList.Remove("Back");
                break;
            case "video card":
                foreach(string s in filesList){

                    if(!s.Contains("data.txt") && !s.Contains("fileName.txt")){

                        string tmp = Path.GetFileName(s)+"   ->  ";
                        string json = File.ReadAllText(s);

                        dynamic obj = JsonConvert.DeserializeObject(json);

                        string s1 = obj.brand;
                        tmp += " [green]Brand[/]: "+s1;
                        
                        string s2 = obj.model;
                        tmp += " [green]Model[/]: "+s2;           
                        
                        string s3 = obj.ram.ToString();
                        tmp += " [green]Ram[/]: "+s3+"Gb";

                        menuList.Add(tmp);

                    }
                }
                if(menuList.Count > 0){
                    menuList.Add("Back");
                    string selection = AnsiConsole.Prompt(
                                        new SelectionPrompt<string>()
                                        .Title("\n[red]Select a product to delete[/]")
                                        .PageSize(menuList.Count + 1)
                                        .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                        .AddChoices(menuList));

                    switch(selection){
                        case "Back":
                            break;
                        default:
                            if(delete)
                                DeleteFile(selection, category);
                            else
                                ModifyProduct(selection, category);
                            break;
                    }
                }else{
                    Console.WriteLine("Video Card category is empty!!!\n\nPress a key...");
                    Console.ReadKey();
                }
                
                menuList.Remove("Back");

                break;
        }
    }

    private static List<string> GetCsvList(string path){    // Ritorna solo i files .csv all'interno di una cartella
        List<string> csvFiles = new List<string>(); 

        foreach(string s in Directory.GetFiles(path)){
            if(s.EndsWith(".csv"))
                csvFiles.Add(Path.GetFileName(s));
        }

        return csvFiles;
    }

    private static void DeleteFile(string selection, string category){  // Rimuove il file selezionato
        bool deleted = true;                                                      // e verifica che non ci siano errori
        string tmp = selection.Remove(9,selection.Length-9).TrimEnd();

        try{
            File.Delete(Path.Combine(CATPATH, category, tmp));
        }catch(Exception e){
            Console.WriteLine($"ERROR: {e.Message}\nPress a key...");
            Console.ReadKey();
            deleted = false;
        }

        if(deleted){
            Console.WriteLine("File successfully deleted!\nPress a key...");
            Console.ReadKey();
        }
    }

    private static void ModifyProduct(string selectionToModify, string category){
        if(category == "cpu"){
            
            string file = selectionToModify.Remove(9,selectionToModify.Length-9).TrimEnd();

            string json = File.ReadAllText(Path.Combine(CATPATH, category, file));

            dynamic obj = JsonConvert.DeserializeObject(json);

            List<string> propertySelection = new List<string>(new [] {"Brand","Model","Mhz","Back"});
            
            bool anotherOne = true;

            while(anotherOne){
                Console.Clear();
                string selection = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("\n[red]Select a Property to Modify[/]")
                                    .PageSize(propertySelection.Count + 1)
                                    .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                    .AddChoices(propertySelection));

                switch(selection){
                    case "Back":
                        anotherOne = false;
                        break;
                    default:
                        if(selection == "Brand"){
                            string newProp;
                            do{
                                Console.WriteLine($"Please enter a new value for the {selection} property: ");
                                newProp = Console.ReadLine();
                                if(newProp!=""){
                                    obj.brand = newProp;
                                }else{
                                    Console.WriteLine("New value can't be empty!\nPress a key...");
                                    Console.ReadKey();
                                }
                            }while(newProp == "");
                        }else if(selection == "Model"){
                            string newProp;
                            do{
                                Console.WriteLine($"Please enter a new value for the {selection} property: ");
                                newProp = Console.ReadLine();
                                if(newProp!=""){
                                    obj.model = newProp;
                                }else{
                                    Console.WriteLine("New value can't be empty!\nPress a key...");
                                    Console.ReadKey();
                                }
                            }while(newProp == "");
                        }else if(selection == "Mhz"){
                            string newProp;
                            do{
                                Console.WriteLine($"Please enter a new value for the {selection} property: ");
                                newProp = Console.ReadLine();
                                if(Int32.TryParse(newProp, out int result)){
                                    obj.mhz = result;
                                }else{
                                    Console.WriteLine("New value must be a integer!\nPress a key...");
                                    Console.ReadKey();
                                }
                            }while(newProp == "");
                        }
                        Console.Clear();
                        string yesOrNot = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("\n[red]Do you want to modify another property?[/]")
                                    .PageSize(2 + 1)
                                    .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                    .AddChoices(new []{"yes","no"}));
                        switch(yesOrNot){
                            case "yes":
                                break;
                            case "no":
                                anotherOne = false;
                                break;
                        }

                        
                    break;
                }
            }
            WriteJsonFile(obj, category, file);
        }else if(category == "video card"){
            string file = selectionToModify.Remove(9,selectionToModify.Length-9).TrimEnd();

            string json = File.ReadAllText(Path.Combine(CATPATH, category, file));

            dynamic obj = JsonConvert.DeserializeObject(json);

            List<string> propertySelection = new List<string>(new [] {"Brand","Model","Ram","Back"});
            
            bool anotherOne = true;

            while(anotherOne){
                Console.Clear();
                string selection = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("\n[red]Select a Property to Modify[/]")
                                    .PageSize(propertySelection.Count + 1)
                                    .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                    .AddChoices(propertySelection));

                switch(selection){
                    case "Back":
                        anotherOne = false;
                        break;
                    default:
                        if(selection == "Brand"){
                            string newProp;
                            do{
                                Console.WriteLine($"Please enter a new value for the {selection} property: ");
                                newProp = Console.ReadLine();
                                if(newProp!=""){
                                    obj.brand = newProp;
                                }else{
                                    Console.WriteLine("New value can't be empty!\nPress a key...");
                                    Console.ReadKey();
                                }
                            }while(newProp == "");
                        }else if(selection == "Model"){
                            string newProp;
                            do{
                                Console.WriteLine($"Please enter a new value for the {selection} property: ");
                                newProp = Console.ReadLine();
                                if(newProp!=""){
                                    obj.model = newProp;
                                }else{
                                    Console.WriteLine("New value can't be empty!\nPress a key...");
                                    Console.ReadKey();
                                }
                            }while(newProp == "");
                        }else if(selection == "Ram"){
                            string newProp;
                            do{
                                Console.WriteLine($"Please enter a new value for the {selection} property: ");
                                newProp = Console.ReadLine();
                                if(Int32.TryParse(newProp, out int result)){
                                    obj.ram = result;
                                }else{
                                    Console.WriteLine("New value must be a integer!\nPress a key...");
                                    Console.ReadKey();
                                }
                            }while(newProp == "");
                        }
                        Console.Clear();
                        string yesOrNot = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("\n[red]Do you want to modify another property?[/]")
                                    .PageSize(2 + 1)
                                    .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                    .AddChoices(new []{"yes","no"}));
                        switch(yesOrNot){
                            case "yes":
                                break;
                            case "no":
                                anotherOne = false;
                                break;
                        }

                        
                    break;
                }
            }
            WriteJsonFile(obj, category, file);
        }else if(category == "mother board"){

            string file = selectionToModify.Remove(9,selectionToModify.Length-9).TrimEnd();

            string json = File.ReadAllText(Path.Combine(CATPATH, category, file));

            dynamic obj = JsonConvert.DeserializeObject(json);

            List<string> propertySelection = new List<string>(new [] {"Brand", "Model", "Socket", "Back"});
            
            bool anotherOne = true;

            while(anotherOne){
                Console.Clear();
                string selection = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("\n[red]Select a Property to Modify[/]")
                                    .PageSize(propertySelection.Count + 1)
                                    .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                    .AddChoices(propertySelection));

                switch(selection){
                    case "Back":
                        anotherOne = false;
                        break;
                    default:
                        if(selection == "Brand"){
                            string newProp;
                            do{
                                Console.WriteLine($"Please enter a new value for the {selection} property: ");
                                newProp = Console.ReadLine();
                                if(newProp!=""){
                                    obj.brand = newProp;
                                }else{
                                    Console.WriteLine("New value can't be empty!\nPress a key...");
                                    Console.ReadKey();
                                }
                            }while(newProp == "");
                        }else if(selection == "Model"){
                            string newProp;
                            do{
                                Console.WriteLine($"Please enter a new value for the {selection} property: ");
                                newProp = Console.ReadLine();
                                if(newProp!=""){
                                    obj.model = newProp;
                                }else{
                                    Console.WriteLine("New value can't be empty!\nPress a key...");
                                    Console.ReadKey();
                                }
                            }while(newProp == "");
                        }else if(selection == "Socket"){
                            string newProp;
                            do{
                                Console.WriteLine($"Please enter a new value for the {selection} property: ");
                                newProp = Console.ReadLine();
                                if(Int32.TryParse(newProp, out int result)){
                                    obj.socket = result;
                                }else{
                                    Console.WriteLine("New value must be a integer!\nPress a key...");
                                    Console.ReadKey();
                                }
                            }while(newProp == "");
                        }
                        Console.Clear();
                        string yesOrNot = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("\n[red]Do you want to modify another property?[/]")
                                    .PageSize(2 + 1)
                                    .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                    .AddChoices(new []{"yes","no"}));
                        switch(yesOrNot){
                            case "yes":
                                break;
                            case "no":
                                anotherOne = false;
                                break;
                        }

                        
                    break;
                }
            }

            WriteJsonFile(obj, category, file);

        }else if(category == "ram"){
            string file = selectionToModify.Remove(9,selectionToModify.Length-9).TrimEnd();

            string json = File.ReadAllText(Path.Combine(CATPATH, category, file));

            dynamic obj = JsonConvert.DeserializeObject(json);

            List<string> propertySelection = new List<string>(new [] {"Brand","Size","Type","Mhz","Back"});
            
            bool anotherOne = true;

            while(anotherOne){
                Console.Clear();
                string selection = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("\n[red]Select a Property to Modify[/]")
                                    .PageSize(propertySelection.Count + 1)
                                    .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                    .AddChoices(propertySelection));

                switch(selection){
                    case "Back":
                        anotherOne = false;
                        break;
                    default:
                        if(selection == "Brand"){
                            string newProp;
                            do{
                                Console.WriteLine($"Please enter a new value for the {selection} property: ");
                                newProp = Console.ReadLine();
                                if(newProp!=""){
                                    obj.brand = newProp;
                                }else{
                                    Console.WriteLine("New value can't be empty!\nPress a key...");
                                    Console.ReadKey();
                                }
                            }while(newProp == "");
                        }else if(selection == "Size"){
                            string newProp;
                            do{
                                Console.WriteLine($"Please enter a new value for the {selection} property: ");
                                newProp = Console.ReadLine();
                                if(Int32.TryParse(newProp, out int result)){
                                    obj.size = result;
                                }else{
                                    Console.WriteLine("New value must be a integer!\nPress a key...");
                                    Console.ReadKey();
                                }
                            }while(newProp == "");

                        }else if(selection == "Type"){
                            string newProp;

                            do{
                                Console.WriteLine($"Please enter a new value for the {selection} property: ");
                                newProp = Console.ReadLine();
                                if(newProp!=""){
                                    obj.type = newProp;
                                }else{
                                    Console.WriteLine("New value can't be empty!\nPress a key...");
                                    Console.ReadKey();
                                }
                            }while(newProp == "");
                        }else if(selection == "Mhz"){
                            string newProp;
                            do{
                                Console.WriteLine($"Please enter a new value for the {selection} property: ");
                                newProp = Console.ReadLine();
                                if(Int32.TryParse(newProp, out int result)){
                                    obj.mhz = result;
                                }else{
                                    Console.WriteLine("New value must be a integer!\nPress a key...");
                                    Console.ReadKey();
                                }
                            }while(newProp == "");
                        }
                        Console.Clear();
                        string yesOrNot = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("\n[red]Do you want to modify another property?[/]")
                                    .PageSize(2 + 1)
                                    .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                    .AddChoices(new []{"yes","no"}));
                        switch(yesOrNot){
                            case "yes":
                                break;
                            case "no":
                                anotherOne = false;
                                break;
                        }
                    break;
                }
            }
            WriteJsonFile(obj, category, file);
        }
    }

    private static void WriteJsonFile(dynamic obj, string category, string file){
        string jsonPath = Path.Combine(CATPATH, category, file);

            bool success = true;

            try{
                using (StreamWriter sw = new StreamWriter(jsonPath)){ 
                    sw.Write(JsonConvert.SerializeObject(obj, Formatting.Indented));
                }
            }catch(Exception ex){
                Console.WriteLine("\nThere was a problem...\n");
                Console.WriteLine($"ERROR --> {ex.Message}\nPress a key...");
                Console.ReadKey();
                success = false;
            }

            if(success){
                Console.WriteLine("Product successfully modified!\nPress a key...");
                Console.ReadKey();
            }
    } 

    private static void InsertProductCsv(string file){          // Legge un file .csv e iserisce tutti i prodotti all'interno
                                                                // creando per ognuno un file .json
        string path = Path.Combine(CSVPATH, file);
        
        string[] lines = File.ReadAllLines(path); 
        string[][] prodotti = new string [lines.Length][];

        for(int i = 0; i < lines.Length; i++){
            prodotti[i] = lines[i].Split(",");
        }

        for(int i = 0; i< prodotti.Length; i++){
            if(i!=0){
                switch(prodotti[i][0]){
                    case "cpu":
                        mhz = new int();
                        brand = "";
                        model = "";

                        cpuFileName = GetFileName(Path.Combine(CATPATH, prodotti[i][0]));

                        for(int j = 1; j < prodotti[i].Length; j++){
                            brand = prodotti[i][1];

                            if(prodotti[i][j].Contains("model")){
                                
                                model = prodotti[i][j].Split(":").Last();

                            }else if(prodotti[i][j].Contains("mhz")){

                                int.TryParse(prodotti[i][j].Split(":").Last(), out int result);
                        
                                mhz = result;
                            }
                        }

                        string jsonCpuPath = Path.Combine(CATPATH, prodotti[i][0], cpuFileName.ToString() + ".json");

                        using (StreamWriter sw = new StreamWriter(jsonCpuPath)){ 
                            sw.Write(JsonConvert.SerializeObject(new {brand, model, mhz}, Formatting.Indented));
                        }

                        File.WriteAllText(Path.Combine(CATPATH, prodotti[i][0], "fileName.txt"), (cpuFileName+1).ToString());

                    break;

                    case "mother board":
                        socket = new int();
                        brand = "";
                        model = "";

                        mbFileName = GetFileName(Path.Combine(CATPATH, prodotti[i][0]));

                        for(int j = 1; j < prodotti[i].Length; j++){
                            brand = prodotti[i][1];

                            if(prodotti[i][j].Contains("model")){
                                
                                model = prodotti[i][j].Split(":").Last();

                            }else if(prodotti[i][j].Contains("socket")){

                                int.TryParse(prodotti[i][j].Split(":").Last(), out int result);
                        
                                socket = result;
                            }
                        }

                        string jsonMbPath = Path.Combine(CATPATH, prodotti[i][0], mbFileName.ToString() + ".json");

                        using (StreamWriter sw = new StreamWriter(jsonMbPath)){ 
                            sw.Write(JsonConvert.SerializeObject(new {brand, model, socket}, Formatting.Indented));
                        }

                        File.WriteAllText(Path.Combine(CATPATH, prodotti[i][0], "fileName.txt"), (mbFileName+1).ToString());
                    break;

                case "ram":
                    brand = "";
                    size = new int();
                    type = "";
                    mhz = new int();                    

                    ramFileName = GetFileName(Path.Combine(CATPATH, prodotti[i][0]));

                    for(int j = 1; j < prodotti[i].Length; j++){
                        brand = prodotti[i][1];

                        if(prodotti[i][j].Contains("type")){
                            
                            type = prodotti[i][j].Split(":").Last();

                        }else if(prodotti[i][j].Contains("mhz")){

                            int.TryParse(prodotti[i][j].Split(":").Last(), out int result);
                        
                            mhz = result;

                        }else if(prodotti[i][j].Contains("size")){

                            int.TryParse(prodotti[i][j].Split(":").Last(), out int result);
                        
                            size = result;
                        }
                    }

                    string jsonRamPath = Path.Combine(CATPATH, prodotti[i][0], ramFileName.ToString() + ".json");

                    using (StreamWriter sw = new StreamWriter(jsonRamPath)){ 
                        sw.Write(JsonConvert.SerializeObject(new {brand, size, type, mhz}, Formatting.Indented));
                    }

                    File.WriteAllText(Path.Combine(CATPATH, prodotti[i][0], "fileName.txt"), (ramFileName + 1).ToString());

                    break;
                case "video card":

                    brand = "";
                    model = "";
                    ram = new int();
                    
                    videoCardFileName = GetFileName(Path.Combine(CATPATH, prodotti[i][0]));

                    for(int j = 1; j < prodotti[i].Length; j++){
                        brand = prodotti[i][1];

                        if(prodotti[i][j].Contains("model")){
                            
                            model = prodotti[i][j].Split(":").Last();

                        }else if(prodotti[i][j].Contains("ram")){

                            int.TryParse(prodotti[i][j].Split(":").Last(), out int result);
                        
                            ram = result;
                        }
                    }

                    string jsonVideoCardPath = Path.Combine(CATPATH, prodotti[i][0], videoCardFileName.ToString() + ".json");

                    using (StreamWriter sw = new StreamWriter(jsonVideoCardPath)){ 
                        sw.Write(JsonConvert.SerializeObject(new {brand, model, ram}, Formatting.Indented));
                    }

                    File.WriteAllText(Path.Combine(CATPATH, prodotti[i][0], "fileName.txt"), (videoCardFileName + 1).ToString());

                    break;
                default:
                    break;
                }
            }
        }
        string question = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                .Title("\n[red]Do you want to delete the loaded file?[/]\n")
                                .PageSize(3)
                                .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                .AddChoices(new []{"Yes","No"}));

        switch(question){   // Una volta caricato il file .csv il programma ci chiede se vogliamo conservare il file rinominandolo e assegnandogli 
                            // l'estensione .csv.old
            case "Yes":     // oppure cancellarlo
                bool goForward = true;
                try{
                    
                    File.Delete(Path.Combine(CSVPATH, file));

                }catch(Exception e){
                    
                    Console.WriteLine("\nThere was a problem...\n");
                    Console.WriteLine($"ERROR --> {e.Message}\nPress a key...");
                    goForward = false;
                    Console.ReadKey();
        
                }
                if(goForward){
                    Console.WriteLine($"{file} is been deleted\n\nPress a key...");
                    Console.ReadKey();
                }
            break;

            case "No":
                bool ok = true;
                string newFile;

                while(ok){
                    Console.Clear();
                    Console.WriteLine("Please enter a filename to archive the .csv: ");
                    newFile = Console.ReadLine();

                    if(newFile != ""){
                        try{
                            File.Move(Path.Combine(CSVPATH, file),Path.Combine(CSVPATH, newFile) + ".csv.old", false);

                            Console.WriteLine($"File successfully archived as {newFile}.csv.old");
                            Console.ReadKey();
                            ok = false;

                        }catch(Exception ex){
                            Console.WriteLine("\nThere was a problem...\n");
                            Console.WriteLine($"ERROR --> {ex.Message}\nPress a key...");
                            Console.ReadKey();
                        }
                    }else{
                        Console.WriteLine("File name can't be empty...\nPress a key...");
                        Console.ReadKey();
                    }

                }

            break;
        }
    }
}

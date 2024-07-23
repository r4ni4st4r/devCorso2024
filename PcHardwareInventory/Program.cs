using Spectre.Console;
using Newtonsoft.Json;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

class Program{
    const string CSVPATH = @".\data\csvFiles";
    const string CATPATH = @".\data\productsCategories"; 
    const string PROPERTIESFILE = "data.txt";
    static List<string> propertiesDataType = new List<string>();
    static int cpuFileName = GetFileName(Path.Combine(CATPATH, "cpu")); 
    static int videoCardFileName = GetFileName(Path.Combine(CATPATH, "video card"));
    static int ramFileName = GetFileName(Path.Combine(CATPATH, "ram"));
    static int mbFileName = GetFileName(Path.Combine(CATPATH, "mother board"));
    static string brand = "";
    static string model = "";
    static int mhz = new int();
    static int socket = new int();


    static void Main(string[] args){
        string selection = "";
        List<string> categories = CategoryOrPropertiesList();
        List<string> csvFiles = CsvFilesList(CSVPATH);
        
        string[] addProductSelection = new string[] {"Insert manually", "Insert from .csv file", "Back",};
        string[] manageWarehouseSelection = new string[] {"View Products", "Add Product", "Remove Product", /* "Add Product Category",
                                                "Remove Product Category",*/ "Exit",};

        while(true){
            Console.Clear();
            selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\n[red]Manage Warehouse[/]")
                .PageSize(manageWarehouseSelection.Count() + 1)
                .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                .AddChoices(manageWarehouseSelection));
            
            switch(selection){
                case "View Products":
                    if(categories.Count > 0){
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

                case "Add Product":
                    selection = AnsiConsole.Prompt(
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
                                        InsertProductManually(CategoryOrPropertiesList(selection), selection);
                                        break;
                                }
                            }else{
                                Console.WriteLine("You have to add a Product Category\n\nPress a key...");
                                Console.ReadKey();
                            }
                            categories.Remove("Back");
                            break; 
                        case "Insert from .csv file":

                            List<string> CsvList = GetCsvList(CSVPATH);
                            
                            if(CsvList.Count > 0){
                                CsvList.Add("Back");

                                selection = AnsiConsole.Prompt(
                                                new SelectionPrompt<string>()
                                                .Title("\n[red]Select .csv file to load[/]")
                                                .PageSize(CsvList.Count + 1)
                                                .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                                .AddChoices(CsvList));

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

                case "Remove Product":
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
                                RemoveProduct(selection);
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

    private static List<string> CsvFilesList(string path){
        List<string> TempCsv = new List<string>(Directory.GetFiles(path)); 

        if(TempCsv.Count > 0){
            for(int i = 0; i < TempCsv.Count; i++){
                TempCsv[i] = TempCsv[i].Remove(0,CSVPATH.Length+1);
            }

            return TempCsv;
        }else
            return new List<string>();
    }

    private static List<string> CategoryOrPropertiesList([Optional] string category){
        if(category == null){

            List<string> productsCategories = new List<string>(Directory.GetDirectories(CATPATH)); 

            if(productsCategories.Count > 0){
                for(int i = 0; i < productsCategories.Count; i++){
                    productsCategories[i] = productsCategories[i].Remove(0, CATPATH.Length+1);
                }
                
                return productsCategories;
            }else
                return new List<string>();

        }else{
            List<string> properties = new List<string>();
            string line = "";
            
            using(StreamReader sr = new StreamReader(Path.Combine(CATPATH, category, PROPERTIESFILE))){
                while((line = sr.ReadLine())!= null){
                    propertiesDataType.Add(line.Split(",").Last());
                    properties.Add(line.Split(",").First());
                }
            }

            return properties;
        }
    }

    private static void InsertProductManually(List<string> properties, string item){
        bool success;
        
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

                using (StreamWriter sw = new StreamWriter(path)){ 
                    sw.Write(JsonConvert.SerializeObject(new {brand, model, mhz}, Formatting.Indented));
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
        }
    }

    private static void ViewProducts(string category){
        Table viewTable = new Table();
        
        List<string> filesList = new List<string>(Directory.GetFiles(Path.Combine(CATPATH, category)));

        switch(category){
            case "cpu":
                if(filesList.Count!=0){
                    viewTable.AddColumns("Brand", "Model", "Mhz");

                    foreach(string s in filesList){

                        string tmp = Path.GetFileName(s);
                        Console.WriteLine("check string --->" + tmp);
                        Console.ReadKey();

                        if(!s.Contains("data.txt")&&!s.Contains("fileName.txt")){

                            string json = File.ReadAllText(s);

                            dynamic obj = JsonConvert.DeserializeObject(json);

                            string s1 = obj.brand;
                            string s2 = obj.model;
                            string s3 = obj.mhz.ToString();
        
                            viewTable.AddRow(s1,s2,s3);
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

                        string tmp = Path.GetFileName(s);
                        Console.WriteLine("check string --->" + tmp);
                        Console.ReadKey();

                        if(!s.Contains("data.txt")&&!s.Contains("fileName.txt")){

                            string json = File.ReadAllText(s);

                            dynamic obj = JsonConvert.DeserializeObject(json);

                            string s1 = obj.brand;
                            string s2 = obj.model;
                            string s3 = obj.socket;
        
                            viewTable.AddRow(s1,s2,s3);
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
                AnsiConsole.WriteLine("Not implemented yet...");
                Console.ReadKey();
                break;
            case "video card":
                AnsiConsole.WriteLine("Not implemented yet...");
                Console.ReadKey();
                break;
        }
    }

    private static int GetFileName(string path) {

        string tmp = File.ReadAllText(Path.Combine(path, "fileName.txt"));

        return Convert.ToInt32(tmp);
    }

    private static void RemoveProduct(string category){
        List<string> filesList = new List<string>(Directory.GetFiles(Path.Combine(CATPATH, category)));
        List<string> menuList = new List<string>();

        switch(category){
            case "cpu":

                foreach(string s in filesList){

                    if(!s.Contains("data.txt")&&!s.Contains("fileName.txt")){

                        string tmp = Path.GetFileName(s)+"   ->  ";
                        string json = File.ReadAllText(s);

                        dynamic obj = JsonConvert.DeserializeObject(json);

                        string s1 = obj.brand;
                        tmp += " Brand: "+s1;
                        string s2 = obj.model;
                        tmp += " Model: "+s2;
                        string s3 = obj.mhz.ToString();
                        tmp += " Mhz: "+s3;

                        menuList.Add(tmp);

                    }
                }
                if(menuList.Count > 0){
                    menuList.Add("Back");
                    string selection = AnsiConsole.Prompt(
                                        new SelectionPrompt<string>()
                                        .Title("\n[red]Select Product to remove[/]")
                                        .PageSize(menuList.Count + 1)
                                        .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                        .AddChoices(menuList));

                    switch(selection){
                        case "Back":
                            break;
                        default:
                            string tmp = selection.Remove(9,selection.Length-9).TrimEnd();
                            
                            File.Delete(Path.Combine(CATPATH, category, tmp));

                            break;
                    }
                }else{
                    Console.WriteLine("CPU category is empty!!!\n\nPress a key...");
                    Console.ReadKey();
                }
                menuList.Remove("Back");


                AnsiConsole.WriteLine("Premi un tasto per continuare...");

                Console.ReadKey();

                break;
            case "mother board":
                foreach(string s in filesList){

                    if(!s.Contains("data.txt")&&!s.Contains("fileName.txt")){

                        string tmp = Path.GetFileName(s) + "   ->  ";
                        string json = File.ReadAllText(s);

                        dynamic obj = JsonConvert.DeserializeObject(json);

                        string s1 = obj.brand;
                        tmp += " Brand: "+s1;
                        string s2 = obj.model;
                        tmp += " Model: "+s2;
                        string s3 = obj.socket.ToString();
                        tmp += " Socket: "+s3;

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
                            string tmp = selection.Remove(9,selection.Length-9).TrimEnd();
                            
                            File.Delete(Path.Combine(CATPATH, category, tmp));

                            break;
                    }
                }else{
                    Console.WriteLine("Motherboard category is empty!!!\n\nPress a key...");
                    Console.ReadKey();
                }
                menuList.Remove("Back");


                AnsiConsole.WriteLine("Premi un tasto per continuare...");

                Console.ReadKey();
                break;
            case "ram":
                AnsiConsole.WriteLine("Not implemented yet...");
                Console.ReadKey();
                break;
            case "video card":
                AnsiConsole.WriteLine("Not implemented yet...");
                Console.ReadKey();
                break;
        }
    }

    private static List<string> GetCsvList(string path){
        List<string> csvFiles = new List<string>(); 

        foreach(string s in Directory.GetFiles(path)){
            if(s.EndsWith(".csv"))
                csvFiles.Add(Path.GetFileName(s));
        }

        return csvFiles;
    }

    private static void InsertProductCsv(string file){
        bool success;

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

        switch(question){
            case "Yes":
                File.Delete(Path.Combine(CSVPATH, file));

                Console.WriteLine($"{file} is been deleted\n\nPress a key...");
                Console.ReadKey();
            break;
            case "No":
                bool ok = true;
                string newFile;
                while(ok){
                    Console.Clear();
                    Console.WriteLine("Please enter a filename to archive the .csv: ");
                    newFile= Console.ReadLine();

                    try{
                        File.Move(Path.Combine(CSVPATH, file),Path.Combine(CSVPATH, newFile) + ".csv.old", false);

                        Console.WriteLine($"File successfully archived as {newFile}.csv.old");
                        Console.ReadKey();
                        ok = false;

                    }catch(Exception e){
                        Console.WriteLine("\nThere was a problem...\n");
                        Console.WriteLine($"ERROR --> {e.Message}\nPress a key...");
                        Console.ReadKey();
                    }

                }
            break;
        }
    }
}

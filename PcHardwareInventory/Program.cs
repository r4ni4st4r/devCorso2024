using Spectre.Console;
using Newtonsoft.Json;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.CompilerServices;
using System.ComponentModel;

class Program{
    const string CSVPATH = @".\data\csvFiles";
    const string CATPATH = @".\data\productsCategories"; 
    const string PROPERTIESFILE = "data.txt";
    static List<string> propertiesDataType = new List<string>();
    static List<int> cpuFileNames = GetFilesNameList(Path.Combine(CATPATH, "cpu")); 


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
                .Title("\nManage Warehouse")
                .PageSize(manageWarehouseSelection.Length + 1)
                .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                .AddChoices(manageWarehouseSelection));
            
            switch(selection){
                case "View Products":
                    if(categories.Count > 0){
                        categories.Add("Back");
                        selection = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("\nSelect a category to visualize")
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
                        .Title("\nProduct isertion")
                        .PageSize(addProductSelection.Length + 1)
                        .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                        .AddChoices(addProductSelection));

                    switch(selection){
                        case "Insert manually":
                            if(categories.Count > 0){
                                categories.Add("Back");
                                selection = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("\nSelect Product Category")
                                    .PageSize(categories.Count + 1)
                                    .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                    .AddChoices(categories));

                                switch(selection){
                                    case "Back":
                                        break;
                                    default:
                                        InsertItem(CategoryOrPropertiesList(selection), selection);
                                        break;
                                }
                            }else{
                                Console.WriteLine("You have to add a Product Category\n\nPress a key...");
                                Console.ReadKey();
                            }
                            categories.Remove("Back");
                            break; 
                        case "Insert from .csv file":
                            
                            break; 
                        case "Back":

                            break; 
                    }

                    break;

                case "Remove Product":
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

    private static void InsertItem(List<string> properties, string item){
        bool success;

        switch(item){
        
            case "cpu":
                string brand = "";
                string model = "";
                int mhz = new int();

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

                string file = ReturnTheNextFileName(cpuFileNames).ToString();

                string path = Path.Combine(CATPATH, item, file+ ".json");

                File.Create(path).Close();

                using (StreamWriter sw = new StreamWriter(path)){ 
                    sw.Write(JsonConvert.SerializeObject(new {brand, model, mhz}));
                }

                break;
        }
    }

    private static void ViewProducts(string category){
        Table viewTable = new Table();
        List<int> tem = cpuFileNames;
        switch(category){
            case "cpu":
                viewTable.AddColumns("Brand", "Model", "Mhz");
                Console.WriteLine(tem[0]);
                break;
        }
    }

    private static List<int> GetFilesNameList(string path) {
        List<int> fileListInt = new List<int>();    
        List<string> files = new List<string>(Directory.GetFiles(path));

        foreach(string file in files) {
            if(file != path+"data.txt"){
                file.Remove(0,path.Length);
                Console.WriteLine(file);
                Console.ReadKey();
            }
        }



        return fileListInt;    
    }

    private static int ReturnTheNextFileName(List<int> fileNames){
        int max=0;
        foreach(int name in fileNames){
            if(name>max){
                max=name;
            }
        }
        
        if(max==0){
            return 0;
        }else
            return max+1;
    }
}

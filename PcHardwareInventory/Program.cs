using Spectre.Console;
using Newtonsoft.Json;
using System.Collections;
using System.Runtime.InteropServices;

class Program{
    const string CSVPATH = @".\data\csvFiles";
    const string CATPATH = @".\data\productsCategories"; 
    const string PROPFILE = "data.txt";


    static void Main(string[] args){
        string selection = "";
        List<string> categories = CategoryList(CATPATH);
        List<string> csvFiles = CsvFilesList(CSVPATH);

        while(true){
            Console.Clear();
            selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\nManage Warehouse")
                .PageSize(14)
                .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                .AddChoices(new string[] {
                    "View Products", "Add Product", "Remove Product", /* "Add Product Category",
                    "Remove Product Category",*/ "Exit",
                }));
            
            switch(selection){
                case "View Products":
                    break;

                case "Add Product":
                    selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\nProduct isertion")
                        .PageSize(14)
                        .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                        .AddChoices(new string[] {
                            "Insert manually", "Insert from .csv file", "Back",
                        }));

                    switch(selection){
                        case "Insert manually":
                            if(categories.Count > 0){
                                categories.Add("Back");
                                selection = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("\nSelect Product Category")
                                    .PageSize(14)
                                    .MoreChoicesText("[grey](Move up and down to make your choice)[/]")
                                    .AddChoices(categories
                                    
                                ));
                                switch(selection){
                                    case "Back":
                                        break;
                                    default:
                                        InsertProductOrCategory(selection, true);
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

    private static List<string> CategoryList(string path){
        List<string> productsCategories = new List<string>(Directory.GetDirectories(CATPATH)); 

        if(productsCategories.Count > 0){
            for(int i = 0; i < productsCategories.Count; i++){
                productsCategories[i] = productsCategories[i].Remove(0, CATPATH.Length+1);
            }
            
            return productsCategories;
        }else
            return new List<string>();
    }

    private static void InsertProductOrCategory(string category, bool product){
        List<string> properties = new List<string>();
        string line = "";

        if(product){
            using(StreamReader sr = new StreamReader(CATPATH + "\"" + category + "\"" + PROPFILE)){
                while((line = sr.ReadLine())!= null )
                    properties.Add(line);
            }

        }
    }


}

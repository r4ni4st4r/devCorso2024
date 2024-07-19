using Newtonsoft.Json;

class Program{
    static void Main(string[] args){
        string path =@"test.json";
        if(!File.Exists(path)){
            File.Create(path).Close();
            File.WriteAllText(path, "[\n");
        }else{
            File.WriteAllText(path, "[\n");
        }

        while(true){
            Console.WriteLine("Inserisci nome e prezzo");
            string nome = Console.ReadLine().Trim();

            //File.AppendAllText(path, JsonConvert.SerializeObject(new { nome, prezzo }));

            
            if(decimal.TryParse(Console.ReadLine(), out decimal prezzo)){
                File.AppendAllText(path, JsonConvert.SerializeObject(new {nome, prezzo}));
                
            }else{
                Console.WriteLine("Prezzo non valido, riprova...\nPremi un tasto...");
                Console.ReadKey();
            }

            Console.WriteLine("Vuoi inserire un altro prodotto? (s/n)");
            if(Console.ReadLine().Trim().ToLower() == "n"){
                break;
            }
            File.AppendAllText(path, ",\n");
        }

        File.AppendAllText(path,"\n]");
    }
}
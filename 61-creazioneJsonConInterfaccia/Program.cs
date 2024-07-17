using Newtonsoft.Json;

class Program{
    static void Main(string[]args ){
        string path =@"test.json";
        File.Create(path).Close();
        File.AppendAllText(path, "[\n");

        while(true){
            Console.WriteLine("Inserisci nome e prezzo");
            string nome = Console.ReadLine();
            string prezzo = Console.ReadLine();

            File.AppendAllText(path, JsonConvert.SerializeObject(new { nome, prezzo }) + "");

            Console.WriteLine("Vuoi inserire un altro prodotto? (s/n)");
            string risposta = Console.ReadLine();
            
            if(risposta == "n"){
                break;
            }
            File.AppendAllText(path, ",\n");
        }

        File.AppendAllText(path,"\n]");
    }
}



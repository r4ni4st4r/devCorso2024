using Newtonsoft.Json;

class Program{
    static void Main(string[]args ){
        string path =@"test.json";
        if(!File.Exists(path)){
            File.Create(path).Close();
            File.AppendAllText(path, "[\n");
        }

        while(true){
            Console.WriteLine("Inserisci nome e prezzo");
            string nome = Console.ReadLine().Trim();
            int prezzo;

            File.AppendAllText(path, JsonConvert.SerializeObject(new { nome, prezzo }));

            Console.WriteLine("Vuoi inserire un altro prodotto? (s/n)");
            if(decimal.TryParse(Console.ReadLine(), out decimal prezzo)){
                File.AppendAllText(path, JsonConvert.SerializeObject(new {nome, prezzo = prezzo.ToString()})+ "\n");
                if(Console.ReadLine().Trim().ToLower() != "s"){
                    break;
                }
            }else{
                Console.WriteLine("Prezzo non valido, riprova...\nPremi un tasto...");
                Console.ReadKey();
            }

            File.AppendAllText(path, ",\n");
        }

        File.AppendAllText(path,"\n]");
    }
}
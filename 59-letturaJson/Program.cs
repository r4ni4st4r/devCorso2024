using Newtonsoft.Json;

class Program{
    static void Main(string[] args){
        const string PATHJSON = @"test.json";
        const string PATHCSV = @"test.csv";

        if(!File.Exists(PATHJSON))
            File.Create(PATHJSON).Close();
        
        File.Create(PATHCSV).Close();

        string json = File.ReadAllText(PATHJSON);
        dynamic obj = JsonConvert.DeserializeObject(json);

        Console.WriteLine(json);
        Console.ReadKey();

        File.AppendAllText(PATHCSV, "nome,cognome,eta,citta,via\n");

        Console.WriteLine(obj.Count.ToString());
        Console.ReadKey();
        
        for(int i=0; i < obj.Count; i++){
            File.AppendAllText(PATHCSV,$"{obj[i].nome},{obj[i].cognome},{obj[i].eta},{obj[i].indirizzo.citta},{obj[i].indirizzo.via}\n");
        }
        //Console.WriteLine($"nome: {obj[0].nome} cognome: {obj[0].cognome} eta: {obj[0].eta} citta: {obj[0].indirizzo.citta} via: {obj[0].indirizzo.via}");
        //Console.WriteLine($"nome: {obj[1].nome} cognome: {obj[1].cognome} eta: {obj[1].eta} citta: {obj[1].indirizzo.citta} via: {obj[1].indirizzo.via}");
    }
}
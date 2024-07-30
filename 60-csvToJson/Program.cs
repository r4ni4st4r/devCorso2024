using Newtonsoft.Json;

class Program{
    static void Main(string[] srgs){
        string path = @"prodotti.csv";
        string[] lines = File.ReadAllLines(path); 
        string[][] prodotti = new string [lines.Length][];

        for(int i = 0; i < lines.Length; i++){
            prodotti[i] = lines[i].Split(",");
        }
        for(int i = 0; i< prodotti.Length; i++){
            if(i!=0){
                string path2 = @""+prodotti[i][0]+".json";
                File.Create(path2).Close();
                File.AppendAllText(path2, JsonConvert.SerializeObject(new{nome = prodotti[i][0], prezzo = prodotti[i][1]}));
            }
        }
    }
}

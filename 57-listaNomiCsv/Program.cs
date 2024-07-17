using System.ComponentModel;

class Program{
    static void Main(string[] args){
        string path = @"test.csv";
        File.Create(path).Close();

        while(true){
            Console.WriteLine("Inserisci nome cognome ed età");

            string nome = Console.ReadLine();
            string cognome = Console.ReadLine();
            string eta = Console.ReadLine();
            using (StreamWriter sw = new StreamWriter(path)){
                sw.WriteLine(nome + "," + cognome + "," + eta + "\n");
            }
            Console.WriteLine("Vuoi inserire un altro nome? (s/n)");
            string risposta =  Console.ReadLine();
            
            if(risposta == "n"){
                break;
            }
        }

    }
}

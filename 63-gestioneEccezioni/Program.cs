using System.Diagnostics.Contracts;

class Program{
    static void Main(string[] args){
        int[] numeri = {1,2,3};

        try{
            string contenuto = File.ReadAllText("file.txt");
            Console.WriteLine(contenuto);
        }
        catch(Exception e){
            Console.WriteLine("***************************************************");
            Console.WriteLine("Il file non esiste");
            Console.WriteLine($"ERRORE NON TRATTATO: {e.Message}");
            Console.WriteLine("***************************************************");
        }


        try{
            Console.WriteLine(numeri[3]);
        }
        catch(Exception e){
            Console.WriteLine("***************************************************");
            Console.WriteLine("Indice non valido");
            Console.WriteLine($"ERRORE NON TRATTATO: {e.Message}");
            Console.WriteLine("***************************************************");
            //return;
        }finally{
            
            //Console.WriteLine("FINE DEL PROGRAMMA");
        }

        string name = null;

        try{
            Console.WriteLine(name.Length);
        }
        catch(Exception e){
            Console.WriteLine("***************************************************");
            Console.WriteLine("Nome non valido");
            Console.WriteLine($"ERRORE NON TRATTATO: {e.Message}");
            Console.WriteLine("***************************************************");
            
        }finally{
            
            //Console.WriteLine("FINE DEL PROGRAMMA");
        }

        try{
            int[] numeriMax = new int[int.MaxValue];
        }
        catch(Exception e){
            Console.WriteLine("***************************************************");
            Console.WriteLine("Memoria insufficiente");
            Console.WriteLine($"ERRORE NON TRATTATO: {e.Message}");
            Console.WriteLine("***************************************************");
        }finally{
            
            Console.WriteLine("FINE DEL PROGRAMMA");
        }

        try{
            int numero = int.Parse("ciao"); 
        }
        catch(Exception e){
            Console.WriteLine("***************************************************");
            Console.WriteLine("numero non valido");
            Console.WriteLine($"ERRORE NON TRATTATO: {e.Message}");
            Console.WriteLine("***************************************************");
        }finally{
            
            Console.WriteLine("FINE DEL PROGRAMMA");
        }

        try{
            int numero = int.Parse(null); 
        }
        catch(Exception e){
            Console.WriteLine("***************************************************");
            Console.WriteLine("numero null");
            Console.WriteLine($"ERRORE NON TRATTATO: {e.Message}");
            Console.WriteLine("***************************************************");
        }finally{
            
            Console.WriteLine("FINE DEL PROGRAMMA");
        }

        try{
            int numero = int.Parse("1000000000000"); 
        }
        catch(Exception e){
            Console.WriteLine("***************************************************");
            Console.WriteLine("numero troppo grande");
            Console.WriteLine($"ERRORE NON TRATTATO: {e.Message}");
            Console.WriteLine($"ERRORE NON TRATTATO: {e.HResult}");
            Console.WriteLine($"DATI AGGIUNTIVI ERRORE: {e.Data}");
            Console.WriteLine("***************************************************");
        }finally{
            
            Console.WriteLine("FINE DEL PROGRAMMA");
        }

        try{
            int zero = 0;
            int numero = 1/zero; 
        }
        catch(Exception e){
            Console.WriteLine("***************************************************\n\n\n");
            Console.WriteLine("divisione per 0");
            Console.WriteLine($"ERRORE NON TRATTATO: {e.Message}");
            Console.WriteLine($"CODICE ERRORE: {e.HResult}");
            Console.WriteLine($"DATI AGGIUNTIVI ERRORE: {e.Data}");
            Console.WriteLine("\n\n\n***************************************************");
        }finally{
            
            Console.WriteLine("FINE DEL PROGRAMMA");
        }



        try{
            StackOverflow();
        }
        catch(Exception e){
            Console.WriteLine("***************************************************");
            Console.WriteLine("StackOverflow");
            Console.WriteLine($"ERRORE NON TRATTATO: {e.Message}");
            Console.WriteLine("***************************************************");
            return;
        }finally{
            
            Console.WriteLine("FINE DEL PROGRAMMA");
        }

        static void StackOverflow(){
            StackOverflow();
        }
    }
    
}
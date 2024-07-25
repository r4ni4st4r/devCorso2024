class   Program{
    public void Main(string[] args){
        StampaMessaggio("La somma è: "+ Somma(3,5));

        int risultatoOut;
        
        int[] numeri = {3,1,4,1,5,9,2,6,5,3,5};
        
        (int minimo, int massimo) risultato = CalcolaMinMax(numeri);

        Console.WriteLine($"Valore massimo: {risultato.massimo}");
        Console.WriteLine($"Valore minimo: {risultato.minimo}");

        int? risultato2 = Dividi(10, 2);

        if(risultato2.HasValue){
            Console.WriteLine($"Il risultato è {risultato2}");
        }else{
            Console.WriteLine($"Divisione per zero");
        }

        Saluta("Pippo");

        SommaOut(2,3,out risultatoOut);

        Console.WriteLine($"valore modificato di risultatoOut = {risultatoOut}");

        string nome = LeggiStringa("Inserisci il tuo nome: ");

        int eta = LeggiIntero("Inserisci la tua eta: ");

        int etaPro = LeggiInteroPro("Inserisci la tua eta: ");

    }

    static (int, int) CalcolaMinMax(int[] numeri){
        int minimo = numeri.Min();
        int massimo = numeri.Max();
        
        return (minimo, massimo);
    }


    public void StampaMessaggio(string messaggio){
        Console.WriteLine(messaggio);
    }


    public int Somma(int a, int b){
        return a + b;
    } 

    static int? Dividi(int a, int b){

        if(b == 0){         //divisione per zero

            return null;    //valore opzionale
        }

        return a/b;         //divisione
    }

    //parametro di default
    static void Saluta(string nome, string saluto = "Ciao"){    // se richiamo la funzione solo con un parametro userà il secondo di 
        Console.WriteLine($"{saluto}, {nome}!");                // default in questo caso "Ciao"
    }

                                                                // ***Puntatori***
    static void SommaOut(int a, int b, out int risultato){      // keyword "out" must be modified
                                                                // keyword "in"  cannot be modified
        risultato = a+b;                                        // keyword "ref" may be modified
    }

    static int LeggiIntero(string messaggio){
        Console.WriteLine(messaggio);

        return Convert.ToInt32(Console.ReadLine());
    }
    static string LeggiStringa(string messaggio){
        Console.WriteLine(messaggio);

        return Console.ReadLine();
    }

    static int LeggiInteroPro(string messaggio){
        int valore;
        bool successo;
        do{
            Console.WriteLine(messaggio);
            successo = int.TryParse(Console.ReadLine(), out valore);

            if(!successo){
                Console.WriteLine("Inserimento non valido, riprova");
            }
        }while(!successo);
        return valore;
    }
}
// versione con console clear in modo da mantenere il menu nella stessa posizione dopo ogni scelta 

List<string> partecipanti = new List<string>();

Random random = new Random();
string nome;
int scelta;
int split;

do
{
    Console.Clear();
    Console.WriteLine("1. Inserisci partecipante");
    Console.WriteLine("2. Visualizza partecipanti");
    Console.WriteLine("3. Ordina partecipanti");
    Console.WriteLine("4. Cerca partecante");
    Console.WriteLine("5. Elimina partecante");
    Console.WriteLine("6. Modifica partecipante");
    Console.WriteLine("7. Divisione in squadre mio");
    Console.WriteLine("8. Divisione in squadre method 2 no random");
    Console.WriteLine("9. Divisione in squadre method 3");
    Console.WriteLine("10. Esci");
    Console.Write("Scelta: ");
    
    int ciao;

    scelta = Convert.ToInt32(Console.ReadLine());

    switch (scelta)
    {
        case 1:
            Console.Write("Nome partecipante: ");
            nome = Console.ReadLine();
            partecipanti.Add(nome);
            break;
        case 2: 
            Console.WriteLine("Elenco partecipanti:");
            foreach (string partecipante in partecipanti)
            {
                Console.WriteLine(partecipante);
            }
            break;
        case 3:
            Console.WriteLine("1. Ordine crescente");
            Console.WriteLine("2. Ordine decrescente");
            Console.Write("Scelta: ");

            int ordine = Convert.ToInt32(Console.ReadLine());
            
            if (ordine == 1)
            {
                partecipanti.Sort();
            }
            else if (ordine == 2)
            {
                partecipanti.Sort();
                partecipanti.Reverse();
            }
            else
            {
                Console.WriteLine("Scelta non valida");
            }
            break;
        case 4:
            Console.Write("Nome partecipante: ");
            nome = Console.ReadLine();
            if (partecipanti.Contains(nome))
            {
                Console.WriteLine("Il partecipante è presente nella lista");
            }
            else
            {
                Console.WriteLine("Il partecipante non è presente nella lista");
            }
            break;
        case 5:
            Console.Write("Nome partecipante: ");

            nome = Console.ReadLine();
            
            if (partecipanti.Contains(nome))
            {
                partecipanti.Remove(nome);
                Console.WriteLine("Il partecipante è stato eliminato dalla lista");
            }
            else
            {
                Console.WriteLine("Il partecipante non è presente nella lista");
            }
            break;
        case 6:
            Console.Write("Nome partecipante: ");
            nome = Console.ReadLine();
            if (partecipanti.Contains(nome))
            {
                Console.Write("Nuovo nome: ");
                string nuovoNome = Console.ReadLine();
                int indice = partecipanti.IndexOf(nome);
                partecipanti[indice] = nuovoNome;
                Console.WriteLine("Il partecipante è stato modificato nella lista");
            }
            else
            {
                Console.WriteLine("Il partecipante non è presente nella lista");
            }
            break;
        case 7:
            List<string> squadra1 = new List<string>();
            List<string> squadra2 = new List<string>();

            foreach (string s in partecipanti){

                if(random.Next(0, 2) == 0){
                    if(partecipanti.Count%2 == 0){
                        if(squadra1.Count < partecipanti.Count/2)
                            squadra1.Add(s);
                        else
                            squadra2.Add(s);
                    }else{
                        if(squadra1.Count <= partecipanti.Count/2)
                            squadra1.Add(s);
                        else
                            squadra2.Add(s);
                    }
                }else{
                    if(partecipanti.Count%2 == 0){
                        if(squadra2.Count < partecipanti.Count/2)
                            squadra2.Add(s);
                        else
                            squadra1.Add(s); 
                    }else{
                        if(squadra2.Count <= partecipanti.Count/2)
                            squadra2.Add(s);
                        else
                            squadra1.Add(s);
                    }
                }

            }

            Console.WriteLine("Elenco squadra 1:");

            foreach (string s1 in squadra1)
            {
                Console.WriteLine(s1);
            }

            Console.WriteLine("Elenco squadra 2:");

            foreach (string s2 in squadra2)
            {
                Console.WriteLine(s2);
            }

            break;

        case 8:
            split = partecipanti.Count/2;
            List<string> squadra3 = partecipanti.GetRange(0, split);
            List<string> squadra4 = partecipanti.GetRange(split, partecipanti.Count - split);

            Console.WriteLine("Squadra 1:");

            foreach(string s in squadra3)
                Console.WriteLine(s);

            Console.WriteLine("Squadra 2:");

            foreach(string s in squadra4)
                Console.WriteLine(s);

            break;

        case 9:
            //crea le due squadre
            split = partecipanti.Count/2;
            List<string> squadra1Random = partecipanti.GetRange(0, split);
            List<string> squadra2Random = partecipanti.GetRange(split, partecipanti.Count - split);
            
            while(partecipanti.Count > 0){
                int index = random.Next(partecipanti.Count);
                string partecipante = partecipanti[index];

                partecipanti.RemoveAt(index);

                if(squadra1Random.Count<squadra2Random.Count){
                    squadra1Random.Add(partecipante);
                }else{
                    squadra2Random.Add(partecipante);
                }
            }

            Console.WriteLine("Elenco squadra 1:");

            foreach (string s1 in squadra1Random)
            {
                Console.WriteLine(s1);
            }

            Console.WriteLine("Elenco squadra 2:");

            foreach (string s2 in squadra2Random)
            {
                Console.WriteLine(s2);
            }
            
            partecipanti.Clear();
            break;

        case 10:
            Console.WriteLine("Arrivederci!");
            break;
        default:
            Console.WriteLine("Scelta non valida");
            break;
    }
    if (scelta != 10)
    {
        Console.WriteLine("Premi un tasto per continuare...");
        Console.ReadKey();
    }
} while (scelta != 10);
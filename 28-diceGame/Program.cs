int puntiUmano = 100; // Entrambi i giocatori iniziano con 100 punti
int puntiPc = 100;
Random random = new Random();

while (puntiUmano > 0 && puntiPc > 0)

// ogni turno, ciascuno lancia due dadi

{
    // Lancio dadi umano
    int dado1Umano = random.Next(1, 7);
    int dado2Umano = random.Next(1, 7);
    int sommaUmano = dado1Umano + dado2Umano;

    // Lancio dadi computer
    int dado1Pc = random.Next(1, 7);
    int dado2Pc = random.Next(1, 7);
    int sommaPc = dado1Pc + dado2Pc;

    Console.WriteLine($"Umano lancia i dadi: {dado1Umano} e {dado2Umano} (Totale: {sommaUmano})");
    Console.WriteLine($"PC lancia i dadi: {dado1Pc} e {dado2Pc} (Totale: {sommaPc})");

    // Calcola la differenza e aggiorna i punteggi
    if (sommaUmano > sommaPc)
    {
        puntiPc -= sommaUmano - sommaPc;
        Console.WriteLine($"Il PC perde {sommaUmano - sommaPc} punti.");
    }
    else if (sommaPc > sommaUmano)
    {
        puntiUmano -= sommaPc - sommaUmano;
        Console.WriteLine($"L'umano perde {sommaPc - sommaUmano} punti.");
    }
    else
    {
        Console.WriteLine("Parità in questo turno.");
    }

    Console.WriteLine($"Punti Umano: {puntiUmano}, Punti PC: {puntiPc}");
    Console.WriteLine("Premi un tasto per il prossimo turno...");
    Console.ReadKey();
}

if (puntiUmano <= 0)
{
    Console.WriteLine("L'umano ha perso!");
}
else
{
    Console.WriteLine("Il PC ha perso!");
}
using Spectre.Console;

﻿int puntiUmano = 100; // Entrambi i giocatori iniziano con 100 punti
int puntiPc = 100;

Random random = new Random();

int dado1Umano;
int dado2Umano;
int sommaUmano;
int dado1Pc;
int dado2Pc;
int sommaPc;

var table = new Table();

table.AddColumn("Punti Umano");
table.AddColumn("Punti PC");
table.AddColumn("Umano lancia i dadi");
table.AddColumn("PC lancia i dadi");


while (puntiUmano > 0 && puntiPc > 0){
    // Lancio dadi umano
    dado1Umano = random.Next(1, 7);
    dado2Umano = random.Next(1, 7);
    sommaUmano = dado1Umano + dado2Umano;

    // Lancio dadi computer
    dado1Pc = random.Next(1, 7);
    dado2Pc = random.Next(1, 7);
    sommaPc = dado1Pc + dado2Pc;

    table.AddRow($"Umano lancia i dadi: {dado1Umano} e {dado2Umano} (Totale: {sommaUmano})");
    table.AddRow($"PC lancia i dadi: {dado1Pc} e {dado2Pc} (Totale: {sommaPc})");

    // Calcola la differenza e aggiorna i punteggi
    if (sommaUmano > sommaPc){
        puntiPc -= sommaUmano - sommaPc;
        table.AddRow($"Il PC perde {sommaUmano - sommaPc} punti.");
    }
    else if (sommaPc > sommaUmano){
        puntiUmano -= sommaPc - sommaUmano;
        table.AddRow($"L'umano perde {sommaPc - sommaUmano} punti.");
    }
    else
        table.AddRow("Parità in questo turno.");
    

    table.AddRow($"Punti Umano: {puntiUmano}, Punti PC: {puntiPc}");
    table.AddRow("Premi un tasto per il prossimo turno...");

    AnsiConsole.Write(table);

    Console.ReadKey();
}

if (puntiUmano <= 0){
    Console.WriteLine("L'umano ha perso!");
}
else
    Console.WriteLine("Il PC ha perso!");
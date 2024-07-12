using Spectre.Console;

int puntiUmano = 50; // Entrambi i giocatori iniziano con 100 punti
int puntiPc = 50;

Random random = new Random();

void SalvaStampaPunteggi(bool umanOrPc, int deltaPunti){ //uman win = TRUE
    int maxLines =10;
    string path=@"topTen.txt";
    string[] lines = new string[maxLines];
    lines = File.ReadAllLines(path);
    Table topTable = new Table();
    

    for(int i=0; i<lines.Length && i<maxLines; i++){
        
        var result = lines[i].Substring(lines[i].Length - 3).Trim();

        int tmp = Int32.Parse(result);

        //Console.WriteLine($"{tmp}");
 
        if(deltaPunti >= tmp){
            int j=i;
            while(j<lines.Length && j<maxLines){
                lines[j+1] = lines[j];
            }
            if(umanOrPc){
                lines[i] = $"{i-1} Posizione - Umano ->  {deltaPunti}";
            }else{
                lines[i] = $"{i-1} Posizione - PC ->  {deltaPunti}";
            }
        }
        
    }
    
    topTable.AddColumn("TOP 10 MIGLIORI VITTORIE");

    foreach(string line in lines){
        File.AppendAllText(path, line + "\n");
        topTable.AddRow(line);
    }

    AnsiConsole.Write(topTable);
    
}

int dado1Umano;
int dado2Umano;
int sommaUmano;
int dado1Pc;
int dado2Pc;
int sommaPc;

/*

var table = new Table();

table.AddColumn("Azione");
table.AddColumn("Punti Umano");
table.AddColumn("Punti PC");
table.AddColumn("Situazione");

*/

while (puntiUmano > 0 && puntiPc > 0){

    var table = new Table();

    table.AddColumn("Azione");
    table.AddColumn("Punti Umano");
    table.AddColumn("Punti PC");
    table.AddColumn("Situazione");


    // Lancio dadi umano
    dado1Umano = random.Next(1, 7);
    dado2Umano = random.Next(1, 7);
    sommaUmano = dado1Umano + dado2Umano;

    // Lancio dadi computer
    dado1Pc = random.Next(1, 7);
    dado2Pc = random.Next(1, 7);
    sommaPc = dado1Pc + dado2Pc;

    
    // Calcola la differenza e aggiorna i punteggi
    if (sommaUmano > sommaPc)
        puntiPc -= sommaUmano - sommaPc;
    else if (sommaPc > sommaUmano)
        puntiUmano -= sommaPc - sommaUmano;
    
    
    if(puntiUmano>puntiPc)
        table.AddRow($"Umano lancia i dadi: {dado1Umano} e {dado2Umano} (Totale: {sommaUmano})\nPC lancia i dadi: {dado1Pc} e {dado2Pc} (Totale: {sommaPc})",
                    $"{puntiUmano}", $"{puntiPc}",$"L'umano è in vantaggio di {puntiUmano-puntiPc}");
    else if(puntiUmano<puntiPc)
        table.AddRow($"Umano lancia i dadi: {dado1Umano} e {dado2Umano} (Totale: {sommaUmano})\nPC lancia i dadi: {dado1Pc} e {dado2Pc} (Totale: {sommaPc})",
                    $"{puntiUmano}", $"{puntiPc}",$"Il PC è in vantaggio di {puntiPc-puntiUmano}");
    else
        table.AddRow($"Umano lancia i dadi: {dado1Umano} e {dado2Umano} (Totale: {sommaUmano})\nPC lancia i dadi: {dado1Pc} e {dado2Pc} (Totale: {sommaPc})",
                    $"{puntiUmano}", $"{puntiPc}","La situazione è in parità");

    AnsiConsole.Write(table);

    AnsiConsole.WriteLine("Premi un tasto per il prossimo turno...");

    Console.ReadKey();

    Console.Clear();
}

if (puntiUmano <= 0){
    Console.WriteLine("L'umano ha perso!");

    SalvaStampaPunteggi(false, puntiPc-puntiUmano);
}else{
    Console.WriteLine("Il PC ha perso!");

    SalvaStampaPunteggi(true, puntiUmano-puntiPc);
}
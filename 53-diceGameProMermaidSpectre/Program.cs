using Spectre.Console;

Table topTable = new Table();
int puntiUmano = 50; // Entrambi i giocatori iniziano con 100 punti
int puntiPc = 50;
string path=@"topTen.txt";

Random random = new Random();

void  PrintBestTen(List<string> listToOrder, Table topTen){
    List<string> orderedList = new List<string>();
    int biggest = Convert.ToInt32(listToOrder[0].Split(',').First());
    int id=0;
    int index = 1; 

    topTable.AddColumn("[blue]TOP 10 SCORES!!![/]");

    while(listToOrder.Count!=0){
        for(int i = 1;i < listToOrder.Count;++i){
            if(Convert.ToInt32(listToOrder[i].Split(',').First()) > biggest){
                    biggest = Convert.ToInt32(listToOrder[i].Split(',').First());
                    id = i;
            }
        }
                
        if(index<11){
            if(listToOrder[id].Split(',').Last()=="human")
                topTable.AddRow("[green]Ha vinto il giocatore con " + listToOrder[id].Split(',').First()+ " punti di scarto[/]");
            else
                topTable.AddRow("[red]Ha vinto il computer con " + listToOrder[id].Split(',').First()+ " punti di scarto[/]");
        }

        if(index == 1){
            using (StreamWriter sw = File.CreateText(path)){
                sw.WriteLine(listToOrder[id]);
            }
            index++;	
        }else if(index<11){
            using (StreamWriter sw = File.AppendText(path)){
                sw.WriteLine(listToOrder[id]);
            }	
            index++;
        }

        listToOrder.Remove(listToOrder[id]);

        if(listToOrder.Count!=0){
            biggest = Convert.ToInt32(listToOrder[0].Split(',').First());
            id=0; 
        }else
            break;
    }

    Console.Clear();
    AnsiConsole.Write(topTable);

}


void SalvaStampaPunteggi(bool human, int deltaPunti){ //uman win = TRUE
    
    List<string> topTenList = new List<string>();  

    using (StreamReader sr = new StreamReader(path)){
        string line;
        while ((line = sr.ReadLine()) != null){
            topTenList.Add(line);
        }
    }

    if(human)
        topTenList.Add(deltaPunti.ToString()+","+"human");
    else
        topTenList.Add(deltaPunti.ToString()+","+"cpu");

    PrintBestTen(topTenList, topTable);

    int index=1;
    
    for(int i=topTenList.Count-1;i>=0;i--){
        if(index<=10){
            if(index == 1){
                using (StreamWriter sw = File.CreateText(path)){
                    sw.WriteLine(topTenList[i]);
                }	
            }else{
                using (StreamWriter sw = File.AppendText(path)){
                    sw.WriteLine(topTenList[i]);
                }	
            }
        }else
            break;
        index++;
    }
}

int dado1Umano;
int dado2Umano;
int sommaUmano;
int dado1Pc;
int dado2Pc;
int sommaPc;

while (puntiUmano > 0 && puntiPc > 0){

    var table = new Table();

    table.AddColumn("[blue]Azione[/]");
    table.AddColumn("[green]Punti Umano[/]");
    table.AddColumn("[red]Punti PC[/]");
    table.AddColumn("[blue]Situazione[/]");


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
        table.AddRow($"Umano lancia i dadi: [green]{dado1Umano}[/] e [green]{dado2Umano}[/] (Totale: [green]{sommaUmano}[/])\nPC lancia i dadi: [red]{dado1Pc}[/] e [red]{dado2Pc}[/] (Totale: [red]{sommaPc}[/])",
                    $"[green]{puntiUmano}[/]", $"[red]{puntiPc}[/]",$"L'umano è in vantaggio di [green]{puntiUmano-puntiPc}[/]");
    else if(puntiUmano<puntiPc)
        table.AddRow($"Umano lancia i dadi: [green]{dado1Umano}[/] e [green]{dado2Umano}[/] (Totale: [green]{sommaUmano}[/])\nPC lancia i dadi: [red]{dado1Pc}[/] e [red]{dado2Pc}[/] (Totale: [red]{sommaPc}[/])",
                    $"[green]{puntiUmano}[/]", $"[red]{puntiPc}[/]",$"Il PC è in vantaggio di [red]{puntiPc-puntiUmano}[/]");
    else
        table.AddRow($"Umano lancia i dadi: [green]{dado1Umano}[/] e [green]{dado2Umano}[/] (Totale: [green]{sommaUmano}[/])\nPC lancia i dadi: [red]{dado1Pc}[/] e [red]{dado2Pc}[/] (Totale: [red]{sommaPc}[/])",
                    $"[green]{puntiUmano}[/]", $"[red]{puntiPc}[/]","La situazione è in parità");

    AnsiConsole.Write(table);

    AnsiConsole.WriteLine("Premi un tasto per il prossimo turno...");

    Console.ReadKey();

    Console.Clear();
}

if (puntiUmano <= 0){
    AnsiConsole.MarkupLine("L'umano ha perso!:abacus:");
    SalvaStampaPunteggi(false, puntiPc-puntiUmano);
}else{
    AnsiConsole.MarkupLine("Il PC ha perso!:abacus:");
    SalvaStampaPunteggi(true, puntiUmano-puntiPc);
}

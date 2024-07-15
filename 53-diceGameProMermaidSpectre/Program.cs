using System.Runtime.Serialization.Formatters;
using Spectre.Console;

Table topTable = new Table();
int puntiUmano = 50; // Entrambi i giocatori iniziano con 100 punti
int puntiPc = 50;

Random random = new Random();
/*
List<string>  OrderList(List<string> listToOrder){
    List<string> orderedList = new List<string>();
    int bigger = 0;
    int id=150; 

    for(int i=0; i<listToOrder.Count(); i++){
        for(int j=0; j<listToOrder.Count(); j++){
            if (i!=j){
                if(Convert.ToInt32(listToOrder[i].Split(',').First())>=Convert.ToInt32(listToOrder[j].Split(',').First())){
                    bigger = Convert.ToInt32(listToOrder[i].Split(',').First());
                }
            }
        }

        if(bigger != 150){
            orderedList.Add(listToOrder[i]);
            listToOrder.Remove(listToOrder[i]);
        }
    }
    return orderedList;
}*/


void SalvaStampaPunteggi(bool human, int deltaPunti){ //uman win = TRUE
    string path=@"topTen.txt";
    //Dictionary<int, (int,string)> topTenDictionary = new Dictionary<int, (int,string)>();
    List<string> topTenList = new List<string>();  
    /*
    int index = 1;

    foreach (string line in File.ReadAllLines(path)){
        if(index<11){
            if (line.Contains(","))
                topTenList.Add(line);
                //topTenDictionary.Add(index, (Convert.ToInt32(line.Split(',').First()), line.Split(',').Last()));
        }
        index++; 
    }*/

    using (StreamReader sr = new StreamReader(path)){
        string line;
        while ((line = sr.ReadLine()) != null){
            topTenList.Add(line);
        }
    }

    if(human)
        topTenList.Add(deltaPunti.ToString()+","+"human");
        //topTenDictionary.Add(11,(deltaPunti,"human"));
    else
        topTenList.Add(deltaPunti.ToString()+","+"cpu");
        //topTenDictionary.Add(11,(deltaPunti,"cpu"));

    int index=1;

    //List<string> orderedList = OrderList(topTenList);
    //topTenList.Sort();
    //Console.WriteLine(topTenList.Count);
    
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


    Table topTable = new Table();
    
/*
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
    */
    
    topTable.AddColumn("[blue]ULTIME 10 PARTITE[/]");

    for(int i=topTenList.Count-1;i>=0;i--){
        if(topTenList[i].Split(',').Last()=="human")
            topTable.AddRow("Ha vinto il giocatore con " + topTenList[i].Split(',').First()+ " di scarto");
        else
            topTable.AddRow("Ha vinto il computer con " + topTenList[i].Split(',').First()+ " di scarto");
    }
    Console.Clear();
    AnsiConsole.Write(topTable);
    
    //Console.WriteLine(Convert.ToInt32(topTenList[0].Split(',').First()));
    
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
        table.AddRow($"Umano lancia i dadi: [green]{dado1Umano}[/] e [green]{dado2Umano}[/])\nPC lancia i dadi: [red]{dado1Pc}[/] e [red]{dado2Pc}[/] (Totale: [red]{sommaPc}[/])",
                    $"[green]{puntiUmano}[/]", $"[red]{puntiPc}[/]",$"Il PC è in vantaggio di [red]{puntiPc-puntiUmano}[/]");
    else
        table.AddRow($"Umano lancia i dadi: [green]{dado1Umano}[/] e [green]{dado2Umano}[/])\nPC lancia i dadi: [red]{dado1Pc}[/] e [red]{dado2Pc}[/] (Totale: [red]{sommaPc}[/])",
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
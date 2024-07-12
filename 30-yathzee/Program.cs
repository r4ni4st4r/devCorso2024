/*

- [ ] Creare array per contenere i risultati dei lanci
- [ ] Oggetto random con intervallo tra 1 e 6
- [ ] utilizzare un metodo per visualizzare il primo lancio
- [ ] Selezionare la logica migliore per permettere all'utente di scegliere quali dati tenere
- [ ] utilizzare un metodo per visualizzare il secondo lancio
- [ ] creare la logica di assegnazione del punteggio e visualizzarlo

*/

string nome;
int[] lancio = new int[5];
int points=0;

Random dado = new Random();

Console.Clear();

void StampaLancio(){
    for(int i=0; i<5; i++)
        Console.WriteLine($"{i+1} Lancio = {lancio[i]}");
}

void LanciaDado(){
    for(int i=0; i<5; i++){
        lancio[i] = dado.Next(1,7);
    }
}

LanciaDado();
StampaLancio();

for(int i=0; i<5; i++){
    Console.WriteLine($"Vuoi tenere il {i+1} lacio con valore {lancio[i]}? y");
    nome = Console.ReadLine();
    if(nome !="y"){
        lancio[i] = dado.Next(1,7);
    }
    if(nome == "x"){
        break;
    }
}

StampaLancio();

for (int i=0; i<5; i++) {
    int pointsTemp = 0;

    for(int j=0; j<5; j++){
        if(lancio[i] == lancio[j]){
            pointsTemp++;
        }
    }

    if(pointsTemp >= points)
        points = pointsTemp;
}

Console.WriteLine($"I tuoi punti sono {points-1}");
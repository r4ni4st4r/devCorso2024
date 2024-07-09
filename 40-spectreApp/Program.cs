
using Spectre.Console;

List<string> nomi = new List<string>();

/*
nomi.Add("Mario Rossi");
nomi.Add("Luca Neri");
nomi.Add("Paolo Verdi");

Console.WriteLine(nomi[0]);
*/

var table = new Table();

table.AddColumn("Nome");
table.AddColumn("Soprannome");
table.AddColumn("Cognome");
table.AddColumn("Anno di nascita");


var partecipanti = new Dictionary<(string, string), (string, int)>{   // Dictionary possono avere coppie di valori o come in 
    {("Mario","Pippo"), ("Rossi", 67)},                               // questo caso due valori per il campo Value 
    {("Luca","Pluto"), ("Neri",45)},
    {("Paolo","Paperino"), ("Verdi",56)},  // tra parentisi --> tupla di valori
};

foreach(var partecipante in partecipanti){
    table.AddRow(partecipante.Key.Item1, partecipante.Key.Item2, partecipante.Value.Item1, partecipante.Value.Item2.ToString());
}

AnsiConsole.Write(table);

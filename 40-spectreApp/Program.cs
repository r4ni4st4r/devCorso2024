﻿using Spectre.Console;

// app che mostra un elenco di partecipanti stampati in una tabella
/*
AnsiConsole.Clear();

var table = new Table();
table.AddColumn("Nome");
table.AddColumn("Cognome");
table.AddColumn("Anno di nascita");

table.AddRow("Mario", "Rossi", "1990");
table.AddRow("Luca", "Verdi", "1980");
table.AddRow("Paolo", "Bianchi", "1970");

AnsiConsole.Write(table);
*/

// app che mostra un elenco di partecipanti stampati in una tabella con una lista di nomi

// AnsiConsole.Clear();

var table2 = new Table();
table2.AddColumn("Nome");
table2.AddColumn("Cognome");
table2.AddColumn("Anno di nascita");

// creo una lista di nomi
var nomi = new List<string> { "Mario", "Luca", "Paolo" };

foreach (var nome in nomi)
{
    table2.AddRow(nome, "Rossi", "1990");
}

AnsiConsole.Write(table2);

// versione con nomi ed cognomi diversi

// AnsiConsole.Clear();

var table3 = new Table();
table3.AddColumn("Nome");
table3.AddColumn("Cognome");
table3.AddColumn("Anno di nascita");

// creo una lista di nomi e cognomi
var nomi2 = new List<string> { "Mario", "Luca", "Paolo" };
var cognomi = new List<string> { "Rossi", "Verdi", "Bianchi" };

for (int i = 0; i < nomi2.Count; i++)
{
    table3.AddRow(nomi2[i], cognomi[i], "1990");
}

AnsiConsole.Write(table3);

// versione con nomi, cognomi ed anni di nascita diversi

// AnsiConsole.Clear();

var table4 = new Table();
table4.AddColumn("Nome");
table4.AddColumn("Cognome");
table4.AddColumn("Anno di nascita");

// creo una lista di nomi, cognomi e anni di nascita
var nomi3 = new List<string> { "Mario", "Luca", "Paolo" };
var cognomi2 = new List<string> { "Rossi", "Verdi", "Bianchi" };
var anni = new List<string> { "1990", "1980", "1970" };

for (int i = 0; i < nomi3.Count; i++)
{
    table4.AddRow(nomi3[i], cognomi2[i], anni[i]);
}

AnsiConsole.Write(table4);

// versione con dizionari di nomi, cognomi

// AnsiConsole.Clear();

var table5 = new Table();
table5.AddColumn("Nome");
table5.AddColumn("Cognome");

// creo un dizionario di nomi, cognomi
var partecipanti = new Dictionary<string, string>
{
    { "Mario", "Rossi" },
    { "Luca", "Verdi" },
    { "Paolo", "Bianchi" }
};

foreach (var partecipante in partecipanti)
{
    table5.AddRow(partecipante.Key, partecipante.Value);
}

AnsiConsole.Write(table5);

// versione con dizionari di nomi, cognomi e anni di nascita

// AnsiConsole.Clear();

var table6 = new Table();
table6.AddColumn("Nome");
table6.AddColumn("Cognome");
table6.AddColumn("Anno di nascita");

// creo un dizionario di nomi, cognomi e anni di nascita
// in questo caso uso le parentesi per creare una tupla cioè una coppia di valori
// il vantaggio è che posso avere più di un valore per chiave e posso accedere ai valori tramite il nome
// in questo caso accedo ai valori tramite Item1 e Item2

var partecipanti2 = new Dictionary<string, (string, string)>
{
    { "Mario", ("Rossi", "1990") },
    { "Luca", ("Verdi", "1980") },
    { "Paolo", ("Bianchi", "1970") }
};

foreach (var partecipante in partecipanti2)
{
    table6.AddRow(partecipante.Key, partecipante.Value.Item1, partecipante.Value.Item2);
}

AnsiConsole.Write(table6);

// versione con tipi di dati diversi

var partecipanti3 = new Dictionary<string, (string, int)>
{
    { "Mario", ("Rossi", 1990) },
    { "Luca", ("Verdi", 1980) },
    { "Paolo", ("Bianchi", 1970) }
};

foreach (var partecipante in partecipanti3)
{
    table6.AddRow(partecipante.Key, partecipante.Value.Item1, partecipante.Value.Item2.ToString());
}

AnsiConsole.Write(table6);

// versione con tuple in Key and Value

var table7 = new Table();
table7.AddColumn("Nome");
table7.AddColumn("Soprannome");
table7.AddColumn("Cognome");
table7.AddColumn("Anno di nascita");

var partecipanti4 = new Dictionary<(string, string), (string, int)>
{
    { ("Mario", "soprannome"), ("Rossi", 1990) },
    { ("Luca", "soprannome"), ("Verdi", 1980) },
    { ("Paolo", "soprannome"), ("Bianchi", 1970) }
};

foreach (var partecipante in partecipanti4)
{
    table7.AddRow(partecipante.Key.Item1, partecipante.Key.Item2, partecipante.Value.Item1, partecipante.Value.Item2.ToString());
}

AnsiConsole.Write(table7);
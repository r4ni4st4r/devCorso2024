# Semplice applicazione console per la gestione di un magazzino
# in questo caso di un negozio di hardware informatico
# ma che potrà essere facilmente adattata a diversi ambiti.

#
#


## Definizione dei Requisiti e Analisi:

-   [ ] L'applicazione dovrà essere in grado di gestire il magazzino di uno store (generico)
-   [ ] Capacità di inserire, rimuovere e modificare prodotti manualmente
-   [ ] Gestire la persistenza dei dati tramite cartelle e singoli file .json
-   [ ] Possibilità di caricare prodotti tramite file .csv
-   [ ] All'aggiunta della categoria modificare i campi del .csv
-   [ ] I possibili utilizzatori saranno tutti i gestori di store (fisici/online)

## Pianificazione e Design dell'Architettura:

-  [ ] Scegli un'architettura adatta (ad es., MVC, MVP, MVVM).
-  [ ] Definisci i modelli di dati e le relazioni tra di essi.
-  [x] Progetta l'interfaccia utente e l'esperienza utente (UI/UX).

## Appunti durante la scrittura del codice:

lista prodotti -> cpu - video card - mother board - ram 

cpu -> brand , model , Mhz
video card -> brand, model, ram
mother board -> brand, model, socket
ram -> size, type, Mhz 




## Sviluppo dei Componenti:

-  [ ] Sviluppa i modelli per la logica di business e l'accesso ai dati.
-  [ ] Crea le viste e i view model/controller per la presentazione.
-  [ ] Implementa la logica di controllo e l'integrazione tra i componenti.

## Integrazione e Middleware:

-  [ ] middleware per la gestione degli errori, logging, ecc.
-  [ ] Integra con database, API esterne o servizi.

## Test e Debugging:

-  [ ] Scrivi test unitari e test di integrazione.
-  [x] Esegui il debugging per individuare e risolvere i bug.

## Documentazione:

-  [x] Documenta il codice e l'architettura.
-  [ ] Prepara manuali per gli sviluppatori e gli utenti.

## Deployment e Manutenzione:

-  [ ] Prepara l'ambiente di produzione.
-  [ ] Effettua il deployment dell'applicazione.
-  [ ] Pianifica la manutenzione e gli aggiornamenti futuri.

Ogni progetto è unico, quindi questa struttura può essere adattata in base alle specifiche esigenze del tuo progetto. Questo layout fornisce una guida generale per aiutarti a organizzare il tuo processo di sviluppo in modo più efficiente.

# ESEMPIO DI PROGETTAZIONE DI UN'APPLICAZIONE CON C# E .NET

Per vedere come funziona questo processo nella pratica, diamo un'occhiata a un esempio di progettazione di un'applicazione con C# e .NET.

## GIOCO INDOVINA LA PAROLA

In questo esempio, progetteremo un'applicazione per giocare al gioco indovina la parola.
Indovina la parola è un gioco di indovinelli in cui un giocatore deve indovinare una parola segreta lettera per lettera.
Il giocatore ha un numero limitato di tentativi per indovinare la parola.
Ogni volta che il giocatore indovina una lettera corretta, la lettera viene rivelata nella parola.
Se il giocatore indovina tutte le lettere della parola prima di esaurire i tentativi, vince. Altrimenti, perde.

## DEFINIZIONE DEI REQUISITI E ANALISI

Prima di iniziare a scrivere il codice, dobbiamo definire gli obiettivi dell'applicazione e le funzionalità chiave.
In questo caso, l'obiettivo è creare un'applicazione console che consenta a un giocatore di giocare al gioco del indovina numero.
- [x] L'applicazione deve consentire al giocatore di inserire una lettera per volta.
- [x] L'applicazione deve mostrare un messaggio di errore se il giocatore inserisce più di una lettera alla volta.
- [x] L'applicazione deve mostrare un messaggio di errore se il giocatore inserisce una lettera che ha già indovinato.
- [x] L'applicazione deve mostrare un messaggio di errore se il giocatore inserisce una lettera che non è presente nella parola.
- [x] L'applicazione deve mostrare un messaggio di errore se il giocatore inserisce un carattere non valido.

## PIANIFICAZIONE E DESIGN DELL'ARCHITETTURA

Ora che abbiamo definito gli obiettivi e le funzionalità dell'applicazione, dobbiamo decidere come organizzare il codice.
In questo caso, useremo un'architettura semplice senza modelli o database ma ci limiteremo ad interagire attraverso file di testo
- [x] L'applicazione deve leggere una parola segreta da un file di testo.
- [x] L'applicazione deve memorizzare le parole indovinate in un file di testo.
- [x] L'applicazione deve memorizzare i tentativi rimanenti in un file di testo.
- [x] L'applicazione deve memorizzare i tentativi falliti in un file di testo.
- [ ] L'applicazione deve memorizzare la parola indovinata in un file di testo.

## DEFINIZIONE DI STRUTTURE E CONVENZIONI

Ora che abbiamo deciso come organizzare il codice, dobbiamo definire le convenzioni di codifica e i nomi standard.
In questo caso, useremo le convenzioni di codifica di C# e .NET e i nomi standard.
- [x] I nomi delle classi devono essere PascalCase.
- [x] I nomi dei metodi devono essere PascalCase.
- [x] I nomi delle variabili devono essere camelCase.
- [x] I nomi delle costanti devono essere UPPERCASE.
- [x] I nomi dei file devono essere lowercase.
- [x] I nomi dei progetti devono essere PascalCase.
- [x] I nomi dei namespace devono essere PascalCase.


## SVILUPPO DEI COMPONENTI

Ora che abbiamo definito le convenzioni di codifica e i nomi standard, possiamo iniziare a scrivere il codice.
In questo caso, creeremo un progetto console per l'applicazione e un progetto di test per i test unitari.
- [x] Creare un progetto console per l'applicazione.
- [ ] Creare un progetto di test per i test unitari.

## TEST E DEBUGGING

Ora che abbiamo integrato i componenti dell'applicazione, dobbiamo testarli e risolvere eventuali bug.
In questo caso, scriveremo test unitari per i componenti dell'applicazione.
- [ ] Scrivere test unitari per i componenti dell'applicazione.
- [x] Eseguire il debugging per individuare e risolvere i bug.

## DOCUMENTAZIONE

Ora che abbiamo testato e risolto i bug dell'applicazione, dobbiamo documentare il codice e l'architettura.
In questo caso, documenteremo il codice e l'architettura dell'applicazione.
- [x] Documentare il codice e l'architettura dell'applicazione.
- [ ] Documentare i test unitari.
- [ ] Documentare la fase di Beta Testing.

- [ ] Documentare la fase di post Beta Testing.
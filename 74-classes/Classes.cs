class Persona{
    private string nome;
    private string cognome;
    private int eta;

    public string Nome{
        get {return nome;}
        set {nome = value;}
    }
    public string Cognome{
        get {return cognome;}
        set {cognome = value;}
    }public int Eta{
        get {return eta;}
        set {eta = value;}
    }
    public Persona(string nome, string cognome, int eta){
        this.nome = nome;
        this.cognome = cognome;
        this.eta = eta;
    }

    public void Stampa(){
        Console.WriteLine($"nome = {Nome}");
        Console.WriteLine($"cognome = {Cognome}");
        Console.WriteLine($"eta = {Eta}");
    }
}

class Studente : Persona{
    private string corso;

    public Studente(string nome, string cognome, int eta, string corso) : base(nome, cognome, eta) {
        this.corso = corso;
    }

    public string Corso{
        get{ return corso; }
        set{ corso = value; }
    }

    public void StampaCorso(){
        Console.WriteLine($"Corso -> {Corso}");
    }
}

class Dado {
    Random rnd = new Random();
    private int facce;

    public Dado(int facce = 6){
        
        this.facce = facce;
    }

    public int Lancia(){
        return rnd.Next(1, facce + 1);
    }
}
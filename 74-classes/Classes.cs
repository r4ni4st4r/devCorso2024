class Persona{
    private string _nome;
    private string _cognome;
    private int _eta;

    public string Nome{
        get {return _nome;}
        set {_nome = value;}
    }
    public string Cognome{
        get {return _cognome;}
        set {_cognome = value;}
    }public int Eta{
        get {return _eta;}
        set {_eta = value;}
    }
    public Persona(string nome, string cognome, int eta){
        this._nome = nome;
        this._cognome = cognome;
        this._eta = eta;
    }

    public virtual void Stampa(){
        Console.WriteLine($"nome = {Nome}");
        Console.WriteLine($"cognome = {Cognome}");
        Console.WriteLine($"eta = {Eta}");
    }
}

class Studente : Persona{
    private string _corso;

    public Studente(string nome, string cognome, int eta, string corso) : base(nome, cognome, eta) {
        this._corso = corso;
    }

    public string Corso{
        get{ return _corso; }
        set{ _corso = value; }
    }

    public override void Stampa(){
        base.Stampa();/*
        Console.WriteLine($"nome = {Nome}");
        Console.WriteLine($"cognome = {Cognome}");
        Console.WriteLine($"eta = {Eta}");*/
        Console.WriteLine($"Corso = {Corso}");
        
    }
}

// Esempio Proprietà automatiche

class Cane{
    public string Nome {get; set;} // -->> con logica
    public string Razza {get; set;}
    public int Eta {get; set;}
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
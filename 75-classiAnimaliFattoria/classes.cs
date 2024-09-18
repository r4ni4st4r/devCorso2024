abstract class Animale{
    private readonly string _nome = "";

    public virtual string Nome{
        get{return _nome;}
    }
    public int Eta{get;set;}
    public int Peso{get;set;}

    public abstract string FaiVerso();

    public abstract string Produce();
}

class Mucca:Animale{
    private string _nome = "Mucca";

    public override string Nome{
        get{return _nome;}
    }
    public Mucca(int eta, int peso){
        Eta = eta; 
        Peso = peso;
    }

    public override string FaiVerso()
    {
        return "Muuuuu";
    }

    public override string Produce()
    {
        return "Latte";
    }
}

class Gallina:Animale{
    private string _nome = "Gallina";

    public override string Nome{
        get{return _nome;}
    }
    public Gallina(int eta, int peso){
        Eta = eta; 
        Peso = peso;
    }

    public override string FaiVerso()
    {
        return "CoccoDe";
    }

    public override string Produce()
    {
        return "Uova";
    }
}

class Maiale:Animale{
    private string _nome = "Maiale";

    public override string Nome{
        get{return _nome;}
    }
    public Maiale(int eta, int peso){
        Eta = eta; 
        Peso = peso;
    }

    public override string FaiVerso()
    {
        return "Oink";
    }

    public override string Produce()
    {
        return "Carne";
    }
}
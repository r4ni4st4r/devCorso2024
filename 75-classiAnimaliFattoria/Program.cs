class Program{
    static void Main(string[] args){
        List<Animale> listaDiAnimali  = new List<Animale>(); 
        Mucca m = new Mucca(2,250);
        listaDiAnimali.Add(m);
        Maiale ma = new Maiale(1,150);
        listaDiAnimali.Add(ma);
        Gallina ga = new Gallina(3,1);
        listaDiAnimali.Add(ga);
        
        foreach(Animale animale in listaDiAnimali){
            Console.WriteLine($"La {animale.Nome} fa {animale.FaiVerso()} e produce {animale.Produce()}");
        }
    }
}

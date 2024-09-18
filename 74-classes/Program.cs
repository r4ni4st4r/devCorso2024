class Program{
    public static void Main(string[] args){
        /*Persona p = new Persona("Francesco", "Rossi", 67);
        p.Stampa();
        p.Cognome = "Bianchi";
        Console.WriteLine();
        p.Stampa();
        Studente s = new Studente("Pippo", "Calo", 77, "Informatica");
        s.Stampa();
        s.StampaCorso();*/
        
        int fac = Convert.ToInt32(Console.ReadLine());

        Dado d1 = new Dado(fac);
        Dado d2 = new Dado();

        Console.WriteLine($"Dado 1 a {fac} facce -> {d1.Lancia()}");
        Console.WriteLine($"Dado 2 a 6 facce -> {d2.Lancia()}");
    }
}
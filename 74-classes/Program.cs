class Program{
    public static void Main(string[] args){
        Persona p = new Persona("Francesco", "Rossi", 67);
        p.Stampa();
        p.Cognome = "Bianchi";
        Console.WriteLine();
        p.Stampa();
        Studente s = new Studente("Pippo", "Calo", 77, "Informatica");
        Console.WriteLine("********************************");
        s.Stampa();
        
        int fac = Convert.ToInt32(Console.ReadLine());

        Dado d1 = new Dado(fac);
        Dado d2 = new Dado();

        Console.WriteLine($"Dado 1 a {fac} facce -> {d1.Lancia()}");
        Console.WriteLine($"Dado 2 a 6 facce -> {d2.Lancia()}");
        Cane c1 = new Cane{Nome = "Fuffi", Razza = "Bastardo", Eta = 7};
        Console.WriteLine($"nome -> {c1.Nome}");
        Console.WriteLine($"razza -> {c1.Razza}");
        Console.WriteLine($"eta -> {c1.Eta}");
        /*
        Cane c2 = new Cane();
        Console.WriteLine($"nome 2 -> {c2.Nome}");*/
    }
}
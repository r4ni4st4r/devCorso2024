class Program{
    static void Main(string[] args){

        /*Utilizzo di ************Min e MAx**************
        int[] numeri = {5,9,1,3,4,65,-3};
        int max = numeri[0];
        int min = numeri[0];
        for(int i=0; i<numeri.Length;i++){
            max = Math.Max(max, numeri[i]);
            min = Math.Min(min, numeri[i]);
        }
        Console.WriteLine($"massimo -> {max}");
        Console.WriteLine($"minimo -> {min}");
        */
/* 
        double[] numeri = {3.14159,2.71828, 1.61803};
        for(int i = 0; i<numeri.Length;i++){
            numeri[i] = Math.Round(numeri[i], 2);
            Console.WriteLine($"numero arrotondato -> {numeri[i]}");
        }
        Arrotondamento   eccesso / difetto    */

        /*
        double[] numeri =  {3.14159,2.71828, 1.61803};
        for(int i = 0; i<numeri.Length;i++){
            double arrotondPerEcc = Math.Ceiling(numeri[i]);
            double arrotondPerDifet = Math.Floor(numeri[i]);
            Console.WriteLine($"numero arrotondato per eccesso -> {arrotondPerEcc}");
            Console.WriteLine($"numero arrotondato per difetto -> {arrotondPerDifet}");
        }
        
        int dividendo = 10;
        int divisore =3;
        int quoziente= Math.DivRem(dividendo, divisore, out int resto);
        Console.WriteLine($"quoziente -> {quoziente}, resto -> {resto}\n");

        try{
            int u = 0;
            int i = 7/u;
        }catch(Exception e){
            Console.WriteLine($"exception e -> {e.Message}");
        }*/

        int numeroNegativo = -10;
        int valoreAssoluto = Math.Abs(numeroNegativo);
        Console.WriteLine($"numero -> {numeroNegativo}, valore Assoluto -> {valoreAssoluto}\n");
    }
}
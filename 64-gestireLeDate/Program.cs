using System.Security.Cryptography.X509Certificates;

public class Program{
    public static void Main(){
        /*  -------------- PRIMA PARTE -----------------
        DateTime birthDate = new DateTime(1981,2,13);
        Console.Clear();
        Console.WriteLine("\nFormato lungo: "+ birthDate.ToLongDateString());
        Console.WriteLine("Mese in formato testuale: "+ birthDate.ToString("MMMM"));
        Console.WriteLine("Formato lungo: "+ birthDate.ToString("dd-MM-yyyy") + "\n");
        -------------- PRIMA PARTE -----------------*/

        /* -------------SECONDA PARTE-------------------
        DateTime today = DateTime.Today;
        DateTime futureDate = today.AddDays(100);
        DateTime pastDate = today.AddDays(-75);

        Console.WriteLine("\n100 giorni da oggi: " + futureDate.ToShortDateString());
        Console.WriteLine("75 giorni prima di oggi: " + pastDate.ToShortDateString());

        DateTime nextBirthday = new DateTime(today.Year,2,13);

        if(nextBirthday < today){
            nextBirthday = nextBirthday.AddYears(1);
        }
        int dayUntilNextBirthday = (nextBirthday - today).Days;
        Console.WriteLine("Giorni fino al prossimo compleanno: "+ dayUntilNextBirthday+"\n");
         -------------SECONDA PARTE-------------------*/


        /*------------TERZA PARTE---------------- comparazione di 2 date
        DateTime date1 = DateTime.Today;
        DateTime date2 = new DateTime(2024,12,31);

        int result = DateTime.Compare(date1, date2);

        if(result<0)
            Console.WriteLine("\nLa prima data è prima della seconda");
        else if(result==0)
            Console.WriteLine("\nLa date sono uguali");
        else
            Console.WriteLine("\nLa prima data è dopo della seconda");

        Console.WriteLine("result --> "+result+"\n");
        ------------TERZA PARTE---------------- comparazione di 2 date*/

        /*------------TERZA QUARTA---------------- comparazione di 2 date in giorni, ore, minuti
        DateTime startDate = DateTime.Today;
        DateTime endDate = new DateTime(2024,12,31);

        TimeSpan difference = endDate - startDate;
        Console.Clear();

        Console.WriteLine("\nDifferenza in giorni: "+ difference.Days);
        Console.WriteLine("Differenza in ore: "+ difference.TotalHours);
        Console.WriteLine("Differenza in minuti: "+ difference.TotalMinutes + "\n");
        ------------TERZA QUARTA---------------- comparazione di 2 date in giorni, ore, minuti*/


        TimeSpan timeSpan = new TimeSpan(3,5,10,0); // 3 giorni, 5 ore, 10 minuti
        DateTime today = DateTime.Today;
        DateTime resultDate = today.Add(timeSpan);
        Console.WriteLine("\nData e ora risultante: " + resultDate + "\n");

        Console.WriteLine();
    }
}
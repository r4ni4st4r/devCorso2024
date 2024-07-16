class   Program{
    public void Main(string[] args){
        StampaMessaggio("La somma è: "+ Somma(3,5));
    }

    public void StampaMessaggio(string messaggio){
        Console.WriteLine(messaggio);
    }

    public int Somma(int a, int b){
        return a + b;
    } 
}
using System.Data.SQLite;

class Program{
    static void Main(string[] args){
        string path = @"database.db";
        if (!File.Exists(path)){
            SQLiteConnection.CreateFile(path);
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;version=3;");
            connection.Open();
            string sql = @"
                        CREATE TABLE prodotti (id INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT UNIQUE, prezzo REAL,
                        quantita INTEGER CHECK (quantita >= 0));
                        INSERT INTO prodotti (nome,prezzo,quantita) VALUES ('P1',1,10);
                        INSERT INTO prodotti (nome,prezzo,quantita) VALUES ('P2',2,20);
                        INSERT INTO prodotti (nome,prezzo,quantita) VALUES ('P3',3,30);";
            SQLiteCommand command = new SQLiteCommand(sql, connection); 
            command.ExecuteNonQuery();
            connection.Close();
        }
        while(true){
            Console.WriteLine("1 - Inserisci Prodotto: ");
            Console.WriteLine("2 - Visualizza Prodotti: ");
            Console.WriteLine("3 - Modifica nome Prodotto: ");
            Console.WriteLine("4 - Modifica prezzo Prodotto: ");
            Console.WriteLine("5 - Modifica quantità Prodotto: ");
            Console.WriteLine("6 - Visualizza Prodotto più caro: ");
            Console.WriteLine("7 - Visualizzare un prodotto: ");
            Console.WriteLine("8 - Elimina Prodotto: ");
            Console.WriteLine("9 - Esci: ");
            Console.WriteLine("Scegli un opzione");
            int scelta = Convert.ToInt32(Console.ReadLine());
            switch(scelta){
                case 1:
                    InserisciProdotto();
                    break;
                case 2:
                    VisualizzaProdotti();
                    break;
                case 3: case 4: case 5:
                    ModificaProdotto(scelta);
                    break;
                case 6:
                    VisualizzaProdottoPiuCaro();
                    break;
                case 8:
                    EliminaProdotto();
                    break;
                case 9:
                    return;
            }
        }
    }
    static void InserisciProdotto(){
        Console.WriteLine("Inserisci il nome del prodotto");
        string name = Console.ReadLine();
        Console.WriteLine("Inserisci il prezzo del prodotto");
        string prezzo = Console.ReadLine();
        Console.WriteLine("Inserisci la quantita del prodotto");
        string quantita = Console.ReadLine();
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;version=3;");
        connection.Open();
        string sql = $"INSERT INTO prodotti (nome, prezzo, quantita) VALUES ('{name}',{prezzo},{quantita})";
        SQLiteCommand command = new SQLiteCommand(sql, connection);
        command.ExecuteNonQuery();
        connection.Close();
    }

    static void VisualizzaProdotti(){
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;version=3;");
        connection.Open();
        string sql = $"SELECT * FROM prodotti";
        SQLiteCommand command = new SQLiteCommand(sql, connection);
        SQLiteDataReader reader = command.ExecuteReader();
        while(reader.Read()){
            Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}");
        }
        connection.Close();
    }
    static void EliminaProdotto(){
        Console.WriteLine("Inserisci il nome del prodotto");
        string name = Console.ReadLine();

        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;version=3;");
        connection.Open();
        string sql = $"DELETE FROM prodotti WHERE nome='{name}'";
        SQLiteCommand command = new SQLiteCommand(sql, connection);
        command.ExecuteNonQuery();
        connection.Close();
    }

    static void ModificaProdotto(int code){
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;version=3;");
        string name;
        string newName;
        string sql = "";
        SQLiteCommand command;
        Console.WriteLine("Inserisci il nome del prodotto da modificare");
        name = Console.ReadLine();
        switch(code){
            case 3:
                Console.WriteLine("Inserisci il nuovo nome");
                newName = Console.ReadLine();
                sql = $"UPDATE prodotti SET nome='{newName}' WHERE nome='{name}'";
            break;
            case 4:
                Console.WriteLine("Inserisci il nuovo prezzo");
                newName = Console.ReadLine();
                sql = $"UPDATE prodotti SET prezzo={newName} WHERE nome='{name}'";
         
            break;
            case 5:
                Console.WriteLine("Inserisci la nuova quantità");
                newName = Console.ReadLine();
                sql = $"UPDATE prodotti SET quantit={newName} WHERE nome='{name}'";
             
            break;
        }
        connection.Open();
        command = new SQLiteCommand(sql, connection);
        command.ExecuteNonQuery();
        connection.Close();
    }

    static void VisualizzaProdottoPiuCaro(){
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;version=3;");
        string sql = $"SELECT * FROM prodotti ORDER BY prezzo DESC LIMIT 1";
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(sql, connection);
        SQLiteDataReader reader = command.ExecuteReader();
        while(reader.Read()){
            Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}");
        }
        connection.Close();
    }
}



using System.Data.SQLite;
class Database
{
    // SQLiteConnection è una classe che rappresenta una connessione a un database SQLite
    // Perchè è un oggetto che rappresenta il modello
    // Si utilizza l'underscore davanti al nome della variabile per indicare che è privata e non accessibile dall'esterno
    private SQLiteConnection _connection;

    public SQLiteConnection Connection{get{return _connection;}}

    public Database()
    {
        _connection = new SQLiteConnection("Data Source = database.db");  // Creazione di una connessione al database
        _connection.Open();                                 // Apertura della connessione
        var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY, name TEXT, status BOOLEAN)", _connection);
        command.ExecuteNonQuery();                          // Esecuzione del comando
    }

    public int AddUser(string name,int status)
    {
        var command = new SQLiteCommand($"INSERT INTO users (name,status) VALUES (@name, @status)", _connection);
        command.Parameters.AddWithValue("@name",name);
        command.Parameters.AddWithValue("@status",status);
        return command.ExecuteNonQuery();                          // Esecuzione del comando
    }

    public int CloseConnection(){
        if(_connection.State == System.Data.ConnectionState.Open){
            _connection.Close();
            return 0;
        }else{
            return 1;
        }
    }

    public List<User> SearchUserByName(string name){
        var command = new SQLiteCommand($"SELECT id, name, status FROM users WHERE name = @name", _connection);
        command.Parameters.AddWithValue("@name",name);
        var reader = command.ExecuteReader();
        var users = new List<User>(); 
        while (reader.Read()){
            User tmp = new User(reader.GetInt32(0),reader.GetString(1),reader.GetInt32(2));
            users.Add(tmp);
        }
        return users; 
    }

    public List<User> GetUsers()
    {
        var command = new SQLiteCommand("SELECT id, name, status  FROM users", _connection); // Creazione di un comando per leggere gli utenti
        var reader = command.ExecuteReader();                   // Esecuzione del comando e creazione di un oggetto per leggere i risultati
        var users = new List<User>();                           // Creazione di una lista per memorizzare i nomi degli utenti
        while (reader.Read()){
            if(reader.GetBoolean(2)){
                User tmp = new User(reader.GetInt32(0),reader.GetString(1),1);
                users.Add(tmp);                                     // Aggiunta del nome dell'utente alla lista
            }
        }
        return users;                                           // Restituzione della lista
    }
    public List<User> GetAllUsers(){
        var command = new SQLiteCommand("SELECT id, name, status  FROM users", _connection); // Creazione di un comando per leggere gli utenti
        var reader = command.ExecuteReader();                   // Esecuzione del comando e creazione di un oggetto per leggere i risultati
        var users = new List<User>();                           // Creazione di una lista per memorizzare i nomi degli utenti
        while (reader.Read()){
            User tmp = new User(reader.GetInt32(0),reader.GetString(1),Convert.ToInt32(reader.GetBoolean(2)));
            users.Add(tmp);                                     // Aggiunta del nome dell'utente alla lista
        }
        return users;    
    }

    public int UpdateUser(string oldName, string newName)
    {
        var command = new SQLiteCommand($"UPDATE users SET name = @newName WHERE name = @oldName", _connection);
        command.Parameters.AddWithValue("@newName", newName);
        command.Parameters.AddWithValue("@oldName", oldName);
        return command.ExecuteNonQuery(); 
    }

    public int DeleteUserByName(string name)
    {
        var command = new SQLiteCommand($"DELETE FROM users WHERE name = @name", _connection);
        command.Parameters.AddWithValue("@name", name);
        return command.ExecuteNonQuery(); 
    }

    public int DeleteUserById(int id)
    {
        var command = new SQLiteCommand($"DELETE FROM users WHERE id = @id", _connection);
        command.Parameters.AddWithValue("@id", id);
        return command.ExecuteNonQuery(); 
    }

    public int ChangeUserStatusById(int id){
        var command = new SQLiteCommand("SELECT status FROM users WHERE id = @id", _connection); 
        command.Parameters.AddWithValue("@id", id);
        var reader = command.ExecuteReader();                                         
        reader.Read();
        bool status = !reader.GetBoolean(0);                                  
        command = new SQLiteCommand($"UPDATE users SET status = @status WHERE id = @id", _connection);
        command.Parameters.AddWithValue("@status", status);
        command.Parameters.AddWithValue("@id", id);
        return command.ExecuteNonQuery();
    }
}
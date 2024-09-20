class View{
    private Database _db;

    public View(Database db) {
        _db = db;
    }

    public void ShowMainMenu(){
        Console.Clear();
        Console.WriteLine("1. Add user");
        Console.WriteLine("2. Change Status By Id");
        Console.WriteLine("3. Show Active users");
        Console.WriteLine("4. Show All users");
        Console.WriteLine("5. Modify user");
        Console.WriteLine("6. Delete user by name");
        Console.WriteLine("7. Delete user by Id");
        Console.WriteLine("8. Search user/s by name");
        Console.WriteLine("9. Close connection");
        Console.WriteLine("10. Check connection");
        Console.WriteLine("11. Exit");
    }

    public void ConnectionStatus(int status){
        if(status == 0){
            Console.Clear();
            Console.WriteLine("Connection open");
            Console.ReadKey();
        }else{
            Console.Clear();
            Console.WriteLine("Connection closed");
            Console.ReadKey();
        }
    }

    public int ShowUsers(List<User> users){
        Console.Clear();
        Console.WriteLine("************************************");
        foreach (var user in users) {
            Console.WriteLine($"id -> {user.Id}\nName -> {user.Name}\nStatus -> {user.Status}");
            Console.WriteLine("************************************");
        }
        Console.ReadKey();

        return users.Count;
    }

    public void ShowResult(int numberOf){
        Console.WriteLine($"{numberOf} Modified!");
        Console.ReadKey();
    }

    public void EmptyDbError(){
        Console.WriteLine($"DB is empty!");
        Console.ReadKey();
    }

    public void ShowError(){
        Console.WriteLine($"Please enter a valid choice!\nPress any key!");
        Console.ReadKey();
    }

    public string GetInput(){
        return Console.ReadLine();
    }
}
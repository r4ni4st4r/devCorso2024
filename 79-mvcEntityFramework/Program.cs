using Microsoft.EntityFrameworkCore;

class Program{
        static void Main(string[] args){
        var db = new Database();                    //Model
        var view = new View(db);                    //View
        var controller = new Controller(db, view);  //Controller
        controller.MainMenu();
    }
}
class User{
    public int Id{get;set;}
    public string Name{get;set;}
    public bool Active{get;set;}
}
class Database : DbContext{
    public DbSet<User> Users{get;set;}
    protected override void OnConfiguring(DbContextOptionsBuilder options){
        options.UseSqlite("Data Source=database.db");
    }
}
class View{
    private Database _db;
    public View(Database db){
        _db = db;
    } 
    public void ShowMainMenu(){
        Console.WriteLine("1 Add User");
        Console.WriteLine("2 Show Users");
        Console.WriteLine("3 Modify User");
        Console.WriteLine("4 Delete User");
        Console.WriteLine("5 Exit");
    }
    public void ShowUsers(List<User> users){
        foreach(var user in users){
            Console.WriteLine(user.Name);
        }
    }
    public string GetInput(){
        return Console.ReadLine();
    }
    public void ShowError(){
        Console.WriteLine("Enter a valid choice!");
    }
}
class Controller{
    private Database _db;
    private View _view;

    public Controller(Database db, View view){
        _db = db;
        _view = view;
    }
    public void MainMenu(){
        int input;
        while(true){
            _view.ShowMainMenu();
            if(!Int32.TryParse(_view.GetInput(),out input))
                input = -1;
            switch(input){
                case 1:
                    AddUser();
                    break;
                case 2:
                    ShowUsers();
                    break;
                case 3:
                    UpdateUser();
                    break;
                case 4:
                    DeleteUser();
                    break;
                case 5:
                    return;
                default:
                    _view.ShowError();
                    break;

            }
        }
    }
    private void AddUser(){
        Console.WriteLine("Enter username: ");
        var name = _view.GetInput();
        _db.Users.Add(new User{Name = name});
        _db.SaveChanges();
    }
    private void ShowUsers(){
        var users = _db.Users.ToList();
        _view.ShowUsers(users);
    }
    private void UpdateUser(){
        Console.WriteLine("Enter username: ");
        var oldName = _view.GetInput();
        Console.WriteLine("Enter new username: ");
        var newName = _view.GetInput();
        User user = null;
        foreach(var u in _db.Users){
            if(u.Name == oldName){
                user = u;
                break;
            }
        }
        if(user!=null){
            user.Name = newName;
            _db.SaveChanges();
        }
    }
    private void DeleteUser(){
        Console.WriteLine("Enter username: ");
        var oldName = _view.GetInput();
        User user = null;
        foreach(var u in _db.Users){
            if(u.Name == oldName){
                user = u;
                break;
            }
        }
        if(user != null){
            _db.Users.Remove(user);
            _db.SaveChanges();
        }
    }
}



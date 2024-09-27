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

class Subscription{
    public int Id{get;set;}
    public string Name{get;set;}
    public decimal Price{get;set;}
}

class Transaction{
    public int Id{get;set;}
    public User User{get;set;}
    public Subscription Subscription{get;set;}
    public DateTime TransactionDate{get;set;}
}

class Database : DbContext{
    public DbSet<User> Users{get;set;}
    public DbSet<Subscription> Subscriptions{get;set;}
    public DbSet<Transaction> Transactions{get;set;}
    protected override void OnConfiguring(DbContextOptionsBuilder options){
        options.UseSqlite("Data Source = database.db");
        //options.UseLazyLoadingProxies();
    }
}
class View{
    private Database _db;
    
    public View(Database db){
        _db = db;
    } 
    public void ShowactiveMenu(){
        Console.Clear();
        Console.WriteLine("0 false");
        Console.WriteLine("1 true");
    }
    public void ShowMainMenu(){
        Console.Clear();
        Console.WriteLine("1 Add User");
        Console.WriteLine("2 Add Subscription");
        Console.WriteLine("3 Show All users");
        Console.WriteLine("4 Show Active users");
        Console.WriteLine("5 Add Transaction");
        Console.WriteLine("6 Modify User");
        Console.WriteLine("7 Change Status By Id");
        Console.WriteLine("8 Delete User");
        Console.WriteLine("9 Delete Subscription");
        Console.WriteLine("10 Show Subscriptions");
        Console.WriteLine("11 Show Transactions");
        Console.WriteLine("12 Exit");
    }
    public void ShowUsers(List<User> users){
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        foreach(var user in users){
            Console.WriteLine($"ID -->> {user.Id}\nNAME -->> {user.Name}");
            Console.WriteLine("--------------------------------------------");
        }
        Console.ReadKey();
    }
    public void ShowSubscriptions(List<Subscription> subs){
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        foreach(Subscription s in subs){
            Console.WriteLine($"NAME -->> {s.Name}\nPRICE -->> {s.Price}");
            Console.WriteLine("--------------------------------------------");
        }
        Console.ReadKey();
    }
    public void ShowTransactions(List<Transaction> trans){
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        foreach(Transaction t in trans){
            Console.WriteLine($"ID -->> {t.Id}\nTransaction Date -->> {t.TransactionDate}\nUser -->> {t.User.Id}\nSubscription -->> {t.Subscription.Name}");
            Console.WriteLine("--------------------------------------------");
        }
        Console.ReadKey();
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
                    AddSubscription();
                    break;
                case 3:
                    ShowUsers(true);
                    break;
                case 4:
                    ShowUsers(false);
                    break;
                case 5:
                    AddPurchase();
                    break;
                case 6:
                    UpdateUser();
                    break;
                case 7:
                    //ChangeStatusById();
                    break;
                case 8:
                    DeleteUser();
                    break;
                case 9:
                    DeleteSubscription();
                    break;
                case 10:
                    ShowSubscriptions();
                    break;
                case 11:
                    ShowTransactions();
                    break;
                case 12:
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
        bool active = ActiveInput();
        _db.Users.Add(new User{Name = name, Active = active});
        _db.SaveChanges();
    }

    private void AddPurchase(){
        Console.WriteLine("Enter subscription: ");
        string subName = _view.GetInput();
        Subscription sub = null;
        foreach(Subscription u in _db.Subscriptions){
            if(u.Name == subName){
                sub = u;
                break;
            }
        }


        Console.WriteLine("Enter username: ");
        var oldName = _view.GetInput();
        User user = null;
        foreach(var u in _db.Users){
            if(u.Name == oldName){
                user = u;
                break;
            }
        }
        
        Transaction prch = new Transaction();
        prch.Subscription = sub;
        prch.User =  user;
        prch.TransactionDate = DateTime.Now;
        _db.Transactions.Add(prch);
        _db.SaveChanges();
    }

    private void AddSubscription(){
        Console.Write("Enter Subcription: ");
        var name = _view.GetInput();
        Console.Write("Enter Price: ");
        decimal price = PriceInput();
        _db.Subscriptions.Add(new Subscription{Name = name, Price = price});
        _db.SaveChanges();
    }
    private void ShowUsers(bool all){
        List<User> users = new List<User>();
        foreach(User u in _db.Users){
            if(all){
                users.Add(u);
            }else if(u.Active)
                users.Add(u);
        }
        _view.ShowUsers(users);
    }
    private void ShowSubscriptions(){
        _view.ShowSubscriptions(_db.Subscriptions.ToList());
    }
    private void ShowTransactions(){
        if(_db.Transactions.ToList().Count == 0){
            Console.WriteLine("No transaction found.");
            Console.ReadLine();
            return;
        }
        // popola i campi delle tabelle collegate
        // var transactions = _db.Transactions.Include(t=>t.User).Include(t=>t.Subscription).ToList();
        foreach (Transaction transaction in _db.Transactions)
        {
            // Carica l'utente associato
            transaction.User = _db.Users.Find(transaction.User.Id);

            // Carica l'abbonamento associato
            transaction.Subscription = _db.Subscriptions.Find(transaction.Subscription.Id);
        }

        _view.ShowTransactions(_db.Transactions.ToList());
    }
    private void ChangeStatusById(){
        _view.ShowUsers(_db.Users.ToList());

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
        if(user != null){
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
    private void DeleteSubscription(){
        Console.WriteLine("Enter subscription: ");
        var oldName = _view.GetInput();
        Subscription sub = null;
        foreach(var s in _db.Subscriptions){
            if(s.Name == oldName){
                sub = s;
                break;
            }
        }
        if(sub != null){
            _db.Subscriptions.Remove(sub);
            _db.SaveChanges();
        }
    }
    private bool ActiveInput(){
        int input;
        while(true){
            _view.ShowactiveMenu();
            if(!Int32.TryParse(_view.GetInput(), out input))
                input = -1;
            switch(input){
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    _view.ShowError();
                    break;
            }
        }
    }
    private decimal PriceInput(){
        Decimal.TryParse(Console.ReadLine(), out decimal input);
        return input;
    }
}



using System.Data.SQLite;

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
                    ChangeUserStatusById();
                    break;
                case 3:
                    ShowUsers();
                    break;
                case 4:
                    ShowAllUsers();
                    break;
                case 5:
                    UpdateUser();
                    break;
                case 6:
                    DeleteUserByName();
                    break;
                case 7:
                    DeleteUserById();
                    break;
                case 8:
                    SearchUserByName();
                    break;
                case 9:
                    _db.CloseConnection();
                    break;
                case 10:
                    CheckConnection();
                    break;
                case 11:
                    _db.CloseConnection();
                    return;
                default:
                    _view.ShowError();
                    break;

            }
        }
    }

    private void CheckConnection(){
        if(_db.Connection.State != System.Data.ConnectionState.Open){
            _view.ConnectionStatus(1);
        }else
            _view.ConnectionStatus(0);
    }

    private void ChangeUserStatusById(){
        if(_db.Connection.State != System.Data.ConnectionState.Open){
            _db.Connection.Open();
        }
        Console.WriteLine("Enter UserId to change status:");
        var input = _view.GetInput();
        if(!Int32.TryParse(input, out int id)){
            _view.ShowError();
        }else
            _view.ShowResult(_db.ChangeUserStatusById(id));
    }
    

    private void DeleteUserById(){
        if(_db.Connection.State != System.Data.ConnectionState.Open){
            _db.Connection.Open();
        }
        Console.WriteLine("Enter id to delete:");
        var id = _view.GetInput();
        if(!Int32.TryParse(id,out int input)){
            _view.ShowError();
        }else
            _view.ShowResult(_db.DeleteUserById(input));
    }
    private void DeleteUserByName(){
        if(_db.Connection.State != System.Data.ConnectionState.Open){
            _db.Connection.Open();
        }
        Console.WriteLine("Enter user to delete:");
        var name = _view.GetInput();
        if(name == "" || Int32.TryParse(name,out int input) || name.Count() > 50){
            _view.ShowError();
        }else
            _view.ShowResult(_db.DeleteUserByName(name));
    }
    private void UpdateUser(){
        if(_db.Connection.State != System.Data.ConnectionState.Open){
            _db.Connection.Open();
        }
        Console.WriteLine("Enter old name:");
        var oldName = _view.GetInput();
        Console.WriteLine("Enter new name:");
        var newName = _view.GetInput();
        if(oldName == "" || newName == "" || Int32.TryParse(newName,out int input) || Int32.TryParse(oldName,out int input2) || oldName.Count()>50||newName.Count()>50){
            _view.ShowError();
        }else
            _view.ShowResult(_db.UpdateUser(oldName, newName));
    }
    private void AddUser(){
        if(_db.Connection.State != System.Data.ConnectionState.Open){
            _db.Connection.Open();
        }
        Console.WriteLine("Enter user name:");
        var name = _view.GetInput();
        Console.WriteLine("Enter user status (1 active / 0 inactive):");
        var statusInput = _view.GetInput();
        if(!Int32.TryParse(statusInput, out int status) || (status != 0 && status != 1))
            _view.ShowError();
        else{
            if(name == "" || Int32.TryParse(name, out int input)|| name.Count() > 50){
                _view.ShowError();
            }else{
                _view.ShowResult(_db.AddUser(name, status));
            }
        }
    }
    private void ShowAllUsers(){
        if(_db.Connection.State != System.Data.ConnectionState.Open){
            _db.Connection.Open();
        }
        var users = _db.GetAllUsers();
        if(users.Count > 0){
            _view.ShowUsers(users);
        }else{
            _view.EmptyDbError();
        }
    }
    private void ShowUsers(){
        if(_db.Connection.State != System.Data.ConnectionState.Open){
            _db.Connection.Open();
        }
        var users = _db.GetUsers();
        if(users.Count > 0){
            _view.ShowUsers(users);
        }else{
            _view.EmptyDbError();
        }
    }
    private void SearchUserByName(){
        if(_db.Connection.State != System.Data.ConnectionState.Open){
            _db.Connection.Open();
        }
        Console.WriteLine("Enter name:");
        var name = _view.GetInput();
        if(name == "" || Int32.TryParse(name,out int input)){
            _view.ShowError();
        }else{
           _view.ShowResult(_view.ShowUsers(_db.SearchUserByName(name)));
        }
    }
}
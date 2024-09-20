using System.ComponentModel.Design;

class User{
    private string _name = "";
    private int _id;
    private bool _status;

    public string Name{ get{ return _name; } set{ _name = value; }}
    public int Id{ get{ return _id; } set{ _id = value; }}
    public bool Status{ get{ return _status; } set{ _status = value; }}

    public User(int id, string name, int status){
        Id = id;
        Name = name;
        if(status == 1)
            Status = true;
        else
            Status = false;
    }
}
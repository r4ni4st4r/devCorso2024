using System.Net.Cache;

class Player{
    private string _name;
    private List<Game> _history;

    public Game SetHistory{set{_history.Add(value);}}
    public List<Game> GetHistory{get {return _history;}}
    public string Name{get{return _name;}}

    public Player(string  name){
        _name = name;
        _history  = new List<Game>();
    }
}
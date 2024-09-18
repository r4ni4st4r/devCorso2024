class Player{
    private string _name;
    private List<Game> _history = new List<Game>();

    public Game HistorySet{set{_history.Add(value);}}
    public List<Game> HistoryGet{get {return _history;}}
    public string Name{get{return _name;}set{_name = value;}}

    public Player(string nm){
        Name = nm;
    }
}
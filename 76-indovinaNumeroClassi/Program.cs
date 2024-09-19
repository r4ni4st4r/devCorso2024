class Program{
    static List<Player> players = new List<Player>();
    static Player currentPlayer;
    static Game currentGame;
    static bool playerExist;
    public static void Main(string[] args){
        bool menuGo;
        
        int result;

        while(true){
            Console.Clear();
            Console.WriteLine("1 - New Game");
            Console.WriteLine("2 - Player Statistics");
            Console.WriteLine("3 - Exit");
            Console.WriteLine("Please make a choice");
            if(!Int32.TryParse(Console.ReadLine(),out result))
                result = -1;
            menuGo = true;
            switch(result){
                case 1:
                    while(menuGo){
                        Console.Clear();
                        Console.WriteLine("1 - New Player");
                        Console.WriteLine("2 - Existing Player");
                        if(!Int32.TryParse(Console.ReadLine(), out result))
                            result = -1;
                        switch(result){
                            case 1:
                                playerExist = false;
                                if(1 == CheckOrCreatePlayer(playerExist)){
                                    NewGame();
                                    menuGo = false;
                                    break;
                                }else
                                    Console.ReadKey();
                                break;
                            case 2:
                                if(players.Count == 0){
                                    Console.WriteLine("Players list is empty...");
                                    Console.ReadKey();
                                    break;
                                }
                                playerExist = true;
                                if(1 == CheckOrCreatePlayer(playerExist)){
                                    NewGame();
                                    menuGo = false;
                                    break;
                                }else{
                                    Console.ReadKey();
                                }
                                break;
                            default:
                                Console.WriteLine("Enter a valid choice!\nPress any key!");
                                Console.ReadKey();
                                break;
                        }
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("1 - Select Player");
                    Console.WriteLine("2 - Back");
                    if(!Int32.TryParse(Console.ReadLine(), out result))
                        result = -1;
                    switch(result){
                        case 1:
                            Console.Clear();
                            int i = 1;
                            foreach(Player ply in players){
                                Console.WriteLine("\n"+i + " - " + ply.Name);
                                i++;
                            }
                            if(!Int32.TryParse(Console.ReadLine(), out result))
                                result = -1;
                            switch(result){
                                case int n when n >= 1 && n <= players.Count:
                                    Console.WriteLine($"{players[result-1].Name} has played {players[result-1].GetHistory.Count} match/es");
                                    Console.ReadKey();
                                    break;
                                default:
                                    Console.WriteLine("Enter a valid choice!\nPress any key!");
                                    Console.ReadKey();
                                    break;
                            }
                            break;
                        case 2:
                            break;
                        default:
                            Console.WriteLine("Enter a valid choice!\nPress any key!");
                            Console.ReadKey();
                            break;
                    }    
                    break;    
                case 3:
                    Console.WriteLine("Bye Bye!\nPress any key!");
                    Console.ReadKey();
                    return;
                default:
                    Console.WriteLine("Enter a valid choice!\nPress any key!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void NewGame(){
        currentGame = new Game();
        while(true){
            Console.Clear();
            Console.WriteLine("Enter a number from 0 to 100... or 1000 for history :)");
            if(!Int32.TryParse(Console.ReadLine(),out int result))
                result = -1;
            switch(result){
                case int n when n >= 0 && n <= 100:
                    if(!currentGame.NumbersAttempted.Contains(result)){
                        if(currentGame.ChechNumber(result) == 1){
                            currentPlayer.SetHistory = currentGame;
                            if(!playerExist)
                                players.Add(currentPlayer);
                            return;
                        }
                    }else{
                        Console.WriteLine("You already tried this number!!!\ndummy!!!");
                        Console.ReadKey();
                    }
                    break;
                case 1000:
                    if(currentGame.NumbersAttempted.Count > 0){
                        Console.Clear();
                        Console.WriteLine("Your imput history: ");
                        foreach(int i in currentGame.NumbersAttempted){
                            Console.Write($"{i} ");
                        }
                        Console.WriteLine("\nPress any key!");
                        
                    }else{
                        Console.WriteLine("This is your first try...");
                    }
                    Console.ReadKey();
                    break;    
                default:
                    Console.WriteLine("Enter a valid choice!\nPress any key!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static int CheckOrCreatePlayer(bool playerExist){
        string name = "";
        if(playerExist){
            Console.Write("Please enter the player name: ");
            name = Console.ReadLine();
            foreach(Player ply in players){
                if(ply.Name == name){
                    currentPlayer = ply;
                    return 1;
                }
            }
            Console.WriteLine("Player not found!");
            return 0;
        }else{
            Console.Write("Please enter the player name: ");
            name = Console.ReadLine();
            if(players.Count>0){
                foreach(Player ply in players){
                    if(ply.Name == name){
                        currentPlayer = ply;
                        Console.WriteLine("Player already exist!\nPress any key!");
                        return 0;
                    }
                }
            }
            currentPlayer = new Player(name);
            return 1;
        }
    }
}
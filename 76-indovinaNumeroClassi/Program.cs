class Program{
    static List<Player> players = new List<Player>();
    static Player currentPlayer;
    static Game currentGame;
    public static void Main(string[] args){
        bool existingPlayer;

        while(true){
            Console.Clear();
            Console.WriteLine("1 - New Game");
            Console.WriteLine("2 - Player Statistics");
            Console.WriteLine("2 - Exit");
            Console.WriteLine("Please make a choice");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch(choice){
                case 1:
                    while(true){
                        Console.Clear();
                        Console.WriteLine("1 - Existing Player");
                        Console.WriteLine("2 - New Player");
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch(choice){
                            case 1:
                                if(players.Count == 0){
                                    Console.WriteLine("Players list is empty...");
                                    break;
                                }
                                existingPlayer = true;
                                if(1 == CheckOrCreatePlayer(existingPlayer))
                                    NewGame();
                                else{
                                    Console.ReadKey();
                                }
                                break;
                            case 2:
                                existingPlayer = false;
                                if(1==CheckOrCreatePlayer(existingPlayer))
                                    NewGame();
                                else
                                    Console.ReadKey();
                                break;
                            default:
                            Console.WriteLine("Enter a valid choice!\nPress any key!");
                            Console.ReadKey();
                            break;
                        }
                    }
                case 2:
                    Console.WriteLine("Not implemented yet...\nPress any key!");
                    Console.ReadKey();
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
            Console.WriteLine("Enter a number from 0 to 100");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch(choice){
                case int n when (n >= 0 && n <= 100):
                    if(currentGame.ChechNumber(choice) == 1){
                        currentPlayer.HistorySet = currentGame;
                        return;
                    }
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
        }
        Console.Write("Please enter the player name: ");
        name = Console.ReadLine();
        currentPlayer = new Player(name);
        return 1;
    }
}

public class Game{
    private Random rnd = new Random();
    private static int _gameId = 1;
    private int _specificGameId = 0;
    private int _numberToHit = -1;
    private int _hints = 7;
    private int _attempts = 0;
    public int Gameid{get{return _specificGameId;}}
    public int NumberToHit{get{return _numberToHit;}}
    public int Attemps{get{return _attempts;}}

    public Game(){
        _specificGameId = _gameId;
        _numberToHit = rnd.Next(0,101);
        _gameId++;
    }

    public int ChechNumber(int value){
        if(value == _numberToHit){
            Console.WriteLine($"You win in {_attempts} attempts");
            Console.ReadKey();
            return 1;
        }else{
            if(_hints>5){
                Console.WriteLine($"You fail the number, retry!");
                Console.ReadKey();
            }else if(_hints<=5&&_hints>2){
                string tmp = value < _numberToHit ? "bigger" : "lower";
                Console.WriteLine($"You fail the number, it must be {tmp}... retry!");
                Console.ReadKey();
            }else{
                string tmp = value < _numberToHit ? "bigger" : "lower";
                string tmp2 = _numberToHit % 2 == 0 ? "pair" : "odd";
                Console.WriteLine($"You fail the number, it must be {tmp}, it is {tmp2}... retry!");
                Console.ReadKey();
            }
            _attempts++;
            _hints--;
            return 0;
        }
    }
}
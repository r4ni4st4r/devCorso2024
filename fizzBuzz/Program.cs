// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

int i = 1;

while(i < 101) {
    
    if(i%5 == 0 && i%3 == 0)
        Console.WriteLine($"{i} --> Fizz Buzz"); 
    else if(i%3 == 0)    
        Console.WriteLine($"{i} --> Fizz");
    else if(i%5 == 0)
        Console.WriteLine($"{i} --> Buzz");
    else
        Console.WriteLine($"{i}"); 
    
    i++;
}

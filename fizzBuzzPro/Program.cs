// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

Random number = new Random();
int i = 1;
int j;

while(i < 101) {
    
    j = number.Next(1, 101);

    if(j%5 == 0 && j%3 == 0)
        Console.WriteLine($"{j} --> Fizz Buzz"); 
    else if(j%3 == 0)    
        Console.WriteLine($"{j} --> Fizz");
    else if(j%5 == 0)
        Console.WriteLine($"{j} --> Buzz");
    else
        Console.WriteLine($"{j}"); 
    
    i++;
}
using System;

class MathProcessor
{
    private double currentResult = 0;
    
    public void Execute()
    {
        Console.WriteLine("Вычислительная система");
        Console.WriteLine("Поддерживаемые действия: +, -, *, /, %, 1/X, X^2, SQRT, СБРОС, ВЫХОД");
        
        while (true)
        {
            try
            {
                Console.Write($"Текущий результат: {currentResult} > ");
                string userInput = Console.ReadLine()?.Trim().ToUpper();
                
                if (string.IsNullOrEmpty(userInput))
                    continue;
                    
                if (userInput == "ВЫХОД")
                    break;
                    
                if (userInput == "СБРОС")
                {
                    currentResult = 0;
                    continue;
                }
                
                HandleInput(userInput);
            }
            catch (Exception error)
            {
                Console.WriteLine($"Ошибка: {error.Message}");
            }
        }
    }
    
    private void HandleInput(string input)
    {
        switch (input)
        {
            case "1/X":
                if (currentResult == 0)
                    throw new DivideByZeroException("Деление на ноль невозможно");
                currentResult = 1 / currentResult;
                break;
                
            case "X^2":
                currentResult *= currentResult;
                break;
                
            case "SQRT":
                if (currentResult < 0)
                    throw new ArgumentException("Корень из отрицательного числа невозможен");
                currentResult = Math.Sqrt(currentResult);
                break;
                
            default:
                ProcessCalculation(input);
                break;
        }
    }
    
    private void ProcessCalculation(string input)
    {
        string[] components = input.Split(' ');
        if (components.Length != 2)
            throw new ArgumentException("Некорректный формат ввода. Используйте: число действие");
            
        if (!double.TryParse(components[0], out double value))
            throw new ArgumentException("Некорректный числовой формат");
            
        string action = components[1];
        
        switch (action)
        {
            case "+":
                currentResult += value;
                break;
                
            case "-":
                currentResult -= value;
                break;
                
            case "*":
                currentResult *= value;
                break;
                
            case "/":
                if (value == 0)
                    throw new DivideByZeroException("Деление на ноль невозможно");
                currentResult /= value;
                break;
                
            case "%":
                currentResult %= value;
                break;
                
            default:
                throw new ArgumentException($"Неизвестное действие: {action}");
        }
    }
}

class Application
{
    static void Main(string[] args)
    {
        MathProcessor processor = new MathProcessor();
        processor.Execute();
    }
}

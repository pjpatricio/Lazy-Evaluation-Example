using LazyEvaluation.Example.DomainModels;


string? ShowUsage(string message)
{
    Console.Write(message);
    return Console.ReadLine();
}

const int defaultSeed = 8;

int inputValue;
bool userHasMadeAChoice;
var input =
    ShowUsage(
        $"Hello from @LazyEvaluation.Example!\nPlease Insert a number to evaluate or press enter to go with default value {defaultSeed} -> ");
do
{
    if (string.IsNullOrEmpty(input))
    {
        inputValue = defaultSeed;
        Console.WriteLine($"using default value {inputValue}...");
        break;
    }

    userHasMadeAChoice = int.TryParse(input, out inputValue);
    if (!userHasMadeAChoice)
    {
        input =
            ShowUsage(
                $"Invalid Number, you can try again... or just press enter -> ");
    }
} while (!userHasMadeAChoice);

var evaluator = new Evaluator<int>();

evaluator.Add((val, additionalVals) => val / 2);
evaluator.Add((val, additionalVals) => val + additionalVals[0], 5);
evaluator.Add((val, additionalVals) => val + 1 + additionalVals[0], 20);
evaluator.Add((val, additionalVals) => val - additionalVals[0] - additionalVals[1], 5, 3);

Console.WriteLine(evaluator.Evaluate(inputValue));
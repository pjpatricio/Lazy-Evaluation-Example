using LazyEvaluation.Example.DomainModels;
using NUnit.Framework;

namespace LazyEvaluation.TestExample;

[TestFixture]
public class TestCases
{
    private static void PrintTestVariables<T>(string desc, T seed, T result, T expectedResult)
    {
        TestContext.WriteLine($"{desc} >> seed->{seed}, result->{result}, expected->{expectedResult}");
    }

    [Test]
    [TestCase(8, 22)]
    [TestCase(9, 22)]
    [TestCase(100, 68)]
    public void TestFromChallenge(int seed, int expectedResult)
    {
        var evaluator = new Evaluator<int>();

        evaluator.Add((val, additionalVals) => val / 2);
        evaluator.Add((val, additionalVals) => val + additionalVals[0], 5);
        evaluator.Add((val, additionalVals) => val + 1 + additionalVals[0], 20);
        evaluator.Add((val, additionalVals) => val - additionalVals[0] - additionalVals[1], 5, 3);

        var result = evaluator.Evaluate(seed);

        PrintTestVariables("Challenge exp",seed, result, expectedResult);

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    [TestCase(9.0, 22.5)]
    public void TestWithDoubles(double seed, double expectedResult)
    {
        var evaluator = new Evaluator<double>();

        evaluator.Add((val, additionalVals) => val / 2);
        evaluator.Add((val, additionalVals) => val + additionalVals[0], 5);
        evaluator.Add((val, additionalVals) => val + 1 + additionalVals[0], 20);
        evaluator.Add((val, additionalVals) => val - additionalVals[0] - additionalVals[1], 5, 3);

        var result = evaluator.Evaluate(seed);

        PrintTestVariables("Doubles exp",seed, result, expectedResult);

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    [TestCase(2.0, 1606.0)]
    [TestCase(3.0, 22509.0)]
    public void TestWithExponentialExpression(double seed, double expectedResult)
    {
        var evaluator = new Evaluator<double>();

        evaluator.Add((val, additionalVals) => val * val + additionalVals[0], seed);
        evaluator.Add((val, additionalVals) => val * val + 2 * additionalVals[0], seed);
        evaluator.Add((val, additionalVals) => val * val + 3 * additionalVals[0], seed);

        var result = evaluator.Evaluate(seed);

        PrintTestVariables("Exponential exp",seed, result, expectedResult);

        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
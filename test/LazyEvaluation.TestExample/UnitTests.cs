using LazyEvaluation.Example.DomainModels;
using NUnit.Framework;
using System;
using System.Reflection.Metadata;

namespace LazyEvaluation.TestExample;

[TestFixture]
public class UnitTests
{
    [Test]
    public void Test_IntWithoutCallingAdd_IsEqualToSeed()
    {
        var evaluator = new Evaluator<int>();

        Assert.That(evaluator, Is.Not.EqualTo(null));
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(-1)]
    [TestCase(99)]
    public void Test_EvaluatorConstructor_IsNotNull(int seed)
    {
        var evaluator = new Evaluator<int>();

        Assert.That(evaluator.Evaluate(seed), Is.EqualTo(seed));
    }

    [Test]
    [TestCase("")]
    [TestCase("1")]
    [TestCase("-1")]
    [TestCase("bla bla")]
    [TestCase(null)]
    public void Test_StringWithoutCallingAdd_IsEqualToSeed(string seed)
    {
        var evaluator = new Evaluator<string>();

        Assert.That(evaluator.Evaluate(seed), Is.EqualTo(seed));
    }

    [Test]
    [TestCase(99, 99)]
    public void Test_Add_WithIdentityValue_WorksAsExpected(int initialValue, int expectedResult)
    {
        var evaluator = new Evaluator<int>();

        evaluator.Add((val, emptyParams) => val);

        Assert.That(evaluator.Evaluate(initialValue), Is.EqualTo(expectedResult));
    }

    [Test]
    [TestCase(1, 10)]
    public void Test_Add_WithMultipleIdentityValues_WorksAsExpected(int initialValue, int numberOfAdds)
    {
        var evaluator = new Evaluator<int>();

        while (numberOfAdds > 0)
        {
            evaluator.Add((val, emptyParams) => val);
            --numberOfAdds;
        }

        Assert.That(evaluator.Evaluate(initialValue), Is.EqualTo(initialValue));
    }

    [Test]
    [TestCase(-1)]
    [TestCase(-2)]
    [TestCase(-99)]
    public void Test_Evaluate_WithAbsoluteOperation_WorksAsExpected(int initialValue)
    {
        var evaluator = new Evaluator<int>();

        evaluator.Add((val, emptyParams) => Math.Abs(val));

        Assert.That(evaluator.Evaluate(initialValue), Is.EqualTo(-initialValue));
    }

    [Test]
    public void Test_Evaluate_WithString_WorksAsExpected()
    {
        var evaluator = new Evaluator<string>();

        evaluator.Add((val, additionalParams) => val + string.Concat(additionalParams), " some String");

        Assert.That(evaluator.Evaluate("initialValue"), Is.EqualTo("initialValue some String"));
    }

    [Test]
    public void Test_Evaluate_WithStringNull_ThrowsArgumentNullException()
    {
        var evaluator = new Evaluator<string>();

        evaluator.Add((val, additionalParams) => val + string.Concat(additionalParams), null);

        Assert.Throws<ArgumentNullException>(delegate { evaluator.Evaluate("initialValue"); });
    }
}
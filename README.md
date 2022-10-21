# Lazy Evaluation Code Challenge example

This repository contains code to implement an Evaluator class as requested in the code challenge.

## The test scenarios

There are three scenarios that I created.

**TestFromChallenge**.

* To test the example from the challenge.

**TestWithDoubles**

* Validating that it was working fine for the same expression of the challenge but with doubles this time.

**TestWithExponentialExpression**

* To test a random exponential expression, it came to my mind

## Running the code

There are two projects in C# (`LazyEvaluation.Example` and `LazyEvaluation.TestExample`).
The first is the source code, and the second is the unit tests for the Evaluator class.

You can run the `LazyEvaluation.Example` EXE to execute the example from the challenge.

* **To compile the code,** open up the solution file and compile in Visual Studio or Rider or type `dotnet build` in the command line.

  The binaries are output to the `\bin` folder
* **To run the code,** just run the EXE from that folder.

## About the code

* `class Evaluator<T>` contains a list of the expressions of type `<Tuple<Func<T, T[], T>, T[]>
  ` to represent the two params passed in the `Add` method.

The method `Add` creates the tuple and adds it to the list and the `Evaluate` is an implementation of the fold operation where
we seed an initial value to the agglomerator, and for each iteration, we pipe the output of the previous function into the next one until we finish
and return the last value.

Here is my original implementation of the `Evaluate` method before I converted to the LINQ version

```CSharp
public T Evaluate(T seed)
{
    var aggregate = seed;

    foreach (var item in _expressions)
    {
        aggregate = item.Item1(aggregate, item.Item2);
    }

    return aggregate;
}
```
## Summary

I hope you find this to be an excellent example of lazy evaluation!

Please create an issue if you have any corrections or suggestions to improve this code.

Thanks!

-- Paulo




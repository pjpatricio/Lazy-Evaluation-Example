namespace LazyEvaluation.Example.DomainModels;

using System;

/// <summary>
///  Class <c>Evaluator</c> models the Lazy evaluation of a set of expressions.
/// </summary>
/// <typeparam name="T">The generic <c>T</c> parameter expresses the type on which the evaluator will operate and ultimately return.</typeparam>
public class Evaluator<T>
{
    /// <summary>
    /// Implements the placeholder for the expressions using a List of Tuples where the first item in the tuple
    /// holds the function and the second item the array of additional parameters
    /// </summary>
    private readonly List<Tuple<Func<T, T[], T>, T[]>> _expressions = new();

    /// <summary>
    /// Method <c>Add</c> Associates a `func`, and additional arguments for said `func` with an `Evaluator`T` instance.
    /// <list type="bullet" >
    /// <listheader>
    /// <term>Note that `func` should:</term>
    /// </listheader>
    /// <item>
    /// <term>Accept an accumulated value of type `T`</term>
    /// </item>
    /// <item>
    /// <term>Accept `additionalArgs` via a second parameter of type `T[]`</term>
    /// </item>
    /// <item>
    /// <term>Return a result of type `T`</term>
    /// </item>
    /// </list>
    /// <param name="func">The function to add to the `Evaluator`T` instance</param>
    /// <param name="additionalArgs">The additional params to pass to the function</param>
    /// </summary>
    public void Add(Func<T, T[], T> func, params T[] additionalArgs)
    {
        _expressions.Add(new Tuple<Func<T, T[], T>, T[]>(func, additionalArgs));
    }

    /// <summary>
    /// Invokes the `Evaluator T` instance's functions, in the order in which they were added, and returns the result. This requires an initial seed value.
    /// </summary>
    /// <param name="seed">The initial seed value to start the evaluation.</param>
    /// <returns>The result of type 'T'</returns>
    public T Evaluate(T seed)
    {
        return _expressions.Aggregate(seed, (current, item) => item.Item1(current, item.Item2));
    }
}
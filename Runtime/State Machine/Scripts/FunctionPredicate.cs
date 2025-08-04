using System;

public class FunctionPredicate : IPredicate
{
    private readonly Func<bool> function;
    
    /// <summary>
    /// Constructor that only allows to set the predicate.
    /// </summary>
    /// <param name="function">The function that will be run to check whether the predicate is true.</param>
    public FunctionPredicate(Func<bool> function) => this.function = function;

    /// <summary>
    /// Executes the predicate function and returns its value.
    /// </summary>
    /// <returns>Return value of the predicate function.</returns>
    public bool Evaluate() => function.Invoke();
}

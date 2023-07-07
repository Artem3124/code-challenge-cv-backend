﻿namespace CSCompiler.Contract.Models;

public class CompilationError
{
    public string Message { get; init; }

    public CompilationError(string message)
    {
        Message = message;
    }
}

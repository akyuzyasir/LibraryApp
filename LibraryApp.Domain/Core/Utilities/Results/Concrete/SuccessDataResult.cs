﻿namespace LibraryApp.Domain.Core.Utilities.Results.Concrete;

public class SuccessDataResult<T> : DataResult<T> where T : class
{
    public SuccessDataResult() : base(default, true)
    { }

    public SuccessDataResult(string message) : base(default, true, message)
    { }

    public SuccessDataResult(T data, string message) : base(data, true, message)
    { }
}

namespace ExpenseTracker.Business.Client.Helpers;

/// <summary>
/// Implementation of Result pattern.
/// Inspired by https://code-maze.com/aspnetcore-result-pattern/
/// but it is not a 1:1 copy, adjusted to needs of this project.
/// </summary>
public class Result<TData>
{
    public bool IsSuccess { get; private set; }
    
    public bool IsLoading { get; private set; }
    
    public TData Data { get; private set; } = default!;

    public string ErrorMessage { get; set; } = string.Empty;
    
    public static Result<TData> Success(TData data)
    {
        return new Result<TData>
        {
            IsSuccess = true,
            IsLoading = false,
            Data = data
        };
    }
    
    public static Result<TData> Error(string message = "")
    {
        return new Result<TData>
        {
            IsSuccess = false,
            IsLoading = false,
            ErrorMessage = message
        };
    }
    
    public static Result<TData> Loading()
    {
        return new Result<TData>
        {
            IsLoading = true
        };
    }
}

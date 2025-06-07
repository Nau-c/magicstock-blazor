namespace MagicStock.Shared.Models;

public class SimpleApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    
    public static SimpleApiResponse<T> Ok(T data) => new() { Success = true, Data = data };
    public static SimpleApiResponse<T> Error(string message) => new() { Success = false, Message = message };
} 
namespace ExpenseTracker.Data.Entities;

/// <summary>
/// Base entity class for all database entities. 
/// Contains only Id (Primary Key) property of generic type.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseEntity<T>
{
    public T Id { get; set; } = default!;
}
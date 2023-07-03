namespace Invoice_Gen.ViewModels;

/// <summary>
/// Used to lock the <see cref="PagedResponse{T}"/> down to specific types as
/// we don't want to be able to create paged lists of arbitrary types 
/// </summary>
public interface IPageable
{

}

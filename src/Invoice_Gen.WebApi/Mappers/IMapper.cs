namespace Invoice_Gen.WebApi.Mappers;

public interface IMapper <TSource, TDestination>
{
    TDestination Convert(TSource source);
    TSource Convert(TDestination destination);
}

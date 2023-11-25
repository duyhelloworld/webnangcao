namespace webnangcao;

public interface ISearchService
{
    Task<IEnumerable<object>> Search(string keyword);
}
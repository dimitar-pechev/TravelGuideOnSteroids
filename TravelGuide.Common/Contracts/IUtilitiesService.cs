namespace TravelGuide.Common.Contracts
{
    public interface IUtilitiesService
    {
        int GetPage(int? page, int pagesCount);

        int? ExtractPageFromQuery(string query);

        string ExtractSearchQueryFromQuery(string query);

        T AssignViewParams<T>(T model, string query, int currentPage, int pagesCount, string baseUrl) where T : IPager;
    }
}

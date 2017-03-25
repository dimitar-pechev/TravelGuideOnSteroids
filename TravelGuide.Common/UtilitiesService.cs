using TravelGuide.Common.Contracts;

namespace TravelGuide.Common
{
    public class UtilitiesService : IUtilitiesService
    {
        public T AssignViewParams<T>(T model, string query, int currentPage, int pagesCount, string baseUrl) where T : IPager
        {
            model.Query = query;
            model.CurrentPage = currentPage;
            model.PreviousPage = currentPage - 1;
            model.NextPage = currentPage + 1;
            model.PagesCount = pagesCount;
            model.BaseUrl = baseUrl;

            return model;
        }

        public int GetPage(int? page, int pagesCount)
        {
            int result;
            if (page == null || page < 1 || page > pagesCount)
            {
                result = 1;
            }
            else
            {
                result = (int)page;
            }

            return result;
        }
    }
}

namespace TravelGuide.Shared
{
    public interface IPager
    {
        string BaseUrl { get; set; }

        int PagesCount { get; set; }

        int CurrentPage { get; set; }

        int PreviousPage { get; set; }

        int NextPage { get; set; }

        string Query { get; set; }
    }
}

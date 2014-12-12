namespace GitHub.DataObjects.Abstract
{
    public interface ISearchCollection
    {
        string SearchValue { get; set; }
        int Page { get; }
        int TotalCount { get; }
        int ItemsPerPage { get; }

        void Reset(string searchValue);
    }
}

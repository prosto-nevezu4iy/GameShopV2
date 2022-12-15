namespace BasketProject.Contracts.Abstracts
{
    public interface IBasketQueryService
    {
        Task<int> CountTotalBasketItems(Guid userId);
    }
}

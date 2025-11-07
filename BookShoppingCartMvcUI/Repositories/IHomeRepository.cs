namespace jerseyShoppingCartMvcUI
{
    public interface IHomeRepository
    {
        Task<IEnumerable<jersey>> Getjerseys(string sTerm = "", int categoryId = 0);
        Task<IEnumerable<category>> categorys();
    }
}
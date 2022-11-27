namespace MarketPlace;

public class UserProduct
{
    public  int UserId { get; set; }
    public int ProductId { get; set; }
    public  long ProductCount { get; set; }

    public UserProduct(int user_id, int product_id, long product_count)
    {
        UserId = user_id;
        ProductId = product_id;
        ProductCount = product_count;
    }
}
namespace MarketPlace;

public class UserProduct
{
    public string ProductName { get; set; }
    public  int UserId { get; set; }
    public  long ProductCount { get; set; }

    public UserProduct(string productName, int userId, long productCount)
    {
        ProductName = productName;
        UserId = userId;
        ProductCount = productCount;
    }
}
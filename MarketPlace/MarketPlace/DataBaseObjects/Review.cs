using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; }
        public int Rating { get; set; }
        public string Message { get; set; }
        public Review(int id, string reviewerName, int rating, string message)
        {
            Id = id;
            ReviewerName = reviewerName;
            Rating = rating;
            Message = message;
        }
    }
}

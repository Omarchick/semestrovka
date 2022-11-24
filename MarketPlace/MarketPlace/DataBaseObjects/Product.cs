using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace
{
    public class Product
    {
        private int _rating;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public List<Review>? Reviews { get; set; }

        public int Rating
        {
            get { return Reviews is not null ? Reviews.Sum(r => r.Rating) : -1; }
            set => _rating = value;
        }

        public Product(int id, string name, string information, int rating = -1)
        {
            Id = id;
            Name = name;
            Information = information;
            Rating = rating;
        }
    }
}

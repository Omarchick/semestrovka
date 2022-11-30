using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace
{
    public class UserProductWithCount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public List<Review>? Reviews { get; set; }
        public decimal Rating { get; set; }
        public long Count { get; set; }


        public UserProductWithCount(int id, string name, string information,decimal rating , long count)
        {
            Id = id;
            Name = name;
            Information = information;
            Rating = rating;
            Count = count;
        }
    }
}
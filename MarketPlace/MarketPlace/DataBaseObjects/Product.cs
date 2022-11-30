﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public List<Review>? Reviews { get; set; }
        public float Rating { get; set; }


        public Product(int id, string name, string information)
        {
            Id = id;
            Name = name;
            Information = information;
            /*Reviews = ReviewRepository.GetReviewsByProductId(id).Result.ToList();
            Rating = Reviews is not null ? Reviews.Sum(r => r.Rating) / Reviews.Count : -1; */
            Rating = 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipDiet 
{
    public class Meal
    {
        public int MealId { get; set; }
        public int MealN { get; set; }
        public Product Product { get; set; }
        public decimal Amount { get; set; }
        public int Kkal { get; set; }
        public DateTime date { get; set; }
        public Meal (int mealn,  Product  prod,  int  kkal,decimal amount)
        {
            this.MealN = mealn;
            this.Product = prod;
            this.Kkal = kkal;
            this.Amount = amount;
        }
        public Meal(int mealn, Product prod, int kkal, DateTime dat)
        {
            this.MealN = mealn;
            this.Product = prod;
            this.Kkal = kkal;
            this.date = dat;
        }
        public Meal() { }
    }
}

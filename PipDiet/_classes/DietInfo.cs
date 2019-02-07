using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipDiet 
{
    public class DietInfo
    {
        public int DayN { get; set; }
        public int MealN { get; set; }
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public int Kkal { get; set; }
        public DietInfo() { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipDiet
{
    public class Diet
    {
        public int DietId { get; set; }
        public int DayNum { get; set; }
        public Meal MealN { get; set; }
        public int Kkal { get; set; }
        public Diet (int day, Meal meal,int kkal)
        {
            this.DayNum = day;
            this.MealN = meal;
            this.Kkal = kkal;
        }
        public Diet() { }

    }
}

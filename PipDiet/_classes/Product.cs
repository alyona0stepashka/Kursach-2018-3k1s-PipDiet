using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipDiet
{
    public class Product
    {
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public double Kkal { get; set; }
        public Product(int id, int usid, string name, double kkal)
        {
            this.ProductID = id;
            this.UserID = usid;
            this.Name = name;
            this.Kkal = kkal;
        }
        public Product() { }
        public override string ToString()
        {
            return this.Name;
        }
    }
}

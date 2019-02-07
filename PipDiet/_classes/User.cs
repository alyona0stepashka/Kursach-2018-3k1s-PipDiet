using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PipDiet
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Sex { get; set; }
        public int Birthday { get; set; }
        public decimal Growth { get; set; }
        public decimal IdealWeight { get; set; }
        public decimal DailyCaloriesNormEasy { get; set; }
        public decimal DailyCaloriesNormNorm { get; set; }
        public decimal DailyCaloriesNormHard { get; set; }
        public int Role { get; set; }
        public User(int id, string login, string password, string sex, int birthday,
                    decimal growth, decimal idealweight, decimal dailyCaloriesNormEasy, decimal dailyCaloriesNormNorm, decimal dailyCaloriesNormHard, int Role)
        {
            this.UserId = id;
            this.Login = login;
            this.Password = password;
            this.Sex = sex;
            this.Birthday = birthday;
            this.Growth = growth;
            this.IdealWeight = idealweight;
            this.DailyCaloriesNormEasy = dailyCaloriesNormEasy;
            this.DailyCaloriesNormNorm = dailyCaloriesNormNorm;
            this.DailyCaloriesNormHard = dailyCaloriesNormHard;
            this.Role = Role;
        }

        public User() { }
    }
}

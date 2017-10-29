using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new JobPortalDbContext())
            {
                System.Console.WriteLine("Pocet zamestanavatelov:");
                System.Console.WriteLine(db.Employers.Count());
            }

            System.Console.ReadLine();

        }
    }
}

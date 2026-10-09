using System;
using System.IO;
using AdmissionEF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AdmissionEF
{
    class Program
    {
        static AdmissionContext CreateContext()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AdmissionContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AdmissionContext(optionsBuilder.Options);
        }

        static void Main(string[] args)
        {
            using (var db = CreateContext())
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                Queries.Query1_AllFaculties(db); Console.ReadKey();
                Queries.Query2_FilteredSpecialties(db); Console.ReadKey();
                Queries.Query3_ApplicationsBySpecialty(db); Console.ReadKey();
                Queries.Query4_SpecialtyAndFaculty(db); Console.ReadKey();
                Queries.Query5_ApplicationsWithFilter(db); Console.ReadKey();
                Queries.Query6_InsertFaculty(db); Console.ReadKey();
                Queries.Query7_InsertSpecialty(db); Console.ReadKey();
                Queries.Query8_DeleteFaculty(db); Console.ReadKey();
                Queries.Query9_DeleteSpecialty(db); Console.ReadKey();
                Queries.Query10_UpdateApplications(db); Console.ReadKey();
            }
        }
    }
}
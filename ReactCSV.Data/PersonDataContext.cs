using Microsoft.EntityFrameworkCore;
using ReactCSV.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactCandidatetracker.Data
{
    public class PersonDataContext :DbContext
    {
        private readonly string _connectionString;
        public PersonDataContext(string connStr)
        {
            _connectionString = connStr;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        public DbSet<Person> People { get; set; }
    }
}

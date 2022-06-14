using CsvHelper;
using Microsoft.EntityFrameworkCore;
using ReactCandidatetracker.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactCSV.Data
{
    public class PeopleRepository
    {
        private string _ConStr;
        public PeopleRepository(string conn)
        {
            _ConStr = conn;
        }
        public string GeneratetPeople(int amount)
        {
            return GetCsv(Enumerable.Range(1, amount).Select(_ =>
           {
               return new Person
               {
                   FirstName = Faker.Name.First(),
                   LastName = Faker.Name.Last(),
                   Address = Faker.Address.StreetAddress(),
                   Email = Faker.Internet.Email(),
                   Age = Faker.RandomNumber.Next(10, 85)
               };
           }).ToList());
        }

        public string GetCsv(List<Person> people)
        {
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);
            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(people);
            return builder.ToString();
        }

        public List<Person> GetfromCsvBytes(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            var streamReader = new StreamReader(memoryStream);
            using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            return reader.GetRecords<Person>().ToList();
        }
        public void AddToDb(List<Person> people)
        {
            using var ctx = new PersonDataContext(_ConStr);
            ctx.People.AddRange(people);
            ctx.SaveChanges();
        }
        public List<Person> GetPeopleFromDb()
        {
            using var ctx = new PersonDataContext(_ConStr);
            return ctx.People.ToList();
        }
        public void Delete()
        {
            using var ctx = new PersonDataContext(_ConStr);
            ctx.Database.ExecuteSqlInterpolated($"DELETE from people");
            ctx.SaveChanges();
        }
    }
}

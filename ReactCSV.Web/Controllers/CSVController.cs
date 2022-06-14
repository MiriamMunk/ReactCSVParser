using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReactCSV.Data;
using ReactCSV.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactCSV.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVController : ControllerBase
    {
        private readonly string _connectionString;
        public CSVController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [Route("generatePeople")]
        [HttpGet]
        public IActionResult GeneratePeople(int amount)
        {
            var repo = new PeopleRepository(_connectionString);
            var peopleCsv = repo.GeneratetPeople(amount);
            byte[] bytes = Encoding.UTF8.GetBytes(peopleCsv);
            return File(bytes, "text/csv", "people.csv");
        }

        [HttpPost]
        [Route("upload")]
        public void Upload(UploadViewModel viewModel)
        {
            var repo = new PeopleRepository(_connectionString);
            int index = viewModel.Base64File.IndexOf(",") + 1;
            string base64 = viewModel.Base64File.Substring(index);
            byte[] imageBytes = Convert.FromBase64String(base64);
            var peopleList = repo.GetfromCsvBytes(imageBytes);
            repo.AddToDb(peopleList);
        }

        [Route("getPeople")]
        [HttpGet]
        public List<Person> GetPeople()
        {
            var repo = new PeopleRepository(_connectionString);
            return repo.GetPeopleFromDb();
        }

        [Route("delete")]
        [HttpGet]
        public void Delete()
        {
            var repo = new PeopleRepository(_connectionString);
            repo.Delete();
        }
    }
}

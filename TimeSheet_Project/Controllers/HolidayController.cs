using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TimeSheet_Project.Models;

namespace TimeSheet_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        [HttpPost]
        [Route("AddHoliday")]
        public IActionResult AddHoliday([FromBody] string newHoliday)
        {
            //    HolidayHelper.EnsureHolidaysForCurrentYear();

           

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resourses", "Holidays.json");

            List<DateTime> holidays = new();
            if (System.IO.File.Exists(filePath))
            {
                var json = System.IO.File.ReadAllText(filePath);
                holidays = JsonConvert.DeserializeObject<List<DateTime>>(json);
            }

            // Accept multiple formats: dd-MM-yyyy or yyyy-MM-dd
            DateTime holidayDate;
            string[] acceptedFormats = { "dd-MM-yyyy", "yyyy-MM-dd" };

            if (!DateTime.TryParseExact(newHoliday, acceptedFormats, null, System.Globalization.DateTimeStyles.None, out holidayDate))
                return BadRequest("Invalid date format. Use 'yyyy-MM-dd' or 'dd-MM-yyyy'.");

            // Save date in standard format: yyyy-MM-dd
            if (holidays.Any(h => h.Date == holidayDate.Date))
                return BadRequest("Holiday already exists.");

            holidays.Add(holidayDate.Date);
            System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(holidays, Formatting.Indented));

            return Ok("Holiday added successfully.");
        }

       
        [HttpGet]
        [Route("GetAllHolidays")]
        public IActionResult GetAllHolidays()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resourses", "Holidays.json");

            if (!System.IO.File.Exists(filePath))
                return BadRequest("Holiday file not found.");

            var json = System.IO.File.ReadAllText(filePath);

            try
            {
                var holidays = JsonConvert.DeserializeObject<List<DateTime>>(json);
                var formattedDates = holidays.Select(d => d.ToString("dd-MM-yyyy")).ToList();
                return Ok(new {Holidays= formattedDates });
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to read holidays: " + ex.Message);
            }
        }



        [HttpDelete]
    [Route("RemoveHoliday")]
    public IActionResult RemoveHoliday([FromBody] string removeDate)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resourses", "Holidays.json");

        if (!System.IO.File.Exists(filePath))
            return BadRequest("Holiday file not found.");

        var json = System.IO.File.ReadAllText(filePath);
        var holidays = JsonConvert.DeserializeObject<List<DateTime>>(json);

        // Try parsing with common formats
        string[] formats = { "yyyy-MM-dd", "dd-MM-yyyy" };
        if (!DateTime.TryParseExact(removeDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateToRemove))
            return BadRequest("Invalid date format. Use yyyy-MM-dd or dd-MM-yyyy.");

        // Normalize both to Date-only for comparison
        dateToRemove = dateToRemove.Date;
        var matched = holidays.FirstOrDefault(h => h.Date == dateToRemove);

        if (matched == default(DateTime))
            return BadRequest("Date not found in holiday list.");

        holidays.Remove(matched);

        System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(holidays, Formatting.Indented));

        return Ok("Holiday removed successfully.");
    }



        [HttpPost]
        [Route("AddFixedHolidays")]
        public IActionResult AddFixedHolidays()
        {
            string result = HolidayHelper.EnsureHolidaysForCurrentYear();
            return Ok(new { message = result });
        }


        //[HttpPost]
        //[Route("AddSaturdayHolidays")]
        //public IActionResult AddSaturdayHolidays([FromBody] List<int> selectedWeeks)
        //{
        //    string basePath = Path.Combine(Directory.GetCurrentDirectory(), "Resourses");
        //    string filePath = Path.Combine(basePath, "Holidays.json");
        //    int currentYear = DateTime.Now.Year;

        //    List<DateTime> holidays = System.IO.File.Exists(filePath)
        //        ? JsonConvert.DeserializeObject<List<DateTime>>(System.IO.File.ReadAllText(filePath))
        //        : new List<DateTime>();

        //    HolidayHelper.AddSaturdayHolidays(selectedWeeks, holidays, currentYear);

        //    // Save updated holidays list
        //    System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(holidays, Formatting.Indented));

        //    return Ok("Saturday holidays added successfully.");
        //}
        [HttpPost]
        [Route("AddSaturdayHolidays")]
        public IActionResult AddSaturdayHolidays([FromBody] List<int> selectedWeeks)
        {
            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "Resourses");
            string filePath = Path.Combine(basePath, "Holidays.json");
            int currentYear = DateTime.Now.Year;

            List<DateTime> holidays = System.IO.File.Exists(filePath)
                ? JsonConvert.DeserializeObject<List<DateTime>>(System.IO.File.ReadAllText(filePath))
                : new List<DateTime>();

            string result = HolidayHelper.AddOrUpdateSaturdayHolidays(selectedWeeks, holidays, currentYear);

            System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(holidays, Formatting.Indented));

            return Ok(result);
        }



    }
}

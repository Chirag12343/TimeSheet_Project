using System.Text.Json;

namespace TimeSheet_Project.Models
{
    public class HolidayService
    {
        public List<DateTime> Holidays { get; private set; }
        public HolidayService(IWebHostEnvironment env)
        {
            // Path to the file inside the Resources folder
            var path = Path.Combine(env.ContentRootPath, "Resources", "holidays.json");

            if (!File.Exists(path))
            {
                Holidays = new List<DateTime>();
                return;
            }

            var json = File.ReadAllText(path);
            Holidays = JsonSerializer.Deserialize<List<DateTime>>(json)
                      ?? new List<DateTime>();
        }


        public bool IsHoliday(DateTime date)
        {
            return Holidays.Contains(date.Date);
        }

    }
}

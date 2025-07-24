using Newtonsoft.Json;

namespace TimeSheet_Project.Models
{
    public class HolidayHelper
    {
        public static string EnsureHolidaysForCurrentYear()
        {
            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "Resourses");
            string filePath = Path.Combine(basePath, "Holidays.json");

            int currentYear = DateTime.Now.Year;

            // Prepare fixed holidays
            List<DateTime> fixedHolidays = new List<DateTime>
    {
        new DateTime(currentYear, 1, 26),
        new DateTime(currentYear, 3, 29),
        new DateTime(currentYear, 8, 15),
        new DateTime(currentYear, 10, 2),
        new DateTime(currentYear, 12, 25)
    };

            // Prepare Sundays
            List<DateTime> sundays = new List<DateTime>();
            DateTime startDate = new DateTime(currentYear, 1, 1);
            DateTime endDate = new DateTime(currentYear, 12, 31);

            while (startDate <= endDate)
            {
                if (startDate.DayOfWeek == DayOfWeek.Sunday)
                    sundays.Add(startDate);
                startDate = startDate.AddDays(1);
            }

            // Combine all holidays
            List<DateTime> allHolidays = fixedHolidays.Concat(sundays).ToList();

            // Load existing holidays from JSON
            List<DateTime> existingHolidays = new();
            if (File.Exists(filePath))
            {
                string existingJson = File.ReadAllText(filePath);
                existingHolidays = JsonConvert.DeserializeObject<List<DateTime>>(existingJson) ?? new List<DateTime>();
            }

            bool anyAdded = false;

            foreach (DateTime h in allHolidays)
            {
                if (!existingHolidays.Any(e => e.Date == h.Date))
                {
                    existingHolidays.Add(h);
                    anyAdded = true;
                }
            }

            if (anyAdded)
            {
                existingHolidays.Sort((a, b) => a.CompareTo(b));
                File.WriteAllText(filePath, JsonConvert.SerializeObject(existingHolidays, Formatting.Indented));
                return " holidays  added successfully.";
            }

            return "All fixed holidays and Sundays are already present.";
        }





        public static string AddOrUpdateSaturdayHolidays(List<int> selectedWeeks, List<DateTime> holidays, int year)
        {
            List<DateTime> allSelectedSaturdays = new();
            List<DateTime> toBeRemoved = new();
            List<DateTime> toBeAdded = new();

            for (int month = 1; month <= 12; month++)
            {
                List<DateTime> saturdays = new();
                int daysInMonth = DateTime.DaysInMonth(year, month);

                for (int day = 1; day <= daysInMonth; day++)
                {
                    DateTime date = new DateTime(year, month, day);
                    if (date.DayOfWeek == DayOfWeek.Saturday)
                        saturdays.Add(date);
                }

                foreach (int week in selectedWeeks)
                {
                    if (week - 1 < saturdays.Count)
                    {
                        DateTime selectedSaturday = saturdays[week - 1];
                        allSelectedSaturdays.Add(selectedSaturday);
                    }
                }
            }

            // Only remove previously added Saturdays (not fixed holidays)
            List<DateTime> existingSaturdays = holidays
                .Where(d => d.DayOfWeek == DayOfWeek.Saturday)
                .ToList();

            // Find Saturdays to remove (not in new list)
            toBeRemoved = existingSaturdays
                .Where(d => !allSelectedSaturdays.Any(s => s.Date == d.Date))
                .ToList();

            // Remove old Saturdays
            foreach (var d in toBeRemoved)
            {
                holidays.RemoveAll(h => h.Date == d.Date);
            }

            // Add new Saturdays not already in list
            foreach (var d in allSelectedSaturdays)
            {
                if (!holidays.Any(h => h.Date == d.Date))
                    toBeAdded.Add(d);
            }

            foreach (var d in toBeAdded)
            {
                holidays.Add(d);
            }

            if (toBeAdded.Count == 0 && toBeRemoved.Count == 0)
                return "Saturday holidays already match selected weeks.";
            else
                return $"Updated Saturday holidays. Added: {toBeAdded.Count}, Removed: {toBeRemoved.Count}";
        }



    }
}

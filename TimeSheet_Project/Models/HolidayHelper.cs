using Newtonsoft.Json;

namespace TimeSheet_Project.Models
{
    public class HolidayHelper
    {




        //public static void EnsureHolidaysForCurrentYear()
        //{
        //    string basePath = Path.Combine(Directory.GetCurrentDirectory(), "Resourses");
        //    string filePath = Path.Combine(basePath, "Holidays.json");
        //    string metadataPath = Path.Combine(basePath, "holiday_metadata.json");

        //    int currentYear = DateTime.Now.Year;

        //    // Read metadata
        //    string metadataJson = File.Exists(metadataPath) ? File.ReadAllText(metadataPath) : "{}";
        //    Dictionary<string, List<int>> metadata = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(metadataJson);
        //    if (metadata == null)
        //        metadata = new Dictionary<string, List<int>>();

        //    if (!metadata.ContainsKey("yearsAdded"))
        //        metadata["yearsAdded"] = new List<int>();

        //    if (!metadata["yearsAdded"].Contains(currentYear))
        //    {
        //        // Step 1: Fixed-date holidays
        //        List<DateTime> newHolidays = new List<DateTime>();
        //        newHolidays.Add(new DateTime(currentYear, 1, 26));
        //        newHolidays.Add(new DateTime(currentYear, 3, 29));
        //        newHolidays.Add(new DateTime(currentYear, 8, 15));
        //        newHolidays.Add(new DateTime(currentYear, 10, 2));
        //        newHolidays.Add(new DateTime(currentYear, 12, 25));

        //        // Step 2: Add 2nd and 4th Saturdays
        //        for (int month = 1; month <= 12; month++)
        //        {
        //            List<DateTime> saturdays = new List<DateTime>();
        //            int daysInMonth = DateTime.DaysInMonth(currentYear, month);
        //            for (int day = 1; day <= daysInMonth; day++)
        //            {
        //                DateTime date = new DateTime(currentYear, month, day);
        //                if (date.DayOfWeek == DayOfWeek.Saturday)
        //                {
        //                    saturdays.Add(date);
        //                }
        //            }

        //            if (saturdays.Count >= 4)
        //            {
        //                newHolidays.Add(saturdays[1]); // 2nd Saturday
        //                newHolidays.Add(saturdays[3]); // 4th Saturday
        //            }
        //        }

        //        // Step 3: Load existing holidays from file
        //        List<DateTime> holidays = new List<DateTime>();
        //        if (File.Exists(filePath))
        //        {
        //            string existingJson = File.ReadAllText(filePath);
        //            holidays = JsonConvert.DeserializeObject<List<DateTime>>(existingJson);
        //        }

        //        // Step 4: Add only non-duplicate holidays
        //        for (int i = 0; i < newHolidays.Count; i++)
        //        {
        //            DateTime h = newHolidays[i];
        //            bool exists = false;

        //            for (int j = 0; j < holidays.Count; j++)
        //            {
        //                if (holidays[j].Date == h.Date)
        //                {
        //                    exists = true;
        //                    break;
        //                }
        //            }

        //            if (!exists)
        //            {
        //                holidays.Add(h);
        //            }
        //        }

        //        // Step 5: Save holidays to file
        //        File.WriteAllText(filePath, JsonConvert.SerializeObject(holidays, Formatting.Indented));

        //        // Step 6: Update metadata
        //        metadata["yearsAdded"].Add(currentYear);
        //        File.WriteAllText(metadataPath, JsonConvert.SerializeObject(metadata, Formatting.Indented));
        //    }
        //}

        //public static bool EnsureHolidaysForCurrentYear()
        //{
        //    string basePath = Path.Combine(Directory.GetCurrentDirectory(), "Resourses");
        //    string filePath = Path.Combine(basePath, "Holidays.json");
        //    string metadataPath = Path.Combine(basePath, "holiday_metadata.json");

        //    int currentYear = DateTime.Now.Year;

        //    string metadataJson = File.Exists(metadataPath) ? File.ReadAllText(metadataPath) : "{}";
        //    Dictionary<string, List<int>> metadata = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(metadataJson)
        //                                           ?? new Dictionary<string, List<int>>();

        //    if (!metadata.ContainsKey("yearsAdded"))
        //        metadata["yearsAdded"] = new List<int>();

        //    // 🔁 Only if the current year is not added already
        //    if (!metadata["yearsAdded"].Contains(currentYear))
        //    {
        //        List<DateTime> newHolidays = new List<DateTime>
        //{
        //    new DateTime(currentYear, 1, 26),
        //    new DateTime(currentYear, 3, 29),
        //    new DateTime(currentYear, 8, 15),
        //    new DateTime(currentYear, 10, 2),
        //    new DateTime(currentYear, 12, 25)
        //};

        //        List<DateTime> holidays = File.Exists(filePath)
        //            ? JsonConvert.DeserializeObject<List<DateTime>>(File.ReadAllText(filePath))
        //            : new List<DateTime>();

        //        foreach (var h in newHolidays)
        //        {
        //            if (!holidays.Any(existing => existing.Date == h.Date))
        //                holidays.Add(h);
        //        }

        //        File.WriteAllText(filePath, JsonConvert.SerializeObject(holidays, Formatting.Indented));

        //        metadata["yearsAdded"].Add(currentYear);
        //        File.WriteAllText(metadataPath, JsonConvert.SerializeObject(metadata, Formatting.Indented));

        //        return true; // ✅ Year was new, holidays added
        //    }

        //    return false; // ❌ Year already processed
        //}


        //public static int? EnsureHolidaysForCurrentYear()
        //{
        //    string basePath = Path.Combine(Directory.GetCurrentDirectory(), "Resourses");
        //    string filePath = Path.Combine(basePath, "Holidays.json");
        //    string metadataPath = Path.Combine(basePath, "holiday_metadata.json");

        //    int currentYear = DateTime.Now.Year;

        //    string metadataJson = File.Exists(metadataPath) ? File.ReadAllText(metadataPath) : "{}";
        //    Dictionary<string, List<int>> metadata = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(metadataJson)
        //                                           ?? new Dictionary<string, List<int>>();

        //    if (!metadata.ContainsKey("yearsAdded"))
        //        metadata["yearsAdded"] = new List<int>();

        //    // 🔁 Only if the current year is not added already
        //    if (!metadata["yearsAdded"].Contains(currentYear))
        //    {
        //        List<DateTime> newHolidays = new List<DateTime>
        //{
        //    new DateTime(currentYear, 1, 26),
        //    new DateTime(currentYear, 3, 29),
        //    new DateTime(currentYear, 8, 15),
        //    new DateTime(currentYear, 10, 2),
        //    new DateTime(currentYear, 12, 25)
        //};

        //        List<DateTime> holidays = File.Exists(filePath)
        //            ? JsonConvert.DeserializeObject<List<DateTime>>(File.ReadAllText(filePath))
        //            : new List<DateTime>();

        //        foreach (var h in newHolidays)
        //        {
        //            if (!holidays.Any(existing => existing.Date == h.Date))
        //                holidays.Add(h);
        //        }

        //        File.WriteAllText(filePath, JsonConvert.SerializeObject(holidays, Formatting.Indented));
        //        metadata["yearsAdded"].Add(currentYear);
        //        File.WriteAllText(metadataPath, JsonConvert.SerializeObject(metadata, Formatting.Indented));

        //        return currentYear; // 🎯 Return year if newly added
        //    }

        //    return null; // Year already exists
        //}




        //    public static string EnsureHolidaysForCurrentYear()
        //    {
        //        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "Resourses");
        //        string filePath = Path.Combine(basePath, "Holidays.json");
        //        string metadataPath = Path.Combine(basePath, "holiday_metadata.json");

        //        int currentYear = DateTime.Now.Year;

        //        string metadataJson = File.Exists(metadataPath) ? File.ReadAllText(metadataPath) : "{}";
        //        Dictionary<string, List<int>> metadata = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(metadataJson)
        //                                               ?? new Dictionary<string, List<int>>();

        //        if (!metadata.ContainsKey("yearsAdded"))
        //            metadata["yearsAdded"] = new List<int>();

        //        List<DateTime> newHolidays = new List<DateTime>
        //{
        //    new DateTime(currentYear, 1, 26),
        //    new DateTime(currentYear, 3, 29),
        //    new DateTime(currentYear, 8, 15),
        //    new DateTime(currentYear, 10, 2),
        //    new DateTime(currentYear, 12, 25)
        //};

        //        List<DateTime> holidays = File.Exists(filePath)
        //            ? JsonConvert.DeserializeObject<List<DateTime>>(File.ReadAllText(filePath))
        //            : new List<DateTime>();

        //        bool anyAdded = false;

        //        foreach (var h in newHolidays)
        //        {
        //            if (!holidays.Any(existing => existing.Date == h.Date))
        //            {
        //                holidays.Add(h);
        //                anyAdded = true;
        //            }
        //        }

        //        if (anyAdded)
        //        {
        //            File.WriteAllText(filePath, JsonConvert.SerializeObject(holidays, Formatting.Indented));

        //            if (!metadata["yearsAdded"].Contains(currentYear))
        //            {
        //                metadata["yearsAdded"].Add(currentYear);
        //                File.WriteAllText(metadataPath, JsonConvert.SerializeObject(metadata, Formatting.Indented));
        //            }

        //            return "Holidays added for current year.";
        //        }

        //        return "Fixed holidays already exist in JSON.";
        //    }

        //    public static string EnsureHolidaysForCurrentYear()
        //    {
        //        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "Resourses");
        //        string filePath = Path.Combine(basePath, "Holidays.json");
        //        string metadataPath = Path.Combine(basePath, "holiday_metadata.json");

        //        int currentYear = DateTime.Now.Year;

        //        // Load metadata
        //        string metadataJson = File.Exists(metadataPath) ? File.ReadAllText(metadataPath) : "{}";
        //        Dictionary<string, List<int>> metadata = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(metadataJson)
        //                                               ?? new Dictionary<string, List<int>>();

        //        if (!metadata.ContainsKey("yearsAdded"))
        //            metadata["yearsAdded"] = new List<int>();

        //        // Prepare fixed holidays
        //        List<DateTime> fixedHolidays = new List<DateTime>
        //{
        //    new DateTime(currentYear, 1, 26),
        //    new DateTime(currentYear, 3, 29),
        //    new DateTime(currentYear, 8, 15),
        //    new DateTime(currentYear, 10, 2),
        //    new DateTime(currentYear, 12, 25)
        //};

        //        // Prepare Sundays
        //        List<DateTime> sundays = new List<DateTime>();
        //        DateTime startDate = new DateTime(currentYear, 1, 1);
        //        DateTime endDate = new DateTime(currentYear, 12, 31);

        //        while (startDate <= endDate)
        //        {
        //            if (startDate.DayOfWeek == DayOfWeek.Sunday)
        //            {
        //                sundays.Add(startDate);
        //            }
        //            startDate = startDate.AddDays(1);
        //        }

        //        // Combine holidays
        //        List<DateTime> allHolidays = new List<DateTime>();
        //        foreach (DateTime d in fixedHolidays)
        //            allHolidays.Add(d);
        //        foreach (DateTime d in sundays)
        //            allHolidays.Add(d);

        //        // Read existing holidays from file
        //        List<DateTime> existingHolidays = new List<DateTime>();
        //        if (File.Exists(filePath))
        //        {
        //            string existingJson = File.ReadAllText(filePath);
        //            existingHolidays = JsonConvert.DeserializeObject<List<DateTime>>(existingJson) ?? new List<DateTime>();
        //        }

        //        bool anyAdded = false;
        //        foreach (DateTime h in allHolidays)
        //        {
        //            bool found = false;
        //            foreach (DateTime existing in existingHolidays)
        //            {
        //                if (existing.Date == h.Date)
        //                {
        //                    found = true;
        //                    break;
        //                }
        //            }
        //            if (!found)
        //            {
        //                existingHolidays.Add(h);
        //                anyAdded = true;
        //            }
        //        }

        //        // Save holidays if any were added
        //        if (anyAdded)
        //        {
        //            existingHolidays.Sort((a, b) => a.CompareTo(b));
        //            File.WriteAllText(filePath, JsonConvert.SerializeObject(existingHolidays, Formatting.Indented));

        //            // Save year metadata
        //            bool yearAlreadyMarked = false;
        //            foreach (int y in metadata["yearsAdded"])
        //            {
        //                if (y == currentYear)
        //                {
        //                    yearAlreadyMarked = true;
        //                    break;
        //                }
        //            }

        //            if (!yearAlreadyMarked)
        //            {
        //                metadata["yearsAdded"].Add(currentYear);
        //                File.WriteAllText(metadataPath, JsonConvert.SerializeObject(metadata, Formatting.Indented));
        //            }

        //            return "Fixed holidays and Sundays added for current year.";
        //        }

        //        return "Fixed holidays and Sundays already exist in JSON.";
        //    }



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

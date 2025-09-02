using EDULIGHT.Configrations;
using EDULIGHT.Entities.ContentData;
using EllipticCurve;
using System.Text.Json;

namespace EDULIGHT.ContentDataSeed
{
    public static class EdulightDbContextSeed
    {
        public async static Task SeedAsync(EdulightDbContext _context)
        {
            if (_context.Categories.Count() == 0)
            {
                var categoriesData = File.ReadAllText(@"ContentDataSeed\JSON\Category.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
                if (categories != null && categories.Count() > 0)
                {
                    await _context.Categories.AddRangeAsync(categories);
                    await _context.SaveChangesAsync();
                }
            }
            if (_context.Roadmaps.Count() == 0)
            {
                var roadmapsData = File.ReadAllText(@"ContentDataSeed\JSON\Roadmap.json");
                var roadmaps = JsonSerializer.Deserialize<List<Roadmap>>(roadmapsData);
                if (roadmaps != null && roadmaps.Count() > 0)
                {
                    await _context.Roadmaps.AddRangeAsync(roadmaps);
                    await _context.SaveChangesAsync();
                }
            }
            if (_context.Courses.Count() == 0)
            {
                var coursesData = File.ReadAllText(@"ContentDataSeed\JSON\Course.json");
                var course = JsonSerializer.Deserialize<List<Course>>(coursesData);
                try
                {
                    if (course != null && course.Count() > 0)
                    {
                        await _context.Courses.AddRangeAsync(course);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while adding courses: {ex.Message}");
                }

            }

        }
    }
}

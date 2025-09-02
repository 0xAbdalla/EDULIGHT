using AutoMapper;
using EDULIGHT.Configrations;
using EDULIGHT.Dto.Category;
using EDULIGHT.Dto.Courses;
using EDULIGHT.Dto.Roadmap;
using EDULIGHT.Entities.ContentData;
using EDULIGHT.Entities.Users;
using EDULIGHT.Repositories;
using EDULIGHT.Repositories.Specification;
using Humanizer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Stripe;
using System.Collections.Generic;
using File = System.IO.File;

namespace EDULIGHT.Services.AppService
{
    public class AppService : IAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly EdulightDbContext _context;

        public AppService(IUnitOfWork unitOfWork,IMapper mapper,IWebHostEnvironment webHostEnvironment,EdulightDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }
        public async Task<IEnumerable<GetCategoryDto>> GetAllCategoriesAsync()
        {
            var spec = new CategorySpecification();
            return _mapper.Map<IEnumerable<GetCategoryDto>>(await _unitOfWork.GetGenericRepositories<Category>().GetAllAsyncWithSpec(spec));
        }
        public async Task<IEnumerable<GetCoursesDto>> GetAllCoursesCategoriesAsync(int id)
        {
            return _mapper.Map<IEnumerable<GetCoursesDto>>(await _unitOfWork.GetGenericRepositories<Course>().GetCoursesCategoriesAsync(id));
        }
        public async Task<IEnumerable<GetRoadmapDto>> GetAllRoadmapsAsync()
        {
            var spec = new RoadmapSpecification();
            return _mapper.Map<IEnumerable<GetRoadmapDto>>(await _unitOfWork.GetGenericRepositories<Roadmap>().GetAllAsyncWithSpec(spec));
        }
        public async Task<GetCoursesDto> GetAboutCourseAsync(int id)
        {
            var spec = new CourseSpecificaion(id);
            var getcourse = await _unitOfWork.GetGenericRepositories<Course>().GetByIdAsyncWithSpec(spec);
            return _mapper.Map<GetCoursesDto>(getcourse);
        }
        public async Task<IEnumerable<GetCoursesDto>> FilterCoursesAsync(GetFiltercourseDto? dto)
        {
            var spec = new CourseSpecificaion(dto);
            return _mapper.Map<IEnumerable<GetCoursesDto>> (await _unitOfWork.GetGenericRepositories<Course>().GetAllAsyncWithSpec(spec));
        }
        public async Task<GetCategoryDto> AddCategoryAsync(PostCategoryDto dto)
        {
            var localfilepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Categories", $"{dto.Poster.FileName}");
            using var datastream = new FileStream(localfilepath, FileMode.Create);
            await dto.Poster.CopyToAsync(datastream);
            var category = new Category
            {
               Name = dto.Name,
               Poster = dto.Poster,
               PosterURL = $"/Images/Categories/{dto.Poster.FileName}"
            };
            var AddCategory = await _unitOfWork.GetGenericRepositories<Category>().AddAsync(category);
            await _unitOfWork.CompleteAsync();
            var MappedCategory = _mapper.Map<GetCategoryDto>(AddCategory);
            return MappedCategory;
        }
        public async Task<GetCoursesDto> AddCourseAsync(PostCoursesDto dto,string instructorname)
        {
            var localfilepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Courses", $"{dto.Poster.FileName}");
            using var datastream = new FileStream(localfilepath, FileMode.Create);
            await dto.Poster.CopyToAsync(datastream);
            var getidcategory = await _context.Categories.FirstOrDefaultAsync(c=>c.Name == dto.Category);
            var getidroadmap = await _context.Roadmaps.FirstOrDefaultAsync(r=>r.Category_id == getidcategory.Category_Id);
            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Poster = dto.Poster,
                PosterURL = $"Images/Courses/{dto.Poster.FileName}",
                Duration = dto.Duration,
                Level = dto.Level,
                Language = dto.Language,
                category_id =getidcategory.Category_Id,
                roadmap_id = getidroadmap.Roadmap_Id,
                CongratulationMessage = dto.CongratulationMessage,
                WelcomeMessage = dto.WelcomeMessage,
                InstructorId = instructorname
            };
            var AddCourse = await _unitOfWork.GetGenericRepositories<Course>().AddAsync(_mapper.Map<Course>(course));
            await _unitOfWork.CompleteAsync();
            foreach (var sectionDto in dto.Sections)
            {
                var section = new Sections
                {
                    Title = sectionDto.Title,
                    CourseId = AddCourse.CourseId
                };
                var addedSection = await _unitOfWork.GetGenericRepositories<Sections>().AddAsync(section);
                await _unitOfWork.CompleteAsync();
                foreach (var contentCourseDto in sectionDto.ContentCourse)
                {
                    var localfilepathvedio = Path.Combine(_webHostEnvironment.WebRootPath, "Vedios", "ContentCourses", $"{contentCourseDto.Content.FileName}");
                    using var datastreamvedio = new FileStream(localfilepath, FileMode.Create);
                    await contentCourseDto.Content.CopyToAsync(datastreamvedio);
                    var contentCourse = new ContentCourse
                    {
                        ContentTitle = contentCourseDto.ContentTitle,
                        ContentType = contentCourseDto.ContentType,
                        ContentUrl = $"Videios/ContentCourse/{contentCourseDto.Content.FileName}"
                    };
                    contentCourse.SectionId = addedSection.Id;
                    await _unitOfWork.GetGenericRepositories<ContentCourse>().AddAsync(contentCourse);
                }
            }

            await _unitOfWork.CompleteAsync();

            var MappedCourse = _mapper.Map<GetCoursesDto>(AddCourse);
            return MappedCourse;
        }
        public async Task<GetCoursesDto> UpdateCourse(PostCoursesDto dto)
        {
            var localfilepath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Courses", $"{dto.Poster.FileName}");
            using var datastream = new FileStream(localfilepath, FileMode.Create);
            await dto.Poster.CopyToAsync(datastream);
            var getcourse = await _context.Courses.FirstOrDefaultAsync(c =>c.Title == dto.Title);
            if (getcourse == null) return null;
            getcourse.Title = dto.Title;
            getcourse.Description = dto.Description;
            getcourse.Price = dto.Price;
            getcourse.Poster = dto.Poster;
            getcourse.PosterURL = $"Images/Courses/{dto.Poster.FileName}";
            getcourse.Duration = dto.Duration;
            getcourse.Level = dto.Level;
            getcourse.Language = dto.Language;
            getcourse.CongratulationMessage = dto.CongratulationMessage;
            getcourse.WelcomeMessage = dto.WelcomeMessage;
            getcourse.sections = new List<Sections>();
            foreach (var dtoSection in dto.Sections)
            {
                var contentCoursesTasks = dtoSection.ContentCourse.Select(async dtoContent =>
                {
                    var videoFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "Videos", "ContentCourses", $"{dtoContent.Content.FileName}");
                    using var videoStream = new FileStream(videoFilePath, FileMode.Create);
                    await dtoContent.Content.CopyToAsync(videoStream);
                    return new ContentCourse
                    {
                        ContentTitle = dtoContent.ContentTitle,
                        ContentType = dtoContent.ContentType,
                        ContentUrl = $"Videos/ContentCourse/{dtoContent.Content.FileName}"
                    };
                });
                // Await all tasks and convert to a list
                var contentCourses = await Task.WhenAll(contentCoursesTasks);
                getcourse.sections.Add(new Sections
                {
                    Title = dtoSection.Title,
                    ContentCourse = contentCourses.ToList(),
                    CourseId = getcourse.CourseId
                });
            }
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<GetCoursesDto>(getcourse);
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var getcourse = await _unitOfWork.GetGenericRepositories<Course>().GetByIdAsync(id);
            if (getcourse == null) return false;

            if (!string.IsNullOrEmpty(getcourse.PosterURL))
            {
                var posterFilePath = Path.Combine(_webHostEnvironment.WebRootPath, getcourse.PosterURL.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (File.Exists(posterFilePath))
                {
                    File.Delete(posterFilePath);
                }
            }
            if (getcourse.sections != null)
            {
                foreach (var section in getcourse.sections)
                {
                    if (section.ContentCourse != null)
                    {
                        foreach (var content in section.ContentCourse)
                        {
                            if (!string.IsNullOrEmpty(content.ContentUrl))
                            {
                                var videoFilePath = Path.Combine(_webHostEnvironment.WebRootPath, content.ContentUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                                if (File.Exists(videoFilePath))
                                {
                                    File.Delete(videoFilePath);
                                }
                            }
                        }
                    }
                }
            }
            await _unitOfWork.GetGenericRepositories<Course>().Delete(getcourse.CourseId);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<GetCoursesDto>> GetAllCoursesAsync(int? pageSize, int? pageIndex)
        {
            var spec = new CourseSpecificaion(pageSize.Value,pageIndex.Value);
            return _mapper.Map<IEnumerable<GetCoursesDto>>(await _unitOfWork.GetGenericRepositories<Course>().GetAllAsyncWithSpec(spec));
        }

        public async Task<IEnumerable<GetCoursesDto>> SearchWithCourses(string? name)
        {
            var spec = new CourseSpecificaion(name);
            return _mapper.Map<IEnumerable<GetCoursesDto>>(await _unitOfWork.GetGenericRepositories<Course>().GetAllAsyncWithSpec(spec));
        }

        public async Task<IEnumerable<GetCoursesDto>> GetAllCoursesRoadmapsAsync(int id)
        {
            return _mapper.Map<IEnumerable<GetCoursesDto>>(await _unitOfWork.GetGenericRepositories<Course>().GetCoursesRoadmapsAsync(id));
        }
    }
}

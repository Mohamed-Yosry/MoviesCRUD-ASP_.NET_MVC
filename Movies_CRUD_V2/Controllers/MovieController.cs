using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies_CRUD_V2.Models;
using Movies_CRUD_V2.ViewModel;

namespace Movies_CRUD_V2.Controllers
{
    public class MovieController : Controller
    {
        //the dbcontext instance
        private readonly MovieDbContext context;

        public MovieController(MovieDbContext contexet)
        {
            this.context = contexet;
        }

        //get all the movies if exist
        public async Task<IActionResult> Index()
        {
            var model = await context.Movie.ToListAsync();
            return View(model);
        }
        //the GET of Create 
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new MoviesViewModel()
            {
                //get all genres to send it with model for the droplist
                Geners = await context.Genre.OrderBy(m => m.Name).ToListAsync(),
            };
            return View("MoviesForm",model);
        }

        // the POST of create to store in the database
        [HttpPost]
        public async Task<IActionResult> Create(MoviesViewModel model)
        {
            // get the files in the form
            var file = Request.Form.Files;
            if(!file.Any())
            {
                model.Geners = await context.Genre.OrderBy(m => m.Name).ToListAsync();
                ModelState.AddModelError("Poster", "no file");
                return View("MoviesForm", model);
            }
            var poster = file.FirstOrDefault();

            using var dataStream = new MemoryStream();
            //store the image in data stream
            await poster.CopyToAsync(dataStream);
            //mapping from the ViewModel to Model
            Movies movie = new Movies()
            {
                Rate = model.Rate,
                Year = model.Year,
                StoryLine = model.StoryLine,
                Title = model.Title,
                GenreId = model.GenreId,
                Poster = dataStream.ToArray(),
            };

            context.Movie.Add(movie);
            context.SaveChanges();

            var movies = await context.Movie.ToListAsync();
            return View("Index", movies);
        }
        //the delete function
        public async Task<IActionResult> Delete(int id)
        {
            //find in the Database by id and if exist delete
            var movie = await context.Movie.FindAsync(id);

            if (movie == null)
                return NotFound();
            context.Movie.Remove(movie);
            context.SaveChanges();
            var model = await context.Movie.ToListAsync();
            return View("Index", model);
        }
        //get the element and send it to the edit Movie form to see the data and change it
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await context.Movie.FindAsync(id);
            if (movie == null)
                return NotFound();
            MoviesViewModel model = new MoviesViewModel() {
                Id = movie.Id,
                Title = movie.Title,
                StoryLine = movie.StoryLine,
                Geners = await context.Genre.OrderBy(m => m.Name).ToListAsync(),
                Year = movie.Year,
                Rate = movie.Rate,
                GenreId=movie.GenreId,
                Poster = movie.Poster
            };
            return View("MoviesForm", model);
        }
        //storing the new data after editing
        [HttpPost]
        public async Task<IActionResult> Edit(MoviesViewModel model)
        {
            var movie = await context.Movie.FindAsync(model.Id);
            movie.StoryLine = model.StoryLine;
            movie.Rate = model.Rate;
            movie.GenreId = model.GenreId;
            movie.Title = model.Title;
            movie.Year = model.Year;
            movie.Poster = model.Poster;

            var file = Request.Form.Files;
            if (!file.Any())
            {
                model.Geners = await context.Genre.OrderBy(m => m.Name).ToListAsync();
                ModelState.AddModelError("Poster", "no file");
                return View("MoviesForm", model);
            }
            else{
                var poster = file.FirstOrDefault();

                using var dataStream = new MemoryStream();
                await poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }

            context.SaveChanges();

            var movies = await context.Movie.ToListAsync();
            return View("Index", movies);
        }
    }
}

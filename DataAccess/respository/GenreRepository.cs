using CinemaObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.respository
{
    public class GenreRepository : IGenresRepository
    {
        public IEnumerable<Genre> GetAllGenres() => GenreDAO.Instance.GetAll();
    }
}

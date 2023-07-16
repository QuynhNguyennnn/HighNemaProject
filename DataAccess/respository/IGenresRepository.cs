using CinemaObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.respository
{
    public interface IGenresRepository
    {
        IEnumerable<Genre> GetAllGenres();
    }
}

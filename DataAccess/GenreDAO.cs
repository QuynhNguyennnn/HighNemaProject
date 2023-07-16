using CinemaObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GenreDAO
    {
        private static GenreDAO instance = null;
        public static readonly object instanceLock = new object();
        public static GenreDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new GenreDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<Genre> GetAll()
        {
            var genres = new List<Genre>();
            try
            {
                using var context = new CinemaProject_v4Context();
                genres = context.Genres.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return genres;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FoodHunter.Web.DataLayer;

namespace FoodHunter.Web.DataLayer
{
    class NewsRepository : INewsRepository
    {
        private readonly DataContext _context;

        public NewsRepository()
        {
            _context = new DataContext();
        }
        public List<News> GetAll()
        {
            return _context.Newses.ToList();
        }

        public News Get(int id)
        {
            return _context.Newses.SingleOrDefault(e => e.NewsId == id);
        }

        public int Insert(News news)
        {
            _context.Newses.Add(news);

            return _context.SaveChanges();
        }

        public int Update(News news)
        {
            News newsToUpdate = _context.Newses.SingleOrDefault(e => e.NewsId == news.NewsId);
            foreach (PropertyInfo property in typeof(News).GetProperties())
            {
                property.SetValue(newsToUpdate, property.GetValue(news, null), null);
            }

            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            News newsToDelete = _context.Newses.SingleOrDefault(e => e.NewsId == id);
            if (newsToDelete != null) _context.Newses.Remove(newsToDelete);

            return _context.SaveChanges();
        }
    }
}

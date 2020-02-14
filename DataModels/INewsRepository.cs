using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcadaAcademy.Models
{
    public interface INewsRepository
    {
        News AddNews(News newsDetails);
        News UpdateNews(News newsChanges);
        News DeleteNews(int id);
        News fetchNews(int id);
        IEnumerable<News> fetchAllNews();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entitys
{
    public class ArticleListModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string reporter { get; set; }
        public DateTime? write_date { get; set; }
        public DateTime? modify_date { get; set; }
        public string keyword { get; set; }

    }
}

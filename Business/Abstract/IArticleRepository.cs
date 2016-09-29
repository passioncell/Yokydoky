using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db;
using Business.Entitys;

namespace Business.Abstract
{
    interface IArticleRepository
    {
        IEnumerable<ArticleListModel> getArticleList(int memberId, int categoryNumber);
        IEnumerable<SearchKeywordModel> getAutoCompleteData(string input);
    }
}

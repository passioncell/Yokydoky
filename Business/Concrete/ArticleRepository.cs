using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Db;
using Business.Entitys;
using System.Text.RegularExpressions;

namespace Business.Concrete
{
    public class ArticleRepository : Common, IArticleRepository
    {
        public IEnumerable<ArticleListModel> getArticleList(int memberId, int categoryNumber)
        {
            var list = (from p in db.Article
                        select new ArticleListModel
                        {
                            id = p.id,
                            title = p.title,
                            content = getSumArticle(p.content),
                            reporter = p.reporter,
                            write_date = p.write_date,
                            modify_date = p.modify_date,
                            keyword = p.keyword
                        }).ToList();

            return list;
            
       
        }

        public IEnumerable<SearchKeywordModel> getAutoCompleteData(string input)
        {
            return (from p in db.SearchKeywordModel where p.keyword.Contains(input) select p).ToList();
        }

        public string getSumArticle(string input)
        {
            List<ArticleTokenModel> dataModelList = new List<ArticleTokenModel>();


            string firstPattern = "<symbol2>";            // Split on hyphens
            string secondParttern = "<symbol1>";

            //심볼2로 자른다!!!
            string[] firstResult = Regex.Split(input, firstPattern);

            //심볼1로 자르고 List에 정리한다!
            foreach (string item in firstResult)
            {
                if (item.Trim().Equals(""))
                {
                    break;
                }
                else
                {
                    ArticleTokenModel tempModel = new ArticleTokenModel();
                    string[] substrings2 = Regex.Split(item, secondParttern);
                    tempModel.content = substrings2[0];
                    tempModel.weight = substrings2[1];
                    dataModelList.Add(tempModel);

                }
            }

            String result = "";
            //로그출력
            foreach (ArticleTokenModel item in dataModelList)
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine("'내용 : {0}  / 중요도 : {1}' \n", item.content, item.weight);
                Console.WriteLine("-------------------------------");
                Console.WriteLine("\n\n");
                result += item.content;
            }

            return result;
        }
        
    }
}

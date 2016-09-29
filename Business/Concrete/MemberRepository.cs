using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Db;

namespace Business.Concrete
{
    public class MemberRepository : Common, IMemberRepository
    {
        public int getUserKeyById(string email)
        {
            return (from p in db.Member where p.email == email select p).First().id;
        }

        public IEnumerable<MemberKeyword> getUserKeyword(int userId)
        {
            var list = (from p in db.MemberKeyword where p.FK_Member == userId select p).ToList();

            return list;
        }

        public string signIn(string email, string password)
        {
            var result = (from p in db.Member where p.email == email && p.password == password select p).Count();

            if(result > 0)
            {
                if (email.Equals("admin"))
                {
                    return "admin signin"; //admin 로그인
                }
                return "signin success"; //일반 로그인
            }else
            {
                return "signin fail"; //로그인 실패
            }
           

        }

        public string signUp(Member member)
        {
            try
            {
                db.signUp(member.email, member.password, member.name, member.birthday, member.sex);
                return "success";
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return e.ToString();
            }
            


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db;

namespace Business.Abstract
{
    interface IMemberRepository
    {
        string signUp(Member member);
        string signIn(string email, string password);
        int getUserKeyById(string email);
        IEnumerable<MemberKeyword> getUserKeyword(int userId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Db;
using Business.Concrete;
using YokyDoky.Filters;

namespace YokyDoky.Controllers
{
    public class HomeController : Controller
    {
        ArticleRepository articleRepository;
        MemberRepository memberRepository;

        public HomeController()
        {
            articleRepository = new ArticleRepository();
            memberRepository = new MemberRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        //회원가입
        public ActionResult SignUp()
        {
            return View();
        }

        //회원가입(POST)
        [HttpPost]
        public ActionResult SignUp(FormCollection collection)
        {
            Member member = new Member();
            member.email = collection["email"];
            member.password = collection["password"];
            member.sex = Int32.Parse(collection["sex"]);
            member.birthday = DateTime.Parse(collection["birthday"]);

            string result = memberRepository.signUp(member);

            if (result.Equals("success"))
            {
                return RedirectToAction("Index");
            } else if (result.Equals("id exist"))
            {
                return Content("<script language='javascript' type='text/javascript'> alert('중복된 ID가 존재합니다.'); window.history.back(); </script>");
            }
            else
            {
                return Content(result);
            }

        }


        //로그인
        public ActionResult SignIn()
        {
            return View();
        }

        //로그인
        [HttpPost]
        public ActionResult SignIn(string id, string pw)
        {
            var result = memberRepository.signIn(id, pw);
            switch (result)
            {
                case "admin signin": //관리자 로그인 성공
                    Session[claGlobal.SessionString_UserID] = id; //아이디 세션저장
                    Session[claGlobal.SessionString_UserType] = UserType.Admin; //유저타입 저장
                    Session[claGlobal.SessionString_UserKey] = GetUserKeyByEmail(id).ToString(); //기본키 저장
                    return RedirectToAction("Index");

                case "signin success": //일반 로그인 성공
                    Session[claGlobal.SessionString_UserID] = id; //아이디 세션저장
                    Session[claGlobal.SessionString_UserType] = UserType.User; //유저타입 저장
                    Session[claGlobal.SessionString_UserKey] = GetUserKeyByEmail(id).ToString(); //기본키 저장
                    return RedirectToAction("Index");

                case "signin fail": //로그인 실패
                    return Content("<script language='javascript' type='text/javascript'> alert('일치하는 ID와 비밀번호가 없습니다.'); window.history.back(); </script>");

            }

            return View();
        }

        public ActionResult LogOut()
        {
            Session[claGlobal.SessionString_UserID] = null;
            Session[claGlobal.SessionString_UserType] = null;
            Session[claGlobal.SessionString_UserKey] = null;
            return RedirectToAction("Index");
        }

        //SummaryNews
        public ActionResult NewsList()
        {
            var listModel = articleRepository.getArticleList(1, 1);
            return View(listModel);
        }

        public ActionResult NewsDetail(int id)
        {
            return View();
        }

        [CustomAuthorize(Roles = UserType.User)]
        [HttpGet]
        public ActionResult SetKeyword(int id)
        {
            var keywordList = memberRepository.getUserKeyword(id);
            return View(keywordList);
        }



        [HttpPost]
        public JsonResult GetAutoCompleteData(FormCollection form)
        {
            string input = form["value"]; //사용자가 입력한 단어
            var list = articleRepository.getAutoCompleteData(input).ToArray();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public int GetUserKeyByEmail(string email)
        {
            return memberRepository.getUserKeyById(email);

        }
    }
}
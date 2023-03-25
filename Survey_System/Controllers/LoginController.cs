using Survey_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Survey_System.Controllers
{
    public class LoginController : Controller
    {
        SurveyEntities2 db = new Models.SurveyEntities2();

        public ActionResult SignIn(String Code,String Password)
        {

            if(Code == null)
            {
                return View();
            }
            else
            {
                var person = db.Person.FirstOrDefault(m => m.Code == Code && m.Password == Password);
                if (person != null)
                {
                    Session["Code"] = person.Code;
                    Session["NameSurname"] = person.NameSurname;
                    if (person.IsAdmin == true)
                    {
                        Session["Admin"] = "Admin";
                    }
                    return RedirectToAction("Create","Answer");
                }
                else
                {
                    ViewBag.Error = "Kullanıcı adı veya şifre hatalı";
                    return View();
                }
            }
          

        
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("SignIn", "Login");
        }
    }
}
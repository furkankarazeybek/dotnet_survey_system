﻿using Survey_System.Models;
using Survey_System.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Survey_System.Controllers
{
    public class QuestionController : BaseController
    {
        SurveyEntities2 db = new Models.SurveyEntities2();

        public ActionResult Index()
        {

            if (Session["Admin"] == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
           
                var model = db.Question.ToList();
                return View(model);
            
           
        }

        public ActionResult Create(Question question)
        {

            if (Session["Admin"] == null)
            {

                return RedirectToAction("SignIn", "Login");

            }
            if (question.QuestionAsked != null)
            {


                question.CreateDate = DateTime.Now;
                question.CreateBy = NameSurname;


                db.Question.Add(question);
                db.SaveChanges();


                return RedirectToAction("Index");

            }

            else
            {
                return View();

            }



        }


        public ActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return HttpNotFound();
            }
            var model = db.Question.Find(Id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Question question)
        {
            db.Entry(question).State = System.Data.Entity.EntityState.Modified;
            db.Entry(question).Property(e => e.CreateBy).IsModified = false;
            db.Entry(question).Property(e => e.CreateDate).IsModified = false;

            question.ModifyBy = NameSurname;
            question.ModifyDate = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return HttpNotFound();
            }
            var person = db.Question.Find(Id);
            db.Question.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelTripProje.Models.Siniflar;

namespace TravelTripProje.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        Context c = new Context();
        BlogYorum blogYorum = new BlogYorum();
        public ActionResult Index()
        {
            //var bloglar = c.Blogs.ToList();
            blogYorum.Deger1 = c.Blogs.ToList();
            blogYorum.Deger3 = c.Blogs.OrderByDescending(x => x.ID).Take(3).ToList();
            return View(blogYorum);
        }


        public ActionResult BlogDetay(int id)
        {
            //var blogbul = c.Blogs.Where(x => x.ID == id).ToList();
            blogYorum.Deger1 = c.Blogs.Where(x => x.ID == id).ToList();
            blogYorum.Deger2 = c.Yorums.Where(x => x.Blogid == id).ToList();
            return View(blogYorum);
        }

        [HttpGet]
        public PartialViewResult YorumYap(int id)
        {
            ViewBag.deger = id;
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult YorumYap(Yorum y)
        {
            c.Yorums.Add(y);
            c.SaveChanges();
            return PartialView();
        }
    }
}
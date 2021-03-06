﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using FluentValidation.Results;

namespace MVCProjeKampi.Controllers
{
    public class WriterPanelMessageController : Controller
    {
        // GET: WriterPanelMessage
        MessageManager mg = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();
        public ActionResult Inbox()
        {
            string p = (string) Session["WriterEmail"];
            var messageValues = mg.GetListInbox(p);
            return View(messageValues);
        }
        public ActionResult SendBox()
        {
            string p = (string) Session["WriterEmail"];
            var messageValues = mg.GetListSendbox(p);
            return View(messageValues);
        }
        public PartialViewResult MessageListMenu()
        {
            string p = (string)Session["WriterEmail"];
            var receiverMail = mg.GetListInbox(p).Count;
            ViewBag.receiverEmail = receiverMail;

            var senderMail = mg.GetListSendbox(p).Count;
            ViewBag.senderEmail = senderMail;
            return PartialView();
        }
        public ActionResult GetInboxMessageDetails(int id)
        {
            var contactValues = mg.GetById(id);
            return View(contactValues);
        }
        public ActionResult GetSendboxMessageDetails(int id)
        {
            var contactValues = mg.GetById(id);
            return View(contactValues);
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            string sender = (string)Session["WriterEmail"];
            ValidationResult results = messageValidator.Validate(message);
            if (results.IsValid)
            {
                message.SenderMail = sender;
                message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mg.MessageAdd(message);
                return RedirectToAction("SendBox");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }
        public ActionResult Draft()
        {
            var draftContent = mg.IsDraft();
            return View(draftContent);
        }

    }
}
﻿using System.Web.Mvc;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.Employers;

namespace PresentationLayer.Controllers
{
    /// <summary>
    /// Administracia employerov
    /// </summary>
    public class EmployerController : Controller
    {
        public IEmployerFacade employerFacade { get; set; }

        // GET: Employer
        public ActionResult Index()
        {
            return View();
        }

        // GET: Employer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employer/Create
        [HttpPost]
        public ActionResult Create(EmployerDto employerDto)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EmployerDto employerDto)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, EmployerDto employerDto)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

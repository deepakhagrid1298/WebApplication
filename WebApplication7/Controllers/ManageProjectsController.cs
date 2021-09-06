using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Service;

namespace WebApplication7.Controllers
{
    public class ManageProjectsController : Controller
    {
        // GET: ManageProject
            private ManageProjectsService manageProjectsService;
            // GET: ManageProjects
            public ActionResult list()
            {
                manageProjectsService = new ManageProjectsService();
                var model = manageProjectsService.Getlist();
                return View(model);
            }
    }
}
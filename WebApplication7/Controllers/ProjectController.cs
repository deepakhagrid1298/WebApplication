using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;
using WebApplication7.Service;

namespace WebApplication7.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        ProjectsService projectsService;
        // GET: Project
        public ActionResult list()
        {
            projectsService = new ProjectsService();
            var model = projectsService.getAllProjects();
            return View(model);
        }
        public ActionResult addProject()
        {
            return View();

        }
        [HttpPost]
        public ActionResult addProject(ProjectsModel model)
        {
            projectsService = new ProjectsService();
            projectsService.addProject(model);
            return RedirectToAction("list");

        }

        public ActionResult UpdateById(int Id)
        {
            projectsService = new ProjectsService();
            var model = projectsService.updateProjectById(Id);
            return View(model);

        }
        [HttpPost]
        public ActionResult updateById(ProjectsModel model)
        {
            projectsService = new ProjectsService();
            projectsService.updateProject(model);
            return RedirectToAction("list");
        }

    }
}
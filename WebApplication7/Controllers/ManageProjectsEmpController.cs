using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Service;

namespace WebApplication7.Controllers
{
    public class ManageProjectsEmpController : Controller
    {
        // GET: ManageProjectsEmp
        private ManageProjectsEmpServices manageProjectsEmpServices;

        // GET: ManageProjectsEmp
        public ActionResult list()
        {
            manageProjectsEmpServices = new ManageProjectsEmpServices();
            var model = manageProjectsEmpServices.GetAllProjectEmp();
            return View(model);
        }
        [HttpGet]
        public ActionResult getAllEmployeeByProjectId(int Id)
        {
            manageProjectsEmpServices = new ManageProjectsEmpServices();
            var model = manageProjectsEmpServices.GetAllEmployeeByProjectId(Id);
            return View(model);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;
using WebApplication7.Service;

namespace WebApplication7.Controllers
{
    public class ManageRoleController : Controller
    {
        // GET: ManageRole
        private ManageRoleService manageRoleService;
        // GET: ManageRole
        public ActionResult list()
        {
            manageRoleService = new ManageRoleService();
            var model = manageRoleService.GetRoles();
            return View(model);
        }

        public ActionResult ManageRole(int id)
        {
            ManageRoleService manageRoleService = new ManageRoleService();
            var model = manageRoleService.GetEmployeeById(id);
            return View(model);

        }
        [HttpPost]
        public ActionResult ManageRole(ManageRoleModel manageRoleModel)
        {
            manageRoleService = new ManageRoleService();
            manageRoleService.ManageRole(manageRoleModel);

            return RedirectToAction("list");
        }


    }
}
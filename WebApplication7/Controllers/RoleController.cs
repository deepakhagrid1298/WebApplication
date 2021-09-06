using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;
using WebApplication7.Service;

namespace WebApplication7.Controllers
{
    public class RoleController : Controller
    {
        private RoleService roleService;
        // GET: Role
        public ActionResult list()
        {
            roleService = new RoleService();
            var model = roleService.GetAllRoles();
            return View(model);
        }
    }
}
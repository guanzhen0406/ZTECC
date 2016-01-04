using System.Collections;
using System.Web.ModelBinding;
using Wicresoft.Solution.IBLL;
using Wicresoft.Solution.PortalModel;
using Wicresoft.Solution.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Wicresoft.Solution.Portal.Inner.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _service;

        public HomeController(IUserService service)
        {
            this._service = service;
        }


        public ActionResult Index()
        {
            
            return View();
        }

    }
}
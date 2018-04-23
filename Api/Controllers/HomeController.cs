using DataAccessLayer.Interfaces;
using Domainlogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Models;
using Models.ViewModels;

namespace Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
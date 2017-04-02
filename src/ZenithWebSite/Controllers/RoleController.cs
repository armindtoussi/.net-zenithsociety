using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZenithWebSite.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ZenithWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {

        private readonly ApplicationDbContext db;

        public RoleController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<ActionResult> Index()
        {
            List<IdentityRole> allRoles = db.Roles.ToList();

            return View(allRoles);
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string name = Request.Form["Name"];
                if (name == null || name.Length == 0)
                {
                    throw new Exception();
                }


                var roleStore = new RoleStore<IdentityRole>(db);
                roleStore.CreateAsync(new IdentityRole { Name = name, NormalizedName = name.ToUpper() });
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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


        // POST: Role/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                var roleStore = new RoleStore<IdentityRole>(db);
                IdentityRole roleToDelete = await roleStore.FindByIdAsync(id);
                if (roleToDelete == null)
                {
                    throw new Exception();
                }
                await roleStore.DeleteAsync(roleToDelete);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
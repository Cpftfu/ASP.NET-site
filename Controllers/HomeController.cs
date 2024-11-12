using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using TeaShop.Models;

namespace TeaShop.Controllers
{
    public class HomeController : Controller
    {
        //новое
        private readonly TeaShopDbContext _context;
        //

		private TeaShopDbContext db;
        public HomeController(TeaShopDbContext context)
        {
            db = context;
        }

        //
		[HttpGet]
        //
		public async Task<IActionResult> Index()
        {
            //return View();
            return View(await db.Teas.ToListAsync());

        }

        //
        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            var user = _context.Customers.FirstOrDefault(x => x.EmailOfCust == email && x.PasswordOfCust == password); ;
            if(user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.SurnameOfCust)
                    new Claim(ClaimTypes.Role, user.RoleID == 1 ? "Customer" : "OtherRole")

                };

                var claimsIdent = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdent);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessege = "Неверные данные";
            return View();
        }
        //

        public IActionResult Create ()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Tea tea)
        {
            db.Teas.Add(tea);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Tea tea = await db.Teas.FirstOrDefaultAsync(p => p.IdTea == id);
                if (tea != null)
                    return View(tea);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Tea tea)
        {
            db.Teas.Update(tea);
            await db.SaveChangesAsync();
            return RedirectToAction("Catalog");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Tea tea = await db.Teas.FirstOrDefaultAsync(p => p.IdTea == id);
                if (tea != null)
                    return View(tea);
            }
            return NotFound();
        }

		[HttpPost]
		public async Task<IActionResult> Delete(int? id)
		{
            if (id != null)
            {
                Tea tea = await db.Teas.FirstOrDefaultAsync(p => p.IdTea == id);
                if (tea != null)
                {
                    db.Teas.Remove(tea);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Catalog");
                }
            }
            return NotFound();
		}


		public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About_Us()
        {
            return View();
        }

        public ActionResult Sale()
        {
            return View();
        }

        public async Task<ActionResult> Catalog()
        {
            return View(await db.Teas.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}

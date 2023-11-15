using Microsoft.AspNetCore.Mvc;
using RetoProgramacionApptelink.Models;
using RetoProgramacionApptelink.Repository;
using System.Drawing.Text;

namespace RetoProgramacionApptelink.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _repository;
        public LoginController(ILoginRepository repository)
        {
            this._repository = repository;
        }


        public IActionResult Login()
        {
            var model = new LoginUser
            {
                IntentosFallidos = 0
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginUser login)
        {
            if (ModelState.IsValid)
            {
                bool loginRealizado = _repository.login(login);

                if (!loginRealizado)
                {
                    login.IntentosFallidos++;
                    int? valor = HttpContext.Session.GetInt32("IntentosFallidos");
                    if (valor.HasValue)
                    {
                        login.IntentosFallidos += valor.Value;
                    }
                    HttpContext.Session.SetInt32("IntentosFallidos", login.IntentosFallidos);
                    if (login.IntentosFallidos >= 3)
                    {
                        return View(login);
                    }
                    else
                    {
                        return View(login);
                    }
                }
                else
                {
                    if (HttpContext.Session.GetInt32("IntentosFallidos").HasValue)
                    {
                        HttpContext.Session.Remove("IntentosFallidos");
                    }
                    return RedirectToAction("Menu");
                }
            }
            else
            {
                login.IntentosFallidos++;
                int? valor = HttpContext.Session.GetInt32("IntentosFallidos");
                if (valor.HasValue)
                {
                    login.IntentosFallidos += valor.Value;
                }
                HttpContext.Session.SetInt32("IntentosFallidos", login.IntentosFallidos);
                if (login.IntentosFallidos >= 3)
                {
                    return View(login);
                }
                else
                {
                    return View(login);
                }
                return View();
            }
        }

        public IActionResult Menu()
        {
            return View();
        }

    }
}

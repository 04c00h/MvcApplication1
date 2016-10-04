using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            API.ObjResponse data = new API.ObjResponse("Авторизация", "Auth", null, null);
            string token = Request.Headers["token"];
            if (token != null)
            {
                List<API.SheetUser> users = new List<API.SheetUser>();
                users.Add(new API.SheetUser() { fio = "Вася Пупкин", value = "2" });
                users.Add(new API.SheetUser() { fio = "Вася Пупкин", value = "2" });
                users.Add(new API.SheetUser() { fio = "Вася Пупкин", value = "2" });
                data = new API.ObjResponse("Ведомость", "Sheet", token, users);
            }
            ViewBag.data = data;
            return View();
        }


        public ActionResult Auth()
        {
            API.ObjResponse dataResp = new API.ObjResponse("Авторизация", "Auth", null, null);
            string data = Request.Form["authData"];
            if (data != null)
            {
                API.ObjAuthRequest obj = System.Web.Helpers.Json.Decode<API.ObjAuthRequest>(data);
                string token = API.userAuth(obj);

                if (token.Length > 0)
                {
                    Request.Headers.Add("token", token);
                    List<API.SheetUser> users = new List<API.SheetUser>();
                    users.Add(new API.SheetUser() { fio = "Вася Пупкин", value = "2" });
                    users.Add(new API.SheetUser() { fio = "Вася Пупкин", value = "2" });
                    users.Add(new API.SheetUser() { fio = "Вася Пупкин", value = "2" });
                    dataResp = new API.ObjResponse("Ведомость", "Sheet", token, users);
                }
            }
            ViewBag.data = dataResp;
            return View("Index");
        }


        public ActionResult Sheet()
        {
            API.ObjResponse dataResp = new API.ObjResponse("Авторизация", "Auth", null, null);
            string data = Request.Form["sheetData"];
            if (data != null)
            {
                API.ObjSheetRequest obj = System.Web.Helpers.Json.Decode<API.ObjSheetRequest>(data);
                string status = API.updValue(obj);
                if (status != "")
                {
                    Response.Write(status);
                }
                else
                {
                    List<API.SheetUser> users = new List<API.SheetUser>();
                    users.Add(new API.SheetUser() { fio = "Вася Пупкин", value = "2" });
                    users.Add(new API.SheetUser() { fio = "Вася Пупкин", value = "2" });
                    users.Add(new API.SheetUser() { fio = "Вася Пупкин", value = "2" });
                    dataResp = new API.ObjResponse("Ведомость", "Sheet", Request.Headers["token"], users);
                }
            }
            ViewBag.data = dataResp;
            return View("Index");
        }

    }
}

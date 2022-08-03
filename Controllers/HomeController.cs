using New_CRUD_Operation.DB_Connection;
using New_CRUD_Operation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace New_CRUD_Operation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DeveloperModel devMdl)
        {
            Alok_KushwahaEntities dbobj = new Alok_KushwahaEntities();

            NDeveloper tblobj = new NDeveloper();

            tblobj.DID = devMdl.DID;
            tblobj.DNAME = devMdl.DNAME;
            tblobj.DNUMBER = devMdl.DNUMBER;
            tblobj.DEMAILID = devMdl.DEMAILID;
            tblobj.DTECHNOLOGY = devMdl.DTECHNOLOGY;
            tblobj.DESIGNATION = devMdl.DESIGNATION;

            if(devMdl.DID == 0)
            {
                dbobj.NDevelopers.Add(tblobj);
                dbobj.SaveChanges();
            }
            else
            {
                dbobj.Entry(tblobj).State = System.Data.Entity.EntityState.Modified;
                dbobj.SaveChanges();
            }
            

            return RedirectToAction("Read");
        }

        public ActionResult Read()
        {
            Alok_KushwahaEntities dbobj = new Alok_KushwahaEntities();

            var tbldata = dbobj.NDevelopers.ToList();

            List<DeveloperModel> mdlobj = new List<DeveloperModel>();

            foreach (var item in tbldata)
            {
                mdlobj.Add(new DeveloperModel
                { 
                    DID = item.DID,
                    DNAME = item.DNAME,
                    DNUMBER = item.DNUMBER,
                    DEMAILID = item.DEMAILID,
                    DTECHNOLOGY = item.DTECHNOLOGY,
                    DESIGNATION = item.DESIGNATION
                });

            }
            return View(mdlobj);
        }

        public ActionResult Update(int DID)
        {
            Alok_KushwahaEntities dbobj = new Alok_KushwahaEntities();

            var UpdateData = dbobj.NDevelopers.Where(S => S.DID == DID).First();

            DeveloperModel mdlobj = new DeveloperModel();

            mdlobj.DID = UpdateData.DID;
            mdlobj.DNAME = UpdateData.DNAME;
            mdlobj.DNUMBER = UpdateData.DNUMBER;
            mdlobj.DEMAILID = UpdateData.DEMAILID;
            mdlobj.DTECHNOLOGY = UpdateData.DTECHNOLOGY;
            mdlobj.DESIGNATION = UpdateData.DESIGNATION;

            ViewBag.Update = "Update";

            return View("Create", mdlobj);
        }

        public ActionResult Delete(int DID)
        {
            Alok_KushwahaEntities dbobj = new Alok_KushwahaEntities();

            var DeleteData = dbobj.NDevelopers.Where(P => P.DID == DID).First();

            dbobj.NDevelopers.Remove(DeleteData);

            dbobj.SaveChanges();

            return RedirectToAction("Read");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            TempData["Login"] = "Login";

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(AvengerModel avenger)
        {
            Alok_KushwahaEntities dbobj = new Alok_KushwahaEntities();

            var Userdata = dbobj.AVENGERS.Where(D => D.AEMAILID == avenger.AEMAILID).FirstOrDefault();

            if(Userdata == null)
            {
                TempData["Email"] = "Email ID Not Found";
            }
            else
            {
                if(Userdata.AEMAILID == avenger.AEMAILID && Userdata.APASSWORD == avenger.APASSWORD)
                {
                    FormsAuthentication.SetAuthCookie(Userdata.AEMAILID, false);
                    Session["Email"] = Userdata.AEMAILID;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Password"] = "Password is Not Match";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            TempData["SignUp"] = "SignUp";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignUp(AvengerModel mdlavenger)
        {
            Alok_KushwahaEntities dbobj = new Alok_KushwahaEntities();

            AVENGER tblobj = new AVENGER();

            tblobj.ANAME = mdlavenger.ANAME;
            tblobj.AEMAILID = mdlavenger.AEMAILID;
            tblobj.APASSWORD = mdlavenger.APASSWORD;
            tblobj.CODE = mdlavenger.CODE;

            dbobj.AVENGERS.Add(tblobj);
            dbobj.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}
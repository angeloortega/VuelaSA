using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appVuelaSA.Models;

namespace appVuelaSA.Controllers
{
    public class LogInController : Controller
    {
        private proyectoRequeEntities db = new proyectoRequeEntities();

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Signin()
        {
            return View();
        }

        public ActionResult SigninAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult btnLogin_Click(FormCollection solicitud)
        {
            string nombreUsuario = solicitud["nombre"].ToString();
            string contrasenna = solicitud["contrasenna"].ToString();

            var users = from m in db.usuario
                        select m;
            users = users.Where(m => m.nombre == nombreUsuario);
            if (users.Count() < 1){
                return View("Login");
            }
            usuario usr = users.First();

            if (contrasenna == usr.contrasenna)
            {
                Console.WriteLine("estoy vivo joder");
                Session["IDUsuario"] = usr.idusuario;

                if (usr.tipousuario.Equals("Empleado"))
                {
                    //empleado
                    return RedirectToAction("MainAdministrador", "Administrador");
                }
                else
                {
                    //cliente
                    return RedirectToAction("MainCliente", "Cliente");
                }

            }
            else {
                return View("Login");
            }
        }

        [HttpPost]
        public ActionResult Signin(FormCollection solicitud)
        {
            try
            {
                usuario usr = new usuario();
                cliente cli = new cliente();

                usr.nombre = solicitud["usuario"].ToString();
                usr.contrasenna = solicitud["contrasenna"].ToString();
                usr.tipousuario = "Cliente";
                usr.idusuario = Int32.Parse(solicitud["pasaporte"]);
                cli.nombre = solicitud["nombre"].ToString();
                cli.paisresidencia = solicitud["paisResidencia"].ToString();
                cli.pasaporte = solicitud["pasaporte"].ToString();
                cli.correo = solicitud["correo"].ToString();
                cli.idcliente = Int32.Parse(solicitud["pasaporte"]);
                cli.idusuario = Int32.Parse(solicitud["pasaporte"]);
                db.usuario.Add(usr);
                db.SaveChanges();
                db.cliente.Add(cli);
                db.SaveChanges();
                return View("Signin");
            }
            catch (Exception e)
            {
                return View("Login");
            }
        }

        [HttpPost]
        public ActionResult btnSignin_Click()
        {
            return View("Signin");
        }

        [HttpPost]
        public ActionResult SigninAdmin(FormCollection solicitud)
        {
            try
            {
                usuario usr = new usuario();
                empleado emp = new empleado();

                usr.nombre = solicitud["usuario"].ToString();
                usr.contrasenna = solicitud["contrasenna"].ToString();
                usr.tipousuario = "Empleado";
                emp.nombre = solicitud["nombre"].ToString();
                emp.idempleado = Convert.ToDecimal(solicitud["id"].ToString());
                db.usuario.Add(usr);
                emp.idusuario = Convert.ToDecimal(solicitud["id"].ToString());
                db.empleado.Add(emp);
                db.SaveChanges();

                return RedirectToAction("MainAdministrador", "Administrador");
            }
            catch (Exception e)
            {
                return View("SigninAdmin");
            }
        }

    }
}
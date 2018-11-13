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
        private proyecto2RequeEntities db = new proyecto2RequeEntities();

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


            int output = db.Validar_LogIn(nombreUsuario, contrasenna, new System.Data.Entity.Core.Objects.ObjectParameter("output", 0));

            Console.WriteLine(output);

            Session["IDUsuario"] = 1;
            
            if (nombreUsuario.Equals("richBoy")) //(output == 1)
            {
                //empleado
                return RedirectToAction("MainAdministrator", "Administrator");
            }
            else
            {
                //cliente
                return RedirectToAction("MainCliente", "Cliente");
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

                cli.nombre = solicitud["nombre"].ToString();
                cli.paisresidencia = solicitud["paisResidencia"].ToString();
                cli.pasaporte = solicitud["pasaporte"].ToString();
                cli.correo = solicitud["correo"].ToString();

                db.usuario.Add(usr);

                var result = db.usuario.SqlQuery("SELECT * FROM dbo.usuario WHERE nombre = @usuario", new SqlParameter("@usuario", usr.nombre));
                usuario temp = result.First();

                cli.idusuario = temp.idusuario;

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

                var result = db.usuario.SqlQuery("SELECT * FROM dbo.usuario WHERE nombre = @usuario)", new SqlParameter("@usuario", usr.nombre));
                usuario temp = result.First();

                emp.idusuario = temp.idusuario;

                db.empleado.Add(emp);
                db.SaveChanges();

                return RedirectToAction("MainAdministrator", "Administrator");
            }
            catch (Exception e)
            {
                return View("SigninAdmin");
            }
        }

    }
}
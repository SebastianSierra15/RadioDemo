﻿using Microsoft.AspNetCore.Mvc;
using RadioDemo.Data;
using RadioDemo.Models;
using Microsoft.AspNetCore.Authorization;

namespace RadioDemo.Controllers
{
    [Authorize(Roles = "Consultar Vendedor")]
    public class VendedorController : Controller
    {
        Procedimientos cn = new Procedimientos();

        public IActionResult Index()
        {
            List<VendedorModel> vendedor = cn.RecopilarVendedores();
            ViewBag.vendedores = vendedor;

            List<TipoDocumentoModel> tipoDocumento = cn.RecopilarTipoDocumentos();
            ViewBag.tipoDocumentos = tipoDocumento;

            List<GeneroModel> genero = cn.RecopilarGeneros();
            ViewBag.generos = genero;

            List<RolModel> rol = cn.RecopilarRoles();
            ViewBag.roles = rol;

            return View();
        }

        public ActionResult Buscar(string id)
        {
            VendedorModel vendedor = cn.BuscarVendedor(id);

            return Json(vendedor);
        }

        [HttpPost]
        [Authorize(Roles = "Editar Vendedor")]
        public ActionResult Editar(string id, string username, string estado, string rol, string nombre1, string nombre2, string apellido1, string apellido2, string correo)
        {
            cn.EditarVendedor(id, username, estado, rol, nombre1.ToUpper(), (nombre2 != null ? nombre2.ToUpper() : null), apellido1.ToUpper(), (apellido2 != null ? apellido2.ToUpper() : null), correo);

            return RedirectToAction("Index", "Vendedor");
        }

        [HttpPost]
        [Authorize(Roles = "Agregar Vendedor")]
        public ActionResult Agregar(string username, string rol, string tipoDocumento, string documento, string nombre1, string nombre2, string apellido1, string apellido2, string fecha, string correo, string genero)
        {
            cn.AgregarVendedor(username, rol, tipoDocumento, documento, nombre1.ToUpper(), (nombre2 != null ? nombre2.ToUpper() : null), apellido1.ToUpper(), (apellido2 != null ? apellido2.ToUpper() : null), fecha, correo, genero);

            return RedirectToAction("Index", "Vendedor");
        }
    }
}

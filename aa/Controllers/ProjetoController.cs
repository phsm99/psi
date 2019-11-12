using aa.BD;
using aa.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace aa.Controllers
{
    public class ProjetoController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Projeto
        public ActionResult Index()
        {
            return View(db.Projetos.ToList());
        }

        // GET: Projeto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeto projeto = db.Projetos.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }

        // GET: Projeto/Create
        public ActionResult Create()
        {
            var usu = db.Usuarios.ToList();
            var model = new ProjetoViewModel
            {
                UsuariosDisponiveis = usu.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Nome
                }).ToList()
            };
            return View(model);
        }

        // POST: Projeto/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(string nome, string UsuariosId)
        {
            string erro = "";
            if (string.IsNullOrWhiteSpace(nome))
            {
                erro += "Favor insira um valor para o campo nome!";
            }

            if (string.IsNullOrWhiteSpace(UsuariosId))
            {
                erro += "\nFavor selecione os usuários para esse projeto!";
            }

            if (!string.IsNullOrWhiteSpace(erro))
            {
                ModelState.AddModelError("", erro);
            }

            if (ModelState.IsValid)
            {

                Projeto projeto = new Projeto
                {
                    Nome = nome,
                    Usuarios = new List<Usuario>()
                };
                UsuariosId.Split(',').ToList().ForEach(x => projeto.Usuarios.Add(db.Usuarios.Find(Convert.ToInt32(x))));
                db.Projetos.Add(projeto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Json(new { Data = erro });

        }

        // GET: Projeto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeto projeto = db.Projetos.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }

            var usu = db.Usuarios.ToList();
            var model = new ProjetoViewModel
            {
                UsuariosDisponiveis = usu.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Nome
                }).ToList()
            };
            model.Nome = projeto.Nome;
            model.UsuariosSelecionados = projeto.Usuarios.Select(x => x.Id.ToString()).ToList();

            return View(model);
        }

        // POST: Projeto/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Projeto projeto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projeto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projeto);
        }

        // GET: Projeto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeto projeto = db.Projetos.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }

        // POST: Projeto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Projeto projeto = db.Projetos.Find(id);
            db.Projetos.Remove(projeto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

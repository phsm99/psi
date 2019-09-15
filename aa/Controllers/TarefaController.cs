using aa.BD;
using aa.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace aa.Controllers
{
    [RedirectingAction]
    public class TarefaController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Tarefa
        public ActionResult Index()
        {
            return View(db.Tarefas.ToList());
        }

        // GET: Tarefa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarefa tarefa = db.Tarefas.Find(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            return View(tarefa);
        }

        public void CriarSelectListResponsaveis()
        {
            var Usuarios = db.Usuarios.ToList();
            List<SelectListItem> lista = new List<SelectListItem>();
            foreach (var usu in Usuarios)
            {
                lista.Add(new SelectListItem { Value = usu.Id.ToString(), Text = usu.Nome });
            }
            ViewBag.Responsaveis = lista;
        }
        // GET: Tarefa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tarefa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                tarefa.DataCriacao = DateTime.Now;
                tarefa.DataAlteracao = DateTime.Now;
                db.Tarefas.Add(tarefa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tarefa);
        }

        // GET: Tarefa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarefa tarefa = db.Tarefas.Find(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }

            CriarSelectListResponsaveis();
            return View(tarefa);
        }

        // POST: Tarefa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                tarefa.DataAlteracao = DateTime.Now;
                db.Entry(tarefa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tarefa);
        }

        // GET: Tarefa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarefa tarefa = db.Tarefas.Find(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            return View(tarefa);
        }

        // POST: Tarefa/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            Tarefa tarefa = db.Tarefas.Find(id);
            if (tarefa == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Tarefas.Remove(tarefa);
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

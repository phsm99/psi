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
    public class EquipeController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Equipe
        public ActionResult Index()
        {
            return View(db.Equipes.ToList());
        }

        // GET: Equipe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipe equipe = db.Equipes.Find(id);
            if (equipe == null)
            {
                return HttpNotFound();
            }
            return View(equipe);
        }

        // GET: Equipe/Create
        public ActionResult Create()
        {
            var usu = db.Usuarios.ToList();
            var model = new EquipeViewModel
            {
                UsuariosDisponiveis = usu.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Nome
                }).ToList()
            };
            return View(model);
        }

        // POST: Equipe/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(string nome, string UsuariosId)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                ModelState.AddModelError("", "Favor insira um valor para o campo nome!");
            }
            if (string.IsNullOrWhiteSpace(UsuariosId))
            {
                ModelState.AddModelError("", "Favor selecione os usuários para essa equipe!");
            }

            if (ModelState.IsValid)
            {

                Equipe equipe = new Equipe
                {
                    Nome = nome,
                    Usuarios = new List<Usuario>()
                };
                UsuariosId.Split(',').ToList().ForEach(x => equipe.Usuarios.Add(db.Usuarios.Find(Convert.ToInt32(x))));
                db.Equipes.Add(equipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(new JsonResult { Data = "ERRO INTERNO!!!!!" });
        }

        // GET: Equipe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipe equipe = db.Equipes.Find(id);
            if (equipe == null)
            {
                return HttpNotFound();
            }

            var usu = db.Usuarios.ToList();
            var model = new EquipeViewModel
            {
                UsuariosDisponiveis = usu.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Nome
                }).ToList()
            };
            model.Nome = equipe.Nome;
            model.UsuariosSelecionados = equipe.Usuarios.Select(x => x.Id.ToString()).ToList();

            return View(model);
        }

        // POST: Equipe/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int equipeId, string nome, string UsuariosId)
        {
            if (ModelState.IsValid)
            {
                Equipe equipe = db.Equipes.Find(equipeId);
                if (equipe == null)
                {
                    throw new Exception("Equipe não encontrada!");
                }

                List<Usuario> usuariosSelecionados = new List<Usuario>();
                UsuariosId.Split(',').ToList().ForEach(x => usuariosSelecionados.Add(db.Usuarios.Find(Convert.ToInt32(x))));

                if (usuariosSelecionados.Count <= equipe.Usuarios.Count)
                {
                    equipe.Usuarios = usuariosSelecionados;
                }
                else
                {
                    equipe.Usuarios = usuariosSelecionados.Concat(equipe.Usuarios).GroupBy(elem => elem.Id).Select(group => group.First()).ToList();
                }

                db.Entry(equipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Equipe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipe equipe = db.Equipes.Find(id);
            if (equipe == null)
            {
                return HttpNotFound();
            }
            return View(equipe);
        }

        // POST: Equipe/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipe equipe = db.Equipes.Find(id);
            db.Equipes.Remove(equipe);
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

using aa.BD;
using aa.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace aa.Controllers
{
    [RedirectingAction]
    public class UsuarioController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Usuario
        public ActionResult Index()
        {
            return View(db.Usuarios.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            if (usuario.ResponsavelId != 0)
            {
                ViewBag.Responsavel = db.Usuarios.Find(usuario.ResponsavelId).Nome;
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            List<Usuario> usu = db.Usuarios.ToList();
            usu.ForEach(x => lista.Add(new SelectListItem { Value = x.Id.ToString(), Text = x.Nome }));
            ViewBag.Responsaveis = lista;
            return View();
        }

        // POST: Usuario/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {

            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> lista = new List<SelectListItem>();
            List<Usuario> usu = db.Usuarios.ToList();
            foreach (var item in usu)
            {
                if (item.Id == usuario.ResponsavelId)
                {
                    lista.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Nome, Selected = true });
                }
                else
                {
                    lista.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Nome });
                }
            }
            lista.RemoveAt(lista.FindIndex(x => x.Value.Equals(id.ToString())));
            ViewBag.Responsaveis = lista;


            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            if (usuario.ResponsavelId == usuario.Id)
            {
                ModelState.AddModelError("", "Não é possível vincular a si mesmo como responsável!!");
            }
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuLogado = null;
            if (Session["Usuario"] != null)
            {
                usuLogado = (Usuario)Session["Usuario"];
            }
            if (usuLogado.Id == id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.Usuarios.Remove(usuario);
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

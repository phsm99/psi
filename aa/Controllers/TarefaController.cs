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
    struct TarefaJson
    {
        public string Title;
        public string Desc;
        public string Start_Date;
        public string End_Date;

    }
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
                tarefa.Status = Status.NãoAtribuida;
                db.Tarefas.Add(tarefa);

                HistoricoTarefa historico = new HistoricoTarefa(db);
                historico.RegistrarAlteracao(TipoAlteracao.Inclusão, tarefa, ((Usuario)Session["Usuario"]).Id);
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

                HistoricoTarefa historico = new HistoricoTarefa(db);
                historico.RegistrarAlteracao(TipoAlteracao.Edição, tarefa, ((Usuario)Session["Usuario"]).Id);
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

        [HttpGet]
        public ActionResult Historico(int id)
        {
            List<HistoricoTarefa> listaHitorico = db.HistoricoTarefas.Where(x => x.TarefaId == id).ToList();
            HistoricoViewModel histView = new HistoricoViewModel(listaHitorico);
            return View(histView);
        }

       

        [HttpGet]
        public ActionResult ObterTarefasCalendario(string data = null)
        {
            JsonResult result = new JsonResult();
            List<Tarefa> tarefas = new List<Tarefa>();
            if (data == null)
            {
                tarefas = db.Tarefas.ToList();
            }
            else
            {
                var dataConvert = Convert.ToDateTime(data);
                tarefas = db.Tarefas.Where(x => x.DataEntrega.Month.Equals(dataConvert.Month)).ToList();
            }
            List<TarefaJson> tarefaJsons = new List<TarefaJson>();

            tarefas.ForEach(x =>
                tarefaJsons.Add(new TarefaJson
                {
                    Title = x.Titulo,
                    Desc = x.Descricao,
                    Start_Date = x.DataEntrega.ToString("yyyy-MM-dd"),
                    End_Date = x.DataEntrega.ToString("yyyy-MM-dd")
                })
            );

            result = Json(tarefaJsons, JsonRequestBehavior.AllowGet);
            return result;
        }


    }

}

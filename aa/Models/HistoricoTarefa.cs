using aa.BD;
using aa.Classes;
using System;
using System.Linq;

namespace aa.Models
{
    public enum TipoAlteracao
    {
        Inclusão,
        Edição,
        Exclusão
    }
    public class HistoricoTarefa
    {
        private static AppContext _context;

        public int Id { get; set; }

        public virtual Tarefa Tarefa { get; set; }
        public int? TarefaId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public int? UsuarioId { get; set; }

        public string Alteracoes { get; set; }

        public DateTime DataAlteracao { get; set; }

        public TipoAlteracao TipoAlteracao { get; set; }

        public HistoricoTarefa(AppContext contexto)
        {
            _context = contexto;
        }

        public HistoricoTarefa()
        {

        }

        public void RegistrarAlteracao(TipoAlteracao alteracao, Tarefa TarefaAtual, int usuarioAlterador)
        {
            HistoricoTarefa historico = new HistoricoTarefa();
            if (alteracao == TipoAlteracao.Inclusão)
            {
                var dic = Utils.ObterAtributosNaoCompostos(TarefaAtual);
                historico.Alteracoes = Utils.SerializarDicionario(dic);
            }
            else if (alteracao == TipoAlteracao.Edição)
            {
                Tarefa TarefaAntiga = _context.Tarefas.AsNoTracking().Where(x => x.Id == TarefaAtual.Id).FirstOrDefault();
                var dic = Utils.CompararObjetos(Utils.ObterAtributosNaoCompostos(TarefaAtual), Utils.ObterAtributosNaoCompostos(TarefaAntiga));
                if (dic.Count == 0)
                {
                    return;
                }
                historico.Alteracoes = Utils.SerializarDicionario(dic);
            }

            historico.DataAlteracao = DateTime.Now;
            historico.TipoAlteracao = alteracao;
            historico.TarefaId = TarefaAtual.Id;
            historico.UsuarioId = usuarioAlterador;
            _context.HistoricoTarefas.Add(historico);}

    }
}

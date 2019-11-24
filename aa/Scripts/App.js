function isNullOrWhitespace(input) {
    return (typeof input === 'undefined' || input == null)
        || input.replace(/\s/g, '').length < 1;
}

function SalvarEquipe() {
    const nome = $('#Nome')[0].value;
    if (isNullOrWhitespace(nome)) {
        toastr.error("Informe os dados de nome");
        return;
    }

    var ids = [];
    $('input[name="UsuariosSelecionados"]:checked').each((i, el) => {
        ids.push(el.value);
    });

    if (ids.length === 0) {
        toastr.info('Selecione ao menos um usuário para essa equipe');
        return;
    }

    $.ajax({
        url: `/Equipe/Create/?nome=${nome}&UsuariosId=${ids}`,
        method: "POST",
        success: function (result) {
            if ($.isPlainObject(result)) {
                toastr.error(result.Data, "Erro");
            }
            else {
                document.location.href = '/Equipe/Index';
            }
        },
        error: function (e) {
            toastr.error(e, "Erro interno!");
        }
    });
}

function EditarEquipe() {
    const nome = $('#Nome')[0].value;
    if (isNullOrWhitespace(nome)) {
        toastr.error("Informe os dados de nome");
        return;
    }

    const EquipeId = $('#Id')[0].value;
    var ids = [];
    if (isNullOrWhitespace(nome)) {
        toastr.error("Informe os dados de nome");
    }

    $('input[name="UsuariosSelecionados"]:checked').each((i, el) => {
        ids.push(el.value);
    });

    if (ids.length === 0) {
        toastr.error('Selecione ao menos um usuário para essa equipe');
        return;
    }

    $.ajax({
        url: `/Equipe/Edit/?equipeId=${EquipeId}&nome=${nome}&UsuariosId=${ids}`,
        method: "POST",
        success: function () {
            document.location.href = '/Equipe/Index';
        },
        error: function (e) {
            toastr.error(e.message, "Erro");
        }
    });
}


function ExcluirEquipe(EquipeId) {
    var r = confirm("Deseja realmente excluir essa equipe?");
    if (r == true) {
        $.ajax({
            url: "/Equipe/DeleteConfirmed/" + EquipeId,
            method: "POST",
            success: function () {
                $("#equipe_" + EquipeId).remove();
                toastr.success('Equipe excluida com sucesso!');
            },
            error: function () {
                toastr.error('Erro ao excluir equipe!');
            }
        });

    }
}

function modalHistoricoTarefa(TarefaId) {
    $.ajax({
        url: `/Tarefa/Historico?id=${TarefaId}`,
        method: "GET",
        success: function (result) {
            $('#infoTarefa').html(result)
            $('#ModalHistorico').modal('show')
        },
        error: function (e) {
            toastr.error("Erro: ", e);
        }
    });
}

function ExcluirTarefa(TarefaId) {
    var r = confirm("Deseja realmente excluir essa tarefa?");
    if (r == true) {
        $.ajax({
            url: "/Tarefa/DeleteConfirmed/" + TarefaId,
            method: "POST",
            success: function () {
                $("#tarefa_" + TarefaId).remove();
                toastr.success('Tarefa excluida com sucesso!');
            },
            error: function () {
                toastr.error('Erro ao excluir tarefa!');
            }
        });

    }
}

function SalvarProjeto() {
    const nome = $('#Nome')[0].value;
    if (isNullOrWhitespace(nome)) {
        toastr.error("Informe os dados de nome");
        return;
    }

    var ids = [];

    $('input[name="UsuariosSelecionados"]:checked').each((i, el) => {
        ids.push(el.value);
    });

    if (ids.length === 0) {
        toastr.info('Selecione ao menos um usuário para esse projeto');
        return;
    }

    $.ajax({
        url: `/Projeto/Create/?nome=${nome}&UsuariosId=${ids}`,
        method: "POST",
        success: function (result) {
            if ($.isPlainObject(result)) {
                toastr.error(result.Data, "Erro");
            }
            else {
                document.location.href = '/Projeto/Index';
            }
        },
        error: function (e) {
            toastr.error(e, "Erro interno!");
        }
    });
}

function EditarProjeto() {
    const nome = $('#Nome')[0].value;
    if (isNullOrWhitespace(nome)) {
        toastr.error("Informe os dados de nome");
        return;
    }

    const ProjetoId = $('#Id')[0].value;
    var ids = [];

    $('input[name="UsuariosSelecionados"]:checked').each((i, el) => {
        ids.push(el.value);
    });

    if (ids.length === 0) {
        toastr.error('Selecione ao menos um usuário para esse projeto!');
        return;
    }

    $.ajax({
        url: `/Projeto/Edit/?projetoId=${ProjetoId}&nome=${nome}&UsuariosId=${ids}`,
        method: "POST",
        success: function (result) {
            if ($.isPlainObject(result)) {
                toastr.error(result.Data, "Erro");
            }
            else {
                document.location.href = '/Projeto/Index';
            }
        },
        error: function (e) {
            toastr.error("Erro Interno!");
        }
    });
}


function ExcluirProjeto(ProjetoId) {
    var r = confirm("Deseja realmente excluir esse projeto?");
    if (r == true) {
        $.ajax({
            url: "/Projeto/DeleteConfirmed/" + ProjetoId,
            method: "POST",
            success: function () {
                $("#projeto_" + ProjetoId).remove();
                toastr.success('Projeto excluido com sucesso!');
            },
            error: function () {
                toastr.error('Erro ao excluir projeto!');
            }
        });

    }
}

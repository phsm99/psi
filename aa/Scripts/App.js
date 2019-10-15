function SalvarEquipe() {
    const nome = $('#Nome')[0].value;
    var ids = [];

    $('input[name="UsuariosSelecionados"]:checked').each((i, el) => {
        ids.push(el.value);
    });

    if (ids.length === 0) {
        alert('Selecione ao menos um usuário para essa equipe');
        return;
    }

    $.ajax({
        url: `/Equipe/Create/?nome=${nome}&UsuariosId=${ids}`,
        method: "POST",
        success: function () {
            document.location.href = '/Equipe/Index';
        },
        error: function (e) {
            alert("Erro: ", e);
        }
    });
}

function EditarEquipe() {
    const nome = $('#Nome')[0].value;
    const EquipeId = $('#Id')[0].value;
    var ids = [];
    if (!nome) {
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
            alert("Erro: ", e.message);
        }
    });
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
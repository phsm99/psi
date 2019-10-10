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
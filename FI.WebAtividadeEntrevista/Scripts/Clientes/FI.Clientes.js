$(document).ready(function () {
    $('#formCadastro').submit(function (e) {
        e.preventDefault();

        var $form = $(this);

        var dados = {
            "Nome": $form.find("#Nome").val(),
            "Sobrenome": $form.find("#Sobrenome").val(),
            "CPF": $form.find("#CPF").val().replace(/\D/g, ''),         
            "Nacionalidade": $form.find("#Nacionalidade").val(),
            "CEP": $form.find("#CEP").val().replace(/\D/g, ''),          
            "Estado": $form.find("#Estado").val(),
            "Cidade": $form.find("#Cidade").val(),
            "Logradouro": $form.find("#Logradouro").val(),
            "Email": $form.find("#Email").val(),
            "Telefone": $form.find("#Telefone").val().replace(/\D/g, '')  
        };

        var token = $form.find('input[name="__RequestVerificationToken"]').val();
        if (token) {
            dados.__RequestVerificationToken = token;
        }

        $.ajax({
            url: window.urlPost || '/Cliente/Incluir', 
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(dados),
            headers: token ? { 'RequestVerificationToken': token } : {},
            error: function (r) {
                if (r.responseJSON && r.responseJSON.Message) {
                    ModalDialog("Ocorreu um erro", r.responseJSON.Message);
                } else {
                    ModalDialog("Ocorreu um erro", "Não foi possível processar sua solicitação.");
                }
            },
            success: function (response) {
                if (response.Result === "OK" && response.Record && response.Record.Id) {
                    var novoIdCliente = response.Record.Id;
                    if (window.SalvarBeneficiariosAposCliente) {
                        window.SalvarBeneficiariosAposCliente(novoIdCliente).done(function () {
                            ModalDialog("Sucesso", "Cliente e beneficiários incluídos com sucesso!");
                            $("#formCadastro")[0].reset();
                        }).fail(function () {
                            ModalDialog("Atenção", "O cliente foi incluído, mas houve um erro ao salvar os beneficiários.");
                        });
                    } else {
                        ModalDialog("Sucesso", response.Message);
                        $("#formCadastro")[0].reset();
                    }
                } else {
                    ModalDialog("Sucesso", response.Message);
                    $("#formCadastro")[0].reset();
                }
            }
        });
    })
})

function ModalDialog(titulo, texto) {
    var textoExibicao = texto;
    if (typeof texto === 'object' && texto !== null) {
        textoExibicao = texto.Message || texto.statusText || JSON.stringify(texto);
    }

    var random = Math.random().toString().replace('.', '');
    var html = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + textoExibicao + '</p>                                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(html);
    $('#' + random).modal('show');
}

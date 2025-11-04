$(document).ready(function () {
    $('#formCadastro').submit(function (e) {
        e.preventDefault();

        var $form = $(this);

        // Normaliza campos com máscara
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
            "Telefone": $form.find("#Telefone").val().replace(/\D/g, '')  // opcional
        };

        // Anti-forgery (se a ação usar [ValidateAntiForgeryToken])
        var token = $form.find('input[name="__RequestVerificationToken"]').val();
        if (token) {
            dados.__RequestVerificationToken = token;
        }

        $.ajax({
            url: window.urlPost || '/Cliente/Adicionar',
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(dados),
            headers: token ? { 'RequestVerificationToken': token } : {},
            error:
                function (r) {
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu un erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r)
                    $("#formCadastro")[0].reset();
                }
        });
    })
})

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}

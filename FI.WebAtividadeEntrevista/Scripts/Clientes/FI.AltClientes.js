$(document).ready(function () {
    if (obj) {
        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #CPF').val(obj.CPF);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Telefone').val(obj.Telefone);
    }

    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "Id": obj.Id, 
                "Nome": $(this).find("#Nome").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "CPF": $(this).find("#CPF").val().replace(/\D/g, ''),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "CEP": $(this).find("#CEP").val().replace(/\D/g, ''),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Email": $(this).find("#Email").val(),
                "Telefone": $(this).find("#Telefone").val().replace(/\D/g, '')
            },
            error: function (r) {
                
                var errorMessage = "Ocorreu um erro interno no servidor.";
                if (r.responseJSON && r.responseJSON.Message) {
                    errorMessage = r.responseJSON.Message;
                }
                ModalDialog("Ocorreu um erro", errorMessage);
            },
            success: function (r) {
              
                ModalDialog("Sucesso!", "Cliente alterado com sucesso.");
                                              
                window.location.href = urlRetorno;
            }
        });
    })
    
})


function ModalDialog(titulo, texto) {
    
    var textoExibicao = texto;
    if (typeof texto === 'object' && texto !== null) {
        textoExibicao = texto.Message || JSON.stringify(texto);
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

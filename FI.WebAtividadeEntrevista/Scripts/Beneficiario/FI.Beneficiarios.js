(function () {
    var placeholderId = 'modal-placeholder-beneficiarios';
    var temp = []; 

    function soNumeros(s) { return (s || '').replace(/\D/g, ''); }
    function fmtCpf(cpf) {
        cpf = soNumeros(cpf);
        if (cpf.length !== 11) return cpf;
        return cpf.substr(0, 3) + '.' + cpf.substr(3, 3) + '.' + cpf.substr(6, 3) + '-' + cpf.substr(9, 2);
    }
    function maskIfAvailable($inp) {
        if (window.Mascaras && Mascaras.CPF) Mascaras.CPF($inp);
    }

    function cpfValido(cpf) {
        cpf = soNumeros(cpf);
        if (!cpf || cpf.length !== 11) return false;
        if (/^(\d)\1{10}$/.test(cpf)) return false;
        function dv(nums, k) {
            var soma = 0;
            for (var i = 0; i < nums.length; i++) soma += parseInt(nums[i], 10) * (k - i);
            var r = soma % 11;
            return r < 2 ? 0 : 11 - r;
        }
        var d1 = dv(cpf.substr(0, 9), 10);
        var d2 = dv(cpf.substr(0, 9) + d1, 11);
        return cpf.substr(9) === '' + d1 + d2;
    }

    function getIdCliente() {
        var v = $('#Id').val() || $('#IdCliente').val() || $('input[name="Id"]').val();
        if ((!v || isNaN(parseInt(v, 10))) && window.obj && (window.obj.Id || window.obj.id)) {
            v = window.obj.Id || window.obj.id;
        }
        var n = parseInt(v || '0', 10);
        return isNaN(n) ? 0 : n;
    }


    function renderTemp() {
        var html = '<table class="table"><thead><tr><th>CPF</th><th>Nome</th><th style="width:160px">Ações</th></tr></thead><tbody>';
        if (!temp.length) {
            html += '<tr><td colspan="3" class="text-center">Nenhum beneficiário adicionado</td></tr>';
        } else {
            temp.forEach(function (b, i) {
                html += '<tr data-i="' + i + '" data-orig-cpf="' + fmtCpf(b.CPF) + '" data-orig-nome="' + b.Nome + '">' +
                    '<td class="col-cpf">' + fmtCpf(b.CPF) + '</td>' +
                    '<td class="col-nome">' + b.Nome + '</td>' +
                    '<td class="col-acoes">' +
                        '<button class="btn btn-primary btn-sm ben-alterar">Alterar</button> ' +
                        '<button class="btn btn-primary btn-sm ben-excluir">Excluir</button>' +
                    '</td>' +
                '</tr>';
            });
        }
        html += '</tbody></table>';
        $('#gridBeneficiarios').html(html);
    }


    function renderServidor(idCliente) {
        $.ajax({
            url: '/Beneficiario/Listar',
            type: 'GET',
            dataType: 'json',
            data: { idCliente: idCliente }
        }).done(function (res) {
            if (res.Result !== 'OK') {
                $('#gridBeneficiarios').html('<div class="text-danger">' + (res.Message || 'Erro ao listar') + '</div>');
                return;
            }
            var rows = (res.Records || []);
            var html = '<table class="table"><thead><tr><th>CPF</th><th>Nome</th><th style="width:160px">Ações</th></tr></thead><tbody>';
            if (!rows.length) {
                html += '<tr><td colspan="3" class="text-center">Nenhum beneficiário cadastrado</td></tr>';
            } else {
                rows.forEach(function (r) {
                    html += '<tr data-id="' + r.Id + '" data-orig-cpf="' + r.CPF + '" data-orig-nome="' + r.Nome + '">' +
                        '<td class="col-cpf">' + r.CPF + '</td>' +
                        '<td class="col-nome">' + r.Nome + '</td>' +
                        '<td class="col-acoes">' +
                            '<button class="btn btn-primary btn-sm ben-alterar">Alterar</button> ' +
                            '<button class="btn btn-primary btn-sm ben-excluir">Excluir</button>' +
                        '</td>' +
                    '</tr>';
                });
            }
            html += '</tbody></table>';
            $('#gridBeneficiarios').html(html);
        }).fail(function (xhr) {
            $('#gridBeneficiarios').html('<div class="text-danger">' + ((xhr.responseJSON && xhr.responseJSON.Message) || 'Falha ao listar') + '</div>');
        });
    }

    function carregarBeneficiarios(idCliente) {
        if (!idCliente) renderTemp(); else renderServidor(idCliente);
    }

    function abrirModalBeneficiarios() {
        var idCliente = getIdCliente();
        var $ph = $('#' + placeholderId);
        if (!$ph.length) {
            $ph = $('<div/>', { id: placeholderId });
            $('body').append($ph);
        }

        $.ajax({
            url: '/Beneficiario/Modal',
            type: 'GET',
            data: { idCliente: idCliente }
        }).done(function (html) {
            $ph.html(html);
            var $modal = $('#modalBeneficiarios');
            if ($modal.length) {

                maskIfAvailable($('#benCpf'));
                $modal.modal('show');
                carregarBeneficiarios(idCliente);
            }
        }).fail(function (xhr) {
            var msg = (xhr.responseJSON && xhr.responseJSON.Message) || 'Não foi possível abrir a tela de beneficiários.';
            alert(msg);
        });
    }

    $(function () {
     
        $(document).on('click', '#btnBeneficiarios, #btn-beneficiarios', function (e) {
            e.preventDefault();
            abrirModalBeneficiarios();
        });


        $(document).on('click', '#btnIncluirBeneficiario', function (e) {
            e.preventDefault();
            var idCliente = getIdCliente();
            var cpf = $('#benCpf').val();
            var nome = $.trim($('#benNome').val());

            if (!cpf || !nome) { alert('Informe CPF e Nome.'); return; }
            if (!cpfValido(cpf)) { alert('CPF inválido'); return; }

            if (!idCliente) {
                
                var cpfNum = soNumeros(cpf);
                if (temp.some(function (t) { return soNumeros(t.CPF) === cpfNum; })) {
                    alert('Já existe beneficiário com este CPF na lista.');
                    return;
                }
                temp.push({ Id: 0, CPF: cpfNum, Nome: nome, IdCliente: 0 });
                $('#benCpf').val(''); $('#benNome').val('');
                renderTemp();
                return;
            }

         
            $.ajax({
                url: '/Beneficiario/Incluir',
                type: 'POST',
                dataType: 'json',
                data: { CPF: soNumeros(cpf), Nome: nome, IdCliente: idCliente }
            }).done(function () {
                $('#benCpf').val(''); $('#benNome').val('');
                carregarBeneficiarios(idCliente);
            }).fail(function (xhr) {
                var msg = (xhr.responseJSON && xhr.responseJSON.Message) || 'Erro ao incluir beneficiário';
                alert(msg);
            });
        });


        $(document).on('click', '#gridBeneficiarios .ben-excluir', function () {
            var $tr = $(this).closest('tr');
            var idCliente = getIdCliente();


            if (!idCliente) {
                var i = +$tr.data('i');
                temp.splice(i, 1);
                renderTemp();
                return;
            }


            var id = $tr.data('id');
            if (!confirm('Confirma a exclusão do beneficiário?')) return;
            $.ajax({
                url: '/Beneficiario/Excluir',
                type: 'POST',
                dataType: 'json',
                data: { id: id }
            }).done(function () {
                carregarBeneficiarios(idCliente);
            }).fail(function (xhr) {
                var msg = (xhr.responseJSON && xhr.responseJSON.Message) || 'Erro ao excluir beneficiário';
                alert(msg);
            });
        });

 
        $(document).on('click', '#gridBeneficiarios .ben-alterar', function () {
            $('#gridBeneficiarios tr[data-editing="1"] .ben-cancelar').trigger('click');

            var $tr = $(this).closest('tr');
            var cpf = $tr.find('.col-cpf').text();
            var nome = $tr.find('.col-nome').text();

            $tr.attr('data-editing', '1');
            $tr.find('.col-cpf').html('<input type="text" class="form-control ben-cpf-edit" value="' + cpf + '">');
            $tr.find('.col-nome').html('<input type="text" class="form-control ben-nome-edit" value="' + nome + '">');

            var $acoes = $tr.find('.col-acoes').empty();
            $('<button class="btn btn-success btn-sm ben-salvar">Salvar</button>').appendTo($acoes);
            $('<span> </span>').appendTo($acoes);
            $('<button class="btn btn-default btn-sm ben-cancelar">Cancelar</button>').appendTo($acoes);

            maskIfAvailable($tr.find('.ben-cpf-edit'));
            $tr.find('.ben-cpf-edit').focus();
        });

       
        $(document).on('click', '#gridBeneficiarios .ben-cancelar', function () {
            var $tr = $(this).closest('tr');
            var origCpf = $tr.attr('data-orig-cpf');
            var origNome = $tr.attr('data-orig-nome');

            $tr.removeAttr('data-editing');
            $tr.find('.col-cpf').text(origCpf);
            $tr.find('.col-nome').text(origNome);

            var $acoes = $tr.find('.col-acoes').empty();
            $('<button class="btn btn-primary btn-sm ben-alterar">Alterar</button>').appendTo($acoes);
            $('<span> </span>').appendTo($acoes);
            $('<button class="btn btn-primary btn-sm ben-excluir">Excluir</button>').appendTo($acoes);
        });

        $(document).on('click', '#gridBeneficiarios .ben-salvar', function () {
            var $tr = $(this).closest('tr');
            var idCliente = getIdCliente();
            var cpf = $tr.find('.ben-cpf-edit').val();
            var nome = $.trim($tr.find('.ben-nome-edit').val());
            if (!cpf || !nome) { alert('Informe CPF e Nome.'); return; }
            if (!cpfValido(cpf)) { alert('CPF inválido'); return; }

            if (!idCliente) {
       
                var i = +$tr.data('i');

                var cpfNum = soNumeros(cpf);
                var dup = temp.some(function (b, idx) { return idx !== i && soNumeros(b.CPF) === cpfNum; });
                if (dup) { alert('Já existe beneficiário com este CPF na lista.'); return; }

                temp[i].CPF = cpfNum;
                temp[i].Nome = nome;
                renderTemp();
                return;
            }


            var id = $tr.data('id');
            $.ajax({
                url: '/Beneficiario/Alterar',
                type: 'POST',
                dataType: 'json',
                data: { Id: id, CPF: soNumeros(cpf), Nome: nome, IdCliente: idCliente }
            }).done(function () {
                renderServidor(idCliente);
            }).fail(function (xhr) {
                var msg = (xhr.responseJSON && xhr.responseJSON.Message) || 'Erro ao salvar beneficiário';
                alert(msg);
            });
        });

        window.SalvarBeneficiariosAposCliente = function (novoIdCliente) {
            if (!temp.length) return $.Deferred().resolve().promise();

            var chamadas = temp.map(function (b) {
                return $.ajax({
                    url: '/Beneficiario/Incluir',
                    type: 'POST',
                    dataType: 'json',
                    data: { CPF: soNumeros(b.CPF), Nome: b.Nome, IdCliente: novoIdCliente }
                });
            });

            return $.when.apply($, chamadas).always(function () {
                temp = [];
            }).promise();
        };
    });
})();

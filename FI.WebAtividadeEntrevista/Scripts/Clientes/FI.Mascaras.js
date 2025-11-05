(function ($) {
    'use strict';


    function formatCPF(value) {
        var d = (value || '').replace(/\D/g, '').slice(0, 11);
        var out = '';
        if (d.length > 0) out = d.substring(0, 3);
        if (d.length >= 4) out += '.' + d.substring(3, 6);
        if (d.length >= 7) out += '.' + d.substring(6, 9);
        if (d.length >= 10) out += '-' + d.substring(9, 11);
        return out;
    }


    $(function () {
        var $cpf = $('#CPF');
        if ($cpf.length) {
            $cpf.on('input', function () {
                this.value = formatCPF(this.value);
            });

            var initial = $cpf.val();
            if (initial) {
                $cpf.val(formatCPF(initial));
            }
        }
    });

})(jQuery);

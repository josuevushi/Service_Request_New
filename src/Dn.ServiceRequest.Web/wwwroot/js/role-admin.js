/**
 * Role Admin JS
 */

'use strict';

(function () {
    // DataTable
    $(function () {
        var dt_role = $('#roleTable');

        if (dt_role.length) {
            var dt_role_table = dt_role.DataTable({
                order: [[0, 'asc']],
                displayLength: 7,
                dom:
                    '<"row"' +
                    '<"col-md-2"<"me-3"l>>' +
                    '<"col-md-10"<"dt-action-buttons text-xl-end text-lg-start text-md-end text-start d-flex align-items-center justify-content-end flex-md-row flex-column mb-4 mb-md-0"fB>>' +
                    '>t' +
                    '<"row"' +
                    '<"col-sm-12 col-md-6"i>' +
                    '<"col-sm-12 col-md-6"p>' +
                    '>',
                lengthMenu: [7, 10, 15, 20],
                language: {
                    sLengthMenu: '_MENU_',
                    search: '',
                    searchPlaceholder: 'Rechercher Rôle',
                    paginate: {
                        next: '<i class="ti ti-chevron-right ti-sm"></i>',
                        previous: '<i class="ti ti-chevron-left ti-sm"></i>'
                    }
                },
                buttons: []
            });
        }
    });
})();

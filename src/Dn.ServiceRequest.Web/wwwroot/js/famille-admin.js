/**
 * Famille Admin JS
 */

'use strict';

// Functions to handle the Delete Famille Sweet Alerts
function showDeleteConfirmation(familleId, familleNom) {
    Swal.fire({
        title: 'Supprimer la Famille',
        html: `<p class="text-danger">Êtes-vous sûr de vouloir supprimer la famille ?<br> <span class="fw-medium text-body">${familleNom}</span></p>`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Supprimer',
        cancelButtonText: 'Annuler',
        customClass: {
            confirmButton: 'btn btn-primary waves-effect waves-light',
            cancelButton: 'btn btn-label-secondary waves-effect waves-light'
        }
    }).then(result => {
        if (result.isConfirmed) {
            const form = document.getElementById(familleId + '-deleteForm');
            if (form) {
                form.submit();
                sessionStorage.setItem('familleAction', 'deleted');
            }
        }
    });
}

(function () {
    // Success Alert
    function showSuccessAlert(action) {
        Swal.fire({
            title: 'Succès',
            text: `Famille ${action} avec succès !`,
            icon: 'success',
            confirmButtonText: 'Ok',
            customClass: {
                confirmButton: 'btn btn-success waves-effect waves-light'
            }
        });
    }

    const action = sessionStorage.getItem('familleAction');
    if (action) {
        showSuccessAlert(action);
        sessionStorage.removeItem('familleAction');
    }

    // Handle Edit Famille Modal
    const handleEditFamilleModal = editButton => {
        const familleId = editButton.getAttribute('data-id');
        const row = editButton.closest('tr');

        if (!row) return;

        const nom = row.querySelector(`.famille-nom-${familleId}`).innerText;
        const prefixe = row.querySelector(`.famille-prefixe-${familleId}`).innerText;

        document.getElementById('EditFamilleId').value = familleId;
        document.getElementById('EditFamille_Nom').value = nom;
        document.getElementById('EditFamille_Prefixe').value = prefixe;
    };

    document.addEventListener('click', function (e) {
        const editButton = e.target.closest('.edit-famille-button');
        if (editButton) {
            handleEditFamilleModal(editButton);
        }
    });

    // Form Validation - Create
    const createForm = document.getElementById('createFamilleForm');
    if (createForm) {
        FormValidation.formValidation(createForm, {
            fields: {
                'NewFamille.Nom': { validators: { notEmpty: { message: 'Veuillez entrer un nom' } } },
                'NewFamille.Prefixe': { validators: { notEmpty: { message: 'Veuillez entrer un préfixe' } } }
            },
            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap5: new FormValidation.plugins.Bootstrap5({
                    eleValidClass: 'is-valid',
                    rowSelector: '.mb-6'
                }),
                submitButton: new FormValidation.plugins.SubmitButton(),
                autoFocus: new FormValidation.plugins.AutoFocus()
            }
        }).on('core.form.valid', function () {
            sessionStorage.setItem('familleAction', 'créée');
            createForm.submit();
        });
    }

    // Form Validation - Edit
    const editForm = document.getElementById('editFamilleForm');
    if (editForm) {
        FormValidation.formValidation(editForm, {
            fields: {
                'Famille.Nom': { validators: { notEmpty: { message: 'Veuillez entrer un nom' } } },
                'Famille.Prefixe': { validators: { notEmpty: { message: 'Veuillez entrer un préfixe' } } }
            },
            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap5: new FormValidation.plugins.Bootstrap5({
                    eleValidClass: 'is-valid',
                    rowSelector: '.mb-6'
                }),
                submitButton: new FormValidation.plugins.SubmitButton(),
                autoFocus: new FormValidation.plugins.AutoFocus()
            }
        }).on('core.form.valid', function () {
            sessionStorage.setItem('familleAction', 'mise à jour');
            editForm.submit();
        });
    }

    // DataTable
    $(function () {
        $('#familleTable').DataTable({
            order: [[2, 'asc']],
            displayLength: 7,
            dom: '<"row"<"col-md-2"<"me-3"l>><"col-md-10"<"dt-action-buttons text-xl-end text-lg-start text-md-end text-start d-flex align-items-center justify-content-end flex-md-row flex-column mb-4 mb-md-0"fB>>>t<"row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>',
            lengthMenu: [7, 10, 15, 20],
            language: {
                sLengthMenu: '_MENU_',
                search: '',
                searchPlaceholder: 'Rechercher Famille',
                paginate: {
                    next: '<i class="ti ti-chevron-right ti-sm"></i>',
                    previous: '<i class="ti ti-chevron-left ti-sm"></i>'
                }
            },
            buttons: []
        });
    });
})();

/**
 * Groupe Admin JS
 */

'use strict';

// Functions to handle the Delete Groupe Sweet Alerts
function showDeleteConfirmation(groupeId, groupeNom) {
    Swal.fire({
        title: 'Supprimer le Groupe',
        html: `<p class="text-danger">Êtes-vous sûr de vouloir supprimer le groupe ?<br> <span class="fw-medium text-body">${groupeNom}</span></p>`,
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
            const form = document.getElementById(groupeId + '-deleteForm');
            if (form) {
                form.submit();
                sessionStorage.setItem('groupeAction', 'supprimé');
            }
        }
    });
}

(function () {
    // Success Alert
    function showSuccessAlert(action) {
        Swal.fire({
            title: 'Succès',
            text: `Groupe ${action} avec succès !`,
            icon: 'success',
            confirmButtonText: 'Ok',
            customClass: {
                confirmButton: 'btn btn-success waves-effect waves-light'
            }
        });
    }

    const action = sessionStorage.getItem('groupeAction');
    if (action) {
        showSuccessAlert(action);
        sessionStorage.removeItem('groupeAction');
    }

    // Handle Edit Groupe Modal
    const handleEditGroupeModal = editButton => {
        const groupeId = editButton.getAttribute('data-id');
        const row = editButton.closest('tr');

        if (!row) return;

        const nom = row.querySelector(`.groupe-nom-${groupeId}`).innerText;

        document.getElementById('EditGroupeId').value = groupeId;
        document.getElementById('EditGroupe_Nom').value = nom;
    };

    document.addEventListener('click', function (e) {
        const editButton = e.target.closest('.edit-groupe-button');
        if (editButton) {
            handleEditGroupeModal(editButton);
        }
    });

    // Form Validation - Create
    const createForm = document.getElementById('createGroupeForm');
    if (createForm) {
        FormValidation.formValidation(createForm, {
            fields: {
                'NewGroupe.Nom': { validators: { notEmpty: { message: 'Veuillez entrer un nom' } } }
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
            sessionStorage.setItem('groupeAction', 'créé');
            createForm.submit();
        });
    }

    // Form Validation - Edit
    const editForm = document.getElementById('editGroupeForm');
    if (editForm) {
        FormValidation.formValidation(editForm, {
            fields: {
                'Groupe.Nom': { validators: { notEmpty: { message: 'Veuillez entrer un nom' } } }
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
            sessionStorage.setItem('groupeAction', 'mis à jour');
            editForm.submit();
        });
    }

    // DataTable
    $(function () {
        $('#groupeTable').DataTable({
            order: [[2, 'asc']],
            displayLength: 7,
            dom: '<"row"<"col-md-2"<"me-3"l>><"col-md-10"<"dt-action-buttons text-xl-end text-lg-start text-md-end text-start d-flex align-items-center justify-content-end flex-md-row flex-column mb-4 mb-md-0"fB>>>t<"row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>',
            lengthMenu: [7, 10, 15, 20],
            language: {
                sLengthMenu: '_MENU_',
                search: '',
                searchPlaceholder: 'Rechercher Groupe',
                paginate: {
                    next: '<i class="ti ti-chevron-right ti-sm"></i>',
                    previous: '<i class="ti ti-chevron-left ti-sm"></i>'
                }
            },
            buttons: []
        });
    });
})();

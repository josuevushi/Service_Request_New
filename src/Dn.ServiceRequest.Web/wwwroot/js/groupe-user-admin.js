/**
 * Groupe User Admin JS
 */

'use strict';

// Functions to handle Delete
function showDeleteConfirmation(guId, userEmail, groupeNom) {
    Swal.fire({
        title: 'Supprimer l\'Association',
        html: `<p class="text-danger">Dissocier <span class="fw-medium text-body">${userEmail}</span> du groupe <span class="fw-medium text-body">${groupeNom}</span> ?</p>`,
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
            const form = document.getElementById(guId + '-deleteForm');
            if (form) {
                form.submit();
                sessionStorage.setItem('guAction', 'supprimée');
            }
        }
    });
}

(function () {
    // Success Alert
    function showSuccessAlert(action) {
        Swal.fire({
            title: 'Succès',
            text: `Association ${action} avec succès !`,
            icon: 'success',
            confirmButtonText: 'Ok',
            customClass: {
                confirmButton: 'btn btn-success waves-effect waves-light'
            }
        });
    }

    const action = sessionStorage.getItem('guAction');
    if (action) {
        showSuccessAlert(action);
        sessionStorage.removeItem('guAction');
    }

    // Handle Edit Modal
    const handleEditGUModal = editButton => {
        const guId = editButton.getAttribute('data-id');
        const row = editButton.closest('tr');

        if (!row) return;

        const groupeId = row.querySelector(`.gu-groupe-${guId}`).getAttribute('data-groupe-id');
        const userId = row.querySelector(`.gu-user-${guId}`).getAttribute('data-user-id');
        const isReceiver = row.querySelector(`.gu-receiver-${guId}`).getAttribute('data-is-receiver') === 'true';

        document.getElementById('EditGroupeUserId').value = guId;
        $('#EditGU_GroupeId').val(groupeId).trigger('change');
        $('#EditGU_UserId').val(userId).trigger('change');
        document.getElementById('EditGU_IsReceiver').checked = isReceiver;
    };

    document.addEventListener('click', function (e) {
        const editButton = e.target.closest('.edit-gu-button');
        if (editButton) {
            handleEditGUModal(editButton);
        }
    });

    // Select2 Init
    $(function () {
        $('.select2').each(function () {
            var $this = $(this);
            $this.wrap('<div class="position-relative"></div>').select2({
                placeholder: $this.find('option:first').text(),
                dropdownParent: $this.parent()
            });
        });
    });

    // Form Validation - Create
    const createForm = document.getElementById('createGroupeUserForm');
    if (createForm) {
        FormValidation.formValidation(createForm, {
            fields: {
                'NewGroupeUser.Groupe_id': { validators: { notEmpty: { message: 'Veuillez sélectionner un groupe' } } },
                'NewGroupeUser.User_id': { validators: { notEmpty: { message: 'Veuillez sélectionner un utilisateur' } } }
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
            sessionStorage.setItem('guAction', 'créée');
            createForm.submit();
        });
    }

    // Form Validation - Edit
    const editForm = document.getElementById('editGroupeUserForm');
    if (editForm) {
        FormValidation.formValidation(editForm, {
            fields: {
                'GroupeUser.Groupe_id': { validators: { notEmpty: { message: 'Veuillez sélectionner un groupe' } } },
                'GroupeUser.User_id': { validators: { notEmpty: { message: 'Veuillez sélectionner un utilisateur' } } }
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
            sessionStorage.setItem('guAction', 'mise à jour');
            editForm.submit();
        });
    }

    // DataTable
    $(function () {
        $('#groupeUserTable').DataTable({
            order: [[1, 'asc']],
            displayLength: 7,
            dom: '<"row"<"col-md-2"<"me-3"l>><"col-md-10"<"dt-action-buttons text-xl-end text-lg-start text-md-end text-start d-flex align-items-center justify-content-end flex-md-row flex-column mb-4 mb-md-0"fB>>>t<"row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>',
            lengthMenu: [7, 10, 15, 20],
            language: {
                sLengthMenu: '_MENU_',
                search: '',
                searchPlaceholder: 'Rechercher Association',
                paginate: {
                    next: '<i class="ti ti-chevron-right ti-sm"></i>',
                    previous: '<i class="ti ti-chevron-left ti-sm"></i>'
                }
            },
            buttons: []
        });
    });
})();

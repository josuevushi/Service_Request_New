/**
 * Groupe Type Admin JS
 */

'use strict';

// Functions to handle Delete
function showDeleteConfirmation(gtId, typeNom, groupeNom) {
    Swal.fire({
        title: 'Supprimer l\'Association',
        html: `<p class="text-danger">Dissocier le type <span class="fw-medium text-body">${typeNom}</span> du groupe <span class="fw-medium text-body">${groupeNom}</span> ?</p>`,
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
            const form = document.getElementById(gtId + '-deleteForm');
            if (form) {
                form.submit();
                sessionStorage.setItem('gtAction', 'supprimée');
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

    const action = sessionStorage.getItem('gtAction');
    if (action) {
        showSuccessAlert(action);
        sessionStorage.removeItem('gtAction');
    }

    // Handle Edit Modal
    const handleEditGTModal = editButton => {
        const gtId = editButton.getAttribute('data-id');
        const row = editButton.closest('tr');

        if (!row) return;

        const groupeId = row.querySelector(`.gt-groupe-${gtId}`).getAttribute('data-groupe-id');
        const typeId = row.querySelector(`.gt-type-${gtId}`).getAttribute('data-type-id');

        document.getElementById('EditGroupeTypeId').value = gtId;
        $('#EditGT_GroupeId').val(groupeId).trigger('change');
        $('#EditGT_TypeId').val(typeId).trigger('change');
    };

    document.addEventListener('click', function (e) {
        const editButton = e.target.closest('.edit-gt-button');
        if (editButton) {
            handleEditGTModal(editButton);
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
    const createForm = document.getElementById('createGroupeTypeForm');
    if (createForm) {
        FormValidation.formValidation(createForm, {
            fields: {
                'NewGroupeType.Groupe_id': { validators: { notEmpty: { message: 'Veuillez sélectionner un groupe' } } },
                'NewGroupeType.Type_id': { validators: { notEmpty: { message: 'Veuillez sélectionner un type' } } }
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
            sessionStorage.setItem('gtAction', 'créée');
            createForm.submit();
        });
    }

    // Form Validation - Edit
    const editForm = document.getElementById('editGroupeTypeForm');
    if (editForm) {
        FormValidation.formValidation(editForm, {
            fields: {
                'GroupeType.Groupe_id': { validators: { notEmpty: { message: 'Veuillez sélectionner un groupe' } } },
                'GroupeType.Type_id': { validators: { notEmpty: { message: 'Veuillez sélectionner un type' } } }
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
            sessionStorage.setItem('gtAction', 'mise à jour');
            editForm.submit();
        });
    }

    // DataTable
    $(function () {
        $('#groupeTypeTable').DataTable({
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

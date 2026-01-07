/**
 * Type Demande Admin JS
 */

'use strict';

// Functions to handle the Delete Type Sweet Alerts
function showDeleteConfirmation(typeId, typeNom) {
    Swal.fire({
        title: 'Supprimer le Type',
        html: `<p class="text-danger">Êtes-vous sûr de vouloir supprimer le type ?<br> <span class="fw-medium text-body">${typeNom}</span></p>`,
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
            const form = document.getElementById(typeId + '-deleteForm');
            if (form) {
                form.submit();
                sessionStorage.setItem('typeAction', 'supprimé');
            }
        }
    });
}

(function () {
    // Success Alert
    function showSuccessAlert(action) {
        Swal.fire({
            title: 'Succès',
            text: `Type ${action} avec succès !`,
            icon: 'success',
            confirmButtonText: 'Ok',
            customClass: {
                confirmButton: 'btn btn-success waves-effect waves-light'
            }
        });
    }

    const action = sessionStorage.getItem('typeAction');
    if (action) {
        showSuccessAlert(action);
        sessionStorage.removeItem('typeAction');
    }

    // Handle Edit Type Modal
    const handleEditTypeModal = editButton => {
        const typeId = editButton.getAttribute('data-id');
        const row = editButton.closest('tr');

        if (!row) return;

        const nom = row.querySelector(`.type-nom-${typeId}`).innerText;
        const familleId = row.querySelector(`.type-famille-${typeId}`).getAttribute('data-famille-id');
        const sla = row.querySelector(`.type-sla-${typeId}`).innerText;
        const jsonForm = row.querySelector(`.type-json-form-${typeId}`).innerText;
        const jsonStep = row.querySelector(`.type-json-step-${typeId}`).innerText;

        document.getElementById('EditTypeId').value = typeId;
        document.getElementById('EditType_Nom').value = nom;
        document.getElementById('EditType_Sla').value = sla;
        document.getElementById('EditType_JsonForm').value = jsonForm;
        document.getElementById('EditType_JsonStep').value = jsonStep;

        // Set Select2 value
        $('#EditType_FamilleId').val(familleId).trigger('change');
    };

    document.addEventListener('click', function (e) {
        const editButton = e.target.closest('.edit-type-button');
        if (editButton) {
            handleEditTypeModal(editButton);
        }
    });

    // Select2 Init
    $(function () {
        $('.select2').each(function () {
            var $this = $(this);
            $this.wrap('<div class="position-relative"></div>').select2({
                placeholder: 'Sélectionner une famille',
                dropdownParent: $this.parent()
            });
        });
    });

    // Form Validation - Create
    const createForm = document.getElementById('createTypeForm');
    if (createForm) {
        FormValidation.formValidation(createForm, {
            fields: {
                'NewType.Nom': { validators: { notEmpty: { message: 'Veuillez entrer un nom' } } },
                'NewType.Famille_id': { validators: { notEmpty: { message: 'Veuillez sélectionner une famille' } } },
                'NewType.Sla': { validators: { notEmpty: { message: 'Veuillez entrer un SLA' } } }
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
            sessionStorage.setItem('typeAction', 'créé');
            createForm.submit();
        });
    }

    // Form Validation - Edit
    const editForm = document.getElementById('editTypeForm');
    if (editForm) {
        FormValidation.formValidation(editForm, {
            fields: {
                'Type.Nom': { validators: { notEmpty: { message: 'Veuillez entrer un nom' } } },
                'Type.Famille_id': { validators: { notEmpty: { message: 'Veuillez sélectionner une famille' } } },
                'Type.Sla': { validators: { notEmpty: { message: 'Veuillez entrer un SLA' } } }
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
            sessionStorage.setItem('typeAction', 'mis à jour');
            editForm.submit();
        });
    }

    // DataTable
    $(function () {
        $('#typeTable').DataTable({
            order: [[2, 'asc']],
            displayLength: 7,
            dom: '<"row"<"col-md-2"<"me-3"l>><"col-md-10"<"dt-action-buttons text-xl-end text-lg-start text-md-end text-start d-flex align-items-center justify-content-end flex-md-row flex-column mb-4 mb-md-0"fB>>>t<"row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>',
            lengthMenu: [7, 10, 15, 20],
            language: {
                sLengthMenu: '_MENU_',
                search: '',
                searchPlaceholder: 'Rechercher Type',
                paginate: {
                    next: '<i class="ti ti-chevron-right ti-sm"></i>',
                    previous: '<i class="ti ti-chevron-left ti-sm"></i>'
                }
            },
            buttons: []
        });
    });
})();

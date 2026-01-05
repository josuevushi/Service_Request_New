

function copyTicket() {
  const ticketEl = document.getElementById("ticketNumber");
  const text = ticketEl.innerText;

  navigator.clipboard.writeText(text).then(() => {
    const oldText = ticketEl.innerText;
    ticketEl.innerText = "Copié ✔";

    setTimeout(() => {
      ticketEl.innerText = oldText;
    }, 1500);
  });
}
$(function () {
  abp.ajax.defaultHeaders = {
    'Content-Type': 'application/json; charset=utf-8',
    'X-XSRF-TOKEN': abp.security.antiForgery.getToken()
  };


  function formatTemps(ms) {
    let secondes = Math.floor(ms / 1000);
    let minutes = Math.floor(secondes / 60);
    let heures = Math.floor(minutes / 60);
    let jours = Math.floor(heures / 24);

    secondes %= 60;
    minutes %= 60;
    heures %= 24;

    const parts = [];
    if (jours > 0) parts.push(jours + " jour" + (jours > 1 ? "s" : ""));
    if (heures > 0) parts.push(heures + " heure" + (heures > 1 ? "s" : ""));
    if (minutes > 0) parts.push(minutes + " minute" + (minutes > 1 ? "s" : ""));
    if (secondes > 0 && jours === 0) parts.push(secondes + " seconde" + (secondes > 1 ? "s" : ""));
    // On affiche les secondes seulement si moins d'un jour pour que ce soit lisible

    return parts.join(" et ");
  }

  let base64Files = [];
  let uploadedPaths = [];

  const uploadZone = document.getElementById("uploadZone");
  const fileInput = document.getElementById("fileInput");
  const fileList = document.getElementById("fileList");

  uploadZone.addEventListener("click", () => fileInput.click());

  fileInput.addEventListener("change", async (event) => {
    await handleFiles(event.target.files);
  });

  uploadZone.addEventListener("dragover", (e) => {
    e.preventDefault();
    uploadZone.style.borderColor = "#0d6efd";
  });

  uploadZone.addEventListener("dragleave", () => {
    uploadZone.style.borderColor = "#6c757d";
  });

  uploadZone.addEventListener("drop", async (e) => {
    e.preventDefault();
    uploadZone.style.borderColor = "#6c757d";
    await handleFiles(e.dataTransfer.files);
  });

  async function handleFiles(files) {
    for (let file of files) {
      const base64 = await toBase64(file);

      const fileObj = {
        id: crypto.randomUUID(),  // ID unique pour supprimer
        FileName: file.name,
        Type: file.type,
        Size: file.size,
        Base64: base64,
        filePath: null // Will be populated after upload
      };

      base64Files.push(fileObj);
      displayFile(fileObj);
      console.log("avant");

      console.log("Bearer " + abp.auth.getToken());

      // Upload the file immediately
      fetch("/api/files", {
        method: "POST",
        headers: {
          'Content-Type': 'application/json',
          'X-XSRF-TOKEN': abp.security.antiForgery.getToken(),
          'Authorization': "Bearer " + abp.auth.getToken()
        },
        body: JSON.stringify(fileObj)
      })
        .then(response => {
          if (response.ok) {
            console.log("File uploaded successfully");
            return response.json();
          } else {
            console.error("File upload failed");
            return response.text().then(text => { throw new Error(text) });
          }
        })
        .then(data => {
          console.log("Upload response:", data);
          if (data.filePath) {
            fileObj.filePath = data.filePath;
            uploadedPaths.push(data.filePath);
            console.log("Current uploaded paths:", uploadedPaths);
          }
        })
        .catch(error => {
          console.error("Error uploading file:", error);
          alert("Erreur lors de l'upload du fichier: " + error.message);
        });
      /* fetch("/api/app/piece-jointe/new-file", {
        method: "POST",
        body: fileObj,
        headers: {
              "Authorization": "Bearer " + abp.auth.getToken(),
            "Accept": "application/json",
            "X-Requested-With": "XMLHttpRequest"
        
    });*/
      /*  dn.serviceRequest.pieceJointes.pieceJointe.postNewFile(fileObj).then(function(result){
    
      console.log(result);
       console.log("apres et success");
    });*/
    }

    console.log("Tableau JSON des fichiers Base64 :", base64Files);
  }

  function displayFile(fileObj) {
    const li = document.createElement("li");
    li.setAttribute("data-id", fileObj.id);

    li.innerHTML = `
    <span>${fileObj.FileName}</span>
    <button class="btn btn-sm btn-danger remove-btn">Supprimer</button>
  `;

    // Action supprimer
    li.querySelector(".remove-btn").addEventListener("click", () => {
      removeFile(fileObj.id);
    });

    fileList.appendChild(li);
  }

  function removeFile(id) {
    // Trouver le fichier
    const fileToDelete = base64Files.find(f => f.id === id);

    if (fileToDelete && fileToDelete.filePath) {
      // Appeler l'API de suppression
      fetch(`/api/files/delete?filePath=${encodeURIComponent(fileToDelete.filePath)}`, {
        method: "DELETE",
        headers: {
          'X-XSRF-TOKEN': abp.security.antiForgery.getToken(),
          'Authorization': "Bearer " + abp.auth.getToken()
        }
      })
        .then(response => {
          if (response.ok) {
            console.log("File deleted from server");
          } else {
            console.error("Failed to delete file from server");
          }
        })
        .catch(error => {
          console.error("Error deleting file:", error);
        });
    }

    // Supprimer du tableau JSON
    base64Files = base64Files.filter(f => f.id !== id);

    if (fileToDelete && fileToDelete.filePath) {
      uploadedPaths = uploadedPaths.filter(p => p !== fileToDelete.filePath);
      console.log("Current uploaded paths after delete:", uploadedPaths);
    }

    // Supprimer du DOM
    const li = fileList.querySelector(`[data-id="${id}"]`);
    if (li) li.remove();

    console.log("Après suppression :", base64Files);
  }

  function toBase64(file) {
    return new Promise((resolve) => {
      const reader = new FileReader();
      reader.onload = () => resolve(reader.result.split(",")[1]);
      reader.readAsDataURL(file);
    });
  }

  //console.log(formatTemps(90061000));
  dn.serviceRequest.types.type.getList('')
    .then(function (result) {
      // result.items contient tes objets JSON
      const $select = $('#ticket-type');

      result.items.forEach(function (item) {
        // Ajouter chaque type comme option
        $select.append(
          $('<option>', {
            value: item.id,       // id de l'objet JSON
            text: item.nom        // nom du type
          })
        );

      });
      $select.append(
        $('<option>', {
          value: '',       // id de l'objet JSON
          text: ''        // nom du type
        }));

      // console.log(result.items); // Pour vérifier
      $('#ticket-type').select2().trigger('change');
    })
    .catch(function (err) {
      console.error('Erreur lors de la récupération des types:', err);
    });

  $('#ticket-type').on('change', function () {
    // SLA
    document.getElementById('sla').value = "XXXXXXXXXX";

    // Catégorie
    document.getElementById('categorie').value = "XXXXXXXXXX";

    // Groupe
    document.getElementById('groupe').value = "XXXXXXXXXX";

    // Recepteur
    document.getElementById('recepteur').value = "XXXXXXXXXX";
    const selectedId = $(this).val(); // récupère l'ID sélectionné
    dn.serviceRequest.types.type.getTypeDetails(selectedId).then(function (result) {
      console.log(result);

      if (result.length > 0) {
        const data = result[0];
        // SLA
        document.getElementById('sla').value = formatTemps(data.sla);
        // Catégorie
        document.getElementById('categorie').value = data.categorie;
        // Groupe
        document.getElementById('groupe').value = data.groupe;
        // Recepteur
        document.getElementById('recepteur').value = data.user;
      }
    });

    console.log('ID sélectionné :', selectedId);

    // Tu peux faire ce que tu veux avec selectedId ici
  });



  $('#ticketForm').on('submit', function (e) {
    e.preventDefault();

    // masquer le bouton normal, afficher le bouton loading
    $("#btnSubmit").addClass("d-none");
    $("#btnLoading").removeClass("d-none");

    // Récupération des valeurs
    const objet = $('#ticket-object').val().trim();
    const description = $('#ticket-description').val().trim();
    const type = $('#ticket-type').val();

    // VALIDATIONS
    let hasError = false;

    if (objet.length < 3) {
      $('#ticket-object').addClass('is-invalid');
      hasError = true;
    } else {
      $('#ticket-object').removeClass('is-invalid');
    }

    if (description.length < 3) {
      $('#ticket-description').addClass('is-invalid');
      hasError = true;
    } else {
      $('#ticket-description').removeClass('is-invalid');
    }

    if (!type) {
      $('#ticket-type').addClass('is-invalid');
      alert("Veuillez sélectionner un type de requête valide.");
      hasError = true;
    } else {
      $('#ticket-type').removeClass('is-invalid');
    }

    if (hasError) {
      // Restaurer les boutons et arrêter le submit
      $("#btnLoading").addClass("d-none");
      $("#btnSubmit").removeClass("d-none");
      return;
    }

    // Construction de l’objet ticket pour affichage
    const ticketData = {
      objet: objet,
      description: description,
      idenType: type,
      jsonFrom: "-",
      fichiers: uploadedPaths
      /*
      categorie: $('#categorie').val(),
      groupe: $('#groupe').val(),
      recepteur: $('#recepteur').val()
      */
    };


    console.log("Données du ticket :", ticketData);
    console.log("Type sélectionné :", type);

    // APPEL AU SERVICE
    dn.serviceRequest.tickets.ticket.getNewTicket(ticketData)
      .then(function (result) {
        // Mettre à jour les champs de la modal
        $('#ticketNumber').text('');

        $("#Ssla").text(document.getElementById('sla').value);
        $("#Scategorie").text($('#categorie').val());
        $("#Sgroupe").text($('#groupe').val());
        $("#Srecepteur").text($('#recepteur').val());

        // Réinitialiser les champs du formulaire
        $('#ticket-object').val('');
        $('#ticket-description').val('');

        // Afficher la modal
        const ticketModal = new bootstrap.Modal(document.getElementById('ticketModal'));
        ticketModal.show();

        // Afficher le numéro du ticket
        $('#ticketNumber').text(result.numero);

        console.log("Résultat du POST :", result);

        // Restaurer les boutons
        $("#btnLoading").addClass("d-none");
        $("#btnSubmit").removeClass("d-none");
      })
      .catch(function (err) {
        console.error("Erreur lors de l'ajout du ticket :", err);
        alert("Erreur lors de la création du ticket. Veuillez réessayer.");
        // Restaurer les boutons
        $("#btnLoading").addClass("d-none");
        $("#btnSubmit").removeClass("d-none");
      });
  });


});


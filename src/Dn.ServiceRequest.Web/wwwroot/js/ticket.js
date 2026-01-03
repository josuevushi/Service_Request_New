



//alert("ok");
/*const data = {
  text: "Hello",
  ticketId: "3fa85f64-5717-4562-b3fc-2c963f66afa6"
};
dn.serviceRequest.comments.comment.create(data)
  .then(result => console.log(result))
  .catch(err => {
    console.error(err);
    console.error(err.response);
  });*/




var chartseries1 = "#16a455";
var configcolors_labelsecondary = "#cbc7c5";
var legendColor = "#100201";
var headingColor = "#ed6517";
var borderColor = "#ed6517";

// 🔹 Pourcentage réel (non limité)
let pourcentageReel = parseInt($('#pourcentageId').val(), 10);
if (isNaN(pourcentageReel) || pourcentageReel < 0) {
  pourcentageReel = 0;
}

// 🔹 Pourcentage graphique (limité à 100 pour le cercle)
let pourcentageGraphique = Math.min(pourcentageReel, 100);

const radialBarChartEl = document.querySelector('#radialBarChart');

const radialBarChartConfig = {
  chart: {
    height: 480,
    type: 'radialBar'
  },
  colors: [chartseries1],
  plotOptions: {
    radialBar: {
      size: 185,
      hollow: {
        size: '75%'
      },
      track: {
        margin: 10,
        background: configcolors_labelsecondary
      },
      dataLabels: {
        name: {
          fontSize: '1.5rem',
          fontFamily: 'Public Sans'
        },
        value: {
          fontSize: '3.7rem',
          color: legendColor,
          fontFamily: 'Public Sans',
          formatter: function () {
            return pourcentageReel + '%';
          }
        },
        total: {
          show: true,
          fontWeight: 400,
          fontSize: '1.0rem',
          color: headingColor,
          label: '',
          formatter: function () {
            return pourcentageReel + '%';
          }
        }
      }
    }
  },
  grid: {
    borderColor: borderColor,
    padding: {
      top: -20,
      bottom: -20
    }
  },
  legend: {
    show: true,
    position: 'bottom',
    labels: {
      colors: legendColor,
      useSeriesColors: false
    }
  },
  stroke: {
    lineCap: 'round'
  },
  series: [pourcentageGraphique],
  labels: ['Progression des tickets']
};

if (radialBarChartEl) {
  const radialChart = new ApexCharts(radialBarChartEl, radialBarChartConfig);
  radialChart.render();
}
/********************************************************************************** */

// **********************************************************************************
// GESTION DES FICHIERS (Unifiée)
// **********************************************************************************

const uploadZone = document.getElementById("uploadZone");
const fileInput = document.getElementById("fileInput");
const fileList = document.getElementById("fileList");

// --- Event Listeners Upload Zone ---
if (uploadZone) {
  uploadZone.addEventListener("click", () => fileInput.click());

  fileInput.addEventListener("change", async (event) => {
    await handleFiles(event.target.files);
    fileInput.value = ''; // Reset pour permettre de re-sélectionner le même fichier
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
}

// --- Fonction Principale : Traitement des nouveaux fichiers ---
async function handleFiles(files) {
  const ticketId = $("#ticketId").val();
  if (!ticketId) {
    abp.notify.error("Erreur: ID du ticket introuvable.");
    return;
  }

  for (let file of files) {
    // 1. Lire en Base64 pour l'upload
    const base64Content = await toBase64(file);

    // 2. Préparer DTO pour FileUploadController
    const uploadData = {
      fileName: file.name,
      base64: base64Content
    };

    // 3. Upload physique via API
    try {
      // Afficher un état de chargement temporaire (optionnel)
      abp.ui.setBusy(uploadZone);

      const uploadResponse = await $.ajax({
        url: '/api/files',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(uploadData)
      });

      // 4. Si upload OK, créer l'entrée en base de données
      const pieceJointeData = {
        ticket_id: ticketId,
        nom: file.name,
        path: uploadResponse.filePath
      };

      const createdPj = await dn.serviceRequest.pieceJointes.pieceJointe.create(pieceJointeData);

      // 5. Ajouter à la liste visuelle
      renderFileItem(createdPj);

      abp.notify.success(`Fichier '${file.name}' ajouté avec succès.`);

    } catch (err) {
      console.error(err);
      let msg = "Erreur lors de l'upload.";
      if (err.responseJSON && err.responseJSON.error && err.responseJSON.error.message) {
        msg += " " + err.responseJSON.error.message;
      } else if (err.responseText) {
        // msg += " " + err.responseText;
      }
      abp.notify.error(msg);
    } finally {
      abp.ui.clearBusy(uploadZone);
    }
  }
}

// --- Fonction d'affichage d'un item fichier (Unifiée) ---
function renderFileItem(file) {
  const li = document.createElement("li");
  li.setAttribute("data-id", file.id);
  li.style.display = "flex";
  li.style.justifyContent = "space-between";
  li.style.alignItems = "center";
  li.className = "mt-2 p-2 border rounded"; // Un peu de style

  const downloadUrl = `/api/files/download?filePath=${encodeURIComponent(file.path)}`;

  li.innerHTML = `
        <div class="d-flex align-items-center">
            <i class="ti ti-file me-2"></i>
            <span>${file.nom}</span>
        </div>
        <div>
          <a href="${downloadUrl}" target="_blank" class="btn btn-sm btn-icon btn-text-secondary rounded-pill me-1" title="Télécharger">
            <i class="ti ti-download"></i>
          </a>
          <button class="btn btn-sm btn-icon btn-text-danger rounded-pill delete-file-btn" data-id="${file.id}" title="Supprimer">
             <i class="ti ti-trash"></i>
          </button>
        </div>
      `;

  // Event listener suppression
  li.querySelector(".delete-file-btn").addEventListener("click", function () {
    const fileId = this.getAttribute("data-id");
    if (confirm("Voulez-vous vraiment supprimer ce fichier ?")) {
      // 1. Supprimer fichier physique
      $.ajax({
        url: `/api/files/delete?filePath=${encodeURIComponent(file.path)}`,
        type: 'DELETE',
        success: function () {
          // 2. Supprimer entrée base
          dn.serviceRequest.pieceJointes.pieceJointe.delete(fileId).then(() => {
            li.remove();
            abp.notify.success("Fichier supprimé.");
          }).catch(err => {
            console.error(err);
            abp.notify.error("Erreur suppression DB.");
          });
        },
        error: function (xhr) {
          // Même si erreur physique (ex: fichier déjà absent), on essaie de nettoyer la DB ?
          // Pour l'instant on notifie l'erreur.
          console.error(xhr);
          abp.notify.error("Erreur suppression fichier physique.");
        }
      });
    }
  });

  fileList.appendChild(li);
}

// --- Helper Base64 ---
function toBase64(file) {
  return new Promise((resolve) => {
    const reader = new FileReader();
    reader.onload = () => resolve(reader.result.split(",")[1]);
    reader.readAsDataURL(file);
  });
}

// --- Chargement initial des fichiers existants ---
(function initFiles() {
  const tId = $("#ticketId").val();
  if (tId) {
    dn.serviceRequest.pieceJointes.pieceJointe.getListByTicketId(tId).then(function (result) {
      console.log("Fichiers existants:", result);
      if (result && result.length > 0) {
        result.forEach(file => renderFileItem(file));
      }
    });
  }
})();
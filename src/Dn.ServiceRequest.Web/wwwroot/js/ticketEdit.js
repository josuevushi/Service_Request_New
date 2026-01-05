
$(function () {
    // Appel de ton service pour récupérer les commentaires
    dn.serviceRequest.comments.comment.getCommentJoin($('#ticketId').val()).then(function (result) {
        console.log(result); // vérifie la structure des données dans la console

        // Conteneur où tu veux afficher les commentaires
        var $commentsContainer = $('#commentsList');
        $commentsContainer.empty(); // vide le conteneur avant d'ajouter les commentaires

        // Parcours chaque commentaire et construit le HTML
        result.forEach(function (comment) {
            var commentHtml = `
            <div class="comment-card">
                <div class="comment-header">
                    <div class="user-info">
                        ${comment.user}
                    </div>
                    <div class="date">${formatDate(comment.date)}</div>
                </div>
                <div class="comment-text">
                    ${comment.commentaire}
                </div>
            </div>
        `;

            // Ajoute le commentaire au conteneur
            $commentsContainer.append(commentHtml);
        });
    });

    // Fonction pour formater la date comme "26/12/2025 • 11:02"
    function formatDate(dateString) {
        var date = new Date(dateString);
        var day = String(date.getDate()).padStart(2, '0');
        var month = String(date.getMonth() + 1).padStart(2, '0');
        var year = date.getFullYear();
        var hours = String(date.getHours()).padStart(2, '0');
        var minutes = String(date.getMinutes()).padStart(2, '0');

        return `${day}/${month}/${year} • ${hours}:${minutes}`;
    }

    $('#ticketId').click(function () {

    });

    $('#saveForm').on('submit', function () {
        var userId = $('#ticket-AssignTo').val();
        if (!userId) {
            abp.notify.warn("Veuillez sélectionner un utilisateur.");
            return false;
        }
        $('#hiddenAssignedTo').val(userId);
    });

    $('#PendingButton').click(function () {

    });

    $('#CloseButton').click(function () {

    });

    // Charger les utilisateurs dans le select
    var $assignToSelect = $('#ticket-AssignTo');
    var currentAssignTo = $assignToSelect.find('option').first().text(); // Récupère la valeur actuelle (placée dans le HTML)

    dn.serviceRequest.tickets.ticket.getUsersInGroups().then(function (users) {
        $assignToSelect.empty(); // Vide le select (enlève l'option par défaut initale)

        // Option par défaut vide ou placeholder si souhaité
        //  $assignToSelect.append(new Option("Sélectionner un utilisateur", ""));

        users.forEach(function (user) {
            var option = new Option(user.userName, user.id); // On utilise le UserName comme value et text
            if (user.userName === currentAssignTo) {
                option.selected = true;
            }
            $assignToSelect.append(option);
        });

        $('#ticket-AssignTo').select2();

    });

});
//alert("ok");

/*
const data = {
  text: "Hello",
  ticket_id: "3fa85f64-5717-4562-b3fc-2c963f66afa6"
};
dn.serviceRequest.comments.comment.create(data).then(function(result){
    console.log(result);
});*/

$("#postComment").click(function () {
    var $btn = $(this); // bouton
    var commentText = $("#newComment").val().trim();

    if (commentText === "") {
        alert("Veuillez écrire un commentaire.");
        return;
    }

    // Désactive le bouton et ajoute le spinner
    $btn.prop("disabled", true);
    var originalHtml = $btn.html(); // sauvegarde le HTML original
    $btn.html('<i class="fa fa-spinner fa-spin" style="margin-right:5px"></i>Envoi...');

    const data = {
        text: commentText,
        ticketId: $('#ticketId').val()
    };

    dn.serviceRequest.comments.comment.getAddCommentJoin(data).then(function (result) {
        console.log(result);
        var now = new Date();
        var hours = now.getHours().toString().padStart(2, '0');
        var minutes = now.getMinutes().toString().padStart(2, '0');
        var day = now.getDate().toString().padStart(2, '0');
        var month = (now.getMonth() + 1).toString().padStart(2, '0');
        var year = now.getFullYear();

        var newComment = `
            <div class="comment-card">
                <div class="comment-header">
                    <div class="user-info">
                        Ton Nom
                    </div>   <div class="date">${day}/${month}/${year} • ${hours}:${minutes}</div>
                </div> <div class="comment-text">${commentText}</div>
            </div>
        `;

        $("#commentsList").prepend(newComment);
        $("#newComment").val("").focus();
        // Rétablit le bouton et le HTML original
        $btn.prop("disabled", false);
        $btn.html(originalHtml);
    }).catch(function (err) {
        console.error(err);
        alert("Erreur lors de l'envoi du commentaire.");
    });
});
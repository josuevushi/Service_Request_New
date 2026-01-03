$(function () {

    dn.serviceRequest.tickets.ticket.getMesTickets().then(function (result) {

        console.log("Tickets :", result);

        const tbody = $("#TableauDeMesTickets tbody");
        tbody.empty();

        result.forEach(t => {

            let statusClass = "bg-label-secondary";
            if (t.status === "Open") statusClass = "bg-label-secondary";
            else if (t.status === "WorkInProgress") statusClass = "bg-label-warning";
            else if (t.status === "Pending") statusClass = "bg-label-danger";
            else if (t.status === "Close") statusClass = "bg-label-success";


            let progressClass = "bg-secondary";
            if (t.pourcentage < 60) progressClass = "bg-secondary";
            else if (t.pourcentage > 60) progressClass = "bg-primary";

            const creationDate = t.creationDate;

            const closureDate = t.estimateDate;

            tbody.append(`
                <tr>
                    <td>
                        <a href="/tickets/edit/${t.id}" class="btn btn-primary btn-sm">
                            <i class="ti ti-align-justified"></i> Détails
                        </a>
                    </td>
                    <td>${t.numero}</td>
                    <td>${t.type}</td>
                    <td>${t.objet}</td>
                    <td>
                        <span class="badge ${statusClass}" style="height:25px;">
                            ${t.status}
                        </span>
                    </td>
                    <td>
                        ${Math.round(t.pourcentage)} %
                        <div class="progress" style="height:15px;">
                            <div class="progress-bar progress-bar-striped ${progressClass}"
                                 style="width:${t.pourcentage}%;"></div>
                        </div>
                    </td>
                    <td>${t.famille}</td>
                    <td>${creationDate}</td>
                    <td>${closureDate}</td>
                </tr>
            `);
        });
         $('#TableauDeMesTickets').DataTable({
        paging: true,
        searching: true,
        ordering: true,
        info: true,
        responsive: false,
        pageLength: 10,
      
    });
    });   
 });  
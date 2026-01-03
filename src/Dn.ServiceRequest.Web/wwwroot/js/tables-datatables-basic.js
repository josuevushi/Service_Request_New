$(document).ready(function () {
    $('#example1').DataTable({
        "paging": true,  // Enable pagination
        "searching": true,  // Enable searching
        "ordering": true,  // Enable sorting
        "info": true  // Show information (e.g., "Showing 1 to 10 of 100 entries")
    });
});
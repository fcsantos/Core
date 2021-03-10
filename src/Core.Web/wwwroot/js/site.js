$(document).ready(function () {

    $('#table').DataTable({
        "ordering": false,
        "info": false
    });
});

$(function () {

    $("#table").on('click', '#Delete', function () {
        var id = $(this).attr("name");

        swal({
            title: "Pretende continuar?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        url: '/Fornecedor/Delete',
                        type: "POST",
                        dataType: "JSON",
                        data: { fornecedorId: id },
                        success: function () {
                        }
                    });
                }
                setTimeout(
                    function () {
                        location.reload();
                    }, 1000);
            });
    });
});
var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": { url: '/admin/product/getproducts' },
        "columns": [
            { data: 'title', "width": "25%" },
            { data: 'isbn', "width": "15%" },
            { data: 'listPrice', "width": "10%" },
            { data: 'auther', "width": "10%" },
            { data: 'category.name', "width": "15%" },
            {
                data: 'productId',
                "render": function (data) {
                    return ` <div class="d-flex justify-content-end">
                                    <a href="/admin/product/product?productId=${data}" class="btn btn-outline-primary mx-2">
                                        <i class="bi bi-pencil"></i> Edit
                                    </a>
                                    <a class="btn btn-danger" id="btnDelete" onClick=Delete("/admin/product/delete?productId=${data}")>
                                        <i class="bi bi-trash"></i> Delete
                                    </a>
                                </div>`
                },
                "width": "25%"
            }
        ]
    })
}

function Delete(path) {
    $('#modelDltBtn').data('path', path);
    $("#confirmModel").modal("show");
}

$('#modelDltBtn').click(function () {
    $("#confirmModel").modal("hide");
    debugger;
    $.ajax({
        type: "Delete",
        url: $('#modelDltBtn').data('path'),
        success: function (data) {
            if (data.success) {
                dataTable.ajax.reload();
                toastr.success(data.message);
            }
            else {
                toastr.error(data.message);
            }
        },
        error: function (data) {
            alert("Error: Product not found");
        }
    })
})
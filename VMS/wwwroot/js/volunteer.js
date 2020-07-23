var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Volunteer/GetAll"
        },
        "columns": [
            { "data": "firstName", "width": "10%" },
            { "data": "lastName", "width": "10%" },
            { "data": "approvalStatus", "width": "10%" },
            {
                "data": "isActive",
                "render": function (data) {    // show a check mark or x          
                    if (data) {
                        return `<i class="fas fa-check text-center"></i>`
                    }
                    else {
                        return `<i class="fas fa-times text-center"></i>`
                    }
                },
                "width": "5%"
            },
            { "data": "availability", "width": "10%" },
            {
                "data": "isDriversLicenseOnFile",
                "render": function (data) {    // show a check mark or x
                    if (data) {
                        return `<i class="fas fa-check text-center"></i>`
                    }
                    else {
                        return `<i class="fas fa-times text-center"></i>`
                    }
                },
                "width": "5%"
            },
            {
                "data": "isSsCardOnFile",
                "render": function (data) {    // show a check mark or x
                    if (data) {
                        return `<i class="fas fa-check text-center"></i>`
                    }
                    else {
                        return `<i class="fas fa-times text-center"></i>`
                    }
                },
                "width": "5%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a onclick=Delete("/Admin/Volunteer/Delete/${data}") class="btn btn-sm btn-info text-white" style="cursor:pointer">
                                    View Opportunity Matches
                                </a>
                            </div>
                           `;
                }, "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Volunteer/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Admin/Volunteer/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                            </div>
                           `;
                }, "width": "10%"
            }
        ],
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}


function filterVolunteers(filter) {

    $.ajax({
        type: "GET",
        url: "/Admin/Volunteer/GetFilters?filter=" + filter,
        data: filter,
        success: function (data) {
            dataTable.clear();
            dataTable.rows.add(data.data).draw();
        }
    }); 
}



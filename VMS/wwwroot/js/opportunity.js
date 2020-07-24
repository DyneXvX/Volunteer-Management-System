var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Opportunity/GetAll"
        },
        "columns": [
            { "data": "opportunityName", "width": "17%" },
            { "data": "centerType", "width": "15%" },
            {
                "data": "datePosted",
                "render": function (data) {            // format date
                    var sqlDate = data.split("-");
                    var jsDate = sqlDate[1] + "/" + sqlDate[2].substr(0, 2) + "/" + sqlDate[0];
                    return jsDate;
                },
                "width": "10%"
            },
            {
                "data": "isOpen",
                "render": function (data) {    // show a check mark or x
                    if (data) {
                        return `<i class="fas fa-check text-center"></i>`
                    }
                    else {
                        return `<i class="fas fa-times text-center"></i>`
                    }
                },
                "width": "7%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a onclick=Delete("/Admin/Volunteer/Delete/${data}") class="btn btn-sm btn-info text-white" style="cursor:pointer">
                                    View Volunteer Matches
                                </a>
                            </div>
                           `;
                }, "width": "13%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Opportunity/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Admin/Opportunity/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                            </div>
                           `;
                }, "width": "10%"
            }
        ]
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

function filterOpportunities(filter) {

    $.ajax({
        type: "GET",
        url: "/Admin/Opportunity/GetFilters?filter=" + filter,
        success: function (data) {
            dataTable.clear();
            dataTable.rows.add(data.data).draw();
        }
    });
}
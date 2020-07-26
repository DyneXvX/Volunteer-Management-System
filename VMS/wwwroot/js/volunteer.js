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
                                <a href="#" onclick="showMatches('${data}')" class="btn btn-sm btn-info text-white" style="cursor:pointer">
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
        success: function (data) {
            dataTable.clear();
            dataTable.rows.add(data.data).draw();
        }
    }); 
}


function showMatches(id) {

    $.ajax({
        type: "GET",
        url: "/Admin/Volunteer/GetSingle?id=" + id,     // get volunteer info using the id
        success: function (vol) {
            $.ajax({
                type: "GET",
                url: "/Admin/Opportunity/GetAll",      // get all opportunities
                success: function (opp) {
                    var matches = [];
                    for (x in opp.data) {
                        if (opp.data[x].isOpen) {     // include open opportunities only
                            if (opp.data[x].centerType == vol.data.volunteerPrefersCenter) {    // match by center type
                                var anObj = {"opportunityName": opp.data[x].opportunityName, "centerType": opp.data[x].centerType};
                                matches.push(anObj);
                            }
                        }
                    }
                    var volunteerName = vol.data.firstName + " " + vol.data.lastName;
                    var myTitle = "Opportunity Matches for: ";
                    var myBody = "";
                    $(document).ready(function () {
                        $(document).ready(function () {
                            $("#myModal").modal("show");      // show the modal, but not before the function below executes
                        });
                        $("#myModal").on('show.bs.modal', function () {           // do the following right before showing the modal
                            $("#modal-title").html(myTitle + volunteerName);      // update the modal title dynamically 

                            if (matches.length > 0) {                            // if opportunities found, format the output
                                for (x in matches) {
                                    myBody += "<p><b>Opportunity Name</b>: ";
                                    myBody += matches[x].opportunityName + "</p>";
                                    myBody += "<p><b>Center Type</b>: ";
                                    myBody += matches[x].centerType + "</p></br>";
                                }
                            }
                            else {
                                myBody += "No opportunities found for " + volunteerName;
                            }
                            
                            $("#modal-body").html(myBody);                        // update the modal body dynamically 
                        });
                    });
                }
            });
        }
    });
}


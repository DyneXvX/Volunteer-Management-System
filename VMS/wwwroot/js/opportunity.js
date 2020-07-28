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
                                <a href="#" onclick="showMatches('${data}')" class="btn btn-sm btn-info text-white" style="cursor:pointer">
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


function showMatches(id) {

    $.ajax({
        type: "GET",
        url: "/Admin/Opportunity/GetSingle?id=" + id,     // get opportunity info using the id
        success: function (opp) {
            $.ajax({
                type: "GET",
                url: "/Admin/Volunteer/GetAll",      // get all volunteers
                success: function (vol) {
                    var matches = [];
                    for (x in vol.data) {
                        if (vol.data[x].isActive) {     // include active volunteers only
                            if (vol.data[x].volunteerPrefersCenter == opp.data.centerType) {    // match by center type
                                var volunteerName = vol.data[x].firstName + " " + vol.data[x].lastName;
                                var anObj = {
                                    "volunteerName": volunteerName, "centerType": vol.data[x].volunteerPrefersCenter, "availability": vol.data[x].availability,
                                    "contact": vol.data[x].cellPhone };
                                matches.push(anObj);
                            }
                        }
                    }
                    var myTitle = "Volunteer Matches for <b>" + opp.data.opportunityName + "</b> opportunity:";
                    var myBody = "";
                    $(document).ready(function () {
                        $(document).ready(function () {
                            $("#myModal").modal("show");      // show the modal, but not before the function below executes
                        });
                        $("#myModal").on('show.bs.modal', function () {           // do the following right before showing the modal
                            $("#modal-title").html(myTitle);      // update the modal title dynamically 

                            if (matches.length > 0) {                            // if opportunities found, format the output
                                for (x in matches) {
                                    myBody += "<p><b>Volunteer Name</b>: ";
                                    myBody += matches[x].volunteerName + "</p>";
                                    myBody += "<p><b>Preferred Center Type</b>: ";
                                    myBody += matches[x].centerType + "</p>";
                                    myBody += "<p><b>Availability</b>: ";
                                    myBody += matches[x].availability + "</p>";
                                    myBody += "<p><b>Contact</b>: ";
                                    myBody += matches[x].contact + "</p></br>";
                                }
                            }
                            else {
                                myBody += "<p class='text-danger'>No Volunteers found for <b>" + opp.data.opportunityName + "</b></p>";
                                myBody += "<p class='text-danger'>Please note that only active volunteers are searched</p>";
                            }

                            $("#modal-body").html(myBody);       // update the modal body dynamically 
                        });
                    });
                }
            });
        }
    });
}


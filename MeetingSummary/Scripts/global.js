$(document).ready(function ()
{
    $('#tblReport').DataTable(
    {
        "processing": "true",
        "stateSave": "true",
        "language": {
            "url": "/Heb.json"
        },
        "ajax": {
            "url": "Home/LoadMeetingsReport",
            "type": "POST"
        },
        "columnDefs": [
                        { "className": "centered", "targets": "_all" }
        ],
        "columns": [
                    {
                        "orderable": false,
                        "data": null,
                        "render": function (date, type, full, meta) {
                            return '<form action="/ClientTransfer/ClientTransfer" method="POST"><button type="button" class="btn btn-default" aria-label="Left Align"><span class="glyphicon glyphicon-edit" aria-hidden="true"></span></button><input type="hidden" id="id" name="id" value="' + full.Id + '"/></form>'
                        }
                    },
                    { "data": "CreationDate" },
                    { "data": "Summary" }
        ]
    });
});

function refreshSummaryDataTable() {
    var dataTable = $('#tblReport').DataTable();
    dataTable.ajax.reload();
}


function clearModal()
{
    $('#newSummaryModal').find('input, textarea').val('');
}

function saveSummary()
{
    var dataToSend = {
        meetingSummary: $("#Summary").val(),
        tasks: $(".tbTasks").map(function(){return $(this).val();}).get(),
        assignments: $(".tbAssignments").map(function () { return $(this).val(); }).get(),
        users: $(".ddlUsers").map(function () { return $(this).val(); }).get()
    };

    $.ajax({
        type: "POST",
        url: "/Home/SaveNewSummary",
        data: dataToSend,
        traditional: true,
        dataType: "json",
        success: function (data) {
            $('#addSummary').modal('toggle');
            clearModal();
            refreshSummaryDataTable();
        },
        beforeSend: function () {
            $(".modalSpinner").show();
        },
        complete: function () {
            $(".modalSpinner").hide();
        }
    });
}
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
                            return '<button type="button" class="btn btn-default" aria-label="Left Align" data-toggle="modal" data-target="#addSummary" onclick="loadSummaryModal(' + full.Id + ')"><span class="glyphicon glyphicon-edit" aria-hidden="true"></span></button>';
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

function loadSummaryModal(id)
{
    var dataToSend = { id: id };

    $.ajax({
        type: "POST",
        url: "/Home/LoadMeetingData",
        data: dataToSend,
        traditional: true,
        dataType: "html",
        success: function (data) {
            $('.modal-content').html(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $(".modalSpinner").hide();
        }
    });
}

function saveSummary(isEdit)
{
    var url = isEdit ? "/Home/UpdateMeeting" : "/Home/SaveNewSummary"
    var dataToSend = {
        meetingId: $("#Id").val(),
        meetingSummary: $("#MeetingSummary").val(),
        tasks: $(".tbTasks").map(function(){return $(this).val();}).get(),
        assignments: $(".tbAssignments").map(function () { return $(this).val(); }).get(),
        users: $(".ddlUsers").map(function () { return $(this).val(); }).get()
    };

    $.ajax({
        type: "POST",
        url: url,
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

function addNewDataRow(type, htmlString) {
    var ul = $("#ul" + type);
    ul.append("<li><input type='textbox' class='tb" + type + "'/> &nbsp;" + htmlString + "</li>");
}
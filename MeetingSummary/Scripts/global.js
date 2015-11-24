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
                        "width": "100px",
                        "orderable": false,
                        "data": null,
                        "render": function (date, type, full, meta) {
                            return '<button type="button" class="btn btn-default" style="float: right;" aria-label="Left Align" data-toggle="modal" data-target="#addSummary" onclick="loadSummaryModal(' + full.Id + ')"><span class="glyphicon glyphicon-edit" aria-hidden="true"></span></button>' + 
                                   '<button type="button" class="btn btn-default" style="float: left;" aria-label="Left Align" data-toggle="modal" data-target="#addSummary" onclick="showConfirmDelete(' + full.Id + ')"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span></button>';
                        }
                    },
                    { "data": "CreationDate" },
                    { "data": "Subject" },
                    { "data": "UpdateDate" }
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
            $('#addSummaryContent').html(data);
        },
        beforeSend: function () {
            $(".modalSpinner").show();
        },
        complete: function () {
            $(".modalSpinner").hide();
        }
    });
}

function showConfirmDelete(id)
{
    var dataToSend = {
        modalTitle: "מחיקת סיכום פגישה",
        modalMessage: "האם אתה בטוח שברצונך למחוק את סיכום הפגישה?",
        action: "deleteRow(" + id + ")"

    };

    $.ajax({
        type: "POST",
        url: "/Home/LoadModalMessage",
        data: dataToSend,
        traditional: true,
        dataType: "html",
        success: function (data) {
            $('.addSummaryContent').html(data);
        }
    });
}

function deleteRow(id)
{
    var dataToSend = { meetingId: id };

    $.ajax({
        type: "POST",
        url: "/Home/DeleteMeetingData",
        data: dataToSend,
        traditional: true,
        success: function (data) {
            $('#addSummary').modal('toggle');
            refreshSummaryDataTable();
        }
    });
}

function saveSummary(isEdit)
{
    var url = isEdit ? "/Home/UpdateMeeting" : "/Home/SaveNewSummary"
    var dataToSend = {
        meetingId: $("#Id").val(),
        creationDate: $("#creationDate").val(),
        meetingSubject: $("#MeetingSubject").val(),
        meetingSummary: $("#MeetingSummary").val(),
        tasks: $(".tbTasks").map(function () { return $(this).val(); }).get(),
        assignments: $(".tbAssignments").map(function () { return $(this).val(); }).get(),
        users: $(".ddlUsers").map(function () { return $(this).val(); }).get(),
        tasksChk: $(".chkTasks").map(function () { return $(this).prop("checked"); }).get(),
        assignmentsChk: $(".chkAssignments").map(function () { return $(this).prop("checked"); }).get()
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

function addNewDataRow(type) {
    var dataToSend = { type: type };

    $.ajax({
        type: "POST",
        url: "/Home/AddComponent",
        data: dataToSend,
        traditional: true,
        dataType: "html",
        success: function (data) {
            $('#div' + type).append(data);
        },
        beforeSend: function () {
            $(".modalSpinner").show();
        },
        complete: function () {
            $(".modalSpinner").hide();
        }
    });
}

function complete(type)
{
    var element = $(event.target);
    element.parents('.tbl' + type).find('.tb' + type + ', .ddlUsers').prop("disabled", element.prop("checked"));
}

function updateImportModal(type)
{
    $("#hImportType").val(type);
}

function updateDataToImport()
{
    var dataToSend = {
        type: $("#hImportType").val(),
        fromDate: $('#importMeeting').val()
    };

    $.ajax({
        type: "POST",
        url: "/Home/GetDataToImport",
        data: dataToSend,
        traditional: true,
        dataType: "html",
        success: function (data) {
            $('#importModalBody').append(data);
        },
        beforeSend: function () {
            $(".modalSpinner").show();
        },
        complete: function () {
            $(".modalSpinner").hide();
        }
    });
}

function clearImportModal()
{
    $("#importModal").find('input').val('');
    $('#importModalBody').html('');
    $('#importModalBody').text('');
}

function saveImport()
{
    $('#importModal').modal('toggle');
    var type = $("#hImportType").val();
    var checkbox = $("#importModalBody").find("input[type='checkbox']:checked");
    var component = checkbox.closest("li");
    checkbox.click();
    $("#ul" + type).append(component);
    clearImportModal();
}
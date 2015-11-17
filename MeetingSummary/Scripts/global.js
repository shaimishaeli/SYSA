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

function refreshClientTransferDataTable() {
    var dataTable = $('#tblReport').DataTable();
    dataTable.ajax.reload();
}
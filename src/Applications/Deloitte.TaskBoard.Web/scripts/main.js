var cardTemplate;

$(function () {
    cardTemplate = $('script[data-template="listitem"]').text().split(/\$\{(.+?)\}/g);

    $(".sortable").sortable({
        connectWith: ".sortable"
    }).disableSelection();

    $("#btn-savetask").click(addTask);
});

function addTask() {
    var form = $("#frm-addtask");
    var data = getFormData(form);

    data.order = 1;
    data.status = "To Do";

    $.ajax({
        type: "POST",
        url: "https://taskboard-prd-api.azurewebsites.net/api/Assignments",
        headers: {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json'
        },
        dataType: "json",
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function (response) {
            renderTask(response);
            $("#modal-editor").modal("toggle");
        },
        error: function (jqXHR, textStatus, errorThrown) { handleApiErrors(jqXHR, textStatus, errorThrown) }
    });

    // 
}

function getFormData($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n["name"]] = n["value"];
    });

    return indexed_array;
}

function renderTask(response) {
    $('#todo-column').append(response.map(function (prop) {
        return cardTemplate.map(render(prop)).join('');
    }));
}

function handleApiErrors(jqXHR, textStatus, errorThrown) {
    alert(errorThrown);
}

function determineTaskOrder(columnName) {
    return 1;
}

function render(props) {
    return function (tok, i) { return (i % 2) ? props[tok] : tok; };
}
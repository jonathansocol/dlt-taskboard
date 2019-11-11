$(function () {
    $(".sortable").sortable({
        connectWith: ".sortable",
        start: function (event, ui) {
            $(".sortable").addClass("drop-area");
        },
        stop: function (event, ui) {
            sortTask(event, ui);
            moveTask(event, ui);
            $(".sortable").removeClass("drop-area");
        }
    }).disableSelection();

    retrieveTasks();

    $("#btn-savetask").click(addTask);
    $("#btn-updatetask").click(updateTask);

    $('.sortable').on('click', '.btn-done', function (e) {
        e.stopPropagation();
        markTaskAsDone(e);
    });
    $('.sortable').on('click', '.btn-edit', function (e) {
        e.stopPropagation();
        loadEditModal(e);
    });
});

function loadEditModal(e) {
    var card = $(e.target).parents(".task-card");

    $("#txt-edit-id").val($(card).find(".task-id").val());
    $("#txt-edit-title").val($(card).find(".task-title").text());
    $("#txt-edit-description").val($(card).find(".task-description").text());
    $("#txt-edit-requester").val($(card).find(".task-requester").text());
    $("#ddl-edit-priority").val($(card).find(".task-priority").text());

    $("#modal-details").modal("toggle");
}

function updateTask() {
    var id = $("#txt-edit-id").val();
    var priority = $("#ddl-edit-priority").val();

    updatePriority(id, priority);

    $(".task-id[value='" + id + "']").parent().find(".task-priority").text(priority);

    $("#modal-details").modal("toggle");
}

function markTaskAsDone(e) {
    $(e.target).parents(".task-card").detach().appendTo("#done-column");

    var id = $(e.target).parents(".task-card").find(".task-id").val()
    var status = $(e.target).parents("ul").attr("data-status");

    updateStatus(id, status);

    var taskCards = $(e.target).parents(".sortable").find(".task-card");

    $.each(taskCards, function (i, value) { updateOrder($(value).find(".task-id").val(), i + 1) });
}

function renderTask(response) {
    var cardTemplate = $("#card-template").clone();

    $(cardTemplate).find(".task-id").val(response.id);
    $(cardTemplate).find(".task-title").text(response.title);
    $(cardTemplate).find(".task-description").text(response.description);
    $(cardTemplate).find(".task-requester").text(response.requester);
    $(cardTemplate).find(".task-priority").text(response.priority);
    $(cardTemplate).find(".task-order").val(response.order);

    $("#" + response.status.replace(/\s+/g, '').toLowerCase() + "-column").append(cardTemplate.html());
}

function moveTask(event, ui) {
    var id = $(ui.item).find(".task-id").val()
    var status = $(ui.item).parents("ul").attr("data-status");

    updateStatus(id, status);
}

function sortTask(event, ui) {
    var taskCards = $(ui.item).parents(".sortable").find(".task-card");

    $.each(taskCards, function (i, value) { updateOrder($(value).find(".task-id").val(), i + 1) });
}

function retrieveTasks() {
    $.ajax({
        type: "GET",
        url: "https://taskboard-prd-api.azurewebsites.net/api/Assignments",
        headers: {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json'
        },
        success: function (response) {
            response.sort(function (a, b) {
                return parseFloat(a.order) - parseFloat(b.order);
            });

            $.each(response, function (i, value) { renderTask(value) });
        },
        error: function (jqXHR, textStatus, errorThrown) { handleApiErrors(jqXHR, textStatus, errorThrown) }
    });
}

function addTask() {
    var form = $("#frm-addtask");
    var data = getFormData(form);

    data.order = $(".todo-column").find(".task-card").length + 1;
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
            $("#modal-editor").find("input,textarea,select").val("").end()
            $("#modal-editor").modal("toggle");
        },
        error: function (jqXHR, textStatus, errorThrown) { handleApiErrors(jqXHR, textStatus, errorThrown) }
    });
}

function updateStatus(id, status) {
    var data = { "status": status };

    $.ajax({
        type: "PATCH",
        url: "https://taskboard-prd-api.azurewebsites.net/api/Assignments/" + id,
        headers: {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json'
        },
        dataType: "json",
        data: JSON.stringify(data),
        success: function (response) {

        },
        error: function (jqXHR, textStatus, errorThrown) { handleApiErrors(jqXHR, textStatus, errorThrown) }
    });
}

function updateOrder(id, order) {
    var data = { "order": order };

    $.ajax({
        type: "PATCH",
        url: "https://taskboard-prd-api.azurewebsites.net/api/Assignments/" + id,
        headers: {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json'
        },
        dataType: "json",
        data: JSON.stringify(data),
        success: function (response) {
        },
        error: function (jqXHR, textStatus, errorThrown) { handleApiErrors(jqXHR, textStatus, errorThrown) }
    });
}

function updatePriority(id, priority) {
    var data = { "priority": priority };

    $.ajax({
        type: "PATCH",
        url: "https://taskboard-prd-api.azurewebsites.net/api/Assignments/" + id,
        headers: {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json'
        },
        dataType: "json",
        data: JSON.stringify(data),
        success: function (response) {
        },
        error: function (jqXHR, textStatus, errorThrown) { handleApiErrors(jqXHR, textStatus, errorThrown) }
    });
}

function handleApiErrors(jqXHR, textStatus, errorThrown) {
    alert(errorThrown);
}

function getFormData($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n["name"]] = n["value"];
    });

    return indexed_array;
}
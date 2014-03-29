$(document).ready(function () {
    getComments();
    $("#btn-save").click(function () {
        var Comment = { "ID": 1, "Username": "", "CommentText": $("#Comment").val(), "CommentDate": 0 }
        $.post("/Home/Create", Comment, function (data) {
            $("#cList li:not(:last)").remove();
            $("#Comment").val("");
            getComments();
        });    
    });
});

function getComments() {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf8",
        url: "/Home/GetComments/",
        data: "{}",
        dataType: "json",
        success: function (data) {
            $("#cList").loadTemplate($("#template"), data, { prepend: true });
        },
    });
};

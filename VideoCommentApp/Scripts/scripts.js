$(document).ready(function () {

    $("#btn-save").click(function () {
        var Comment = { "ID": 1, "Username": "rassapi", "CommentText": $("#comment").val(), "CommentDate": 0 }
        $.post("/Home/Create", Comment, function (data) {
            data.CommentDate = new Date(parseInt(data.CommentDate.substr(6)));
            $("#cList").loadTemplate($("#template"), data, {append: true});
        });
    });

});
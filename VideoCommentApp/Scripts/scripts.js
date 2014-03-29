$(document).ready(function () {

    $("#btn-save").click(function () {
        var Comment = { "ID": 1, "Username": "rassapi", "CommentText": $("#comment").val(), "CommentDate": 0 }
        $.post("/Home/Create", Comment, function (data) {
            alert("You are a Magnificent Bastard!")
        });
        $.ajax
    });

});
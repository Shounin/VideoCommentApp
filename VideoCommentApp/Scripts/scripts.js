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

    $('#cList').on('click', '.like-comment', function ()  {
        var elem = $(this);
        var Like = { "ID": elem.data('id'), "Username": ""}
        $.post("/Home/ChangeLikes", Comment, function (data) {
            $("#cList li:not(:last)").remove();
            getComments();
        });
        if ($(".button-text", this).text() == "Like") {
            $(".button-text", this).text("Unlike");
        }
        else {
            $(".button-text", this).text("Like");
        }
        
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

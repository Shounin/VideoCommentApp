$(document).ready(function () {
    getComments();
    $("#btn-save").click(function () {
        if ( $.trim( $("#Comment").val() ) == '' ){
            $(".com-text").prop("class", "form-control com-text has-error");
            $(".com-text").prop("placeholder", "You cannot post empty comments!");
            $("#Comment").val("");
            return;
        }
        else if ($(".com-text").prop("class").length > "form-control com-text".length) {
            $(".com-text").prop("class", "form-control com-text")
            $(".com-text").prop("placeholder", "Your comment goes here..");
        }
        var Comment = { "ID": 1, "Username": "", "CommentText": $("#Comment").val(), "CommentDate": 0, "LState": "Like" }
        $.post("/Home/Create", Comment, function (data) {
            $("#cList li:not(:last)").remove();
            $("#Comment").val("");
            getComments();
        });    
    });

    $('#cList').on('click', '.like-comment', function ()  {
        var elem = $(this);
        var Like = { "ID": elem.data('id'), "Username": "" }
        //if ($(".button-text", this).text() == "Like") {
        //    $(".button-text", this).text("Unlike");
        //}
        //else {
        //    $(".button-text", this).text("Like");
        //}
        $.post("/Home/ChangeLikes", Like, function (data) {
            $("#cList li:not(:last)").remove();
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

function getHasLiked() {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf8",
        url: "/Home/GetHasLiked/",
        data: "{}",
        dataType: "json",
        success: function (data) {
            return data;
        },
    });
}

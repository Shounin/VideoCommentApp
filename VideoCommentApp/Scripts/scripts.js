$(document).ready(function () {
    //Words cannot describe how amazed we are that anything in here works.
    getComments();
    $("#btn-save").click(function () {
        //Making sure you cannot post an empty comment
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
        //making a new comment object and giving it to the controller
        var Comment = { "ID": 1, "Username": "", "CommentText": $("#Comment").val(), "CommentDate": 0, "LState": "Like" }
        $.post("/Home/Create", Comment, function (data) {
            //clearing the list except for the form.  If we wanted to make it so that it refreshes only part of the list we would 
            //have it clear the entire list, but we aren't entirely sure how to make that all work.
            $("#cList li:not(:last)").remove();
            $("#Comment").val("");
            getComments();
        });    
    });

    $('#cList').on('click', '.like-comment', function ()  {
        var elem = $(this);
        var Like = { "ID": elem.data('id'), "Username": "" }
        $.post("/Home/ChangeLikes", Like, function (data) {
            //we decided to have this refresh the list too.
            $("#cList li:not(:last)").remove();
            getComments();
        });     
    });
});
//Json black magic, pretty sure it's made of lies.
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


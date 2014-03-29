$(document).ready(function () {

    $("#btn-save").click(function () {
        var Comment = { "ID": 1, "Username": "rassapi", "CommentText": $("#comment").val(), "CommentDate": 0 }
        $.post("/Home/Create", Comment, function (data) {
            
        });
            $.ajax( {
                type: "GET",
                contentType: "application/json; charset=utf8",
                url: "/Home/Create/",
                data: "{}",
                dataType: "json",
                success: function( data ) {
                    $("#cList").loadTemplate($("#template"),
                        {
                            Username: data.Username,
                            date: data.CommentDate,
                            post: data.CommentText
                        });
                },
                error : function( xhr, err ) {
                    // Note: just for debugging purposes!
                    alert( "readyState: " + xhr.readyState +
                    "\nstatus: " + xhr.status );
                    alert( "responseText: " + xhr.responseText );
                }
            });
    });

});
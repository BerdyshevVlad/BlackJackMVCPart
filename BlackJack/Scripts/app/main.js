function Start() {
    var data = {
        botCount: 3,
        userName: "Test"
    }
    $.ajax({
        type: 'POST',
        url: 'api/Game/Start',
        data: data,
        success: function (data) {
            $('#result').replaceWith(data);
            alert("Refresh");
        }
    });
}
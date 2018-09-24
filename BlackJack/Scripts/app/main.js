function Start() {
    var data = {
        botCount: 3,
        userName: "Test"
    }
    $.ajax({
        type: 'POST',
        url: 'http://localhost:50610/api/game/start',
        data: data,
        success: function (data) {
            $('#result').replaceWith(data);
            alert("Refresh");
        }
    });
}
$('#more').hide();
$('#enough').hide();


function OnConfirm() {
    var botCountValue = $('#botCount').val();
    var userNameValue = $('#userName').val();

    function isBotCountCorrect() {
        if (botCountValue > 5 || botCountValue < 1) {
            $('#errorBotCountMessage').append("Please, enter number from 1 to 5 ");
        }
    }

    function isUserNameExist() {
        if (userNameValue == "") {
            $('#errorBotCountMessage').append("Please, enter you name");
        }
    }

    if (botCountValue > 5 || botCountValue < 1 || userNameValue == "") {
        $('#errorBotCountMessage').text("");
        isBotCountCorrect();
        isUserNameExist();

        return;
    }


    var data = { botCount: botCountValue, userName: userNameValue };
    $.ajax({
        type: 'POST',
        url: 'http://localhost:64520/Game/Start',
        data: data,
        success: function (data) {
            $('#result').replaceWith(data);

        }
    });
    $('#more').show();
    $('#enough').show();
};


function OnMore() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:64520/Game/more',
        success: function(data) {
            $('#result').replaceWith(data);
        }
    });
}


function OnEnough() {
    $('#enough').prop('disabled', true);
    $('#more').prop('disabled', true);
    $.ajax({
        type: 'GET',
        url: 'http://localhost:64520/Game/enough',
        success: function (data) {
            $('#result').replaceWith(data);
        }
    });
}
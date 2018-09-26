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
    } else {
        var data = { botCount: botCountValue, userName: userNameValue };
        $.ajax({
            type: 'POST',
            url: 'http://localhost:64520/Game/Start',
            data: data,
            success: function (data) {
                $('#result').replaceWith(data);
            },
            complete: function () {
                $('#more').show();
                $('#enough').show();
            }
        });
    }


   
};



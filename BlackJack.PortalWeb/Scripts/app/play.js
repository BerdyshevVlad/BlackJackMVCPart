function GetPlayersScors() {
    var arr = [];
    $("#table tr td").each(function () {
        arr.push(this.id);
    });

    var playerScoreList = [];

    for (var i = 0; i < arr.length; i += 2) {

        var playerName = $('#' + arr[i]).text().replace(/ /g, '');
        var playerScore = $('#' + arr[i + 1]).text().replace(/ /g, '');
        playerScoreList.push({ name: playerName, score: playerScore });
    }

    return playerScoreList;
}


function DefineTheWinners() {

    var maxWinScore = 21;
    var playerScoreList = GetPlayersScors();
    var players = [];
    for (var k = 0; k < playerScoreList.length; k++) {
        if (playerScoreList[k].score <= maxWinScore) {
            players.push(playerScoreList[k]);
        }
    }

    var maxScore = Math.max.apply(Math, players.map(function (o) { return o.score; }));

    var winners = [];
    for (var i = 0; i < players.length; i++) {
        if (players[i].score == maxScore) {
            winners.push(players[i]);
        }
    }

    var winnerStr = "";
    for (var j = 0; j < winners.length; j++) {
        winnerStr += winners[j].name + " ";
    }

    winnerStr += " have score " + maxScore;

    return winnerStr;
}

function OnConfirm() {

    var maxBotCount = 5;
    var minBotCount = 1;

    var botCountValue = $('#botCount').val();
    var userNameValue = $('#userName').val();


    function isBotCountCorrect() {
        if (botCountValue > maxBotCount || botCountValue < minBotCount) {
            $('#errorBotCountMessage').append("Please, enter number from 1 to 5 ");
        }
    }

    function isUserNameExist() {
        if (userNameValue == "") {
            $('#errorBotCountMessage').append("Please, enter you name");
        }
    }

    if (botCountValue > maxBotCount || botCountValue < minBotCount || userNameValue == "") {
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
                var dealerPositionIndex = 0;
                var maxWinScore = 21;
                var playersScorList = GetPlayersScors();
                if (playersScorList[dealerPositionIndex].score == maxWinScore) {
                    var winners = DefineTheWinners();
                    jQuery.noConflict();
                    $('#exampleModal').modal('show');
                    $('#modal-body').text("Game is over! Winners are: " + winners);
                    $('#enough').prop('disabled', true);
                    $('#more').prop('disabled', true);
                }
                if (playersScorList[dealerPositionIndex].score != maxWinScore) {
                    $('#more').show();
                    $('#enough').show();
                }
            }
        });
    }
};

function OnMore() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:64520/Game/more',
        success: function (data) {
            $('#result').replaceWith(data);
        },
        complete: function () {
            var userPositionIndex = 1;
            var maxWinScore = 21;
            var playersScors = GetPlayersScors();
            if (playersScors[userPositionIndex].score >= maxWinScore) {
                var winners = DefineTheWinners();
                jQuery.noConflict();
                $('#exampleModal').modal('show');
                $('#modal-body').text("Game is over! Winners are: " + winners);
                $('#enough').prop('disabled', true);
                $('#more').prop('disabled', true);
            }
        }
    });
}


function OnEnough() {
    $('#enough').prop('disabled', true);
    $('#more').prop('disabled', true);

    var winners;

    $.ajax({
        type: 'GET',
        url: 'http://localhost:64520/Game/enough',
        success: function (data) {
            $('#result').replaceWith(data);
        },
        complete: function () {
            winners = DefineTheWinners();
            jQuery.noConflict();
            $('#exampleModal').modal('show');
            $('#modal-body').text("Game is over! Winners are: " + winners);
        }
    });
}



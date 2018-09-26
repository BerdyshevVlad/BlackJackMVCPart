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
                winners = DefineTheWinners();
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

    var winnerStr="";
    for (var j = 0; j < winners.length; j++) {
        winnerStr += winners[j].name+" ";
    }

    winnerStr += " have score " + maxScore;

    return winnerStr;
}
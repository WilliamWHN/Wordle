$(document).ready(function () {

    let attempt = 1
    let box = 0
    let wordToGuess = $("#word").val()
    let take = ""
    let gameover = false

    $(document).on('keydown', function (event) {
        if (!gameover) {
            var regex = new RegExp("^[a-zA-Z]$");
            var key = event.key
            let letterBox = $(".attempt-" + attempt).children(".case-" + box).children()
            if (!regex.test(key)) {
                if (key == "Backspace" && box > 0) {
                    box--
                    letterBox = $(".attempt-" + attempt).children(".case-" + box).children()
                    letterBox.text("")
                    take = take.slice(0,-1)
                }
                else if (key == "Enter") {
                    validateWord(take, attempt)
                }
                else {
                    event.preventDefault()
                    return false;
                }
            }
            else {
                if (box < wordToGuess.length) {
                    letterBox.text(key.toUpperCase())
                    take += key.toLowerCase()
                    box++
                }
            }
        }

    });

    function validateWord(word, tries) {
        if (word === wordToGuess) {
            $.ajax({
                url: 'Score/Create',
                type: 'POST',
                contentType: 'application/json;',
                data: JSON.stringify({ Tries: attempt, ScorePoints: word.length * 100 / attempt, Word: word }),
                success: function (e) {
                    gameover = true;
                    alert("Bien joué, vous avez trouvé le mot en " + attempt + "essais")
                }
            });

        }
        else if (word.length == wordToGuess.length) {
            take = ""
            attempt++
            box = 0
        }
    }
});
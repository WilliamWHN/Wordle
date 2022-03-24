$(document).ready(function () {

    let attempt = 1
    let box = 0
    let wordToGuess = $("#word").val()
    let word = ""

    $(document).on('keydown', function (event) {
        var regex = new RegExp("^[a-zA-Z]$");
        var key = event.key
        let letterBox = $(".attempt-" + attempt).children(".case-" + box).children()
        if (!regex.test(key)) {
            if (key == "Backspace" && box > 0) {
                box--
                letterBox = $(".attempt-" + attempt).children(".case-" + box).children()
                letterBox.text("")
                word = word.slice(0,-1)
            }
            else if (key == "Enter") {       
                validateWord(word)
            }
            else {
                event.preventDefault()
                return false;
            }
        }
        else {
            if (box < wordToGuess.length) {
                letterBox.text(key.toUpperCase())
                word += key.toLowerCase()
                box++
            }
        }

        
    });

    function validateWord(word) {
        if (word === wordToGuess) {
            alert("AH OUI OUI OUI, c'est gagné !")
        }
    }
});
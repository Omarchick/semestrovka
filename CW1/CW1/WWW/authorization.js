async function getApi() {
    var dev = document.getElementById("api");
    var response = await fetch("/newsForAll");
    var responseText = await response.text();
    dev.innerText = responseText;
}
/*$("#getNews").click(function () {
    $.ajax({
        url: '/news',
        type: 'GET',
        contentType: 'application/json',
        success: function (data) {
            $("#api").text(data)
        }
    })
})*/
async function register() {
    let nameEl = document.getElementById("inputName");
    let passwordEl = document.getElementById("inputPassword");
    let errorBlock = document.getElementById("errorBlock");
    if (passwordEl.value.length < 8) {
        errorBlock.innerText = "The password must contain 8 characters or more!";
    }
    else {
        var response = await fetch("/reg", { method: "POST", body: nameEl.value + ":" + passwordEl.value });
        let responseText = await response.text();
        if (responseText == "All done!") {
            errorBlock.innerText = "You have successfully registered!";
            document.location.href = "http://localhost:5555/";
        }
        else {
            errorBlock.innerText = "Invalid user name or such user name already exists!";
        }
    }
}

async function signIn() {
    let nameEl = document.getElementById("inputName");
    let passwordEl = document.getElementById("inputPassword");
    let errorBlock = document.getElementById("errorBlock");
    if (passwordEl.value.length < 8) {
        errorBlock.innerText = "The password must contain 8 characters or more!";
    }
    else {
        var response = await fetch("/signIn", { method: "POST", body: nameEl.value + ":" + passwordEl.value });
        let responseText = await response.text();
        if (responseText == "All done!") {
            errorBlock.innerText = "You have successfully entered!";
            document.location.href ="http://localhost:5555/";
        }
        if(responseText == "Not registered!") {
            errorBlock.innerText = "Incorrect data!";
        }
    }
}


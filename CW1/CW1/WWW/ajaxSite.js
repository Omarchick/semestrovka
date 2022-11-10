async function getApi() {
    const dev = document.getElementById("api");
    const response = await fetch("/news");
    const responseText = await response.text();
    if (!checkSession(responseText) && document.location.pathname === "/") {
        document.location.href = "http://localhost:5555/authorization";
    }
    else {
        dev.innerText = responseText;
    }
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
async function exitAccount() {
    document.location.href = "http://localhost:5555/authorization";
}

async function changeData() {
    let nameEl = document.getElementById("inputName");
    let passwordEl = document.getElementById("inputPassword");
    let firstInputName = undefined;
    let firstInputPass = undefined;
    let errorBlock = document.getElementById("errorBlock");
    let doBt = document.getElementById("doBtn");
    var response = await fetch("/signInForChangeData", { method: "POST", body: nameEl.value + ":" + passwordEl.value });
    let responseText = await response.text();
    if (!checkSession(responseText)) {
        return;
    }
    if (responseText === "All done!") {
        firstInputName = nameEl.value;
        firstInputPass = passwordEl.value;
        errorBlock.innerText = "Input new data and click Do!";
        doBt.onclick = async function () {
            response = await fetch("/changeData", { method: "POST", body: firstInputName + ":" + firstInputPass + ":" + nameEl.value + ":" + passwordEl.value });
            responseText = await response.text();
            if (!checkSession(responseText)) {
                return;
            }
            if (responseText === "Right!") {
                errorBlock.innerText = "You have successfully changed your account's data!";
            }
            else if (passwordEl.value.length < 8) {
                errorBlock.innerText = "The password must contain 8 characters or more!";
            }
            else {
                errorBlock.innerText = "This user already exists or you inputed incorrect data!";
            }
        }
    }
    else {
        errorBlock.innerText = "Input the real data and to verify your data and click Change Data!";
    }
}

async function deleteUser() {
    let nameEl = document.getElementById("inputName");
    let passwordEl = document.getElementById("inputPassword");
    let errorBlock = document.getElementById("errorBlock");
    if (passwordEl.value.length < 8) {
        errorBlock.innerText = "The password must contain 8 characters or more!";
    }
    else {
        var response = await fetch("/deleteUser", { method: "POST", body: nameEl.value + ":" + passwordEl.value });
        let responseText = await response.text();
        if (!checkSession(responseText)) {
            return;
        }
        if (responseText === "All done!") {
            errorBlock.innerText = "You have successfully delete your account!";
            document.location.href = "http://localhost:5555/authorization";
        }
        else {
            errorBlock.innerText = "Incorrect data!";
        }
    }
}

function checkSession(responseText) {
    if (responseText === "Lost!") {
        document.location.href = "http://localhost:5555/authorization";
        return false;
    }
    return true;
}
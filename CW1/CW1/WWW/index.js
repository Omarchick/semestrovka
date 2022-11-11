let errorBlock = document.getElementById("errorBlock");
let myButton = document.getElementById("createCompBtn");
let textName = document.getElementById("textName");
let inputText = document.getElementById("inputName");
let inputInfo = document.getElementById("inputInfo");
var response = await fetch("/createCompany", { method: "POST", body: inputText.value + ":" + inputInfo.value });
console.log(2);
let responseText = await response.text();
if (responseText == "All done!") {
    myButton.onclick = function () {
        let element = document.createElement("div");
        element.textContent = inputText.value.concat(' - ');
        element.textContent = element.textContent.concat(inputInfo.value);
        inputText.value = "";
        textName.appendChild(element);
    }
}

let errorBlock = document.getElementById("errorBlock");
let myButton = document.getElementById("createCompBtn");
let textName = document.getElementById("textName");
let inputText = document.getElementById("inputName");
let inputInfo = document.getElementById("inputInfo");
var response = await fetch("/createOtziv", { method: "POST", body: inputText.value + ":" + inputInfo.value });
let responseText = await response.text();
if (responseText == "All done!") {
    myButton.onclick = function () {
        let element = document.createElement("div");
        element.textContent = inputText.value.concat(' - ');
        element.textContent = element.textContent.concat(inputInfo.value);
        inputText.value = "";
        textName.appendChild(element);
    }
}

let errorBlock = document.getElementById("errorBlock");
let myButton = document.getElementById("createCompBtn");
let textName = document.getElementById("textName");
let inputText = document.getElementById("inputName");
let inputInfo = document.getElementById("inputInfo");
var response = await fetch("/Find", { method: "POST", body: inputText.value + ":" + inputInfo.value });
let responseText = await response.text();
if (responseText == "All done!") {
    myButton.onclick = function () {
        errorBlock.value = "Finded";
    }
}


let errorBlock = document.getElementById("errorBlock");
let myButton = document.getElementById("createCompBtn");
let textName = document.getElementById("textName");
let inputText = document.getElementById("inputName");
let inputInfo = document.getElementById("inputInfo");
var response = await fetch("/DeleteCompany", { method: "POST", body: inputText.value + ":" + inputInfo.value });
let responseText = await response.text();
if (responseText == "All done!") {
    myButton.onclick = function () {
        textName.removeChild(element);
    }
}
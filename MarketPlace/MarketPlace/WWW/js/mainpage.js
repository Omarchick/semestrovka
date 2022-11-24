async function register() {
    let nameEl = document.getElementById("inputName");
    let passwordEl = document.getElementById("inputPassword");
    let errorBlock = document.getElementById("errorBlock");
    if (passwordEl.value.length < 8) {
        errorBlock.innerText = "The password must contain 8 characters or more!";
    }
    else {
        console.log(nameEl.value)
        let response = await fetch("/register", { method: "POST", body: JSON.stringify(new User(0,nameEl.value, passwordEl.value))});
        let responseText = await response.text();
        if (responseText == "All done!") {
            errorBlock.innerText = "You have successfully registered!";
           document.location.href = "http://localhost:1111/products";
        }
        else {
            errorBlock.innerText = "Invalid user name or such user name already exists. \n" +
                "The length of the name must be 2 or more. \n" +
                "The password should be more complicated.";
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
        console.log(nameEl.value)
        let response = await fetch("/signIn", { method: "POST", body: JSON.stringify(new User(0,nameEl.value, passwordEl.value))});
        let responseText = await response.text();
        console.log(responseText + "RT");
        if (responseText == "All done!") {
            errorBlock.innerText = "You have successfully entered!";
            document.location.href ="http://localhost:1111/products";
        }
        else{
            errorBlock.innerText = "Incorrect data!";
        }
    }
}

function User(id = 0, name, password, balance = 0){
    this.Id = id;
    this.Name = name;
    this.Password = password;
    this.Balance = balance;
}

async function redirectProducts(){
    document.location.href("/products");
}
async function redirectProductsNotRegister(){
    document.location.href="http://localhost:1111/productsNotRegistered";
}



async function GetPersonInformation() {
    let parentItem = document.getElementById("personInformation");
    let item = document.createElement("div");
        let response = await fetch('/getPersonInfo').catch();
        if (response.ok) {
            let user = JSON.parse(await response.text());
            item.innerHTML = `
        <form>
            <p class="userData">
                <strong title="Name" class="userInfo" id="userName">${user.Name}</strong>
                <text title="CountMoney" class="userInfo" id="UserBalance">Balance: ${user.Balance}⚡</text>
                <button class="settingsBtn" onclick="changeBalance()" id="changeBalance">Put Money</button>
                <button class="settingsBtn" onclick="configure()" id="settings">Settings</button>
                <button class="settingsBtn" onclick="buyProducts()" id="buyProdcut">Buy Products</button>
            </p>      
        </form>
    `;
        } else {
            item.innerHTML = `
        <p class="enter">
            <input class="input_data" id="inputName" placeholder="Name">
            <input class="input_data" id="inputPassword" placeholder="Password"  type="password">
            <button class="enterBtn" onclick="register()" id="reg">Register</button>
            <button class="enterBtn" onclick="signIn()" id="singIn">Sign in</button>
        </p>
    `;
        }
        parentItem.appendChild(item);
}
async function configure() {
    
}
async function changeBalance() {
}

async function buyProducts() {
    
}
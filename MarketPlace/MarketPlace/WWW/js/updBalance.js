let isSending = false;
async function updBalance() {
    if (!isSending){
        let response = await fetch('/updBalance');
        if (response.ok) {
            isSending = true;
            balanceElement = document.querySelector('#UserBalance');
            let balance = Number(balanceElement.textContent.
            replace('Balance: ', '').replace('⚡', ''));
            setTimeout(() => {
                if (balanceElement != null){
                    balanceElement.textContent = "Balance: " + (balance + 1) + "⚡";
                    document.querySelector("#addBal").disabled = false;
                    isSending = false;
                }
            }, 5000)
            document.querySelector('#UserBalance').textContent = "Balance: LOADING...";
            document.querySelector("#addBal").disabled = true;
        }
    }
}


async function getProducts() {
    /*let result = await (await fetch('/getProductsFromDB')).text();
    productsOnDB = JSON.parse(result);
    for (let i = 0; i < productsOnDB.length; i++) {
        await addProductItemWithIndex(productsOnDB[i].Name, productsOnDB[i].Information, productsOnDB[i].Rating, productsOnDB[i].Count, productsOnDB[i].Id, productsOnDB[i].Price);
    }
    setTimeout(() => {
        balanceElement = document.querySelector('#UserBalance');
        if (balanceElement != null){
            balance = Number(balanceElement.textContent.
            replace('Balance: ', '').replace('⚡', ''));
            console.log(balance);
        }
    }, 500)*/
}

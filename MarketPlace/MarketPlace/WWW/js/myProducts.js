async function addProductItem(id, name, information, rating, count, realId, price) {
    let productItem = document.getElementById("productItemCollector");
    let item = document.createElement("div");
    /*    item.setAttribute('id', "productItem");*/
    //<b title="${rating}" class="productRating" style="display: flex; flex-direction: column; text-align: left">${rating}
    let starRating = "";
    if (rating < 1){
        starRating = "No rating!";
    }
    for (let i = 0; i < Math.floor(rating); i++){
        starRating+= "★";
    }
    item.innerHTML = `
    <div class="productItem" id="${id}product" style="top: ${getCount()}vh; left: 30vw">
        <strong title="${name}" style="display: flex; flex-direction: column; 
        white-space: nowrap; overflow: hidden; text-overflow: ellipsis; ">${name}
            <div class="form_item">
                <div class="rating rating_set">
                    <div class="rating_body">
                        <div class="rating_active" style="width: ${rating * 20}%"></div>
                        <div title="${starRating}" class="rating_items">
                            <input type="radio" class="rating_item" value="0" name="rating">
                            <input type="radio" class="rating_item" value="1" name="rating">
                            <input type="radio" class="rating_item" value="2" name="rating">
                            <input type="radio" class="rating_item" value="3" name="rating">
                            <input type="radio" class="rating_item" value="4" name="rating">
                            <input type="radio" class="rating_item" value="5" name="rating">
                        </div>
                    </div>
                    <div title="Рейтинг - ${rating}" class="rating_value">${rating}</div>
                    <div title="${count}" class="productCount">${count}</div>

                </div>
            </div>
                <textarea title="${information}" class="productInfo" maxlength="250" readonly>${information}</textarea>
                <button title="Delete from cart." class="deleteBtn" onclick="changeProductCount(-1, Number(this.parentElement.parentElement.id.replace('product', '')), ${realId})">-</button>
                <button title="Add into cart." class="addBtn" onclick="changeProductCount(1, Number(this.parentElement.parentElement.id.replace('product', '')), ${realId})">+</button>
                <button title="Make a review to this product." class="makeReview">
                    <img class="btnImg" src="/pictures/message.png" alt="reviewImage" style="pointer-events: none"/>
                </button>
        </strong>
        <div class="productPrice">${price}<text style="font-size: 3vh; margin-top: 0.4vh">⚡</text></div>   
        </div>
    `;
    /*    <div class="productItem" id="classNameproduct" style="top: 35vh; left: 30vw">
            <strong style="display: flex; flex-direction: column;">${name}
                <b class="productRating" stclassNamedisplay: flex; flex-direction: column; text-align: left">{rating}</b>
            <label>
                <button class="deleteBtn">-
                </butclassName
                <button class="addBtn">+</buttonclassName         <textarea class="productInfo" maxlclassName="250" readonlymaxLengthation}<readOnlya>
        </label>
    </strong>
    </div>*/

    /*    <div class="productItem" id="classNameproduct" style="top: ${getCount()}vh; left: 30vw">
            <strong style="display: flex; flex-direction: column">${name}
                <label>
                <textarea class="productInfo" maxlength="250" readonly>${information}</textarea>
                    <readOnlya>
                </label>
            </strong>
        </div>*/
    productsId.push(id);
    productsOnPage.push(new Product(id, name, information, rating, count, realId, price));
    productItem.appendChild(item);
}

async function removeProduct(id) {
    if (id < productsId.length) {
        document.getElementById(String(id) + "product").remove();
        await moveUpElements(id);
        count -= 70;
        productsId.pop();
        //productsOnPage.slice(id, id);
        for (let i = id; i < productsOnPage.length - 1; i++){
            productsOnPage[i] = productsOnPage[i + 1];
            productsOnPage[i].id--;
        }
        //document.removeChild(document.getElementById("1"));
    }
}

let isSending = false;
var productsOnPage = [];
var productsId = [];
var productsOnDB = [];
var count = -35;

let balanceElement;
let balance;

function getCount() {
    count += 70;
    return count;
}

async function moveUpElements(deletedId) {
    for (let i = deletedId + 1; i < productsId.length; i++) {
        let product = document.getElementById(i + "product");
        product.setAttribute("id",
            (Number(product.id.replace("product", "")) - 1) + "product");
        
/*        let deleteBtn = product.querySelector('.deleteBtn');
        let addBtn = product.querySelector('.addBtn');*/
/*        deleteBtn.onclick.replace(', ' + deletedId + ',', ', ' + Number(deletedId) - 1 + ',')
        addBtn.onclick.replace(', ' + deletedId + ',', ', ' + Number(deletedId) - 1 + ',')*/
/*        console.log(addBtn.onclick.toString());
        console.log(deletedId);
        let replacedString =  (addBtn.onclick.toString()).replace(', ' + i + ',', ', ' + (Number(i) - 1) + ',');
        let elem = document.createElement('div');
        elem.innerHTML = `
            <button title="Add into cart." class="addBtn" onclick="${replacedString}">+</button>`;
        addBtn = elem;
        addBtn.onclick;
        console.log(replacedString);
        console.log(product.id + " Новый");
        console.log("Спускаем");*/
        product.setAttribute("style", "top:" + (Number(product.id.replace("product", "")) * 70 + 35 + "vh"));
        //document.querySelector('#reg').style.top = '10vh';
        //document.getElementById("2").style.top = '10vh';
    }
}

async function addProductItemWithIndex(name, information, rating, count, realId, price) {
    await addProductItem(productsId.length, name, information, rating, count, realId, price);
}

async function getProductsFromDB() {
    let result = await (await fetch('/getUserProducts')).text();
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
    }, 500)
}

function Product(id, name, information, rating, count, realId, price){
    this.Id = id;
    this.Name = name;
    this.Information = information;
    this.Rating = rating;
    this.Count = count;
    this.RealId = realId;
    this.Price = price;
}

/*async function changeProductCount(count, id, productId) {
    let element = document.getElementById(id + "product");
    let countElement = element.querySelector('.productCount');
    let productCount = Number(countElement.textContent);
    let balance = Number(document.querySelector('#UserBalance').textContent.
    replace('Balance: ', '').replace('⚡', ''));
    let price = Number(element.querySelector('.productPrice'.textContent));
    if (count < 0 && productCount <= 0){
        await removeProduct(id);
        await fetch("/deleteUserProduct",
            {method: "POST", body: JSON.stringify(new UserProduct(-1, productId, count))});
        return;
    }
    if (productCount >= 0 && balance >= price) {
        let response = await fetch("/addProductCount",
            {method: "POST", body: JSON.stringify(new UserProduct(-1, productId, count))});
        if (response.status === 200) {
            countElement.title = Number(countElement.title) + count;
            countElement.textContent = Number(countElement.textContent) + count;
        } else if (response.status === 205) {
            await removeProduct(id);
        } else {
            let errorBlock = document.getElementById('errorBlock');
            errorBlock.innerText = "Error!";
            setTimeout(TurnOffErrorText, 3000);
        }
    }
}*/

let timeout = 1000;
async function changeProductCount(count, id, productId) {
    if (sendingUserProducts.length > timeout / 1000 * 100)
    {
        sendingUserProducts = [];
        await reloadPage();
    }
    let element = productsOnPage[id];
    let productCount = element.Count;
    let price = element.Price

    let countElement = document.getElementById(id + "product").querySelector('.productCount');

    /*    if (count < 0 && productCount <= 0){
            let response = await fetch("/deleteUserProduct",
                {method: "POST", body: JSON.stringify(new UserProduct(-1, productId, count))});
            document.querySelector('#UserBalance').textContent = "Balance: " + JSON.parse(await response.text()).Balance + "⚡";
            return;
        }*/
    //if ProductCount On Page > 0
    if (productCount > 0){
        console.log(productCount + " Count");
        /*        let response = await fetch("/addProductCount",
                    { method: "POST", body: JSON.stringify(new UserProduct(-1, productId, count))});
                if (response.ok){*/
        //let dbData = JSON.parse(await response.text());
        //if we wonna add product CHECK balance
        if (count > 0 && balance - count * price >= 0)
        {

            /*                document.querySelector('#UserBalance').textContent = "Balance: " + dbData.Balance + "⚡";
                            countElement.title = dbData.ProductCount;
                            countElement.textContent = dbData.ProductCount;*/
            balance = balance - price * count;
            balanceElement.textContent = "Balance: " + balance + "⚡";
            countElement.title = Number(countElement.title) + count;
            countElement.textContent = Number(countElement.textContent) + count;
            await AddSendingData(productId, count);
        }
        else if (count < 0)
        {
            /*                document.querySelector('#UserBalance').textContent = "Balance: " + dbData.Balance + "⚡";
                            countElement.title = dbData.ProductCount;
                            countElement.textContent = dbData.ProductCount;*/
            balance = balance - count * price;
            balanceElement.textContent = "Balance: " + balance + "⚡";
            console.log(balanceElement);
            element.Count = Number(countElement.textContent) + count
            countElement.title = element.Count;
            countElement.textContent = element.Count;
            console.log(balance + " after " + count * price);
            await AddSendingData(productId, count);

            /*                countElement.title = Number(countElement.title) + count;
                            countElement.textContent = Number(countElement.textContent) + count;*/
        }
    }
    else {
        console.log("PrCount < 0")
        if (count > 0 && balance - count * price >= 0)
        {
            console.log(count + " count " + price + "  price")
            console.log(balance + " before " + count * price);

            /*                document.querySelector('#UserBalance').textContent = "Balance: " + dbData.Balance + "⚡";
                            countElement.title = dbData.ProductCount;
                            countElement.textContent = dbData.ProductCount;*/
            balance = balance - count * price;
            balanceElement.textContent = "Balance: " + balance + "⚡";
            element.Count = Number(countElement.textContent) + count
            countElement.title = element.Count;
            countElement.textContent = element.Count;
            console.log(balance + " after " + count * price);
            await AddSendingData(productId, count);

            /*            countElement.title = Number(countElement.title) + count;
                        countElement.textContent = Number(countElement.textContent) + count;*/
        }
    }
    if (count < 0 && countElement.textContent <= 0){
        console.log("rem");
        await removeProduct(id);
    }
    /*        else {
                let errorBlock = document.getElementById('errorBlock');
                errorBlock.innerText = "Error!";
                setTimeout(TurnOffErrorText, 3000);
            }
        }*/
}



let sendingUserProducts = [];
async function AddSendingData(productId, productCount) {
    sendingUserProducts.push(new UserProduct(-1, productId, productCount));
    console.log(isSending);
    if (!isSending){
        SendDataToDB();
    }
}
let c = 0;
function SendDataToDB() {
    isSending = true;
    window.setTimeout(() => {
        let response = fetch("/addProducts",
            { method: "POST", body: JSON.stringify(sendingUserProducts)});
        sendingUserProducts = [];
        response.then(response => {if (!response.ok){
            location.reload();
        }}).catch(() =>  {window.clearTimeout(); reloadPage();});
        /*location.reload();
                if (response.ok){
      /*            console.log(response);
                    let result = response.text();
                    console.log(response);
                    let productsOnDB = JSON.parse(result);
                    for (let i = 0; i < productsOnDB.length; i++) {
                        addProductItemWithIndex(productsOnDB[i].Id, productsOnDB[i].Count, productsOnDB[i].Balance);
                    }
        }*/
        c++;
        console.log(c + " отправилось");
        isSending = false;
    }, timeout)
    console.log("efef");
}

function reloadPage() {
    location.reload();
}


async function TurnOffErrorText() {
    let errorBlock = document.getElementById('errorBlock');
    errorBlock.innerText = "";
}


function UserProduct(userId, productId, productCount){
    this.UserId = userId;
    this.ProductId = productId;
    this.ProductCount = productCount;
}

/*
async function changeProductCountTwo(count, id, productId) {
    let element = document.getElementById(id + "product");
    let countElement = element.querySelector('.productCount');
    let productCount = Number(countElement.textContent);
    let balance = Number(document.querySelector('#UserBalance').textContent.
    replace('Balance: ', '').replace('⚡', ''));
    let price = Number(element.querySelector('.productPrice'.textContent));
    if (count < 0 && productCount <= 0){
        await removeProduct(id);
        await fetch("/deleteUserProduct",
            {method: "POST", body: JSON.stringify(new UserProduct(-1, productId, count))});
        return;
    }
    if (productCount >= 0 && balance >= price) {
        let response = await fetch("/addProductCount",
            {method: "POST", body: JSON.stringify(new UserProduct(-1, productId, count))});
        if (response.status === 200) {
            countElement.title = Number(countElement.title) + count;
            countElement.textContent = Number(countElement.textContent) + count;
        } else if (response.status === 205) {
            await removeProduct(id);
        } else {
            let errorBlock = document.getElementById('errorBlock');
            errorBlock.innerText = "Error!";
            setTimeout(TurnOffErrorText, 3000);
        }
    }
}*/
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
                    <img class="btnImg" src="/pictures/message.png" alt="reviewImage"/>
                </button>
        </strong>
        <div class="productPrice">${price}</div>   
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
    productsOnPage.push(new Product(id, name, information, rating, count, realId));
    productItem.appendChild(item);
}

var productsOnPage = [];
var productsId = [];
var productsOnDB = [];
var count = -35;

function getCount() {
    count += 70;
    return count;
}

async function addProductItemWithIndex(name, information, rating, count, realId, price) {
    await addProductItem(productsId.length, name, information, rating, count, realId, price);
}

async function getProductsFromDB() {
    let result = await (await fetch('/getProductsFromDB')).text();
    productsOnDB = JSON.parse(result);
    for (let i = 0; i < productsOnDB.length; i++) {
        await addProductItemWithIndex(productsOnDB[i].Name, productsOnDB[i].Information, productsOnDB[i].Rating, productsOnDB[i].Count, productsOnDB[i].Id, productsOnDB[i].Price);
    }
}

function Product(id, name, information, rating, count, realId) {
    this.Id = id;
    this.Name = name;
    this.Information = information;
    this.Rating = rating;
    this.Count = count;
    this.RealId = realId;
}

async function changeProductCount(count, id, productId) {
    let element = document.getElementById(id + "product");
    let countElement = element.querySelector('.productCount');
    let productCount = Number(countElement.textContent);
    let balance = Number(document.querySelector('#UserBalance').textContent.
    replace('Balance: ', '').replace('⚡', ''));
    let price = Number(element.querySelector('.productPrice').textContent);
/*    if (count < 0 && productCount <= 0){
        let response = await fetch("/deleteUserProduct",
            {method: "POST", body: JSON.stringify(new UserProduct(-1, productId, count))});
        document.querySelector('#UserBalance').textContent = "Balance: " + JSON.parse(await response.text()).Balance + "⚡";
        return;
    }*/
    if (productCount >= 0){
        let response = await fetch("/addProductCount",
            { method: "POST", body: JSON.stringify(new UserProduct(-1, productId, count))});
        if (response.ok){
            let dbData = JSON.parse(await response.text());
            if (count > 0 && balance >= count * price)
            {
                console.log(1221);
                console.log(dbData);
                document.querySelector('#UserBalance').textContent = "Balance: " + dbData.Balance + "⚡";
                countElement.title = dbData.ProductCount;
                countElement.textContent = dbData.ProductCount;
                /*                document.querySelector('#UserBalance').textContent = "Balance: " + String(balance - price * count) + "⚡";*/
/*                countElement.title = Number(countElement.title) + count;
                countElement.textContent = Number(countElement.textContent) + count;*/
            }
            else if (count < 0)
            {
                console.log(dbData);
                console.log(2121);
                document.querySelector('#UserBalance').textContent = "Balance: " + dbData.Balance + "⚡";
                countElement.title = dbData.ProductCount;
                countElement.textContent = dbData.ProductCount;
/*                document.querySelector('#UserBalance').textContent = "Balance: " + String(balance - price * count) + "⚡";*/
/*                countElement.title = Number(countElement.title) + count;
                countElement.textContent = Number(countElement.textContent) + count;*/
            }
        }
        else {
            let errorBlock = document.getElementById('errorBlock');
            errorBlock.innerText = "Error!";
            setTimeout(TurnOffErrorText, 3000);
        }
    }
}

async function TurnOffErrorText() {
    let errorBlock = document.getElementById('errorBlock');
    errorBlock.innerText = "";
}


function UserProduct(userId, productId, productCount, price){
    this.UserId = userId;
    this.ProductId = productId;
    this.ProductCount = productCount;
    this.Price = price;
}
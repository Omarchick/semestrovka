async function addProductItem(name, information, id, rating, count) {
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
        <strong title="${name}" style="display: flex; flex-direction: column; white-space: nowrap; overflow: hidden; text-overflow: ellipsis; ">${name}  
            <div class="form_item">
                <div class="rating rating_set">
                    <div class="rating_body">
                        <div class="rating_active" style="width: ${rating * 20}%"></div>
                        <div title="${starRating}" class="rating_items">
                            <input type="radio" class="rating_item" value="1" name="rating">
                            <input type="radio" class="rating_item" value="2" name="rating">
                            <input type="radio" class="rating_item" value="3" name="rating">
                            <input type="radio" class="rating_item" value="4" name="rating">
                            <input type="radio" class="rating_item" value="5" name="rating">
                        </div>
                    </div>
                    <div title="Рейтинг - ${rating}" class="rating_value">${rating}</div>
                </div>
            </div>
                <textarea title="${information}" class="productInfo" maxlength="250" readonly>${information}</textarea>
                <button title="Delete from cart." class="deleteBtn">-</button>
                <button title="Add into cart." class="addBtn">+</button>
                <button title="Make a review to this product." class="makeReview">
                    <img class="btnImg" src="/pictures/message.png" alt="reviewImage"/>
                </button>
                <b title="${count}" class="productCount" style="display: flex; flex-direction: row; text-align: left;
                 position: relative; top: -58.5vh; right: -30vw;
                 background-color: #0adfff; border-radius: 1vh">${count}</b>
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
    productsOnPage.push(new Product(id, name, information, rating, count));
    productItem.appendChild(item);
}

async function removeProduct(id) {
    if (id < productsId.length) {
        document.getElementById(String(id) + "product").remove();
        await moveUpElements(id);
        count -= 70;
        productsId.pop();
        productsOnDB.slice(id, id);
        //document.removeChild(document.getElementById("1"));
    }
}

var productsOnPage = [];
var productsId = [];
var productsOnDB = [];
var count = -35;

function getCount() {
    count += 70;
    return count;
}

async function moveUpElements(deletedId) {
    for (let i = deletedId + 1; i < productsId.length; i++) {
        let product = document.getElementById(i + "product");
        document.getElementById(i + "product").setAttribute("id",
            (Number(product.id.replace("product", "")) - 1) + "product");
        product.setAttribute("style", "top:" + (Number(product.id.replace("product", "")) * 70 + 35 + "vh"));
        //document.querySelector('#reg').style.top = '10vh';
        //document.getElementById("2").style.top = '10vh';
    }
}

async function addProductItemWithIndex(name, information, rating, count) {
    await addProductItem(name, information, productsId.length, rating, count);
}

async function getProductsFromDB() {
    let result = await (await fetch('/getProductsFromDB')).text();
    productsOnDB = JSON.parse(result);
    for (let i = 0; i < productsOnDB.length; i++) {
        await addProductItemWithIndex(productsOnDB[i].Name, productsOnDB[i].Information, productsOnDB[i].Rating, productsOnDB[i].Count);
    }
}

function Product(id, name, information, rating, count) {
    this.Id = id;
    this.Name = name;
    this.Information = information;
    this.Rating = rating;
    this.Count = count;
}
async function addReviewItem(id, name, information, rating, realId, price) {
    let productItem = document.getElementById("reviewItemCollector");
    let item = document.createElement("div");
    let starRating = makeStars(rating);
    item.innerHTML = `
    <div class="reviewItem" id="${id}review" style="top: calc(${getCount()} * (1vmin + 1vmax));">
        <strong title="${name}" style="
        flex-direction: column;
        text-align: center;
        white-space: nowrap;
        top: 2%;
        -webkit-text-stroke: calc(0.01 * (1vw + 2vh)) #ddcd02;
        font-family: Luminari, fantasy;
        text-overflow: ellipsis;
        position: absolute;
        font-size: calc(2 * (1vmin + 1vmax));
        ">${name}
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
                </div>
            </div>
                <textarea title="${information}" class="productInfo" maxlength="250" readonly>${information}</textarea>
                <div style="position: relative; top: calc(6 * (1vmin - 1vmax))">
                    <button title="Delete from cart." class="deleteBtn" onclick="changeProductCount(-1, Number(this.parentElement.parentElement.parentElement.id.replace('product', '')), ${realId})">-</button>
                    <button title="Add review." class="addBtn" onclick="changeProductCount(1, Number(this.parentElement.parentElement.parentElement.id.replace('review', '')), ${realId})">+</button>
                </div>
        </strong>
        <div title="${price}" class="reviewPrice">${price}<text style="font-size: calc((1vmin/ 2 + 1vmax)); margin-top: font-size: calc((1vw/ 2 + 1vh/ 4))">⚡</text></div>
    </div>
    `;
    reviewsId.push(id);
    reviewOnPage.push(new Review(id, name, information, rating, count, realId, price));
    reviewItem.appendChild(item);
}
let productId = -1;
let isSending = false;
var productsOnPage = [];
var reviewsId = [];
var productsOnDB = [];
var count = -14;

let balanceElement;
let balance;

function getCount() {
    count += 21;
    return count;
}

async function addReviewItemWithIndex(name, information, rating, realId, price) {
    await addReviewItem(reviewsId.length, name, information, rating, realId, price);
}

async function getProductId() {
    let cookie = document.cookie;
    let data = cookie.split(";");
    for (let i = 0; i < data.length; i++) {
        let info = data[i].split("=");
        if (JSON.stringify(info[0]).replace(" ", "") === JSON.stringify("id")) {
            productId = info[1];
        }
    }
    return productId;
}

function CookieData(name, path, express) {
    this.Name = name;
    this.Path = path;
    this.Express = express;
}

async function getReviews() {
    console.log(window.location)
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


let timeout = 1000;
async function changeProductCount(count, id, productId) {
    if (sendingUserProducts.length > timeout / 100)
    {
        sendingUserProducts = [];
        await reloadPage();
    }
    let element = productsOnPage[id];
    let productCount = element.Count;
    let price = element.Price

    let countElement = document.getElementById(id + "review").querySelector('.productCount');
    if (productCount > 0){
        console.log(productCount + " Count");
            if (count > 0 && balance - count * price >= 0)
            {
                balance = balance - price * count;
                balanceElement.textContent = "Balance: " + balance + "⚡";
                countElement.title = Number(countElement.title) + count;
                countElement.textContent = Number(countElement.textContent) + count;
                await AddSendingData(productId, count, id);
            }
            else if (count < 0)
            {
                balance = balance - count * price;
                balanceElement.textContent = "Balance: " + balance + "⚡";
                element.Count = Number(countElement.textContent) + count
                countElement.title = element.Count;
                countElement.textContent = element.Count;
                await AddSendingData(productId, count, id);
            }
        }
        else {
            console.log("count < 0")
        if (count > 0 && balance - count * price >= 0)
        {
            balance = balance - count * price;
            balanceElement.textContent = "Balance: " + balance + "⚡";
            element.Count = Number(countElement.textContent) + count
            countElement.title = element.Count;
            countElement.textContent = element.Count;
            console.log(balance + " after " + count * price);
            await AddSendingData(productId, count, id);
        }
    }
}

let sendingUserProducts = [];
async function AddSendingData(productId, productCount, id) { 
    productsOnDB[id].Count += productCount;
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
            c++;
        isSending = false;
    }, 1000)
}

function reloadPage() {
    location.reload();
}


function UserProduct(userId, productId, productCount){
    this.UserId = userId;
    this.ProductId = productId;
    this.ProductCount = productCount;
}

function makeStars(rating) {
    let starRating ="";
    if (rating < 1){
        starRating = "No rating!";
    }
    for (let i = 0; i < Math.floor(rating); i++){
        starRating += "★";
    }
    return starRating;
}
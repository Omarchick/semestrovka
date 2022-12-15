async function addReviewItem(id, name, information, rating, realId, price) {
    let reviewItem = document.getElementById("reviewItemCollector");
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
                <textarea title="${information}" class="reviewInfo" maxlength="250">${information}</textarea>
        </strong>
        <div title="${price}" class="reviewPrice">${price}<text style="font-size: calc((1vmin/ 2 + 1vmax)); margin-top: font-size: calc((1vw/ 2 + 1vh/ 4))">⚡</text></div>
    </div>
    `;
    reviewsId.push(id);
    reviewsOnPage.push(new Review(id, name, information, rating, realId, price));
    reviewItem.appendChild(item);
}


async function addReviewItemCanEdit(id, name, information, rating, realId, price) {
    let reviewItem = document.getElementById("reviewItemCollector");
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
                <textarea title="${information}" class="reviewInfo" maxlength="250">${information}</textarea>
                <div style="position: relative; top: calc(6 * (1vmin - 1vmax))">
                    <button title="Delete review." class="deleteBtn" onclick="changeReviewCount(-1, Number(this.parentElement.parentElement.parentElement.id.replace('review', '')), ${realId})">-</button>
                    <button title="Add review." class="addBtn" onclick="changeReviewCount(1, Number(this.parentElement.parentElement.parentElement.id.replace('review', '')), ${realId})">+</button>
                </div>
        </strong>
        <div title="${price}" class="reviewPrice">${price}<text style="font-size: calc((1vmin/ 2 + 1vmax)); margin-top: font-size: calc((1vw/ 2 + 1vh/ 4))">⚡</text></div>
    </div>
    `;
    reviewsId.push(id);
    reviewsOnPage.push(new Review(id, name, information, rating, realId, price));
    reviewItem.appendChild(item);
}




let productId = -1;
let productRating = 0;
let isSending = false;
let reviewsOnPage = [];
let reviewsOnDB = [];
let reviewsId = [];
let count = -14;

let balanceElement;
let balance;


async function getReviewsFromDB() {
    getReviewsFromDBCanEdit();
    console.log("DOE")
    getReviewsFromDBNotEdit();
}

async function getReviewsFromDBNotEdit() {
    let result = await (await fetch('/getUserReviews', {method: "POST", body: JSON.stringify(productId)})).text();
    reviewsOnDB = JSON.parse(result);
    for (let i = 0; i < reviewsOnDB.length; i++) {
        await addReviewItemWithIndex(reviewsOnDB[i].Name, reviewsOnDB[i].Message, reviewsOnDB[i].Rating, reviewsOnDB[i].Id, reviewsOnDB[i].Price);
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

async function getReviewsFromDBCanEdit() {
    let result = await (await fetch('/getUserReviewsCanEdit', {method: "POST", body: JSON.stringify(productId)})).text();
    reviewsOnDB = JSON.parse(result);
    for (let i = 0; i < reviewsOnDB.length; i++) {
        await addReviewItemWithIndexCanEdit(reviewsOnDB[i].Name, reviewsOnDB[i].Message, reviewsOnDB[i].Rating, reviewsOnDB[i].Id, reviewsOnDB[i].Price);
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

function getCount() {
    count += 21;
    return count;
}

async function addReviewItemWithIndex(name, information, rating, realId, price) {
    await addReviewItem(reviewsId.length, name, information, rating, realId, price);
}

async function addReviewItemWithIndexCanEdit(name, information, rating, realId, price) {
    await addReviewItemCanEdit(reviewsId.length, name, information, rating, realId, price);
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
async function getProductData() {
    let cookie = document.cookie;
    let data = cookie.split(";");
    for (let i = 0; i < data.length; i++) {
        let info = data[i].split("=");
        if (JSON.stringify(info[0]).replace(" ", "") === JSON.stringify("rating")) {
            productRating = info[1];
        }
        if (JSON.stringify(info[0]).replace(" ", "") === JSON.stringify("id")) {
            productId = info[1];
        }
    }
}
function CookieData(name, path, express) {
    this.Name = name;
    this.Path = path;
    this.Express = express;
}

async function getReviews() {
    console.log(window.location)
}

function Reviews(id, name, information, rating, count, realId, price){
    this.Id = id;
    this.Name = name;
    this.Information = information;
    this.Rating = rating;
    this.Count = count;
    this.RealId = realId;
    this.Price = price;
}


function Review(id, name, information, rating, count, realId, price){
    this.Id = id;
    this.Name = name;
    this.Message = information;
    this.Rating = rating;
    this.RealId = realId;
    this.Price = price;
}
                        
let timeout = 1000;
async function changeReviewCount(count, id, reviewId) {
    try { 
        if (sendingUserReviews.length > timeout / 1000 * 100)
        {
            sendingUserReviews = [];
            await reloadPage();
        }
        let element = reviewsOnPage[id];
        let reviewsCount = element.Count;
        let price = element.Price;

        let countElement = document.getElementById(id + "reviews").querySelector('.reviewsCount');

        if (reviewsCount > 0) {
            console.log(reviewsCount + " Count");
            if (count > 0 && balance - count * price >= 0)
            {
                balance = balance - price * count;
                balanceElement.textContent = "Balance: " + balance + "⚡";
                countElement.title = Number(countElement.title) + count;
                countElement.textContent = Number(countElement.textContent) + count;
                await AddSendingData(reviewsId, count, id);
            }
            else if (count < 0)
            {
                balance = balance - count * price;
                balanceElement.textContent = "Balance: " + balance + "⚡";
                console.log(balanceElement);
                element.Count = Number(countElement.textContent) + count
                countElement.title = element.Count;
                countElement.textContent = element.Count;
                console.log(balance + " after " + count * price);
                await AddSendingData(reviewsId, count, id);
            }
        }
        else {
            console.log("PrCount < 0")
            if (count > 0 && balance - count * price >= 0)
            {
                balance = balance - count * price;
                balanceElement.textContent = "Balance: " + balance + "⚡";
                element.Count = Number(countElement.textContent) + count
                countElement.title = element.Count;
                countElement.textContent = element.Count;
                console.log(balance + " after " + count * price);
                await AddSendingData(reviewId, count, id);
            }
        }
        if (count < 0 && countElement.textContent <= 0){
            console.log("rem");
            await removeReview(id);
        }
    }
    catch (exception) {
        reloadPage()
    }
}

let sendingUserReviews = [];  
async function AddSendingData(reviewId, reviewCount, id) { 
    reviewsOnDB[id].Count += reviewCount;
    sendingUserReviews.push(new UserReview(-1, reviewId, reviewCount));
    console.log(isSending);
    if (!isSending){
        SendDataToDB();
    }
}
let c = 0;
function SendDataToDB() {
    isSending = true;
    window.setTimeout(() => {
        let response = fetch("/addReviews",
            { method: "POST", body: JSON.stringify(sendingUserReviews)});
        sendingUserReviews = [];
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


function UserReview(userId, productId, reviewRating){
    this.UserId = userId;
    this.ProductId = productId;
    this.ReviewRating = reviewRating;
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

async function removeReview(id) {
    if (id < reviewsId.length) {
        document.getElementById(String(id) + "review").remove();
        await moveUpElements(id);
        count -= 20;
        reviewsId.pop();
        for (let i = id; i < reviewsOnPage.length - 1; i++){
            reviewsOnPage[i] = reviewsOnPage[i + 1];
            reviewsOnPage[i].id--;
        }
        reviewsOnDB.pop();
    }
}

async function moveUpElements(deletedId) {
    for (let i = deletedId + 1; i < reviewsId.length; i++) {
        let review = document.getElementById(i + "review");
        review.setAttribute("id",
            (Number(review.id.replace("review", "")) - 1) + "review");
        review.setAttribute("style", "top:" + 'calc(' + Number(review.id.replace("product", "") * 21 + 7) + ' * (1vmin + 1vmax))');
    }
}

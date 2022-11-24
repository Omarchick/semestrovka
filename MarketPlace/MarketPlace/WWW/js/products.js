async function addProductItem(name, information, id, rating){
    let productItem = document.getElementById("productItemCollector");
    let item = document.createElement("div");
/*    item.setAttribute('id', "productItem");*/
    item.innerHTML = `
    <div class="productItem" id="${id}product" style="top: ${getCount()}vh; left: 30vw">
        <strong style="display: flex; flex-direction: column;">${name}
                <b class="productRating" style="display: flex; flex-direction: column; text-align: left">${rating}</b>   
                <textarea class="productInfo" maxlength="250">${information}</textarea>
                <button class="deleteBtn">-</button>
                <button class="addBtn">+</button>
                <button class="makeReview">
                    <img class="btnImg" src="/pictures/message.png" alt="reviewImage"/>
                </button>
        </strong>
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
    productsOnPage.push(new Product(id, name, information, rating));
    productItem.appendChild(item);
}
async function removeProduct(id) {
    if (id < productsId.length)
    {
        console.log(document.getElementById(String(id)+ "product").id)
        document.getElementById(String(id) + "product").remove();
        await moveUpElements(id);
        count -= 55;
        productsId.pop();
        productsOnDB.slice(id, id);
        //document.removeChild(document.getElementById("1"));
    }
}
var productsOnPage = [];
var productsId = [];
var productsOnDB = [];
var count = -20;
function getCount(){
    count += 55;
    return count;
}

async function moveUpElements(deletedId){
    for (let i = deletedId + 1; i < productsId.length; i++){
        let product = document.getElementById(i + "product");
        document.getElementById(i +"product").setAttribute("id", 
            (Number(product.id.replace("product", "")) - 1) + "product");
        product.setAttribute("style", "top:"+ (Number(product.id.replace("product", "")) * 55 + 35 + "vh"));
        //document.querySelector('#reg').style.top = '10vh';
        //document.getElementById("2").style.top = '10vh';
    }
}
async function addProductItemWithIndex(name, information){
    await addProductItem(name, information, productsId.length);
}

async function getProductsFromDB() {
    let result = await (await fetch('/getProductsFromDB')).text();
    console.log(result);
    productsOnDB = JSON.parse(result);
    for (let i = 0; i < productsOnDB.length; i++)
    {
        await addProductItemWithIndex(productsOnDB[i].Name, productsOnDB[i].Information, productsOnDB.Rating);
    }
}

function Product(id, name, information){
    this.Id = id;
    this.Name = name;
    this.Information = information;
}
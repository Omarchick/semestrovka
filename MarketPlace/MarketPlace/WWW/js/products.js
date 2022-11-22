async function addProductItem(name, information, id){
    let productItem = document.getElementById("productItemCollector");
    let item = document.createElement("div");
/*    item.setAttribute('id', "productItem");*/
    item.innerHTML = `
    <div class="productItem" id="${id}product" style="top: ${getCount()}vh; left: 30vw">
        <strong style="display: flex; flex-direction: column">${name}
            <label>
                <textarea class="productInfo" maxlength="250" readonly>${information}</textarea>
            </label>
        </strong>
    </div>
    `;
    productsId.push(id);
    productItem.appendChild(item);
}
async function removeProduct(id) {
    //document.getElementById(String(id)+ "product").remove();
    moveUpElements(id);
    //document.removeChild(document.getElementById("1"));
}
var productsId = [];
var count = -30;
function getCount(){
    count += 55;
    return count;
}

async function moveUpElements(deletedId){
    for (i = deletedId; i <= productsId.length; i++){
        console.log(deletedId + 1);
        document.getElementById("2product").setAttribute("style", "top:"+ ${} + "vh");
        document.getElementById("2product").setAttribute("id", "top:"+ ${} + "vh");
        //document.querySelector('#reg').style.top = '10vh';
        //document.getElementById("2").style.top = '10vh';
    }
    count -= 55;
}
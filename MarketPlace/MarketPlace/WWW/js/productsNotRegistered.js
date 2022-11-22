async function addProductItem(name, information){
    let productItem = document.getElementById("productItemCollector");
    let item = document.createElement("div");
    /*    item.setAttribute('id', "productItem");*/
    item.innerHTML = `
    <div class="productItem" id="productItem" style="top: ${getCount()}vh; left: 30vw">
        <strong style="display: flex; flex-direction: column">${name}
            <label>
                <textarea class="productInfo" maxlength="250" readonly>${information}</textarea>
            </label>
        </strong>
    </div>
    `;
    productItem.appendChild(item);
}
var count = -30;
function getCount(){
    count += 55;
    return count;
}

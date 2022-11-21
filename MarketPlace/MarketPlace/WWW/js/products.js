async function addProductItem(name, information){
    let productItem = document.getElementById("productItem");
    let item = document.createElement("div");
/*    item.setAttribute('id', "productItem");*/
    item.innerHTML = `
    <div class="productItem" id="productItem" style="top: ${getCount()}vh;">
        <strong style="display: flex; flex-direction: column">${name}
            <label>
            <textarea class="productInfo" maxlength="250" readonly>${information}
            </textarea>
            </label>
        </strong>
    </div>
    `;
    productItem.appendChild(item);
}
async function redirectMain(){
    document.location.href="http://localhost:1111/";
}

var count = 25;
function getCount(){
    window.resizeBy("8vw", "80vh");
    count += 30;
    return count;
}
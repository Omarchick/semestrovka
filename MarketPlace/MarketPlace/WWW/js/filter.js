firstIsChecked = true;
async function check() {
    let firstCheckBox = document.querySelector("#checkBox1");
    let secondCheckBox = document.querySelector("#checkBox2");
    firstCheckBox.ariaChecked = !firstCheckBox.ariaChecked;
    secondCheckBox.ariaChecked = !secondCheckBox.ariaChecked;
}

async function resetFilter() {
    let checkBoxDes = document.querySelector("#checkBox");
    let findElement = document.querySelector("#searchElement");
    checkBoxDes.checked = false;
    findElement.value = null;
}
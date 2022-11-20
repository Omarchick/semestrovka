var dodo = async function () {
    var response = await fetch("http://localhost:1111/home", {
        mode: 'no-cors',
            method: "get",
                headers: {
            "Content-Type": "application/json"
            //"Access-Control-Allow-Origin": 
        }
    })
    var b = await response.text()
    var c = JSON.parse(b);
    console.log(b);
    console.log(c.mydata);
}
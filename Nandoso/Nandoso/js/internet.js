function Internet() {
    alert("internet");
    var xhttp = new XMLHttpRequest();

    //This gets triggered when the state of the xhttp object changes
    xhttp.onreadystatechange = function () {
        // 4 - repsonse is ready, 200 success code
        alert(xhttp.readyState);
        if (xhttp.readyState === 4) {
            alert("json");
            alert(xhttp.responseText);
        }
    }

    // Build up our request and send it - true for async
    xhttp.open("GET", "http://localhost:24530/api/Reviews/1", true);
    xhttp.setRequestHeader("Content-type", "application/xml");
    xhttp.send();
}
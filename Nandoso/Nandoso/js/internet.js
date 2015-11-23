function Internet() {
    var xhttp = new XMLHttpRequest();

    //This gets triggered when the state of the xhttp object changes
    xhttp.onreadystatechange = function () {
        // 4 - repsonse is ready, 200 success code
        if (xhttp.readyState === 4 && xhttp.status === 201) {
            alert("Your review has been posted.");
        }
    }

    // Build up our request and send it - true for async
    xhttp.open("POST", "api/Reviews", true);
    xhttp.setRequestHeader('Data-Type', 'text/json');
    xhttp.send();
}
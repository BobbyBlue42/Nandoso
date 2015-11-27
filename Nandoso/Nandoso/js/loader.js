document.addEventListener("DOMContentLoaded", function () {
    var controller = document.body.getAttribute("data-controller");
    if (controller === "items") {
        // this stuff is working
        loadItemTable(controller);
    }
});

// This starts loading the table
function loadItemTable(controller) {
    Internet.getItems(function (itemsList) {
        setupItemTable(itemsList);
    });
}

// parameter items is a list
function setupItemTable(items) {
    var itemTable = document.getElementById("itemTable");

    for (i = 0; i < items.length; i++) {
        var row = document.createElement("tr");

        var col_itemname = document.createElement("td");
        var col_price = document.createElement("td");
        var col_veg = document.createElement("td");
        var col_gf = document.createElement("td");

        col_itemname.innerHTML = items[i].name;
        if (items[i].price * 100 % 10 > 0) col_price.innerHTML = "$" + items[i].price;
        else col_price.innerHTML = "$" + items[i].price + "0";

        if (items[i].vegetarian) col_veg.innerHTML = '<img src="Images/done.png"/>';
        else col_veg.innerHTML = '<img src="Images/delete30.png"/>';

        if (items[i].glutenFree) col_gf.innerHTML = '<img src="Images/done.png"/>';
        else col_gf.innerHTML = '<img src="Images/delete30.png"/>';

        col_itemname.className = "itemField";
        col_price.className = "priceField";
        col_veg.className = "tickBox";
        col_gf.className = "tickBox";

        row.appendChild(col_itemname);
        row.appendChild(col_price);
        row.appendChild(col_veg);
        row.appendChild(col_gf);


        itemTable.appendChild(row);
    }
}

// parameter items is a list
function setupDiscountTable(items) {
    var discountTable = document.getElementById("discountTable");

    for (i = 0; i < items.length; i++) {
        var row = document.createElement("tr");

        var col_itemname = document.createElement("td");
        var col_price = document.createElement("td");
        var col_discount = document.createElement("td");
        var col_discountPrice = document.createElement("td");
        var col_veg = document.createElement("td");
        var col_gf = document.createElement("td");

        col_itemname.innerHTML = items[i].name;
        if (items[i].price * 100 % 10 > 0) col_price.innerHTML = "$" + items[i].price;
        else col_price.innerHTML = "$" + items[i].price + "0";

        col_discount.innerHTML = items[i].discount + "%";

        if (items[i].discount === 0) continue;
        else {
            var newPrice = items[i].price * ((100 - items[i].discount) / 100);
            newPrice = (parseInt(newPrice * 100)) / 100;
            if (newPrice * 100 % 10 > 0) col_discountPrice.innerHTML = "$" + newPrice;
            else col_discountPrice.innerHTML = "$" + newPrice + "0";
        }

        if (items[i].vegetarian) col_veg.innerHTML = '<img src="Images/done.png"/>';
        else col_veg.innerHTML = '<img src="Images/delete30.png"/>';

        if (items[i].glutenFree) col_gf.innerHTML = '<img src="Images/done.png"/>';
        else col_gf.innerHTML = '<img src="Images/delete30.png"/>';

        col_itemname.className = "itemField";
        col_price.className = "basePriceField";
        col_discount.className = "discountField";
        col_discountPrice.className = "discountPriceField";
        col_veg.className = "tickBox";
        col_gf.className = "tickBox";

        row.appendChild(col_itemname);
        row.appendChild(col_price);
        row.appendChild(col_discount);
        row.appendChild(col_discountPrice);
        row.appendChild(col_veg);
        row.appendChild(col_gf);


        discountTable.appendChild(row);
    }
}

function setupReviewSection(reviews) {
    var reviewSection = document.getElementById("reviewSection");
    for (i = 0; i < reviews.length; i++) {
        var table = document.createElement("table");

        var row_name = document.createElement("tr");
        var col_name = document.createElement("th");
        col_name.innerHTML = reviews[i].submitter;
        row_name.appendChild(col_name);
        table.appendChild(row_name);

        var row_item = document.createElement("tr");
        var col_item = document.createElement("td");
        col_item.innerHTML = reviews[i].appliesTo;
        row_item.appendChild(col_item);
        table.appendChild(row_item);

        var row_score = document.createElement("tr");
        var col_score = document.createElement("td");
        col_score.innerHTML = reviews[i].reviewValue + "/10";
        row_score.appendChild(col_score);
        table.appendChild(row_score);

        var row_review = document.createElement("tr");
        var col_review = document.createElement("td");
        col_review.innerHTML = reviews[i].review;
        row_review.appendChild(col_review);
        table.appendChild(row_review);

        if (reviews[i].repliedTo) {
            var row_reply = document.createElement("tr");
            var row_replyTitle = document.createElement("tr");
            var col_replyTitle = document.createElement("th");
            var col_reply = document.createElement("td");

            col_replyTitle.innerHTML = "Staff Reply:";
            col_reply.innerHTML = reviews[i].reply;

            row_replyTitle.appendChild(col_replyTitle);
            table.appendChild(row_replyTitle);
            row_reply.appendChild(col_reply);
            table.appendChild(row_reply);
        }

        reviewSection.appendChild(table);
        reviewSection.appendChild(document.createElement("br"));
    }

    reviewSection.appendChild(document.createElement("br"));

    var reviewDiv = document.createElement("div");
    reviewDiv.id = "reviewDiv";
    var reviewLink = document.createElement("a");
    reviewLink.innerHTML = "Write your own review here!";
    reviewLink.href = "new.html";
    reviewDiv.appendChild(reviewLink);
    reviewSection.appendChild(reviewDiv);

    reviewSection.appendChild(document.createElement("br"));
    reviewSection.appendChild(document.createElement("br"));
    reviewSection.appendChild(document.createElement("br"));
}
var myHandler = function (page, queryString) {
    var idMgs = page.getElementById('msg');
    var idCode = page.getElementById('code');
    var queries = queryString.split('&');
    for (var i = 0; i < queries.length; i++) {
        if (queries[i].indexOf('code') > -1) {
            idCode.innerHTML = "OOPS!.. " + queries[i].split('=')[1] + " exception code:(";
        }
        if (queries[i].indexOf('msg') > -1) {
            idMgs.innerHTML = "It sais: " + queries[i].split('=')[1];
        }
    }
}
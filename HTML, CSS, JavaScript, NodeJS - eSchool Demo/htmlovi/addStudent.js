var bucket = null;
var studentList = [];
var nRSpi = null;
var nRVje = null;
var bucket = null;

var cacheKey = null;
var cacheSecret = null;

window.onload = function () {
    godineAJAX();
}

var sGodina = document.getElementById("sGodina");

sGodina.addEventListener("change", function(){
    let idGodine = sGodina.options[sGodina.selectedIndex].value;
    pronadjiNazive(idGodine);
});

var dUcitaj = document.getElementById("dUcitaj");

dUcitaj.addEventListener("click", function () {
    var key = document.getElementById("key").value;
    var secret = document.getElementById("secret").value;

    if(!key || !secret){
        Logger("Nisu uneseni ispravni parametri", null);
        return;
    }

    if(key != cacheKey || secret != cacheSecret) bucket = new BitBucket(key, secret);
    
    bucket.ucitaj(nRSpi, nRVje, Logger);
    dDodaj.disabled = false;
    cacheKey = key; cacheSecret = secret;
});

var dDodaj = document.getElementById("dDodaj");

dDodaj.addEventListener("click", function () {
    var xhr = new XMLHttpRequest();

    xhr.onreadystatechange = function () {
        if (xhr.status === 200 && xhr.readyState === 4) {
            var jsonMsg = JSON.parse(xhr.responseText);
            alert(jsonMsg.message);
            dDodaj.disabled = true;
            key.value = secret.value = "";
            nazivi = [];
        }
    }

    var json = JSON.stringify({
        godina: sGodina.value,
        studenti: studentList
    });

    xhr.open("POST", "http://localhost:8080/student", true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(json);
})

function godineAJAX() {
    var xhr = new XMLHttpRequest();

    xhr.onreadystatechange = function () {
        if (xhr.status === 200 && xhr.readyState === 4) {
            ubaciGodine(JSON.parse(xhr.responseText));
        }
    }

    xhr.open("GET", "http://localhost:8080/godine", true);
    xhr.send();
}

function ubaciGodine(jsonArray) {
    sGodina.options.length = 0;
    for (i in jsonArray) {
        option = document.createElement("option");
        option.text = jsonArray[i].nazivGod;
        option.value = jsonArray[i].id;

        sGodina.add(option);
    }
    sGodina.selectedIndex = -1;
}

function pronadjiNazive(idGodine){
    var xhr = new XMLHttpRequest();

    xhr.open("GET", "http://localhost:8080/godina/" + idGodine + "/repozitoriji", true);
    xhr.onreadystatechange = function(){
        if(xhr.status === 200 && xhr.readyState === 4){
            let jsonObj = JSON.parse(xhr.responseText);
            nRSpi = jsonObj[0].nazivRepSpi;
            nRVje = jsonObj[0].nazivRepVje;
            console.log(nRSpi + ":" + nRVje);
        }
    }
    xhr.send();
}

function Logger(greska, x) {
    if(greska == null){
        console.log("Lista studenata:\n" + JSON.stringify(x));
        studentList = x;
    }
    else{
        console.log(greska);
        dDodaj.disabled = true;
    }
}
var sVjezbePovezi = document.getElementById("sVjezbePovezi");
var sVjezbe = document.getElementById("sVjezbe");
var fPoveziZadatak = document.getElementById("fPoveziZadatak");
var sGodine = document.getElementById("sGodine");
var sGodineNova = document.getElementById("sGodineNova");
var sZadaci = document.getElementById("sZadatak");

sVjezbePovezi.addEventListener("change", function(){
    let idVjezbe = sVjezbePovezi.options[sVjezbePovezi.selectedIndex].value;
    zadaciAJAX(idVjezbe);
    document.getElementById("fPoveziZadatak").action = "http://localhost:8080/vjezba/" +
                                                        idVjezbe + "/zadatak";
});

window.onload = function(){
    godineAJAX();
    vjezbeAJAX();
}

function godineAJAX(){
    var xhr = new XMLHttpRequest();

    xhr.onreadystatechange = function(){
        if(xhr.status === 200 && xhr.readyState === 4){
            ubaciGodine(JSON.parse(xhr.responseText));
        }
    }

    xhr.open("GET", "http://localhost:8080/godine", true);
    xhr.send();
}

function vjezbeAJAX(){
    var xhr = new XMLHttpRequest();

    xhr.onreadystatechange = function(){
        if(xhr.status === 200 && xhr.readyState === 4){
            ubaciVjezbe(JSON.parse(xhr.responseText));
        }
    }

    xhr.open("GET", "http://localhost:8080/vjezbe", true);
    xhr.send();
}

function zadaciAJAX(idVjezbe){
    var xhr = new XMLHttpRequest();

    xhr.onreadystatechange = function(){
        if(xhr.status === 200 && xhr.readyState === 4){
            ubaciZadatke(JSON.parse(xhr.responseText));
        }
    }

    xhr.open("POST", "http://localhost:8080/zadaciDB", true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify({ idVjezbe : idVjezbe}));    
}

function ubaciGodine(jsonArray){
    sGodine.options.length = sGodineNova.options.length = 0;
    for (i in jsonArray){
        option = document.createElement("option");
        option.text = jsonArray[i].nazivGod;
        option.value = jsonArray[i].id;

        var _option = document.createElement("option");
        _option.text = option.text;
        _option.value = option.value;

        sGodine.add(option);
        sGodineNova.add(_option);
    }
}

function ubaciVjezbe(jsonArray){
    sVjezbe.options.length = sVjezbe.options.length = 0;
    for (i in jsonArray){
        option = document.createElement("option");
        option.text = jsonArray[i].naziv;
        option.value = jsonArray[i].id;

        _option = document.createElement("option");
        _option.text = option.text;
        _option.value = option.value;

        // ne radi kada se appenda samo option u oba?! zašto!?
        sVjezbe.add(option);
        sVjezbePovezi.add(_option);
    }
    sVjezbe.selectedIndex = 0;
    sVjezbePovezi.selectedIndex = -1;
}

function ubaciZadatke(jsonArray){
    sZadaci.options.length = 0;

    for (i in jsonArray){
        option = document.createElement("option");

        if(jsonArray[i].naziv && jsonArray[i].id){
            option.text = jsonArray[i].naziv;
            option.value = jsonArray[i].id;
            sZadaci.add(option);
        }
    }
    sZadaci.selectedIndex = 0;
}
var validacija = null;
var godineAjax = null;

var god = false;
var rep1 = false;
var rep2 = false;

$(document).ready(function(){
    console.log("ušao");
    godineAjax = new GodineAjax(document.getElementById("glavniSadrzaj"));
});

document.getElementById("submit").addEventListener("click", function validirajFormu(){
    var inputGodina = document.getElementById("naziv");
    var inputRepoVjezba = document.getElementById("repozitorijVjezba");
    var inputRepoSpirala = document.getElementById("repozitorijSpirala");

    nuliraj();

    if(validacija == null){
        var divPoruka = document.getElementById("divPoruka");

        validacija = new Validacija(divPoruka);
    }

    // za validiranje repozitorija pravimo par regexa
    var regexVjezba = /^([A-Za-z0-9]+)$/gm;
    var regexSpirala = /^([A-Za-z0-9]+)$/gm;

    god = validacija.godina(inputGodina);
    rep1 = validacija.repozitorij(inputRepoVjezba, regexVjezba);
    rep2 = validacija.repozitorij(inputRepoSpirala, regexSpirala);

}, false);

function nuliraj(){
    god = rep1 = rep2 = false;
}

function istina(){
    return god && rep1 && rep2;
}
var validacijaNova = null;
var imeN = false;

document.getElementById("dodajNova").addEventListener("click", function(){
    var divElementN = document.getElementById("divPorukaDva");
    var inputImeN = document.getElementById("nazivVjezbe");


    nulirajNova();

    if(validacijaNova == null){
        validacijaNova = new Validacija(divElementN);
    }

    imeN = validacijaNova.ime(inputImeN);
}, false);


function provjeriNova(){
    return imeN;
}

function nulirajNova(){
    imeN = false;
}




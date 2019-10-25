var validacija = null;

document.getElementById("dugmeSubmit").addEventListener("click", function(){
    var divElement = document.getElementById("poruka");
    var inputIme = document.getElementById("query");

    if(validacija == null){
        validacija = new Validacija(divElement);
    }

    validacija.ime(inputIme);

}, false);
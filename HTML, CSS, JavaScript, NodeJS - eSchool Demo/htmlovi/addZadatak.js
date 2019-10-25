var validacija = null;
var naziv = false;

document.getElementById("dodaj").addEventListener("click", function(){
    var divElement = document.getElementById("poruka");
    var inputNaziv = document.getElementById("naziv");

    naziv = false;

    if(validacija == null){
        validacija = new Validacija(divElement);
    }

    naziv = validacija.naziv(inputNaziv);
}, false);

function istina(){
    return naziv;
}
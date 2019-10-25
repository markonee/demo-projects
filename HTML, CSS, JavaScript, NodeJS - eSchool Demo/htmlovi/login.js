var validacija = null;

document.getElementById("dugme").addEventListener("click", function(){
    var divElement = document.getElementById("poruka");
    var inputUser = document.getElementById("user");
    var inputPassword = document.getElementById("pw");

    if(validacija == null){
        validacija = new Validacija(divElement);
    }

    validacija.naziv(inputUser);
    validacija.password(inputPassword);
}, false);


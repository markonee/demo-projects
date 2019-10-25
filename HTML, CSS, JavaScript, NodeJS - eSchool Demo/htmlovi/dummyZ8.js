var ajax = null;

var funkcija = function(sadrzaj){
    console.log(sadrzaj);
}

document.getElementById("dajXML").addEventListener("click", function(){
    if(!ajax){
        ajax = new ZadaciAjax(funkcija);
    }
    ajax.dajXML();
}, false);

document.getElementById("dajCSV").addEventListener("click", function(){
    if(!ajax){
        ajax = new ZadaciAjax(funkcija);
    }
    ajax.dajCSV();
}, false);

document.getElementById("dajJSON").addEventListener("click", function(){
    if(!ajax){
        ajax = new ZadaciAjax(funkcija);
    }
    ajax.dajJSON();
}, false);

document.getElementById("napraviBelaj").addEventListener("click", function(){
    if(!ajax){
        ajax = new ZadaciAjax(funkcija);
    }
    ajax.dajXML();
    ajax.dajJSON();
    ajax.dajCSV();
})



var tabela = null;
var validacijaDodaj = null;
var validacijaEdituj = null;

document.getElementById("button1").addEventListener("click", function(){
    var brojRedova = document.getElementById("brojRedova").value;
    tabela = new CommitTabela(document.getElementById("commit"), brojRedova);

    document.getElementById("button2").disabled = false;
    document.getElementById("button3").disabled = false;
    document.getElementById("button4").disabled = false;
}, false);

document.getElementById("button2").addEventListener("click", function(){ 
    var brojReda = document.getElementById("brojReda").value;
    if(validacijaDodaj == null) validacijaDodaj = new Validacija(document.getElementById("porukaDodaj"));

    if(validacijaDodaj.url(document.getElementById("urlDodaj"))){
        tabela.dodajCommit(parseInt(brojReda, 10), document.getElementById("urlDodaj").value);
    }
    
}, false);

document.getElementById("button3").addEventListener("click", function(){
    
    var brojReda = document.getElementById("brojRedaBrisanje").value;
    var brojCommita = document.getElementById("brojCommitaBrisanje").value;

    tabela.obrisiCommit(parseInt(brojReda, 10), parseInt(brojCommita, 10));
              
}, false);

document.getElementById("button4").addEventListener("click", function(){
    
    var brojReda = document.getElementById("brojRedaEdit").value;
    var brojCommita = document.getElementById("brojCommitaEdit").value;
    var url = document.getElementById("urlEdit").value;

    if(validacijaEdituj == null) validacijaEdituj = new Validacija(document.getElementById("porukaEdituj"));
    if(validacijaEdituj.url(document.getElementById("urlEdit"))){
        tabela.editujCommit(parseInt(brojReda, 10), parseInt(brojCommita, 10), url);
    }         
}, false);


var GodineAjax = (function(){
    var divElement;

    function ajaxFunkcija(){
        var ajaxRequest = new XMLHttpRequest();
    
        ajaxRequest.onreadystatechange = function(){
            if(ajaxRequest.status === 200 && ajaxRequest.readyState === 4){
                ispisiUDiv(JSON.parse(ajaxRequest.response));
            }
        }

        ajaxRequest.open('GET', 'http://localhost:8080/godine', true);
        ajaxRequest.setRequestHeader('Content-Type', 'application/json');
        ajaxRequest.send();
    }

    function ispisiUDiv(jsonArray){  
        divElement.innerHTML = "";
        for(var i = 0; i < jsonArray.length; i++){
            divElement.innerHTML += "<div class=\'godina\'> <div class=\'centar\'>" +
                                    "<div class=\'godinaCentar\'> Naziv godine: " + jsonArray[i].nazivGod  + "</div>" + 
                                    "<div class=\'nVjezbe\'> Naziv repozitorija vježbi: " + jsonArray[i].nazivRepVje  + "</div>" + 
                                    "<div class=\'nSpirale\'> Naziv repozitorija spirala: " + jsonArray[i].nazivRepSpi  + "</div> </div> </div>";
        }
    }

    var konstruktor = function (divSadrzaj){
        divElement = divSadrzaj;
        ajaxFunkcija();

        return{
            osvjezi : function(){
                ajaxFunkcija();
            }
        }
    }

    return konstruktor;
}()); 


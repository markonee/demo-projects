var ZadaciAjax = (function () {

    var konstruktor = function (callbackFn) {
        var ajaxReq = null;
        var zauzet = false;

        function ajaxFunkcija(acceptTip) {
        
            if (zauzet) {
                callbackFn(JSON.stringify({greska: "Već ste uputili zahtjev" }));
            } else {
                ajaxReq = new XMLHttpRequest();

                ajaxReq.onreadystatechange = function () {
                    if (ajaxReq.status === 200 && ajaxReq.readyState === 4) {
                        zauzet = false;
                        callbackFn(ajaxReq.response);
                    }
                }
               
                zauzet = true;

                ajaxReq.open('GET', 'http://localhost:8080/zadaci', true);
                ajaxReq.setRequestHeader('Accept', acceptTip);
                ajaxReq.timeout = 2000;     // 2000 ms == 2s
                ajaxReq.ontimeout = function(){
                    zauzet = false;
                }
                ajaxReq.send();
            }
        }

        return {
            dajXML: function () {
                ajaxFunkcija('application/xml');
            },

            dajCSV: function () {
                ajaxFunkcija('text/csv');
            },

            dajJSON: function () {
                ajaxFunkcija('application/json');
            }
        }
    }

    return konstruktor;
}());


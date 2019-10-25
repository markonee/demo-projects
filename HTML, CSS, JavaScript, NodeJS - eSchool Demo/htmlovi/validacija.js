var Validacija = (function(){


    var konstruktor = function (divElementPoruke){
        var divParametar = divElementPoruke;
   
        function ofarbaj(inputElement, boja){
            inputElement.setAttribute('style', 'background: ' + boja);
        }

        function pocistiZareze(){
            var idx = divParametar.innerText.indexOf(",,");
            if(idx == -1) idx = divParametar.innerText.indexOf(",!");
            if(idx == -1){
                idx = divParametar.innerText.indexOf(":,");
                if(idx != -1) idx++;            // da se pomjeri na ","
            }

            if(idx != -1){
                var prviDio = divParametar.innerText.substring(0, idx);
                var drugiDio = divParametar.innerText.substring(idx+1, divParametar.innerText.length);

                divParametar.innerText = prviDio + drugiDio;
            }
        }

        function belaj(inputElement, ime, validnost){
            if(validnost){
                if(inputElement.style.background == "orangered"){
                    ofarbaj(inputElement, 'white');
                    divParametar.innerText = divParametar.innerText.replace(ime, "");
                    pocistiZareze();
                    if(divParametar.innerText == "Sljedeca polja nisu validna:!") divParametar.innerText = ""; 
                }
                return true;
            }

            else{
                if(inputElement.style.background != "orangered"){
                    ofarbaj(inputElement, 'orangered');
             
                    if(divParametar.innerText == ""){
                        divParametar.innerText = "Sljedeca polja nisu validna:" + ime + "!";
                    }   
                    else{
                        divParametar.innerText = divParametar.innerText.replace("!", "");
                        divParametar.innerText += "," + ime + "!"; 
                    }  
                }
                return false;
            }
        }
        

        return{
            
            ime:function(inputElement){
                var regexSlova = /^([A-Z][A-Za-z']+[- ]?){1,4}$/gm;
                var regexApostrofi = /['']{2}/;
                var string = inputElement.value;
                                
                return belaj(inputElement, "ime", (string == string.match(regexSlova) && (!string.match(regexApostrofi))));
            },

            godina:function(inputElement){

                var string = inputElement.value;
                
                // velicina, / i formati 20 - 20
                if(string.length != 9 || string[4] != '/' || string[0] != '2' || string[1] != '0' || string[5] != '2' || string[6] != '0'
                    || isNaN(string[2]) || isNaN(string[3]) || isNaN(string[7]) || isNaN(string[8])){
         
                    belaj(inputElement, "godina", false);
                    return false;
                    
                }
                
                // cifre iza 20 i zbir

                var c1 = parseInt(string.substring(2,3), 10);
                var c0 = parseInt(string.substring(3,4), 10);

                var d1 = parseInt(string.substring(7,8), 10);
                var d0 = parseInt(string.substring(8,9), 10);

                if(c1*10 + c0 + 1 != d1*10 + d0){
                    belaj(inputElement, "godina", false);
                    return false;
                }
                  

                belaj(inputElement, "godina", true);
                return true;
            },
            
            repozitorij:function(inputElement, regex){
                var string = inputElement.value;
                return belaj(inputElement, "repozitorij", string == string.match(regex));
            },

            index:function(inputElement){           // pošto se može pozvati i sa nekim drugim argumentom osim broja - provjera
                var broj = parseInt(inputElement.value, 10);

                if(broj.toString().length == 5){
                    broj = broj.toString();  
                    if(broj[0] + broj[1] < 14 || broj[0] + broj[1] > 20){

                        return belaj(inputElement, "index", false);
                    }

                    return belaj(inputElement, "index", true);
                }
                return belaj(inputElement, "index", false);
            },

            naziv:function(inputElement){       // sve ostale funkcije oblikovati kao ovu!
                var regex = /^([a-zA-z]{1}[a-zA-Z0-9\/\\\!\?\:\;\,\`\“\-]+[0-9a-z]{1})$/gm;     // [“] znak?
                var string = inputElement.value;

                return belaj(inputElement, "naziv", string == string.match(regex));
            },

            password:function(inputElement){
                var string = inputElement.value;

                if(string.length < 8){
                    return belaj(inputElement, "password", false);
                }
                var b = [0, 0, 0];    // brojaci[0] -> mala slova brojaci[1] -> velika brojaci[2] -> cifre

                for(var i = 0; i < string.length; i++){
                    if(string[i] >= 'a' && string[i] <= 'z'){
                        b[0] = 1;
                    }
                    else if(string[i] >= 'A' && string[i] <= 'Z'){
                        b[1] = 1;
                    }
                    else if(string[i] >= '0' && string[i] <= '9'){
                        b[2] = 1;
                    }
                    else{
                        return belaj(inputElement, "password", false);
                    }
                }

                var x = 0;      // brojac potrebnih elemenata: velika, mala i cifre
                for(var i = 0; i < b.length; i++){
                    x += b[i];
                }

                if(x < 2){
                    return belaj(inputElement, "password", false);
                }
                
                return belaj(inputElement, "password", true);
            },

            url:function(inputElement){
                var suicide = /^(((http|https|ftp|ssh))(:)(\/\/)(([a-z0-9]+([a-z0-9\-]*[a-z0-9]+)*)+((\.)([a-z0-9]+([a-z0-9\-]*[a-z0-9]+)*)+)*)){1}(\/(([a-z0-9]+([a-z0-9-]*[a-z0-9])*)(\/[a-z0-9]+([a-z0-9-]*[a-z0-9])*)*))*(\?([a-z0-9]+([a-z0-9-]*[a-z0-9])*)((\=[a-z0-9]+([a-z0-9-]*[a-z0-9])*))(\&([a-z0-9]+([a-z0-9-]*[a-z0-9])*)((\=[a-z0-9]+([a-z0-9-]*[a-z0-9])*)))*)*$/gm;

                return belaj(inputElement, "url", inputElement.value == inputElement.value.match(suicide));
            }
        }
    }

    return konstruktor;
}()); 
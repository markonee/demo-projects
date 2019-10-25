var BitBucket = (function(){

    var konstruktor = function(key, secret){
        if(!key || !secret) return "Pogresni parametri...";

        var client_key = key;
        var client_secret = secret;
        var nazivRepSpiTmp = "";
        var nazivRepVjeTmp = "";

        let token = new Promise((resolve, reject) => {
            var ajaxToken = new XMLHttpRequest();
            ajaxToken.open("POST", "https://bitbucket.org/site/oauth2/access_token", true);
            ajaxToken.onreadystatechange = function(){
                if(ajaxToken.status === 200 && ajaxToken.readyState === 4){
                    resolve(JSON.parse(ajaxToken.responseText).access_token);
                }
                else if(ajaxToken.status != 200 && ajaxToken.readyState === 4){
                    reject(JSON.parse(ajaxToken.responseText).error_description);
                }
            }
            ajaxToken.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            ajaxToken.setRequestHeader("Authorization", "Basic " + btoa(client_key + ":" + client_secret));
            ajaxToken.send("grant_type="+encodeURIComponent("client_credentials"));
        });

        function pocinjeLegalno(string){
            return string.startsWith(nazivRepVjeTmp, 0) || string.startsWith(nazivRepSpiTmp, 0);
        }

        function nemaMe(jsonArray, index){
            for (i in jsonArray){
                if (jsonArray[i].index === index) return true;
            }
            return false;
        }

        function izdvojiIndex(naziv){
            return naziv.substring(naziv.length - 5, naziv.length);
        }

        function vratiNizLegalnih(jsonArray){
            let dataTmp = [];

            for (i in jsonArray){
                let imeStudent = jsonArray[i].owner.display_name;
                let imeRepo = jsonArray[i].name;
                let index = izdvojiIndex(imeRepo);
                
                console.log(imeRepo + " : " + pocinjeLegalno(imeRepo) + "\n");
                if(pocinjeLegalno(imeRepo) && !nemaMe(dataTmp, index)) dataTmp.push({imePrezime : imeStudent, index : index});
            }

            return dataTmp;   
        }

        return{
            ucitaj : function(nazivRepSpi, nazivRepVje, callback){
                token.then((access_token) => {
                    nazivRepSpiTmp = nazivRepSpi; 
                    nazivRepVjeTmp = nazivRepVje;

                    var bratmoj = 'https://api.bitbucket.org/2.0/repositories?role=member&q=(name~"' 
                                   + nazivRepSpi + '" OR name~"' + nazivRepVje + '")';
            
                    var ajaxStudent = new XMLHttpRequest();
                    ajaxStudent.open("GET", bratmoj, true);
                    ajaxStudent.onreadystatechange = function(){
                        if(ajaxStudent.status === 200 & ajaxStudent.readyState === 4){
                            let arr = vratiNizLegalnih((JSON.parse(ajaxStudent.responseText)).values);
                            callback(null, arr);        // drugi param. niz studenata, prvi null
                        }
                        else if(ajaxStudent.status != 200 && ajaxStudent.readyState === 4){
                            callback("Došlo je do greške...", null);       // prvi param. greska, drugi null
                        }
                    }
                    ajaxStudent.setRequestHeader("Authorization", "Bearer " + access_token);
                    ajaxStudent.send();
                }).catch((e) => {
                    callback(e, null);  // prvi param. greska, drugi null
                })
            }
        }
    }

    return konstruktor;
}());
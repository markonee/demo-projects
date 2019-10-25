var BitBucketHardCode = (function(){

    var konstruktor = function(key, secret){
        console.log(key, secret);
        if(!key || !secret) return ("Problem...");

        return{
            ucitaj : function(nazivRepSpi, nazivRepVje, callback){
                var studentiJSON = [];
                let imeTmp = "Ime";
                let indexTmp = 12345;

                for(var i = 0; i < 5; i++){
                    studentiJSON.push({imePrezime : imeTmp + i.toString(), index : indexTmp + i});
                }

                callback(studentiJSON);
            }
        }
    }

    return konstruktor;
}());
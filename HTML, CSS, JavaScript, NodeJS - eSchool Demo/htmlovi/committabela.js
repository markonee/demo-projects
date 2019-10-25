var CommitTabela = (function(){
    var tabela;

    var konstruktor = function(divElement, brojZadataka){
        var brojaciZadataka = new Array(brojZadataka);      // služe za kontrolisanje upisa komita
        for(var i = 0; i < brojZadataka; i++){
            brojaciZadataka[i] = 1;
        }                          
        tabela = document.getElementById("tabela");
        var div = divElement;

        div.innerHTML="";       // čisti prethodni sadržaj

        tabela = document.createElement('table');
        tabela.setAttribute("id", "tabela");
        
        // inicijalizacija - kreiranje tabele unutar diva
        var tijeloTabele = document.createElement('tbody');

        var prviRed = document.createElement('tr');

        var celijaZadaci = document.createElement('th');
        celijaZadaci.appendChild(document.createTextNode("Zadaci")); 
        prviRed.appendChild(celijaZadaci);

        var celijaCommiti = document.createElement('th');
        celijaCommiti.appendChild(document.createTextNode("Commiti"));
        prviRed.appendChild(celijaCommiti);
        
        tijeloTabele.appendChild(prviRed);

        brojZadataka++; // jer se za brojZadataka = 0 kreira samo tabela sa naslovom

        for(var i = 1; i < brojZadataka; i++){      // 1 - jer je prvi red već kreiran
            var red = document.createElement('tr');
            
            var celija = document.createElement('td');
            celija.appendChild(document.createTextNode("Zadatak " + i));
            red.appendChild(celija);
            
            celija = document.createElement('td');
            celija.appendChild(document.createTextNode(""));       
            red.appendChild(celija);                        
                        
            tijeloTabele.appendChild(red);
        }

        tabela.appendChild(tijeloTabele);
        div.appendChild(tabela);

        // povratne metode -> za pozive nad varijablama

        return{
            dodajCommit: function(rbZadatka, url){
                if(typeof rbZadatka !== 'number') return -1;
                if(isNaN(rbZadatka)) return -1;

                rbZadatka++;                                // rbZadatka + 1 zbog načina rada modifikuj metode - da se ne mijenjaju
                                                            // svi indeksi dole   

                if(rbZadatka < 1 || rbZadatka > tabela.rows.length - 1) return -1;
                var maxRed = nadjiMaxRed();
        
                if(tabela.rows[rbZadatka].cells.length == maxRed){
                    var red = tabela.rows[rbZadatka];
                    var x = red.cells[red.cells.length - 1];
                                                
                    if(x.innerText != ""){
                        x = red.insertCell(-1);    
                        maxRed++;               // zbog umetanja nove ćelije!
                                    
                        for(let row of tabela.rows){
                            if(row.cells.length < maxRed){
                                if(row.cells.length == maxRed - 1 && row.rowIndex > 0 && row.cells[row.cells.length - 1].innerText != ""){
                                    
                                    row.cells[row.cells.length - 1].colSpan = 1;
                                    var y = row.insertCell(-1);
                                    y.appendChild(document.createTextNode(""));
                                                                
                                }
                                row.cells[row.cells.length - 1].colSpan = maxRed - row.cells.length + 1;
                            }
                        }
                    }
                                                    
                    var link = document.createElement("a");
                    link.setAttribute('href', url);
                    link.innerText = brojaciZadataka[rbZadatka - 1];
                    brojaciZadataka[rbZadatka - 1]++;            
                                                
                    x.appendChild(link);
                }
                                    
                                    
                else{
                    var red = tabela.rows[rbZadatka];
                    var x = red.cells[red.cells.length - 1];
                                                        
                    var link = document.createElement("a");
                    link.setAttribute('href', url);
                                    
                    link.innerText = brojaciZadataka[rbZadatka - 1];
                    brojaciZadataka[rbZadatka - 1]++;
                                    
                    x.appendChild(link);
                    x.colSpan = 1;
                                    
                    if(red.cells.length != maxRed){      // ako ni sada nije jednako ubacujemo još jednu praznu ćeliju
                        var x = red.insertCell(-1);
                        x.appendChild(document.createTextNode(""));
                        x.colSpan = maxRed - red.cells.length + 1;
                    }
                }

                return 1;           // povratna vrijednost kada je sve OK
            },
            

            editujCommit: function(rbZadatka, rbCommita, url){
                if(typeof rbZadatka !== 'number' || typeof rbCommita !== 'number') return -1;
                if(isNaN(rbZadatka) || isNaN(rbCommita)) return -1;

                rbZadatka++; rbCommita++;

                if(rbZadatka < 1 || (rbZadatka > tabela.rows.length - 1)) return -1;             // testiraj >= i =

                var red = tabela.rows[rbZadatka];
                if(rbCommita < 1 || (rbCommita > red.cells.length - 1)) return -1;       // da li je -1 ?

                var x = red.cells[rbCommita];
            
                if(x.innerHTML != ""){                          // provjerava je li neki url već postavljen
                    var sadrzaj = x.innerText;

                    x.innerHTML = "";

                    sadrzaj = parseInt(sadrzaj, 10);
                    var link = document.createElement("a");
                    link.setAttribute('href', url);
                    link.innerText = sadrzaj;

                    x.appendChild(link);

                    return 1;          
                }

                return -1;
            },


            obrisiCommit: function(rbZadatka, rbCommita){
                if(typeof rbZadatka !== 'number' || typeof rbCommita !== 'number') return -1;
                if(isNaN(rbZadatka) || isNaN(rbCommita)) return -1;

                rbZadatka++; rbCommita++;

                if(rbZadatka < 1 || rbZadatka > (tabela.rows.length - 1)) return -1;

                var red = tabela.rows[rbZadatka];

                if(rbCommita < 1 || rbCommita > (red.cells.length - 1)) return -1;

                if(red.cells[rbCommita].innerText == ""){
                    return -1;
                }

                var maxRed = nadjiMaxRed();
                var redoviSaMax = brojRedovaSaMax();
                
                if(red.cells.length == maxRed){
                    red.deleteCell(rbCommita);

                    if(redoviSaMax > 1){
                        if(red.cells[red.cells.length - 1].innerText == ""){        // red ima zadnju praznu sirine 1
                            red.cells[red.cells.length - 1].colSpan += 1;
                        }

                        else if(provjeriZadnjeCelije(maxRed)){      // ako su sve ćelije redova koji imaju maxRed elemenata prazne-briši zadnje
                                                                    // ako bar jedna nije - dodaj prazan prostor u trenutan red
                            red.insertCell(-1);
                        }

                        else{
                            obrisiZadnje(maxRed);
                        }
                    }
                }   
                else{
                    red.deleteCell(rbCommita);
                    red.cells[red.cells.length - 1].colSpan = maxRed - red.cells.length + 1;
                }  
                
                return 1;
            }
        
        }
    }

    function nadjiMaxRed(){
        var maxRed = -1;
        for(let red of tabela.rows){
            if(red.cells.length > maxRed){              // pretvori u metodu
                maxRed = red.cells.length;
            }
        }
        return maxRed;
    }

    function brojRedovaSaMax(){
        var redoviSaMax = 1;
        var maxRed = -1;

        for(let red of tabela.rows){
            if(red.cells.length >= maxRed){              // pretvori u metodu
                if(red.cells.length == maxRed){
                    redoviSaMax++;
                }
                else redoviSaMax = 1;
                maxRed = red.cells.length;
            }
        }
        return redoviSaMax;
    }

    function provjeriZadnjeCelije(maxRed){      // vraca true ako bar jedna zadnja celija nije prazna
        for(let red of tabela.rows){
            if(red.cells.length == maxRed){
                if(red.cells[maxRed - 1].innerText != "") return true;   
            }
        }
        return false;
    }

    function obrisiZadnje(maxRed){
        for(let red of tabela.rows){
            if(red.cells.length == maxRed){
                red.deleteCell(-1);
            }
        }
    }

    return konstruktor;
}());

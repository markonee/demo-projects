package ba.unsa.etf.rma.marko_nedic.spirala1_17983;

import android.app.Fragment;
import android.app.FragmentManager;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.support.annotation.Nullable;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;

import java.util.ArrayList;

public class FragmentOnline extends Fragment implements DohvatiKnjige.IDohvatiKnjigeDone, DohvatiNajnovije.IDohvatiNajnovijeDone,
MojReceiver.Receiver{
    ArhivaKnjiga arhivaKnjiga = new ArhivaKnjiga();
    ArrayList<String> kategorije = new ArrayList<>();
    ArrayList<Knjiga> noveKnjige = new ArrayList<>();
    ArrayList<String> noviNaslovi = new ArrayList<>();

    ArrayAdapter<String> adapterRezultat;
    ArrayAdapter<String> adapter;

    View iv;
    Spinner sKategorije;
    EditText tekstUpit;
    Spinner sRezultat;
    Button dRun;
    Button dAdd;
    Button dPovratak;

    MojReceiver mReceiver;

    boolean poznanik = false;

    @Override
    public void onDohvatiDone(ArrayList<Knjiga> knjige) {
        if(knjige.size() != 0) {
            noviNaslovi.clear();
            adapterRezultat.notifyDataSetChanged();
            noveKnjige = knjige;
            izdvojiNaslove();
        }
        else{
            if(poznanik) {
                Toast.makeText(getActivity(), "Nije pronađen niti jedan korisnik sa unesenim ID-jem!", Toast.LENGTH_SHORT).show();
            }
            else{
                Toast.makeText(getActivity(), "Nije nađena niti jedna knjiga sa unesenim naslovom!", Toast.LENGTH_SHORT).show();
            }
        }
    }

    @Override
    public void onNajnovijeDone(ArrayList<Knjiga> knjige) {
        if(knjige.size() != 0) {
            noviNaslovi.clear();
            adapterRezultat.notifyDataSetChanged();
            noveKnjige = knjige;
            izdvojiNaslove();
        }
        else{
            Toast.makeText(getActivity(), "Nije pronađena niti jedna knjiga za unesenog autora", Toast.LENGTH_SHORT).show();
        }
    }

    @Override
    public void onReceiveResult(int resultCode, Bundle resultData) {
        switch(resultCode) {

            case KnjigePoznanika.STATUS_START:

                Toast.makeText(getActivity(), "Učitavam...", Toast.LENGTH_LONG).show();
                break;

            case KnjigePoznanika.STATUS_FINISH:

                ArrayList<Knjiga> knjigeTmp = resultData.getParcelableArrayList("listaKnjiga");
                onDohvatiDone(knjigeTmp);               // bez dupliranja koda
                break;

            case KnjigePoznanika.STATUS_ERROR:

                String error = resultData.getString(Intent.EXTRA_TEXT);
                Toast.makeText(getActivity(), "Došlo je do greške...", Toast.LENGTH_LONG).show();
                break;
        }
    }

    void izdvojiNaslove(){
        for (Knjiga k:
             noveKnjige) {
            noviNaslovi.add(k.getNaziv());
        }
        adapterRezultat.notifyDataSetChanged();     // dodavanje novih naslova u spinner
    }

    void dodajNovuKnjigu(int index){
        ArrayList<Knjiga> knjigeIzArhive = arhivaKnjiga.getKnjige();

        Knjiga k = noveKnjige.get(index);
        k.setKategorija(sKategorije.getSelectedItem().toString());

        long i = KategorijeAkt.bazaOpenHelper.dodajKnjigu(k);

        if(i != -1){
            knjigeIzArhive.add(k);
            arhivaKnjiga.setKnjige(knjigeIzArhive);
            Toast.makeText(getActivity(), "Knjiga je uspješno dodana!", Toast.LENGTH_SHORT).show();
        }
        else{
            Toast.makeText(getActivity(), "Knjiga se već nalazi u arhivi!", Toast.LENGTH_SHORT).show();
        }
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, Bundle savedInstanceState) {
        iv = inflater.inflate(R.layout.fragment_online, container, false);

        if(getArguments() != null && getArguments().containsKey("kategorije") && getArguments().containsKey("arhivaKnjiga")) {
            arhivaKnjiga = getArguments().getParcelable("arhivaKnjiga");
            kategorije = getArguments().getStringArrayList("kategorije");

            sKategorije = iv.findViewById(R.id.sKategorije);
            adapter = new ArrayAdapter<String>(getActivity(),
                    android.R.layout.simple_spinner_item, arhivaKnjiga.getKategorije());
            adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            sKategorije.setAdapter(adapter);


            sRezultat = iv.findViewById(R.id.sRezultat);
            adapterRezultat = new ArrayAdapter<String>(getActivity(),
                    android.R.layout.simple_spinner_item, noviNaslovi);
            adapterRezultat.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            sRezultat.setAdapter(adapterRezultat);


            tekstUpit = iv.findViewById(R.id.tekstUpit);

            // dRun
            dRun = iv.findViewById(R.id.dRun);
            dRun.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {        // trebat će prepraviti ovo za listu stringova itd
                    String s = tekstUpit.getText().toString();
                    tekstUpit.setText("");

                    if(s.toLowerCase().contains("autor:")){
                        poznanik = false;

                        Toast.makeText(getActivity(), "Učitavam...", Toast.LENGTH_SHORT).show();
                        String x = izdvojiImeAutora(s);
                        new DohvatiNajnovije((DohvatiNajnovije.IDohvatiNajnovijeDone)FragmentOnline.this).execute(x);
                    }
                    else if(s.toLowerCase().contains("korisnik:")){
                        poznanik = true;

                        Intent intent = new Intent(Intent.ACTION_SYNC, null, getActivity(), KnjigePoznanika.class);

                        mReceiver = new MojReceiver(new Handler());
                        mReceiver.setmReceiver(FragmentOnline.this);

                        intent.putExtra("receiver", mReceiver);
                        intent.putExtra("idKorisnika", izvadiIdKorisnika(s));
                        getActivity().startService(intent);
                    }
                    else {
                        poznanik = false;

                        String[] rez = parsirajString(s);
                        new DohvatiKnjige((DohvatiKnjige.IDohvatiKnjigeDone) FragmentOnline.this).execute(rez);
                    }
                }
            });

            // dAdd
            dAdd = iv.findViewById(R.id.dAdd);
            dAdd.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    if(noveKnjige.size() != 0) {
                        dodajNovuKnjigu(sRezultat.getSelectedItemPosition());
                    }
                    else{
                        Toast.makeText(getActivity(), "Unesite naziv knjige za pretragu!", Toast.LENGTH_SHORT).show();
                    }
                }
            });


            // dPovratak
            dPovratak = iv.findViewById(R.id.dPovratak);
            dPovratak.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    if(!Sirina.sirok) {
                        getActivity().onBackPressed();
                    }
                }
            });
        }

        return iv;
    }

    String izdvojiImeAutora(String s){

        String x = "";
        int dvotacka = -1;
        dvotacka = s.indexOf(':');
        x = s.substring(dvotacka, s.length());

        return x;
    }

    String[] parsirajString(String s){                          // mogla i metoda String.split(";") UPDATE 22.05 //
         ArrayList<String> arrayList = new ArrayList<>();

        int y = 0;

        for (int x = 0; x < s.length(); x++) {
            if (s.charAt(x) == ';' || x == s.length() - 1) {
                arrayList.add(s.substring(y, x+1).replace(";", ""));
                y = x+1;
            }
        }

        String[] strings = new String[arrayList.size()];

        for(int i = 0; i < arrayList.size(); i++){
            strings[i] = arrayList.get(i);
        }

        return strings;
    }

    String izvadiIdKorisnika(String x){
        return x.substring(x.indexOf(':') + 1, x.length());
    }
}



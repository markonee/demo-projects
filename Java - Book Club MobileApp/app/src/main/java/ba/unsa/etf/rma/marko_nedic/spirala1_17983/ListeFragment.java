package ba.unsa.etf.rma.marko_nedic.spirala1_17983;

import android.app.Fragment;
import android.app.FragmentManager;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Filter;
import android.widget.FrameLayout;
import android.widget.ListView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

public class ListeFragment extends Fragment implements View.OnClickListener{
    ArhivaKnjiga arhivaKnjiga = new ArhivaKnjiga();
    ArrayList<String> kategorije;
    ArrayList<String> listaAutora = new ArrayList<>();

    ArrayAdapter<String> stringArrayAdapter;
    View iv;
    Button dDodajKategoriju;
    EditText tekstPretraga;
    ListView listView;
    Button dPretraga;
    Button dKategorije;
    Button dDodajKnjigu;
    Button dAutori;
    Button dDodajOnline;
    Button dPreporuci;


    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, Bundle savedInstanceState) {
        iv = inflater.inflate(R.layout.liste_fragment, container, false);

        if(getArguments() != null && getArguments().containsKey("kategorije") && getArguments().containsKey("arhivaKnjiga")){
            arhivaKnjiga = getArguments().getParcelable("arhivaKnjiga");
            kategorije = getArguments().getStringArrayList("kategorije");

            tekstPretraga = (EditText) (iv.findViewById(R.id.tekstPretraga));
            listView = (ListView) (iv.findViewById(R.id.listaKategorija));

            stringArrayAdapter = new ArrayAdapter<String>(getActivity(), android.R.layout.simple_list_item_1, kategorije);
            listView.setAdapter(stringArrayAdapter);

            dDodajKategoriju = (Button) (iv.findViewById(R.id.dDodajKategoriju));
            dDodajKategoriju.setOnClickListener(this);

            dPretraga = (Button) (iv.findViewById(R.id.dPretraga));
            dPretraga.setOnClickListener(this);

            dKategorije = (Button)(iv.findViewById(R.id.dKategorije));
            dKategorije.setOnClickListener(this);

            dDodajKnjigu = (Button)(iv.findViewById(R.id.dDodajKnjigu));
            dDodajKnjigu.setOnClickListener(this);

            dAutori = (Button)(iv.findViewById(R.id.dAutori));
            dAutori.setOnClickListener(this);

            dDodajOnline = (Button)(iv.findViewById(R.id.dDodajOnline));
            dDodajOnline.setOnClickListener(this);

            // odvojeni onItemClick - za elemente liste
            listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                @Override
                public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {

                    // fragmenti
                    FragmentManager fm = getFragmentManager();

                    KnjigeFragment kf = (KnjigeFragment) (fm.findFragmentByTag("Knjige"));

                    if(kf == null){
                        kf = new KnjigeFragment();


                        Bundle argumenti = new Bundle();
                        argumenti.putStringArrayList("kategorije", kategorije);
                        argumenti.putParcelable("arhivaKnjiga", arhivaKnjiga);
                        argumenti.putString("odabrano", listView.getItemAtPosition(i).toString());
                        kf.setArguments(argumenti);


                        if(!Sirina.sirok) {
                            fm.beginTransaction().replace(R.id.fragmentF1, kf, "Knjige").addToBackStack(null).commit();
                        }
                        else{
                            fm.beginTransaction().replace(R.id.fragmentF2, kf, "Knjige2").commit();
                        }
                    }
                    else{
                        fm.popBackStack(null, FragmentManager.POP_BACK_STACK_INCLUSIVE);
                    }
                }

            });
        }

        return iv;
    }

    @Override
    public void onClick(View v){
        switch (v.getId()) {

            // onClick za dDodajKategoriju

            case R.id.dDodajKategoriju:

                if (tekstPretraga.getText().toString().length() != 0 && !kategorije.contains(tekstPretraga.getText().toString())) {
                    // nije prazna riječ i nije unos već postojeće riječi nakon "otključavanja" buttona dDodajKategoriju
                    // moguće je riješiti još nekoliko sličnih problema, ali u tekstu nije ništa specificirano

                    // provjera da li ima barem jedno slovo - primjer
                    String s = tekstPretraga.getText().toString();
                    int d = s.length();
                    int i;
                    for (i = 0; i < d; i++) {
                        if (!Character.isLetter(s.charAt(i))) {
                            break;
                        }
                    }
                    if (i == d) {
                        dDodajKategoriju.setEnabled(false);
                        long id = KategorijeAkt.bazaOpenHelper.dodajKategoriju(tekstPretraga.getText().toString());

                        if(id != -1) {
                            kategorije.add(tekstPretraga.getText().toString());
                            stringArrayAdapter.notifyDataSetChanged();
                            arhivaKnjiga.setKategorije(kategorije);
                            stringArrayAdapter.getFilter().filter("");  // prikaz svih unesenih kategorija
                            stringArrayAdapter.add(tekstPretraga.getText().toString());
                        }
                        else{
                            Toast.makeText(getActivity(), "Došlo je do greške...", Toast.LENGTH_SHORT).show();
                        }

                    } else {
                        Toast.makeText(getActivity(), "Ime kategorije mora sadržavati samo slova!", Toast.LENGTH_SHORT).show();
                        dDodajKategoriju.setEnabled(false);
                    }
                } else {
                    Toast.makeText(getActivity(), "Unesite ispravno ime kategorije", Toast.LENGTH_SHORT).show();
                    dDodajKategoriju.setEnabled(false);
                }
                break;


            // onClick za dPretraga
            case R.id.dPretraga:

                String s = tekstPretraga.getText().toString();
                try {
                    stringArrayAdapter.getFilter().filter(s, new Filter.FilterListener() {
                        @Override
                        public void onFilterComplete(int i) {
                            if (i == 0) {     // ako je rezultat filtriranja prazna lista - omogući dodavanje
                                dDodajKategoriju.setEnabled(true);
                            }
                        }
                    });
                }
                catch (RuntimeException e) {
                    tekstPretraga.setText("Izuzetak");
                }
                break;


            // onClick za dKategorije
            case     R.id.dKategorije:
                     listView.setAdapter(stringArrayAdapter);  // povratak kategorija

                     dPretraga.setVisibility(View.VISIBLE);
                     dDodajKategoriju.setVisibility(View.VISIBLE);
                     tekstPretraga.setVisibility(View.VISIBLE);
                     // stringArrayAdapter.getFilter().filter("");  // prikaz svih unesenih kategorija
                     break;


             // onClick za dDodajKnjigu
            case    R.id.dDodajKnjigu:

                if(kategorije.size() != 0) {
                    // fragmenti
                    FragmentManager fm = getFragmentManager();

                    if(Sirina.sirok){
                        FrameLayout f = (FrameLayout)(getActivity().findViewById(R.id.fragmentF3));
                        if(f.getVisibility() == View.GONE) f.setVisibility(View.VISIBLE);
                        DodavanjeKnjigeFragment df = (DodavanjeKnjigeFragment)fm.findFragmentByTag("DodavanjeSiroka");

                        if(df == null) {
                            df = new DodavanjeKnjigeFragment();

                            Bundle argumenti = new Bundle();
                            argumenti.putStringArrayList("kategorije", kategorije);
                            argumenti.putParcelable("arhivaKnjiga", arhivaKnjiga);
                            df.setArguments(argumenti);

                            fm.beginTransaction().replace(R.id.fragmentF3, df, "DodavanjeSiroka").commit();
                        }
                        else{
                            fm.popBackStack(null, FragmentManager.POP_BACK_STACK_INCLUSIVE);
                        }
                    }

                    else {
                        DodavanjeKnjigeFragment df = (DodavanjeKnjigeFragment) (fm.findFragmentByTag("Dodavanje"));

                        if (df == null) {
                            df = new DodavanjeKnjigeFragment();

                            Bundle argumenti = new Bundle();
                            argumenti.putStringArrayList("kategorije", kategorije);
                            argumenti.putParcelable("arhivaKnjiga", arhivaKnjiga);
                            df.setArguments(argumenti);

                            fm.beginTransaction().replace(R.id.fragmentF1, df, "Dodavanje").addToBackStack(null).commit();
                        } else {
                            fm.popBackStack(null, FragmentManager.POP_BACK_STACK_INCLUSIVE);
                        }
                    }
                }
                else {
                    Toast.makeText(getActivity(), "Unesite kategorije knjiga da bi omogućili dodavanje", Toast.LENGTH_SHORT).show();
                }
                break;


            // onClick za dAutori
            case    R.id.dAutori:
                    izdvojiAutore();
                    dPretraga.setVisibility(View.GONE);
                    dDodajKategoriju.setVisibility(View.GONE);
                    tekstPretraga.setVisibility(View.GONE);
                    ArrayAdapter<String> adapterAutora = new ArrayAdapter<String>(getActivity(), android.R.layout.simple_list_item_1, listaAutora);
                    listView.setAdapter(adapterAutora);
                    break;


            // onClick za dDodajOnline
            case    R.id.dDodajOnline:

                    if(kategorije.size() != 0) {
                        FragmentManager fm = getFragmentManager();
                        FragmentOnline fo = (FragmentOnline) (fm.findFragmentByTag("Online"));

                        if (!Sirina.sirok) {
                            if (fo == null) {
                                fo = new FragmentOnline();

                                Bundle argumenti = new Bundle();
                                argumenti.putStringArrayList("kategorije", kategorije);
                                argumenti.putParcelable("arhivaKnjiga", arhivaKnjiga);
                                fo.setArguments(argumenti);

                                fm.beginTransaction().replace(R.id.fragmentF1, fo, "Online").addToBackStack(null).commit();
                            } else {
                                fm.popBackStack(null, FragmentManager.POP_BACK_STACK_INCLUSIVE);
                            }
                        } else {
                            if (fo == null) {
                                fo = new FragmentOnline();
                                Bundle argumenti = new Bundle();
                                argumenti.putStringArrayList("kategorije", kategorije);
                                argumenti.putParcelable("arhivaKnjiga", arhivaKnjiga);
                                fo.setArguments(argumenti);

                                fm.beginTransaction().replace(R.id.fragmentF2, fo, "Online").commit();
                            }
                        }
                    }
                    else {
                        Toast.makeText(getActivity(), "Trenutno nije unešena niti jedna kategorija!", Toast.LENGTH_SHORT).show();
                    }
                    break;
        }
    }

    Map<String, Integer> izdvojiAutore(){
        Map<String, Integer> mapa = new HashMap<String, Integer>();

        ArrayList<String> autoriKnjiga = KategorijeAkt.bazaOpenHelper.selectAutori();       // ovdje smiještamo sve autore iz baze - brže to
                                                                                            // nego sve knjige pa sa njima raditi.

        for (String x : autoriKnjiga) {
            Integer count = mapa.get(x);
            mapa.put(x, count == null ? 1 : count + 1);     // update u hash mapi - povecaj vrijednost za 1
                                                            // ako vec postoji kljuc ili stavi vrijednost na 1
                                                            // ako je nova.
        }

        Set<String> kljucevi = mapa.keySet();
        listaAutora.clear();        // slijedi konacno dodavanje elemenata u listu stringova
                                    // koja ce se prikazati na listView-u fragmenta

        for(String s : kljucevi){
            int vrijednost = mapa.get(s);
            listaAutora.add(s + ", broj knjiga: " + vrijednost);
        }
        return mapa;
    }
}

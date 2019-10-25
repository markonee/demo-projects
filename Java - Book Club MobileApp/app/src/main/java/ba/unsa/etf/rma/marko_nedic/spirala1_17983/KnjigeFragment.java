package ba.unsa.etf.rma.marko_nedic.spirala1_17983;

import android.app.Fragment;
import android.app.FragmentManager;
import android.graphics.Color;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Toast;

import java.util.ArrayList;

public class KnjigeFragment extends Fragment{
    ArhivaKnjiga arhivaKnjiga;
    ArrayList<String> kategorije;
    String odabrano;
    KnjigaAdapter adapter;

    ArrayList<Knjiga> knjige = new ArrayList<>();
    ArrayList<Knjiga> elementiListe = new ArrayList<>();

    ArrayList<Autor> autori = new ArrayList<>();                // ovo će se koristiti za prikaz liste autora
                                                                // nakon klika na dugme PRIKAŽI AUTORE.
    View iv;
    Button dPovratak;
    ListView listaKnjiga;

    boolean autor = false;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, Bundle savedInstanceState) {
        iv = inflater.inflate(R.layout.knjige_fragment, container, false);

        if(getArguments() != null && getArguments().containsKey("kategorije") && getArguments().containsKey("arhivaKnjiga")) {
            arhivaKnjiga = getArguments().getParcelable("arhivaKnjiga");
            kategorije = getArguments().getStringArrayList("kategorije");
            odabrano = getArguments().getString("odabrano");

            // izvuci ime autora - ako je selektovan autor to je dio od pocetka do zareza

            if(odabrano != null && odabrano.contains(",")){
                int index = odabrano.indexOf(",");
                odabrano = odabrano.substring(0, index);
                autor = true;
            }

            dPovratak = (Button) (iv.findViewById(R.id.dPovratak));
            listaKnjiga = (ListView) (iv.findViewById(R.id.listaKnjiga));

            knjige = arhivaKnjiga.getKnjige();

            if(!autor) {         // pravimo upit za odabranu kategoriju - sve knjige te kategorije!
                int id = KategorijeAkt.bazaOpenHelper.dajIdKategorije(odabrano);
                elementiListe = KategorijeAkt.bazaOpenHelper.knjigeKategorije(id);
            }

            else{
                int id = KategorijeAkt.bazaOpenHelper.provjeriAutora(odabrano);
                elementiListe = KategorijeAkt.bazaOpenHelper.knjigeAutora(id);
            }

            adapter = new KnjigaAdapter(getActivity(), elementiListe);
            listaKnjiga.setAdapter(adapter);
        }

        dPovratak.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(!Sirina.sirok) {
                    getFragmentManager().popBackStack(null, FragmentManager.POP_BACK_STACK_INCLUSIVE);
                }
            }
        });

        return iv;
    }
}

package ba.unsa.etf.rma.marko_nedic.spirala1_17983;

import android.app.Fragment;
import android.app.FragmentManager;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewManager;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.Toast;

import java.util.ArrayList;

import static android.app.Activity.RESULT_OK;

public class DodavanjeKnjigeFragment extends Fragment implements View.OnClickListener {
    private static String praviPut = null;
    private static final int KOD = 1;

    ArhivaKnjiga arhivaKnjiga = new ArhivaKnjiga();
    ArrayList<String> kategorije;
    View iv;

    Button dPonisti;
    Button dUpisiKnjigu;
    Button dNadjiSliku;
    ArrayAdapter<String> adapter;
    Spinner spinner;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, Bundle savedInstanceState) {
        iv = inflater.inflate(R.layout.dodavanje_knjige_fragment, container, false);


        if(getArguments() != null && getArguments().containsKey("kategorije") && getArguments().containsKey("arhivaKnjiga")) {
            arhivaKnjiga = getArguments().getParcelable("arhivaKnjiga");
            kategorije = getArguments().getStringArrayList("kategorije");

            spinner = (Spinner) (iv.findViewById(R.id.sKategorijaKnjige));

            ArrayAdapter<String> adapter = new ArrayAdapter<String>(getActivity(),
                    android.R.layout.simple_spinner_item, arhivaKnjiga.getKategorije());
            adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            spinner.setAdapter(adapter);

            dPonisti = (Button) (iv.findViewById(R.id.dPonisti));
            dPonisti.setOnClickListener(this);

            dUpisiKnjigu = (Button) (iv.findViewById(R.id.dUpisiKnjigu));
            dUpisiKnjigu.setOnClickListener(this);

            dNadjiSliku = (Button) (iv.findViewById(R.id.dNadjiSliku));
            dNadjiSliku.setOnClickListener(this);
        }

        return iv;
    }

    @Override
    public void onClick(View v) {

        switch (v.getId()) {

            // onClick za dUpisiKnjigu
            case    R.id.dUpisiKnjigu:

                EditText imeAutora = (EditText) (iv.findViewById(R.id.imeAutora));
                EditText nazivKnjige = (EditText) (iv.findViewById(R.id.nazivKnjige));
                ImageView naslovnaStr = (ImageView) (iv.findViewById(R.id.naslovnaStr));

                if (praviPut != null && nazivKnjige.getText().length() != 0 && imeAutora.getText().length() != 0) {
                    Knjiga k =  new Knjiga(nazivKnjige.getText().toString(),
                            imeAutora.getText().toString(), spinner.getSelectedItem().toString(), praviPut);

                    long i = KategorijeAkt.bazaOpenHelper.dodajKnjigu(k);


                    if(i != -1){
                        arhivaKnjiga.getKnjige().add(k);
                        Toast.makeText(getActivity(), "Knjiga je uspješno dodana!", Toast.LENGTH_SHORT).show();
                        imeAutora.setText("");
                        nazivKnjige.setText("");
                        naslovnaStr.setImageResource(0);
                        spinner.setSelection(0);
                    }
                    else{
                        Toast.makeText(getActivity(), "Knjiga se već nalazi u arhivi!", Toast.LENGTH_SHORT).show();
                    }

                    }
                    else{
                    Toast.makeText(getActivity(), "Ispunite tekstualna polja" +
                            " i učitajte ispravnu sliku iz galerije!", Toast.LENGTH_SHORT).show();
                    }
                    break;

            // onClick za dNadjiSliku
            case    R.id.dNadjiSliku:

                Intent intent = new Intent();
                intent.setType("image/*");
                intent.setAction(Intent.ACTION_GET_CONTENT);

                // da li se na navedeni može neka aplikacija odgovoriti
                if(intent.resolveActivity(getActivity().getPackageManager()) != null) {
                    startActivityForResult(intent, KOD);
                }
                else {
                    Toast.makeText(getActivity(), "Izbor slike nije moguć!", Toast.LENGTH_SHORT).show();
                }
                break;


            // onClick za dPonisti
            case    R.id.dPonisti:
                // fragmenti
                FragmentManager fm = getFragmentManager();

                ListeFragment lf = (ListeFragment) fm.findFragmentByTag("Lista");

                if(Sirina.sirok){
                    FrameLayout f = (FrameLayout)(getActivity().findViewById(R.id.fragmentF3));
                    if(f.getVisibility() == View.VISIBLE) f.setVisibility(View.GONE);
                }

                if(lf == null){
                    lf = new ListeFragment();

                    Bundle argumenti = new Bundle();
                    argumenti.putStringArrayList("kategorije", kategorije);
                    argumenti.putParcelable("arhivaKnjiga", arhivaKnjiga);
                    lf.setArguments(argumenti);

                    fm.beginTransaction().replace(R.id.fragmentF1, lf, "Lista").commit();
                }
                else{
                    getFragmentManager().popBackStack(null, FragmentManager.POP_BACK_STACK_INCLUSIVE);
                }
                break;
        }
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        if (requestCode == KOD && resultCode == RESULT_OK && data != null && data.getData() != null) {
            Uri uri = data.getData();

            // u atribut klase Knjiga ce se slati putanja da bi se u
            // listi knjiga mogla ucitati slika
            praviPut = uri.toString();

            if (praviPut != null) {
                ImageView imageView = (ImageView) iv.findViewById(R.id.naslovnaStr);
                imageView.setImageURI(uri);
            }
        }
    }

    boolean provjeriUArhivi(ArrayList<Knjiga> knjige, Knjiga k){
        for (Knjiga x:
                knjige) {
            if(x.getId().equals(k.getId()) || x.getNaziv().equals(k.getNaziv())) return true;
        }
        return false;
    }
}

package ba.unsa.etf.rma.marko_nedic.spirala1_17983;

import android.app.FragmentManager;
import android.content.Context;
import android.content.res.Resources;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.squareup.picasso.Picasso;

import java.util.List;

/**
 * Created by Marko on 22.3.2018..
 */

public class KnjigaAdapter extends ArrayAdapter<Knjiga>{

    private Context ctx;
    private List<Knjiga> knjige;


    public KnjigaAdapter(@NonNull Context context, List<Knjiga> knjige){
        super(context, 0, knjige);
        ctx = context;
        this.knjige = knjige;
    }

    @NonNull
    @Override
    public View getView(int position, @Nullable View convertView, @NonNull ViewGroup parent){
        View elementListe = convertView;
        if(elementListe == null)
            elementListe = LayoutInflater.from(ctx).inflate(R.layout.element_liste,parent,false);

        final View klinac = elementListe;   // zbog pristupa u clickListeneru

        final Knjiga trenutnaKnjiga  = knjige.get(position);

        ImageView eNaslovna = elementListe.findViewById(R.id.eNaslovna);
        TextView eNaziv = elementListe.findViewById(R.id.eNaziv);
        TextView eAutor = elementListe.findViewById(R.id.eAutor);

        TextView eDatumObjavljivanja = elementListe.findViewById(R.id.eDatumObjavljivanja);
        TextView eOpis = elementListe.findViewById(R.id.eOpis);
        TextView eBrojStranica = elementListe.findViewById(R.id.eBrojStranica);

        eAutor.setText(trenutnaKnjiga.dajSveAutoreString());
        eNaziv.setText(trenutnaKnjiga.getNaziv());

        eDatumObjavljivanja.setText(trenutnaKnjiga.getDatumObjavljivanja());
        eOpis.setText(trenutnaKnjiga.getOpis());

        if(trenutnaKnjiga.getBrojStranica() != -1) {
            eBrojStranica.setText(trenutnaKnjiga.getBrojStranica() + "");
        }
        else eBrojStranica.setText("");


        if(trenutnaKnjiga.getNaslovnaStranica() == null) {
            Picasso.with(getContext()).load(trenutnaKnjiga.getSlika().toString()).into(eNaslovna);
        }
        else{
            eNaslovna.setImageURI(Uri.parse(trenutnaKnjiga.getNaslovnaStranica()));
        }

        if(trenutnaKnjiga.isSelektovana()){
            elementListe.setBackgroundColor(ctx.getResources().getColor(R.color.svijetloPlava));
        }

        else{
            elementListe.setBackgroundColor(ctx.getResources().getColor((R.color.sivaLista)));
        }


        Button dPreporuci = elementListe.findViewById(R.id.dPreporuci);
        dPreporuci.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                FragmentManager fm = ((KategorijeAkt) ctx).getFragmentManager();

                FragmentPreporuci fp = (FragmentPreporuci)(fm.findFragmentByTag("Preporuci"));
                if(!Sirina.sirok){
                    if(fp == null){
                        fp = new FragmentPreporuci();
                        Bundle argumenti = new Bundle();
                        argumenti.putParcelable("knjigaPreporuci", trenutnaKnjiga);

                        fp.setArguments(argumenti);

                        fm.beginTransaction().replace(R.id.fragmentF1, fp, "Preporuci").addToBackStack(null).commit();
                    }
                }

                else{
                    if(fp == null){
                        fp = new FragmentPreporuci();
                        Bundle argumenti = new Bundle();
                        argumenti.putParcelable("knjigaPreporuci", trenutnaKnjiga);


                        fp.setArguments(argumenti);

                        fm.beginTransaction().replace(R.id.fragmentF2, fp, "Preporuci").commit();
                    }
                }

            }
        });

        Button dOznaci = elementListe.findViewById(R.id.dOznaci);
        dOznaci.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(trenutnaKnjiga.isSelektovana()){
                    trenutnaKnjiga.setSelektovana(false);
                    klinac.setBackgroundColor(ctx.getResources().getColor((R.color.sivaLista)));
                }
                else{
                    trenutnaKnjiga.setSelektovana(true);
                    klinac.setBackgroundColor(ctx.getResources().getColor((R.color.svijetloPlava)));
                }

                KategorijeAkt.bazaOpenHelper.updateKnjigu(trenutnaKnjiga);
            }
        });

        return elementListe;
    }
}

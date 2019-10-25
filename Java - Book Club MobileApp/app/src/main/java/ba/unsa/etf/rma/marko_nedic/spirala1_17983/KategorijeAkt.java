package ba.unsa.etf.rma.marko_nedic.spirala1_17983;

import android.app.FragmentManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.FrameLayout;
import android.widget.Toast;


import java.util.ArrayList;

public class KategorijeAkt extends AppCompatActivity {

    ArhivaKnjiga arhivaKnjiga = new ArhivaKnjiga();
    ArrayList<String> kategorije = new ArrayList<String>();
    public static BazaOpenHelper bazaOpenHelper;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_pocetni);


        bazaOpenHelper = new BazaOpenHelper(this);
        kategorije = bazaOpenHelper.selectKategorije();
        arhivaKnjiga.setKategorije(kategorije);

        bazaOpenHelper.LogirajKategorije();
        bazaOpenHelper.LogirajKnjige();
        bazaOpenHelper.LogirajAutore();
        bazaOpenHelper.LogirajAutorstva();

        // fragmenti
        FragmentManager fm = getFragmentManager();
        FrameLayout sirokiFrame = (FrameLayout) (findViewById(R.id.fragmentF2));

        Bundle argumentiK = new Bundle();
        Bundle argumentiL = new Bundle();

        if (sirokiFrame != null) {
            Sirina.sirok = true;

            KnjigeFragment kf = (KnjigeFragment) (fm.findFragmentById(R.id.fragmentF2));

            if (kf == null) {
                kf = new KnjigeFragment();

                argumentiK.putStringArrayList("kategorije", kategorije);
                argumentiK.putParcelable("arhivaKnjiga", arhivaKnjiga);

                kf.setArguments(argumentiK);
                fm.beginTransaction().replace(R.id.fragmentF2, kf, "Knjige2").commit();
            }
            else{
                fm.popBackStack(null, FragmentManager.POP_BACK_STACK_INCLUSIVE);
            }
            ListeFragment lf = (ListeFragment) fm.findFragmentById(R.id.fragmentF1);
            if (lf == null) {
                lf = new ListeFragment();

                argumentiL.putStringArrayList("kategorije", kategorije);
                argumentiL.putParcelable("arhivaKnjiga", arhivaKnjiga);

                lf.setArguments(argumentiL);
                fm.beginTransaction().replace(R.id.fragmentF1, lf, "Lista").commit();
            }
            else{
                fm.popBackStack(null, FragmentManager.POP_BACK_STACK_INCLUSIVE);
            }

        } else {
            Sirina.sirok = false;

            ListeFragment lf = (ListeFragment) (fm.findFragmentById(R.id.fragmentF1));

            if (lf == null) {
                argumentiL.putStringArrayList("kategorije", kategorije);
                argumentiL.putParcelable("arhivaKnjiga", arhivaKnjiga);
                argumentiL.putBoolean("siroki", Sirina.sirok);
                lf = new ListeFragment();
                lf.setArguments(argumentiL);

                fm.beginTransaction().replace(R.id.fragmentF1, lf, "Lista").commit();
            } else {
                fm.popBackStack(null, FragmentManager.POP_BACK_STACK_INCLUSIVE);
            }
        }
    }
}


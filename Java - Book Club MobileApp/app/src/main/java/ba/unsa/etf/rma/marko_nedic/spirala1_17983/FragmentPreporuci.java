package ba.unsa.etf.rma.marko_nedic.spirala1_17983;

import android.app.Fragment;
import android.content.ActivityNotFoundException;
import android.content.ContentProvider;
import android.content.ContentResolver;
import android.content.Intent;
import android.database.Cursor;
import android.net.Uri;
import android.os.Bundle;
import android.provider.ContactsContract;
import android.support.annotation.Nullable;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.SpinnerAdapter;
import android.widget.Toast;

import java.util.ArrayList;

public class FragmentPreporuci extends Fragment{

    public class Kontakt{
        private String ime;
        private String eMail;

        public Kontakt(String ime, String eMail) {
            this.ime = ime;
            this.eMail = eMail;
        }

        public String getIme() {
            return ime;
        }

        public String geteMail() {
            return eMail;
        }
    }


    private View iv;
    private Knjiga knjiga;
    private Spinner sKontakti;
    private Button dPosalji;
    private ArrayList<Kontakt> kontakti = new ArrayList<>();

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, Bundle savedInstanceState) {
        iv = inflater.inflate(R.layout.fragment_preporuci, container, false);

        if(getArguments() != null && getArguments().containsKey("knjigaPreporuci")){
            // kupi listu kontakata sa uređaja
            kontaktiSelect();

            knjiga = getArguments().getParcelable("knjigaPreporuci");

            sKontakti = iv.findViewById(R.id.sKontakti);
            dPosalji = iv.findViewById(R.id.dPosalji);

            SpinnerAdapter sa = new ArrayAdapter<String>(getActivity(),
                    android.R.layout.simple_spinner_item, dajAdrese());

            sKontakti.setAdapter(sa);

            dPosalji.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    int index = sKontakti.getSelectedItemPosition();

                    Intent intent = new Intent(Intent.ACTION_SEND);

                    String s = "Zdravo " + nadjiKontakta(sKontakti.getSelectedItem().toString()) + ", "
                            + "\n" + "Pročitaj knjigu " + knjiga.getNaziv() + " od " +
                    knjiga.getImeAutora() + "!";

                    intent.setData(Uri.parse("mailto:"));
                    intent.setType("text/plain");
                    intent.putExtra(Intent.EXTRA_EMAIL, new String[]{sKontakti.getSelectedItem().toString()});
                    intent.putExtra(Intent.EXTRA_SUBJECT, "Preporučena knjiga!");
                    intent.putExtra(Intent.EXTRA_TEXT, s);


                    try{
                        startActivity(Intent.createChooser(intent, "Pošalji mail!"));
                    }
                    catch (ActivityNotFoundException e){
                        Toast.makeText(getActivity(), "Nije pronađena niti jedna aplikacija za slanje e-maila!", Toast.LENGTH_SHORT).show();
                    }
                }
            });
        }

        return iv;
    }

    private void kontaktiSelect(){
        ContentResolver cr = getActivity().getContentResolver();

        Cursor cursor = cr.query(ContactsContract.Contacts.CONTENT_URI, null, null, null, null);

        if(cursor.getCount() != 0){
            while(cursor.moveToNext()) {
                int indexID = cursor.getColumnIndex(ContactsContract.Contacts._ID);
                int indexIme = cursor.getColumnIndex(ContactsContract.Contacts.DISPLAY_NAME);

                String _id = cursor.getString(indexID);

                Cursor cursor1 = cr.query(ContactsContract.CommonDataKinds.Email.CONTENT_URI, null,
                        ContactsContract.CommonDataKinds.Email.CONTACT_ID + " = ?",
                        new String[]{_id}, null);

                while(cursor1.moveToNext()) {
                    int indexMail = cursor1.getColumnIndex(ContactsContract.CommonDataKinds.Email.DATA);
                    kontakti.add(new Kontakt(cursor.getString(indexIme), cursor1.getString(indexMail)));
                }
            }
        }
    }

    private ArrayList<String> dajAdrese(){
        ArrayList<String> arrayList = new ArrayList<>();

        for (Kontakt k:
             kontakti) {
            arrayList.add(k.geteMail());
        }

        return arrayList;
    }

    private String nadjiKontakta(String x){
        for (Kontakt k:
             kontakti) {
            if(k.geteMail().equals(x)) return k.getIme();
        }

        return "";
    }
}

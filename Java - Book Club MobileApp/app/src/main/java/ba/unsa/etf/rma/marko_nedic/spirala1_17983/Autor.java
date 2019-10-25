package ba.unsa.etf.rma.marko_nedic.spirala1_17983;

import android.os.Parcel;
import android.os.Parcelable;

import java.util.ArrayList;

public class Autor implements Parcelable{
    // atributi

    private String imeiPrezime;
    ArrayList<String> knjige;

    // override za parcelable

    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel parcel, int i) {
        parcel.writeString(imeiPrezime);
        parcel.writeList(knjige);
    }

    protected Autor(Parcel in) {
        imeiPrezime = in.readString();
        knjige = in.readArrayList(String.class.getClassLoader());
    }

    public static final Creator<Autor> CREATOR = new Creator<Autor>() {
        @Override
        public Autor createFromParcel(Parcel in) {
            return new Autor(in);
        }

        @Override
        public Autor[] newArray(int size) {
            return new Autor[size];
        }
    };

    // konstruktori

    public Autor(){
        knjige = new ArrayList<>();
    }

    public Autor(String imeiPrezime, String id){
        knjige = new ArrayList<>();
        this.imeiPrezime = imeiPrezime;
        knjige.add(id);
    }

    public Autor(String imeiPrezime){
        knjige = new ArrayList<>();
        this.imeiPrezime = imeiPrezime;
    }


    // geteri i seteri

    public String getImeiPrezime() {
        return imeiPrezime;
    }

    public void setImeiPrezime(String imeiPrezime) {
        this.imeiPrezime = imeiPrezime;
    }

    public ArrayList<String> getKnjige() {
        return knjige;
    }

    public void setKnjige(ArrayList<String> knjige) {
        this.knjige = knjige;
    }

    // dodatne metode

    public void dodajKnjigu(String id){
        if(!knjige.contains(id)) knjige.add(id);
    }
}

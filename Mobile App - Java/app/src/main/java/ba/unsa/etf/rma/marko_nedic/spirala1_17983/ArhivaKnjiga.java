package ba.unsa.etf.rma.marko_nedic.spirala1_17983;

import android.os.Parcel;
import android.os.Parcelable;
import android.util.Log;

import java.io.Serializable;
import java.util.ArrayList;

/**
 * Created by Marko on 21.3.2018..
 */

public class ArhivaKnjiga implements Parcelable{
    // atributi

    private static ArrayList<Knjiga> knjige;
    private ArrayList<String> kategorije;

    // konstruktori

    public ArhivaKnjiga(ArrayList<Knjiga> knjige, ArrayList<String> kategorije) {
        this.knjige = knjige;
        this.kategorije = kategorije;
    }

    public ArhivaKnjiga() {
        knjige = new ArrayList<>();
        kategorije = new ArrayList<>();
    }


    // override za parcelable

    protected ArhivaKnjiga(Parcel in){
        knjige = in.readArrayList(ClassLoader.getSystemClassLoader());
        kategorije = in.readArrayList(ClassLoader.getSystemClassLoader());
    }


    @Override
    public void writeToParcel(Parcel parcel, int i) {
        parcel.writeList(knjige);
        parcel.writeList(kategorije);
    }


    public static final Creator<ArhivaKnjiga> CREATOR = new Creator<ArhivaKnjiga>() {
        @Override
        public ArhivaKnjiga createFromParcel(Parcel parcel) {
            return new ArhivaKnjiga(parcel);
        }

        @Override
        public ArhivaKnjiga[] newArray(int i) {
            return new ArhivaKnjiga[i];
        }
    };

    @Override
    public int describeContents() {
        return 0;
    }

    // geteri i seteri


    public ArrayList<Knjiga> getKnjige() {
        return knjige;
    }

    public void setKnjige(ArrayList<Knjiga> knjige) {
        this.knjige = knjige;
    }

    public ArrayList<String> getKategorije() {
        return kategorije;
    }

    public void setKategorije(ArrayList<String> kategorije) {
        this.kategorije = kategorije;
    }

}

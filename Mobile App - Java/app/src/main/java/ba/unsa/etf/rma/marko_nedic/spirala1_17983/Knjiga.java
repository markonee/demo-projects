package ba.unsa.etf.rma.marko_nedic.spirala1_17983;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable;

import java.io.Serializable;
import java.net.URI;
import java.net.URL;
import java.util.ArrayList;

/**
 * Created by Marko on 21.3.2018..
 */

public class Knjiga implements Parcelable {
    // atributi

    private String id;
    private String naziv;
    private ArrayList<Autor> autori;
    private String opis;
    private String datumObjavljivanja;
    private URL slika;
    private int brojStrinica;
    private String kategorija;
    private String naslovnaStranica;        // Uri slike sa uređaja
    private boolean selektovana;

    // konstruktori

    public Knjiga(String id, String naziv, ArrayList<Autor> autori, String opis, String datumObjavljivanja, URL slika, int brojStrinica, boolean selektovana) {
        this.id = id;
        this.naziv = naziv;
        this.autori = autori;
        this.opis = opis;
        this.datumObjavljivanja = datumObjavljivanja;
        this.slika = slika;
        this.brojStrinica = brojStrinica;
        kategorija = "";
        naslovnaStranica = null;
        this.selektovana = selektovana;
    }

    public Knjiga(String id, String naziv, ArrayList<Autor> autori, String opis, String datumObjavljivanja, URL slika, int brojStrinica) {
        this.id = id;
        this.naziv = naziv;
        this.autori = autori;
        this.opis = opis;
        this.datumObjavljivanja = datumObjavljivanja;
        this.slika = slika;
        this.brojStrinica = brojStrinica;
        kategorija = "";
        naslovnaStranica = null;
    }

    public Knjiga(String id, String naziv, ArrayList<Autor> autori, String opis, String datumObjavljivanja, URL slika, int brojStrinica, String kategorija, String naslovnaStranica, boolean selektovana) {
        this.id = id;
        this.naziv = naziv;
        this.autori = autori;
        this.opis = opis;
        this.datumObjavljivanja = datumObjavljivanja;
        this.slika = slika;
        this.brojStrinica = brojStrinica;
        this.kategorija = kategorija;
        this.naslovnaStranica = naslovnaStranica;
        this.selektovana = selektovana;
    }

    public Knjiga(String naziv, String autor, String kategorija, String naslovnaStranica) {
        this.naziv = naziv;
        autori = new ArrayList<>();
        autori.add(new Autor(autor));           // ovdje nema id-ja za ručno unesene knjige
        this.kategorija = kategorija;
        this.naslovnaStranica = naslovnaStranica;
        slika = null;
        id = "";
    }

    // override za parcelable


    protected Knjiga(Parcel in) {
        id = in.readString();
        naziv = in.readString();
        autori = in.createTypedArrayList(Autor.CREATOR);
        opis = in.readString();
        datumObjavljivanja = in.readString();
        brojStrinica = in.readInt();
        kategorija = in.readString();
        naslovnaStranica = in.readString();
        selektovana = in.readByte() != 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeString(id);
        dest.writeString(naziv);
        dest.writeTypedList(autori);
        dest.writeString(opis);
        dest.writeString(datumObjavljivanja);
        dest.writeInt(brojStrinica);
        dest.writeString(kategorija);
        dest.writeString(naslovnaStranica);
        dest.writeByte((byte) (selektovana ? 1 : 0));
    }

    @Override
    public int describeContents() {
        return 0;
    }

    public static final Creator<Knjiga> CREATOR = new Creator<Knjiga>() {
        @Override
        public Knjiga createFromParcel(Parcel in) {
            return new Knjiga(in);
        }

        @Override
        public Knjiga[] newArray(int size) {
            return new Knjiga[size];
        }
    };

    // geteri i seteri


    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getNaziv() {
        return naziv;
    }

    public void setNaziv(String naziv) {
        this.naziv = naziv;
    }

    public ArrayList<Autor> getAutori() {
        return autori;
    }

    public void setAutori(ArrayList<Autor> autori) {
        this.autori = autori;
    }

    public String getOpis() {
        return opis;
    }

    public void setOpis(String opis) {
        this.opis = opis;
    }

    public String getDatumObjavljivanja() {
        return datumObjavljivanja;
    }

    public void setDatumObjavljivanja(String datumObjavljivanja) {
        this.datumObjavljivanja = datumObjavljivanja;
    }

    public URL getSlika() {
        return slika;
    }

    public void setSlika(URL slika) {
        this.slika = slika;
    }

    public int getBrojStranica() {
        return brojStrinica;
    }

    public void setBrojStranica(int brojStranica) {
        this.brojStrinica = brojStranica;
    }

    public String getKategorija() {
        return kategorija;
    }

    public void setKategorija(String kategorija) {
        this.kategorija = kategorija;
    }

    public String getNaslovnaStranica() {
        return naslovnaStranica;
    }

    public void setNaslovnaStranica(String naslovnaStranica) {
        this.naslovnaStranica = naslovnaStranica;
    }

    public boolean isSelektovana() {
        return selektovana;
    }

    public void setSelektovana(boolean selektovana) {
        this.selektovana = selektovana;
    }

    public String getImeAutora(){
        if(autori.size() != 0) {
            return autori.get(0).getImeiPrezime();
        }
        return "Nepoznat autor";      // nema nijedan
    }

    @Override
    public String toString() {
        return "Knjiga{" +
                "nazivKnjige='" + naziv + '\'' +
                ", imeAutora='" + autori.get(0).toString() + '\'' +
                ", kategorija='" + kategorija + '\'' +
                '}';
    }

    public String dajSveAutoreString(){
        String s = "";
        if(autori.size() > 0) {
            for (Autor a : autori) {
                s += a.getImeiPrezime() + ", ";
            }
            return s.substring(0, s.length() - 2);        // obriše zadnja dva znaka ", "!
        }
        return "Nepoznat autor";
    }

}

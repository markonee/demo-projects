package ba.unsa.etf.rma.marko_nedic.spirala1_17983;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLEncoder;
import java.util.ArrayList;

public class BazaOpenHelper extends SQLiteOpenHelper {
    private Context ctx;
    private String hardkodURL = "https://free4kwallpaper.com/wp-content/uploads/2016/01/Unique-Diamond-Abstract-4K-Wallpaper.jpg";


    public static final String IME_BAZE = "RMAKnjige.db";
    public static final int VERZIJA_BAZE = 3;

    // _id se koristi u svim tabelama
    public static final String ID = "_id";

    // elementi za tabele

    // tabela Kategorije
    public static final String TABELA_KATEGORIJA = "Kategorija";
    public static final String NAZIV_KATEGORIJA = "naziv";

    private final String KATEGORIJA_KREIRANJE = "create table " +
        TABELA_KATEGORIJA + " (" + ID + " integer primary key autoincrement, " +
            NAZIV_KATEGORIJA + " text not null);";

    // tabela Knjiga
    public static final String TABELA_KNJIGA = "Knjiga";
    public static final String NAZIV_KNJIGA = "naziv"; // iako ima i gore definiran...
    public static final String OPIS_KNJIGA = "opis";
    public static final String DATUM_KNJIGA = "datumObjavljivanja";
    public static final String STRANICE_KNJIGA = "brojStranica";
    public static final String ID_WEB_SERVIS = "idWebServis";
    public static final String ID_KATEGORIJE_KNJIGA = "idkategorije";
    public static final String SLIKA_KNJIGA = "knjiga";
    public static final String PREGLEDANA_KNJIGA = "pregledana";

    private final String KNJIGA_KREIRANJE = "create table " +
            TABELA_KNJIGA + " (" + ID + " integer primary key autoincrement, " +
            NAZIV_KNJIGA + " text not null, " +
            OPIS_KNJIGA + " text, " +
            DATUM_KNJIGA + " text, " +
            STRANICE_KNJIGA + " integer, " +
            ID_WEB_SERVIS + " text, " +
            ID_KATEGORIJE_KNJIGA + " integer not null, " +
            SLIKA_KNJIGA + " text, " +
            PREGLEDANA_KNJIGA + " integer not null);";

    // tabela Autor
    public static final String TABELA_AUTOR = "Autor";
    public static final String IME_AUTOR = "ime";
    private final String AUTOR_KREIRANJE = "create table " +
            TABELA_AUTOR + " (" + ID + " integer primary key autoincrement, " +
            IME_AUTOR + " text not null);";

    // tabela Autorstvo
    public static final String TABELA_AUTORSTVO = "Autorstvo";
    public static final String ID_AUTORA_AUTORSTVO = "idautora";
    public static final String ID_KNJIGE_AUTORSTVO = "idknjige";

    private final String AUTORSTVO_KREIRANJE = "create table " +
            TABELA_AUTORSTVO + " (" + ID + " integer primary key autoincrement, " +
            ID_AUTORA_AUTORSTVO + " integer not null, " +
            ID_KNJIGE_AUTORSTVO + " integer not null);";

    public BazaOpenHelper(Context context){
        super(context, IME_BAZE, null, VERZIJA_BAZE);
        ctx = context;
    }

    public BazaOpenHelper(Context context, String name, SQLiteDatabase.CursorFactory factory, int version) {
        super(context, name, factory, version);
    }

    @Override
    public void onCreate(SQLiteDatabase sqLiteDatabase) {
        sqLiteDatabase.execSQL(KATEGORIJA_KREIRANJE);
        sqLiteDatabase.execSQL(KNJIGA_KREIRANJE);
        sqLiteDatabase.execSQL(AUTOR_KREIRANJE);
        sqLiteDatabase.execSQL(AUTORSTVO_KREIRANJE);
    }

    @Override
    public void onUpgrade(SQLiteDatabase sqLiteDatabase, int i, int i1) {
        sqLiteDatabase.execSQL("DROP TABLE IF EXISTS " + TABELA_KATEGORIJA);
        sqLiteDatabase.execSQL("DROP TABLE IF EXISTS "  + TABELA_KNJIGA);
        sqLiteDatabase.execSQL("DROP TABLE IF EXISTS "  + TABELA_AUTOR);
        sqLiteDatabase.execSQL("DROP TABLE IF EXISTS "  + TABELA_AUTORSTVO);
        onCreate(sqLiteDatabase);
    }

    public ArrayList<String> selectKategorije(){
        ArrayList<String> kategorije = new ArrayList<>();
        SQLiteDatabase sqLiteDatabase;

        try{
            sqLiteDatabase = getWritableDatabase();
        }
        catch (Exception e){
            sqLiteDatabase = getReadableDatabase();
        }
        Cursor cursor = sqLiteDatabase.query(TABELA_KATEGORIJA, null, null, null, null, null, null);
        int indexNAZIV = cursor.getColumnIndex(NAZIV_KATEGORIJA);

        while(cursor.moveToNext()){
            kategorije.add(cursor.getString(indexNAZIV));
        }

        return kategorije;
    }

    public ArrayList<String> selectAutori(){
        ArrayList<String> autori = new ArrayList<>();

        // ide query kroz tabelu autorstva, da bi se svaki autor unio onoliko puta koliko ima knjiga

        SQLiteDatabase sqLiteDatabase;
        try{
            sqLiteDatabase = getWritableDatabase();
        }
        catch (Exception e){
            sqLiteDatabase = getReadableDatabase() ;
        }

        Cursor cursor = sqLiteDatabase.query(TABELA_AUTORSTVO, null, null, null, null, null, null);
        int indexAUTOR = cursor.getColumnIndex(ID_AUTORA_AUTORSTVO);

        while(cursor.moveToNext()){
            autori.add(dajImeAutoraZaID(cursor.getInt(indexAUTOR)));
        }

        return autori;
    }

    long dodajKategoriju(String naziv){
        if(!selectKategorije().contains(naziv)){
            ContentValues cv = new ContentValues();
            cv.put(NAZIV_KATEGORIJA, naziv);
            return getWritableDatabase().insert(TABELA_KATEGORIJA, null, cv);
        }
        return -1;
    }

    public int dajIdKategorije(String imeKategorije){
        String where = NAZIV_KATEGORIJA + " = " + "'" + imeKategorije + "'" ;

        Cursor cursor = getWritableDatabase().query(TABELA_KATEGORIJA, null, where, null,
                null, null, null);

        int indexID = cursor.getColumnIndex(ID);

        cursor.moveToFirst();

        return cursor.getInt(indexID);
    }

    public int provjeriAutora(String imeAutora){
        String where = IME_AUTOR + " = " + "'" + imeAutora + "'";
        Cursor cursor = getWritableDatabase().query(TABELA_AUTOR, null, where, null,
                null, null, null);

        if(cursor.getCount() > 0){
            cursor.moveToFirst();
            return cursor.getInt(cursor.getColumnIndex(ID));
        }

        return -1;
    }

    public void dodajAutore(ArrayList<Autor> autori){
        for (Autor a:
             autori) {
            if(provjeriAutora(a.getImeiPrezime()) == -1){
                ContentValues cv = new ContentValues();
                cv.put(IME_AUTOR, a.getImeiPrezime());
                getWritableDatabase().insert(TABELA_AUTOR, null, cv);
            }
        }
    }

    private boolean provjeriKnjigu(String naziv){
        naziv = naziv.replace("'", "");

        String where = NAZIV_KNJIGA + " = " + "'" + naziv + "'";
        Cursor cursor = getWritableDatabase().query(TABELA_KNJIGA, null, where, null, null,
                null, null);

        if(cursor.getCount() > 0) return true;

        return false;
    }

    public long dodajKnjigu(Knjiga knjiga){
        long id = -1;
        String naziv = knjiga.getNaziv().replace("'", ""); // sa apostrofom ne moze u bazu

        if(!provjeriKnjigu(naziv)) {


            int idKategorije = dajIdKategorije(knjiga.getKategorija());

            ContentValues cvKnjige = new ContentValues();
            cvKnjige.put(NAZIV_KNJIGA, naziv);
            cvKnjige.put(OPIS_KNJIGA, knjiga.getOpis());
            cvKnjige.put(DATUM_KNJIGA, knjiga.getDatumObjavljivanja());
            cvKnjige.put(STRANICE_KNJIGA, knjiga.getBrojStranica());
            cvKnjige.put(ID_WEB_SERVIS, knjiga.getId());
            cvKnjige.put(ID_KATEGORIJE_KNJIGA, idKategorije);
            if(knjiga.getSlika() != null) {
                cvKnjige.put(SLIKA_KNJIGA, knjiga.getSlika().toString());
            }
            else{
                cvKnjige.put(SLIKA_KNJIGA, hardkodURL);
            }
            if(knjiga.isSelektovana()) cvKnjige.put(PREGLEDANA_KNJIGA, 1);
            else cvKnjige.put(PREGLEDANA_KNJIGA, 0);

            id = getWritableDatabase().insert(TABELA_KNJIGA, null, cvKnjige);
            if(id != -1){
                // ubacivanje autora
                dodajAutore(knjiga.getAutori());

                for (Autor a:
                     knjiga.getAutori()) {
                    int idAutora = provjeriAutora(a.getImeiPrezime());
                    // sad ide ubacivanje u tabelu autorstva

                    ContentValues cvAutorstvo = new ContentValues();
                    cvAutorstvo.put(ID_AUTORA_AUTORSTVO, idAutora);
                    cvAutorstvo.put(ID_KNJIGE_AUTORSTVO, id);
                    getWritableDatabase().insert(TABELA_AUTORSTVO, null, cvAutorstvo);
                }
            }
        }

        LogirajKategorije();
        LogirajKnjige();
        LogirajAutore();
        LogirajAutorstva();
        return id;
    }

    private Cursor dajAutoreZaKnjiguAutorstvo(int idKnjige){
        String where = ID_KNJIGE_AUTORSTVO + " = " + idKnjige;
        SQLiteDatabase sqLiteDatabase;

        try{
            sqLiteDatabase = getWritableDatabase();
        }
        catch (Exception e){
            sqLiteDatabase = getReadableDatabase();
        }

        return sqLiteDatabase.query(TABELA_AUTORSTVO, null, where, null,
                null, null, null);
    }

    private String dajImeAutoraZaID(int idAutora){
        String where = ID + " = " + idAutora;
        SQLiteDatabase sqLiteDatabase;

        try{
            sqLiteDatabase = getWritableDatabase();
        }
        catch (Exception e){
            sqLiteDatabase = getReadableDatabase();
        }

        Cursor cursor = sqLiteDatabase.query(TABELA_AUTOR, null, where, null,
                null, null, null);

        cursor.moveToFirst();
        return cursor.getString(cursor.getColumnIndex(IME_AUTOR));
    }

    public ArrayList<Knjiga> knjigeKategorije(long idKategorije){
        ArrayList<Knjiga> knjige = new ArrayList<>();

        SQLiteDatabase sqLiteDatabase;          // dodaj ovakvu provjeru svuda gdje se ne upisuje.
        try{
            sqLiteDatabase = getWritableDatabase();
        }
        catch (Exception e){
            sqLiteDatabase = getReadableDatabase();
        }

        String where = ID_KATEGORIJE_KNJIGA + " = " + idKategorije;

        Cursor cursor = sqLiteDatabase.query(TABELA_KNJIGA, null, where, null, null, null, null);

        // indexi kolona
        int indexID = cursor.getColumnIndex(ID);
        int indexNAZIV = cursor.getColumnIndex(NAZIV_KNJIGA);
        int indexOPIS = cursor.getColumnIndex(OPIS_KNJIGA);
        int indexDATUM = cursor.getColumnIndex(DATUM_KNJIGA);
        int indexSTRANICE = cursor.getColumnIndex(STRANICE_KNJIGA);
        int indexWEB = cursor.getColumnIndex(ID_WEB_SERVIS);
        int indexSLIKA = cursor.getColumnIndex(SLIKA_KNJIGA);
        int indexPREGLEDANA = cursor.getColumnIndex(PREGLEDANA_KNJIGA);

        while(cursor.moveToNext()){
            Cursor cursorAutor = dajAutoreZaKnjiguAutorstvo(cursor.getInt(indexID));

            ArrayList<Autor> autori = new ArrayList<>();

            if(cursorAutor.getCount() > 0) {
                while (cursorAutor.moveToNext()) {
                    String imeAutora = dajImeAutoraZaID(cursorAutor.getInt(cursorAutor.getColumnIndex(ID_AUTORA_AUTORSTVO)));
                    autori.add(new Autor(imeAutora,
                            cursor.getString(indexWEB)));       // pravi novog autora {ime: Baza, knjige: idWebService trenutne}
                }
            }

            String naziv = cursor.getString(indexNAZIV);
            String opis = cursor.getString(indexOPIS);
            String datum = cursor.getString(indexDATUM);
            int stranice = cursor.getInt(indexSTRANICE);
            String idWS = cursor.getString(indexWEB);
            String urlSlike = cursor.getString(indexSLIKA);
            int selektovana = cursor.getInt(indexPREGLEDANA);


            boolean atributSelektovana = false;
            if(selektovana == 1) atributSelektovana = true;

            try {
                knjige.add(new Knjiga(idWS, naziv, autori, opis, datum, new URL(urlSlike), stranice, atributSelektovana));
            }
            catch (MalformedURLException e){

            }
        }
        return knjige;
    }

    private Cursor dajKnjigeAutoraAutorstvo(long idAutora){
        SQLiteDatabase sqLiteDatabase;          // dodaj ovakvu provjeru svuda gdje se ne upisuje.
        try{
            sqLiteDatabase = getWritableDatabase();
        }
        catch (Exception e){
            sqLiteDatabase = getReadableDatabase();
        }

        String where = ID_AUTORA_AUTORSTVO + " = " + idAutora;

        return sqLiteDatabase.query(TABELA_AUTORSTVO, null, where, null, null, null, null);
    }


    ArrayList<Knjiga> knjigeAutora(long idAutora){
        ArrayList<Knjiga> knjige = new ArrayList<>();


        SQLiteDatabase sqLiteDatabase;          // dodaj ovakvu provjeru svuda gdje se ne upisuje.
        try{
            sqLiteDatabase = getWritableDatabase();
        }
        catch (Exception e){
            sqLiteDatabase = getReadableDatabase();
        }

        Cursor cursorIDKnjiga = dajKnjigeAutoraAutorstvo(idAutora);

        while(cursorIDKnjiga.moveToNext()) {
            String where = ID + " = " + cursorIDKnjiga.getInt(cursorIDKnjiga.getColumnIndex(ID_KNJIGE_AUTORSTVO));

            Cursor cursor = sqLiteDatabase.query(TABELA_KNJIGA, null, where, null, null, null, null);

            // indexi kolona
            int indexID = cursor.getColumnIndex(ID);
            int indexNAZIV = cursor.getColumnIndex(NAZIV_KNJIGA);
            int indexOPIS = cursor.getColumnIndex(OPIS_KNJIGA);
            int indexDATUM = cursor.getColumnIndex(DATUM_KNJIGA);
            int indexSTRANICE = cursor.getColumnIndex(STRANICE_KNJIGA);
            int indexWEB = cursor.getColumnIndex(ID_WEB_SERVIS);
            int indexSLIKA = cursor.getColumnIndex(SLIKA_KNJIGA);
            int indexPREGLEDANA = cursor.getColumnIndexOrThrow(PREGLEDANA_KNJIGA);

            while (cursor.moveToNext()) {
                Cursor cursorAutor = dajAutoreZaKnjiguAutorstvo(cursor.getInt(indexID));

                ArrayList<Autor> autori = new ArrayList<>();

                if (cursorAutor.getCount() > 0) {
                    while (cursorAutor.moveToNext()) {
                        String imeAutora = dajImeAutoraZaID(cursorAutor.getInt(cursorAutor.getColumnIndex(ID_AUTORA_AUTORSTVO)));
                        autori.add(new Autor(imeAutora,
                                cursor.getString(indexWEB)));       // pravi novog autora {ime: Baza, knjige: idWebService trenutne}
                    }
                }

                String naziv = cursor.getString(indexNAZIV);
                String opis = cursor.getString(indexOPIS);
                String datum = cursor.getString(indexDATUM);
                int stranice = cursor.getInt(indexSTRANICE);
                String idWS = cursor.getString(indexWEB);
                String urlSlike = cursor.getString(indexSLIKA);
                int selektovana = cursor.getInt(indexPREGLEDANA);


                boolean atributSelektovana = false;
                if(selektovana == 1) atributSelektovana = true;

                try {
                    knjige.add(new Knjiga(idWS, naziv, autori, opis, datum, new URL(urlSlike), stranice, atributSelektovana));
                }
                catch (MalformedURLException e){

                }
            }
        }

        return knjige;
    }

    public void updateKnjigu(Knjiga knjiga){
        String naziv = knjiga.getNaziv().replace("'", "");
        SQLiteDatabase sqLiteDatabase;

        try{
            sqLiteDatabase = getWritableDatabase();
        }
        catch (Exception e){
            sqLiteDatabase = getReadableDatabase();
        }

        int atribut = 0;
        if(knjiga.isSelektovana()) atribut = 1;

        String where = NAZIV_KNJIGA + " = " + "'" + naziv + "'";

        ContentValues contentValues = new ContentValues();
        contentValues.put(PREGLEDANA_KNJIGA, atribut);

        sqLiteDatabase.update(TABELA_KNJIGA, contentValues, where, null);
    }

    public void LogirajKategorije(){
        SQLiteDatabase sqLiteDatabase;
        try{
            sqLiteDatabase = getWritableDatabase();
        }
        catch (Exception e){
            sqLiteDatabase = getReadableDatabase();
        }

        Cursor cursor = sqLiteDatabase.query(TABELA_KATEGORIJA, null, null, null, null, null, null);
        while (cursor.moveToNext()){
            Log.i("Kategorija: ", cursor.getInt(0) + ". " + cursor.getString(1));
        }
    }

    public void LogirajKnjige(){

        SQLiteDatabase sqLiteDatabase;
        try{
            sqLiteDatabase = getWritableDatabase();
        }
        catch (Exception e){
            sqLiteDatabase = getReadableDatabase();
        }

        Cursor cursor = sqLiteDatabase.query(TABELA_KNJIGA, null, null, null, null, null, null);

        int indexID = cursor.getColumnIndex(ID);
        int indexNAZIV = cursor.getColumnIndex(NAZIV_KNJIGA);
        int indexKategorije = cursor.getColumnIndex(ID_KATEGORIJE_KNJIGA);


        while (cursor.moveToNext()){
            Log.i("Knjiga: ", cursor.getInt(indexID) + ". " + cursor.getString(indexNAZIV) + ", " +
                    cursor.getInt(indexKategorije));
        }
    }

    public void LogirajAutore(){
        SQLiteDatabase sqLiteDatabase;
        try{
            sqLiteDatabase = getWritableDatabase();
        }
        catch (Exception e){
            sqLiteDatabase = getReadableDatabase();
        }

        Cursor cursor = sqLiteDatabase.query(TABELA_AUTOR, null, null, null, null, null, null);
        while (cursor.moveToNext()){
            Log.i("Autor: ", cursor.getInt(0) + ". " + cursor.getString(1));
        }
    }

    public void LogirajAutorstva(){
        SQLiteDatabase sqLiteDatabase;
        try{
            sqLiteDatabase = getWritableDatabase();
        }
        catch (Exception e){
            sqLiteDatabase = getReadableDatabase();
        }

        Cursor cursor = sqLiteDatabase.query(TABELA_AUTORSTVO, null, null, null, null, null, null);
        while (cursor.moveToNext()){
            Log.i("Autorstvo: ", cursor.getInt(cursor.getColumnIndex(ID_AUTORA_AUTORSTVO)) + ", "
                    + cursor.getInt(cursor.getColumnIndex(ID_KNJIGE_AUTORSTVO)));
        }
    }

}

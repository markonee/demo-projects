package ba.unsa.etf.rma.marko_nedic.spirala1_17983;

import android.app.IntentService;
import android.content.Intent;
import android.os.Bundle;
import android.os.ResultReceiver;
import android.support.annotation.Nullable;
import android.util.Log;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLEncoder;
import java.util.ArrayList;

public class KnjigePoznanika extends IntentService {
    private String hardkodURL = "https://free4kwallpaper.com/wp-content/uploads/2016/01/Unique-Diamond-Abstract-4K-Wallpaper.jpg";


    private ArrayList<Knjiga> knjige = new ArrayList<>();
    Bundle bundle = new Bundle();           // vraća podatke

    final public static int STATUS_START = 0;
    final public static int STATUS_FINISH = 1;
    final public static int STATUS_ERROR = 2;

    public KnjigePoznanika(String name) {
        super(name);
    }

    public KnjigePoznanika(){
        super(null);
    }

    @Override
    public void onCreate() {
        super.onCreate();
    }

    @Override
    protected void onHandleIntent(@Nullable Intent intent) {

        final ResultReceiver receiver = intent.getParcelableExtra("receiver");
        final String s = intent.getStringExtra("idKorisnika");

        // 103409367781610008702 - kod mog profila na GoogleBooks.

        receiver.send(STATUS_START, Bundle.EMPTY);

        String query = null;

        try {
            query = URLEncoder.encode(s, "utf-8");
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }

        URL url = null;
        String url1 = "https://www.googleapis.com/books/v1/users/" + query + "/bookshelves";

        try {
            url = new URL(url1);

            HttpURLConnection urlConnection = (HttpURLConnection) url.openConnection();

            InputStream in = new BufferedInputStream(urlConnection.getInputStream());

            String rezultat = convertStreamToString(in);

            JSONObject jo = new JSONObject(rezultat);

            JSONArray niz = jo.getJSONArray("items");

            for(int i = 0; i < niz.length(); i++){
                JSONObject trenutnaPolica = niz.getJSONObject(i);

                String x = "";
                try{
                    x = trenutnaPolica.getString("selfLink");
                    x += "/volumes";                // za pristupanje podacima o knjigama
                    dodajKnjigeSaPolice(x);
                }
                catch (JSONException e){
                    e.printStackTrace();
                    bundle.putString(Intent.EXTRA_TEXT , e.toString());
                    receiver.send(STATUS_ERROR, bundle);
                }
            }
        }
        catch (MalformedURLException e) {
            e.printStackTrace();
            bundle.putString(Intent.EXTRA_TEXT , e.toString());
            receiver.send(STATUS_ERROR, bundle);
        }
        catch (IOException e) {
            e.printStackTrace();
            bundle.putString(Intent.EXTRA_TEXT , e.toString());
            receiver.send(STATUS_ERROR, bundle);
        } catch (JSONException e) {
            e.printStackTrace();
            bundle.putString(Intent.EXTRA_TEXT , e.toString());
            receiver.send(STATUS_ERROR, bundle);
        }
        bundle.putParcelableArrayList("listaKnjiga", knjige);
        receiver.send(STATUS_FINISH, bundle);
    }

    private void dodajKnjigeSaPolice(String x){

        URL url = null;

        try {
            url = new URL(x);

            HttpURLConnection urlConnection = (HttpURLConnection) url.openConnection();

            InputStream in = new BufferedInputStream(urlConnection.getInputStream());

            String rezultat = convertStreamToString(in);

            JSONObject jo = new JSONObject(rezultat);

            JSONArray niz = jo.getJSONArray("items");

            for (int i = 0; i < niz.length(); i++) {

                JSONObject trenutni = niz.getJSONObject(i);     // trenutna knjiga u nizu - max 5

                String id = trenutni.getString("id");

                JSONObject nizPodataka = trenutni.getJSONObject("volumeInfo");

                String naziv = null;
                try {
                    naziv = nizPodataka.getString("title");
                } catch (JSONException e) {
                    e.printStackTrace();
                }

                ArrayList<Autor> autori = new ArrayList<>();

                try {
                    JSONArray nizAutora = nizPodataka.getJSONArray("authors");
                    for (int j = 0; j < nizAutora.length(); j++) {
                        autori.add(new Autor(nizAutora.getString(j), id));
                    }
                } catch (JSONException e) {
                    e.printStackTrace();
                }

                String opis = null;
                try {
                    opis = nizPodataka.getString("description");
                } catch (JSONException e) {
                    e.printStackTrace();
                }

                String datumObjavljivanja = null;
                try {
                    datumObjavljivanja = nizPodataka.getString("publishedDate");
                } catch (JSONException e) {
                    e.printStackTrace();
                }

                int brojStranica = -1;
                try {
                    brojStranica = nizPodataka.getInt("pageCount");
                } catch (JSONException e) {
                    e.printStackTrace();
                }

                String urlSlike = null;

                try {
                    JSONObject imageLinks = nizPodataka.getJSONObject("imageLinks");
                    urlSlike = imageLinks.getString("thumbnail");
                } catch (JSONException e) {
                    e.printStackTrace();
                }


                URL urlAtribut;
                if (urlSlike != null) {
                    urlAtribut = new URL(urlSlike);
                } else {
                    urlAtribut = new URL(hardkodURL);        // signal da je prazno
                }

                Knjiga k = new Knjiga(id, naziv, autori, opis, datumObjavljivanja, urlAtribut, brojStranica);


                knjige.add(k);
            }
        }
        catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    private String convertStreamToString(InputStream is) {
        BufferedReader reader = new BufferedReader( new InputStreamReader(is));
        StringBuilder sb = new StringBuilder();
        String line = null ;
        try {
            while ((line = reader.readLine()) != null ) {
                sb.append(line + " \n " );
            }
        } catch (IOException e) {
        } finally {
            try {
                is.close();
            } catch (IOException e) {
            }
        }
        return sb.toString();
    }
}

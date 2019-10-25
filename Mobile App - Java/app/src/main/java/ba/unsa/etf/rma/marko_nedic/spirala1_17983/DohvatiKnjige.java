package ba.unsa.etf.rma.marko_nedic.spirala1_17983;

import android.os.AsyncTask;
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

public class DohvatiKnjige extends AsyncTask<String, Integer, Void>{
    private String hardkodURL = "https://free4kwallpaper.com/wp-content/uploads/2016/01/Unique-Diamond-Abstract-4K-Wallpaper.jpg";

    private IDohvatiKnjigeDone pozivatelj;
    ArrayList<Knjiga> knjige = new ArrayList<>();

    public DohvatiKnjige(IDohvatiKnjigeDone p){
        pozivatelj = p;
    }

    public interface IDohvatiKnjigeDone{
        public void onDohvatiDone(ArrayList<Knjiga> knjige);
    }

    @Override
    protected Void doInBackground(String... strings) {

        int d = strings.length;

        for(int x = 0; x < d; x++) {
            String query = null;

            try {
                query = URLEncoder.encode(strings[x], "utf-8");
            } catch (UnsupportedEncodingException e) {
                e.printStackTrace();
            }

            URL url = null;
            String url1 = "https://www.googleapis.com/books/v1/volumes?q=intitle:" + query + "&maxResults=5 ";

            try {
                url = new URL(url1);

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
                    }
                    catch (JSONException e) {
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
            } catch (MalformedURLException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            } catch (JSONException e) {
                e.printStackTrace();
            }
        }

        return null;
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

    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);
        pozivatelj.onDohvatiDone(knjige);
    }
}

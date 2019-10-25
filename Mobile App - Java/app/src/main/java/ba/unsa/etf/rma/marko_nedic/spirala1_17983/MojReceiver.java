package ba.unsa.etf.rma.marko_nedic.spirala1_17983;

import android.os.Bundle;
import android.os.Handler;
import android.os.ResultReceiver;

public class MojReceiver extends ResultReceiver {
    private Receiver mReceiver;

    public MojReceiver(Handler handler) {
        super(handler);
    }

    public void setmReceiver(Receiver mReceiver) {
        this.mReceiver = mReceiver;
    }

    public interface Receiver{
        public void onReceiveResult(int resultCode, Bundle resultData);
    }

    @Override
    protected void onReceiveResult(int resultCode, Bundle resultData) {
        if (mReceiver != null){
            mReceiver.onReceiveResult(resultCode, resultData);
        }
    }
}

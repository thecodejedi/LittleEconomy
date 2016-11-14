using UnityEngine;
using System.Collections;

public class SendEventUpwards : MonoBehaviour {

    public string EventName;

    public void Send() {
        gameObject.SendMessageUpwards(EventName, SendMessageOptions.DontRequireReceiver);
    }
}

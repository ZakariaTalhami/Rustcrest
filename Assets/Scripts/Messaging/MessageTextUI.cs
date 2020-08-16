using UnityEngine;
using UnityEngine.UI;


public class MessageTextUI : MonoBehaviour {
    [SerializeField]
    private Text text;

    public void setMessage(string message)
    {
        text.text = message;
    }

}
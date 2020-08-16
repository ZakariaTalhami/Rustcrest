using UnityEngine;
using System.Collections;

public class MessageHandler : MonoBehaviour {

    // TODO: Move Event to  UIEventHandler
    #region  Event
    public delegate void MessageRequest(string messageText);
    public static event MessageRequest onMessageRequest;

    public static void DisplayMessage(string message)
    {
        onMessageRequest?.Invoke(message);
    }
    
    #endregion
    
    public float messageDuration = 3f;
    private MonoPool<MessageTextUI> messagePool;
    private CoroutineQueue messageQueue;
    private void Start() {
        onMessageRequest += GotMessageRequest;

        // Setup Message Action Queue
        messageQueue = new CoroutineQueue(this);
        messageQueue.StartLoop();

        // Setup MessageText pool
        messagePool = gameObject.AddComponent<MessageTextPool>();
        messagePool.objectPrefab = Resources.Load<MessageTextUI>("UI/MessageText");
    }
    private void GotMessageRequest(string message)
    {
        messageQueue.EnqueueAction(SetMessage(message));
    }

    private IEnumerator SetMessage(string message)
    {
        MessageTextUI textUI = messagePool.GetFreeObject();
        textUI.setMessage(message);

        yield return new WaitForSeconds(messageDuration);

        messagePool.SetObjectFree(textUI);
    }
}
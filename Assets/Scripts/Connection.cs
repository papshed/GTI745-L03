using UnityEngine;
using NativeWebSocket;

public class Connection : MonoBehaviour {
    private const int PORT = 8000;

    [SerializeField] private GameObject sphere;

    private WebSocket websocket;

    private async void Start() {
        websocket = new WebSocket($"ws://localhost:{PORT}");

        websocket.OnOpen += () => Debug.Log("[OnOpen] ⚡ Connection open!");

        websocket.OnError += (error) => Debug.Log("[OnError] Error! " + error);

        websocket.OnClose += (closeCode) => Debug.Log("[OnClose] Connection closed!");

        websocket.OnMessage += (bytes) => Debug.Log("[OnMessage] " + System.Text.Encoding.UTF8.GetString(bytes));

        // Keep sending messages at every 0.3s
        InvokeRepeating(nameof(sendWebSocketMessage), 0.0f, 0.3f);

        await websocket.Connect();
    }

    private void Update() {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    private async void sendWebSocketMessage() {
        if (websocket.State == WebSocketState.Open) {
            // Sending bytes
            await websocket.Send(new byte[] { 10, 20, 30 });

            // Sending plain text
            await websocket.SendText("plain text message");
        }
    }

    private async void OnApplicationQuit() {
        await websocket.Close();
    }
}

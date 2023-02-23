import WebSocket, { WebSocketServer } from 'ws';

const server = new WebSocketServer({ port: 8000 });

server.on('connection', (ws) => {
    ws.on('error', console.error);

    ws.on('message', (data, isBinary) => {
        server.clients.forEach((client) => {
            if (client.readyState === WebSocket.OPEN)
                client.send(data, { binary: isBinary });
        });
    });
});

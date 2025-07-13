document.addEventListener("DOMContentLoaded", () => {
    const config = window.chatConfig;
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/hubs/chat")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    const messageList = document.getElementById("messageList");
    const messageInput = document.getElementById("messageInput");
    const sendForm = document.getElementById("sendMessageForm");

    connection.on("ReceiveMessage", (message) => {
        appendMessage(message);
    });

    function appendMessage(message) {
        const isMe = message.senderUserId.toLowerCase() === config.senderUserId.toLowerCase();
        const alignment = isMe ? "justify-content-end" : "justify-content-start";
        const bubbleClass = isMe ? "bg-primary text-white" : "bg-light border";
        const textAlign = isMe ? "text-end" : "text-start";

        const createdAt = new Date(message.createdAt).toLocaleTimeString([], { hour: "2-digit", minute: "2-digit", hour12: false });

        const html = `
            <div class="mb-2 d-flex ${alignment}">
                <div class="px-3 py-2 rounded-4 ${bubbleClass} shadow-sm" style="max-width: 75%;">
                    <div class="${textAlign}">${message.content}</div>
                    <small class="text-muted d-block ${textAlign}" style="font-size: 0.75rem;">${createdAt}</small>
                </div>
            </div>
        `;
        messageList.insertAdjacentHTML("beforeend", html);
        messageList.scrollTop = messageList.scrollHeight;
    }

    connection.start().then(() => {
        console.log("✅ SignalR connected");
        connection.invoke("JoinConversation", config.conversationId);
    }).catch(console.error);

    sendForm.addEventListener("submit", async (e) => {
        e.preventDefault();
        const content = messageInput.value.trim();
        if (!content) return;

        await fetch("/api/Chat/send", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                conversationId: config.conversationId,
                senderUserId: config.senderUserId,
                coachId: config.coachId,
                content: content,
                messageType: "TEXT"
            })
        });

        messageInput.value = "";
    });
});
